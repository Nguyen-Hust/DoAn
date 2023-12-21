using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Enums;
using WebAPI.Models.Mail;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhieuSuaChuaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISendMailService _sendMailService;
        public PhieuSuaChuaController(ApplicationDbContext context, ISendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<PhieuSuaChuaDto>>> GetList(PhieuSuaChuaInputDto input)
        {
            var phieuSuaChua = await _context.PhieuSuaChua.ToListAsync();
            if (phieuSuaChua.Count > 0)
            {
                var totalCount = phieuSuaChua.Count;
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var nhanVien = _context.NhanSu.FirstOrDefault(_ => _.AccountId == userId);
                var items = phieuSuaChua
                    .Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Where(_ => !input.Date.HasValue || input.Date.Value.Date == _.CreateTime.Value.Date)
                    .Where(_ => nhanVien?.Id == _.NhanVienId || nhanVien?.LaQuanLyThietBi == true)
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000).Select(_ => new PhieuSuaChuaDto
                    {
                        Id = _.Id,
                        ChiTietThietBiId = _.ChiTietThietBiId,
                        CreateTime = _.CreateTime,
                        LyDo = _.LyDo,
                        NhanVienId = _.NhanVienId,
                        TrangThai = _.TrangThai,
                        Ma = _.Ma,
                    }).ToList();
                return new PagedResultDto<PhieuSuaChuaDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<PhieuSuaChuaDto>
            {
                Items = new List<PhieuSuaChuaDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<PhieuSuaChuaDto> CreateAsync(PhieuSuaChuaDto input)
        {
            var entity = new PhieuSuaChuaEntity
            {
                Id = input.Id,
                ChiTietThietBiId = input.ChiTietThietBiId,
                CreateTime = DateTime.Now,
                LyDo = input.LyDo,
                NhanVienId = input.NhanVienId,
                TrangThai = (int)TrangThaiPhieuSuaChuaEnum.Waiting,
                Ma = input.Ma
            };
            _context.PhieuSuaChua.Add(entity);
            await _context.SaveChangesAsync();
            entity.Ma = $"PSC_{entity.Id}";
            _context.PhieuSuaChua.Update(entity);

            var users = _context.NhanSu.Where(_ => !string.IsNullOrEmpty(_.AccountId) && _.LaQuanLyThietBi == true).ToList();
            var t = new List<ThongBaoNguoiDungEntity>();
            var thongBao = new ThongBaoEntity
            {
                Message = $"Phiếu sửa chữa: {entity.Ma} được tạo mới",
                Subject = "Sửa chữa thiết bị",
                SendTime = DateTime.Now,
                MetaData = "",
            };
            _context.ThongBao.Update(thongBao);
            await _context.SaveChangesAsync();

            for (int i = 0; i < users.Count; i++)
            {
                var notiUser = new ThongBaoNguoiDungEntity
                {
                    ThongBaoId = thongBao.Id,
                    UserId = users[i].AccountId,
                };
                t.Add(notiUser);

                try
                {
                    if (!string.IsNullOrEmpty(users[i].Email))
                    {
                        MailContent content = new MailContent
                        {
                            To = users[i].Email,
                            Subject = "Sửa chữa thiết bị",
                            Body = @$"<p>Dear <strong>{users[i].Ten}</strong></p>
                                <p>Phiếu sửa chữa: <strong>{entity.Ma}</strong> được tạo mới</p>
                                <p>Happy closing !!!</p>>"
                        };

                        await _sendMailService.SendMail(content);
                    }
                }
                catch {}
                
            }
            _context.ThongBaoNguoiDung.AddRange(t);

            await _context.SaveChangesAsync();
            return input;
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<PhieuSuaChuaDto> GetByIdAsync(int id)
        {
            var entity = await _context.PhieuSuaChua.FindAsync(id);
            return new PhieuSuaChuaDto
            {
                Id = entity.Id,
                ChiTietThietBiId = entity.ChiTietThietBiId,
                CreateTime = entity.CreateTime,
                LyDo = entity.LyDo,
                NhanVienId = entity.NhanVienId,
                TrangThai = entity.TrangThai,
                Ma = entity.Ma
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var entity = await _context.PhieuSuaChua.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if(entity.TrangThai == (int)TrangThaiPhieuSuaChuaEnum.Waiting)
            {
                _context.PhieuSuaChua.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                return new CommonResultDto<int>("Phiếu đã được duyệt");
            }
            return new CommonResultDto<int>(id);
        }

        [HttpPost]
        [Route("approve")]
        public async Task<CommonResultDto<int>> ApproveAsync(int id)
        {
            var entity = await _context.PhieuSuaChua.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if (entity.TrangThai == (int)TrangThaiPhieuSuaChuaEnum.Waiting)
            {
                entity.TrangThai = (int)TrangThaiPhieuSuaChuaEnum.Approve;
                _context.PhieuSuaChua.Update(entity);
                await _context.SaveChangesAsync();

                var users = _context.NhanSu.Where(_ => !string.IsNullOrEmpty(_.AccountId) && _.Id == entity.NhanVienId).ToList();
                var t = new List<ThongBaoNguoiDungEntity>();
                var thongBao = new ThongBaoEntity
                {
                    Message = $"Phiếu sửa chữa: {entity.Ma} được duyệt",
                    Subject = "Sửa chữa thiết bị",
                    SendTime = DateTime.Now,
                    MetaData = "",
                };
                _context.ThongBao.Update(thongBao);
                await _context.SaveChangesAsync();

                for (int i = 0; i < users.Count; i++)
                {
                    var notiUser = new ThongBaoNguoiDungEntity
                    {
                        ThongBaoId = thongBao.Id,
                        UserId = users[i].AccountId,
                    };
                    t.Add(notiUser);
                    try
                    {
                        if (!string.IsNullOrEmpty(users[i].Email))
                        {
                            MailContent content = new MailContent
                            {
                                To = users[i].Email,
                                Subject = "Sửa chữa thiết bị",
                                Body = @$"<p>Dear <strong>{users[i].Ten}</strong></p>
                                <p>Phiếu sửa chữa: <strong>{entity.Ma}</strong> được duyệt</p>
                                <p>Happy closing !!!</p>>"
                            };

                            await _sendMailService.SendMail(content);
                        }
                    }
                    catch { }
                }
                _context.ThongBaoNguoiDung.AddRange(t);
                await _context.SaveChangesAsync();
            }
            else
            {
                return new CommonResultDto<int>("Phiếu đã được duyệt");
            }
            return new CommonResultDto<int>(id);
        }
        [HttpPost]
        [Route("completed")]
        public async Task<CommonResultDto<int>> CompletedAsync(int id)
        {
            var entity = await _context.PhieuSuaChua.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if (entity.TrangThai == (int)TrangThaiPhieuSuaChuaEnum.Approve)
            {
                entity.TrangThai = (int)TrangThaiPhieuSuaChuaEnum.Completed;
                _context.PhieuSuaChua.Update(entity);
                await _context.SaveChangesAsync();

                var users = _context.NhanSu.Where(_ => !string.IsNullOrEmpty(_.AccountId) && _.Id == entity.NhanVienId).ToList();
                var t = new List<ThongBaoNguoiDungEntity>();
                var thongBao = new ThongBaoEntity
                {
                    Message = $"Phiếu sửa chữa: {entity.Ma} đã hoàn thành sửa chữa",
                    Subject = "Sửa chữa thiết bị",
                    SendTime = DateTime.Now,
                    MetaData = "",
                };
                _context.ThongBao.Update(thongBao);
                await _context.SaveChangesAsync();

                for (int i = 0; i < users.Count; i++)
                {
                    var notiUser = new ThongBaoNguoiDungEntity
                    {
                        ThongBaoId = thongBao.Id,
                        UserId = users[i].AccountId,
                    };
                    t.Add(notiUser);
                    try
                    {
                        if (!string.IsNullOrEmpty(users[i].Email))
                        {
                            MailContent content = new MailContent
                            {
                                To = users[i].Email,
                                Subject = "Sửa chữa thiết bị",
                                Body = @$"<p>Dear <strong>{users[i].Ten}</strong></p>
                                <p>Phiếu sửa chữa: <strong>{entity.Ma}</strong> đã hoàn thành sửa chữa</p>
                                <p>Happy closing !!!</p>>"
                            };

                            await _sendMailService.SendMail(content);
                        }
                    }
                    catch { }
                }
                _context.ThongBaoNguoiDung.AddRange(t);
                await _context.SaveChangesAsync();
            }
            else
            {
                return new CommonResultDto<int>("Phiếu đã được duyệt");
            }
            return new CommonResultDto<int>(id);
        }
        [HttpPost]
        [Route("deny")]
        public async Task<CommonResultDto<int>> DenyAsync(int id)
        {
            var entity = await _context.PhieuSuaChua.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if (entity.TrangThai == (int)TrangThaiPhieuSuaChuaEnum.Waiting)
            {
                entity.TrangThai = (int)TrangThaiPhieuSuaChuaEnum.Deny;
                _context.PhieuSuaChua.Update(entity);
                await _context.SaveChangesAsync();

                var users = _context.NhanSu.Where(_ => !string.IsNullOrEmpty(_.AccountId) && _.Id == entity.NhanVienId).ToList();
                var t = new List<ThongBaoNguoiDungEntity>();
                var thongBao = new ThongBaoEntity
                {
                    Message = $"Phiếu sửa chữa: {entity.Ma} bị từ chối",
                    Subject = "Sửa chữa thiết bị",
                    SendTime = DateTime.Now,
                    MetaData = "",
                };
                _context.ThongBao.Update(thongBao);
                await _context.SaveChangesAsync();

                for (int i = 0; i < users.Count; i++)
                {
                    var notiUser = new ThongBaoNguoiDungEntity
                    {
                        ThongBaoId = thongBao.Id,
                        UserId = users[i].AccountId,
                    };
                    t.Add(notiUser);
                    try
                    {
                        if (!string.IsNullOrEmpty(users[i].Email))
                        {
                            MailContent content = new MailContent
                            {
                                To = users[i].Email,
                                Subject = "Sửa chữa thiết bị",
                                Body = @$"<p>Dear <strong>{users[i].Ten}</strong></p>
                                <p>Phiếu sửa chữa: <strong>{entity.Ma}</strong> bị từ chối</p>
                                <p>Happy closing !!!</p>>"
                            };

                            await _sendMailService.SendMail(content);
                        }
                    }
                    catch { }
                }
                _context.ThongBaoNguoiDung.AddRange(t);
                await _context.SaveChangesAsync();
            }
            else
            {
                return new CommonResultDto<int>("Phiếu đã được duyệt");
            }
            return new CommonResultDto<int>(id);
        }

        [HttpGet]
        [Route("get-danh-sach-thiet-bi")]
        public async Task<List<ThongTinChiTietThietBiSelectDto>> GetDanhSachThietBiAsync()
        {
            var thietBiYTe = await _context.ThongTinChiTietThietBi.ToListAsync();
            var items = thietBiYTe
                .Select(_ => new ThongTinChiTietThietBiSelectDto
                {
                    Id = _.Id,
                    Ma = _.Ma,
                    NhanVienId = _.NhanVienId,
                    DaXuat = _.DaXuat
                }).ToList();
            return items;

            }
    }
}
