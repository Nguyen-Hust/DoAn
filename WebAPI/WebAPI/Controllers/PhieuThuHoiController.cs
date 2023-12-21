using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.PhieuBanGiao;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.PhieuThuHoi;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;
using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhieuThuHoiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PhieuThuHoiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<PhieuThuHoiDto>>> GetList(SearchListDto input)
        {
            var entity = await _context.PhieuThuHoi.Include(_ => _.ChiTietPhieuThuHoi).ToListAsync();
            if (entity.Count > 0)
            {
                var totalCount = entity.Count;
                var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000).Select(_ => new PhieuThuHoiDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        CreateTime = _.CreateTime,
                        NhanVienId = _.NhanVienId,
                        TongThietBi = _.ChiTietPhieuThuHoi.Count
                    }).ToList();
                return new PagedResultDto<PhieuThuHoiDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<PhieuThuHoiDto>
            {
                Items = new List<PhieuThuHoiDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<PhieuThuHoiDto> CreateAsync(PhieuThuHoiDto input)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVienId = _context.NhanSu.First(_ => _.AccountId == userId).Id;
            var entity = new PhieuThuHoiEntity
            {
                Id = input.Id,
                CreateTime = DateTime.Now,
                NhanVienId = nhanVienId,
                Ma = input.Ma,
            };
            _context.PhieuThuHoi.Add(entity);
            await _context.SaveChangesAsync();
            entity.Ma = $"PTH_{entity.Id}";
            var listThietBi = _context.ThongTinChiTietThietBi.Where(_ => input.DanhSachThietBiId.Contains(_.Id)).ToList();
            foreach (var item in listThietBi)
            {
                var chiTiet = new ChiTietPhieuThuHoiEntity
                {
                    Id = input.Id,
                    PhieuThuHoiId = entity.Id,
                    MaThietBi = item.Ma,
                    NhanVienId = item.NhanVienId ?? 0
                };
                _context.ChiTietPhieuThuHoi.Add(chiTiet);
                item.NhanVienId = null;
            }
            _context.UpdateRange(listThietBi);
            await _context.SaveChangesAsync();
            return input;
        }

        [HttpGet]
        [Route("get-by-id")]
        public PhieuThuHoiDto GetByIdAsync(int id)
        {
            var entity = _context.PhieuThuHoi.Include(_ => _.ChiTietPhieuThuHoi).ToList().Find(_ => _.Id == id);
            return new PhieuThuHoiDto
            {
                Id = entity.Id,
                DanhSachThietBi = entity.ChiTietPhieuThuHoi.Select(_ => new ThongTinChiTietThietBiDto { Ma = _.MaThietBi, NhanVienId = _.NhanVienId}).ToList(),
                CreateTime = entity.CreateTime,
                NhanVienId = entity.NhanVienId,
            };
        }

        [HttpGet]
        [Route("get-danh-sach-thiet-bi")]
        public async Task<List<ThongTinChiTietThietBiSelectDto>> GetDanhSachThietBiAsync()
        {
            var thietBiYTe = await _context.ThongTinChiTietThietBi.ToListAsync();
            var items = thietBiYTe
                .Where(_ => _.NhanVienId.HasValue && _.DaXuat != true)
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