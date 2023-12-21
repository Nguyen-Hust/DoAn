using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Enums;
using WebAPI.Models.PhieuBaoDuong;
using WebAPI.Models.PhieuNhapXuat;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhieuBaoDuongController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PhieuBaoDuongController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<PhieuBaoDuongDto>>> GetList(PhieuBaoDuongInputDto input)
        {
            var entity = await _context.PhieuBaoDuong.Include(_ => _.ChiTietPhieuBaoDuong).ToListAsync();
            if (entity.Count > 0)
            {
                var totalCount = entity.Count;
                var items = entity
                    .Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Where(_ => !input.Date.HasValue || input.Date.Value.Date == _.CreateTime.Value.Date)
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000).Select(_ => new PhieuBaoDuongDto
                    {
                        Id = _.Id,
                        CreateTime = _.CreateTime,
                        NhanVienId = _.NhanVienId,
                        TrangThai = _.TrangThai,
                        TongThietBi = _.ChiTietPhieuBaoDuong.Count,
                        Ma = _.Ma
                    }).ToList();
                return new PagedResultDto<PhieuBaoDuongDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<PhieuBaoDuongDto>
            {
                Items = new List<PhieuBaoDuongDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<PhieuBaoDuongDto> CreateAsync(PhieuBaoDuongDto input)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVienId = _context.NhanSu.First(_ => _.AccountId == userId).Id;
            var entity = new PhieuBaoDuongEntity
            {
                Id = input.Id,
                CreateTime = DateTime.Now,
                NhanVienId = nhanVienId,
                TrangThai = (int)TrangThaiPhieuBaoDuongEnum.Waiting,
                Ma = input.Ma,
            }; 
            _context.PhieuBaoDuong.Add(entity);
            await _context.SaveChangesAsync();
            entity.Ma = $"PBD_{entity.Id}";
            _context.PhieuBaoDuong.Update(entity);
            foreach (var item in input.DanhSachThietBi)
            {
                var chiTiet = new ChiTietPhieuBaoDuongEntity
                {
                    Id = 0,
                    PhieuBaoDuongId = entity.Id,
                    ChiTietThietBiId = item 
                };
                _context.ChiTietPhieuBaoDuong.Add(chiTiet);
            }
            await _context.SaveChangesAsync();
            return input;
        }

        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<PhieuBaoDuongDto>> Update(int id, PhieuBaoDuongDto input)
        {
            if (id != input.Id)
            {
                return new CommonResultDto<PhieuBaoDuongDto>("bad request");
            }
            var entity = await _context.PhieuBaoDuong.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<PhieuBaoDuongDto>("Not found");
            }
            var temp = _context.ChiTietPhieuBaoDuong.Where(_ => _.PhieuBaoDuongId == id);
            _context.ChiTietPhieuBaoDuong.RemoveRange(temp);
            await _context.SaveChangesAsync();
            foreach (var item in input.DanhSachThietBi)
            {
                var chiTiet = new ChiTietPhieuBaoDuongEntity
                {
                    Id = 0,
                    PhieuBaoDuongId = entity.Id,
                    ChiTietThietBiId = item
                };
                _context.ChiTietPhieuBaoDuong.Add(chiTiet);
            }
            await _context.SaveChangesAsync();
            return new CommonResultDto<PhieuBaoDuongDto>(input);
        }

        [HttpGet]
        [Route("get-by-id")]
        public PhieuBaoDuongDto GetByIdAsync(int id)
        {
            var entity = _context.PhieuBaoDuong.Include(_ => _.ChiTietPhieuBaoDuong).ToList().Find(_ => _.Id == id);
            return new PhieuBaoDuongDto
            {
                Id = entity.Id,
                DanhSachThietBi = entity.ChiTietPhieuBaoDuong.Select(_ => _.ChiTietThietBiId).ToList(),
                CreateTime = entity.CreateTime,
                NhanVienId = entity.NhanVienId,
                TrangThai = entity.TrangThai,
                Ma = entity.Ma
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var entity = await _context.PhieuBaoDuong.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if (entity.TrangThai == (int)TrangThaiPhieuBaoDuongEnum.Waiting)
            {
                _context.PhieuBaoDuong.Remove(entity);
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
            var entity = await _context.PhieuBaoDuong.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if (entity.TrangThai == (int)TrangThaiPhieuBaoDuongEnum.Waiting)
            {
                entity.TrangThai = (int)TrangThaiPhieuBaoDuongEnum.Approve;
                _context.PhieuBaoDuong.Update(entity);
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
            var entity = await _context.PhieuBaoDuong.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            if (entity.TrangThai == (int)TrangThaiPhieuBaoDuongEnum.Approve)
            {
                entity.TrangThai = (int)TrangThaiPhieuBaoDuongEnum.Completed;
                _context.PhieuBaoDuong.Update(entity);
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
                .Where(_ => _.NgayNhap.AddMonths(_.ThoiGianBaoDuong??0) < DateTime.Now && _.DaXuat != true)
                .Select(_ => new ThongTinChiTietThietBiSelectDto
                {
                    Id = _.Id,
                    Ma = _.Ma,
                    NhanVienId = _.NhanVienId
                }).ToList();
            return items;

        }

        [HttpGet]
        [Route("get-total-phieu-bao-duong")]
        public async Task<ThongKeDto> GetThongKePhieuBaoDuong()
        {
            var phieuBaoDuong = await _context.PhieuBaoDuong.Include(_ => _.ChiTietPhieuBaoDuong).ToListAsync();
            var tongSo = 0;
            var tongTien = 0;
            foreach (var item in phieuBaoDuong)
            {
                tongSo = tongSo + item.ChiTietPhieuBaoDuong.Count;
            }
            var result = new ThongKeDto()
            {
                TongSoLuong = tongSo,
                TongSoTien = tongTien
            };
            return result;
        }
    }
}