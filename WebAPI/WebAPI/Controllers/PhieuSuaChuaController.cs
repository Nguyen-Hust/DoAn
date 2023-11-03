using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Enums;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhieuSuaChuaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PhieuSuaChuaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<PhieuSuaChuaDto>>> GetList(PhieuSuaChuaInputDto input)
        {
            var phieuSuaChua = await _context.PhieuSuaChua.ToListAsync();
            if (phieuSuaChua.Count > 0)
            {
                var totalCount = phieuSuaChua.Count;
                var items = phieuSuaChua
                    .Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Where(_ => !input.Date.HasValue || input.Date.Value.Date == _.CreateTime.Value.Date)
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
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVienId = _context.NhanSu.FirstOrDefault(_ => _.AccountId == userId)?.Id;
            var items = thietBiYTe.Where(_ => _.NhanVienId == nhanVienId)
                .Select(_ => new ThongTinChiTietThietBiSelectDto
                {
                    Id = _.Id,
                    Ma = _.Ma,
                    NhanVienId = _.NhanVienId
                }).ToList();
            return items;

            }
    }
}
