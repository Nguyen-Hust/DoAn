using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Enums;
using WebAPI.Models.PhieuBanGiao;
using WebAPI.Models.PhieuBaoDuong;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.Shared;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhieuBanGiaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PhieuBanGiaoController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<PhieuBanGiaoDto>>> GetList(SearchListDto input)
        {
            var entity = await _context.PhieuBanGiao.Include(_ => _.ChiTietPhieuBanGiao).ToListAsync();
            if (entity.Count > 0)
            {
                var totalCount = entity.Count;
                var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000).Select(_ => new PhieuBanGiaoDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        NhanVienNhan = _.NhanVienNhan,
                        CreateTime = _.CreateTime,
                        NhanVienId = _.NhanVienId,
                        TongThietBi = _.ChiTietPhieuBanGiao.Count
                    }).ToList();
                return new PagedResultDto<PhieuBanGiaoDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<PhieuBanGiaoDto>
            {
                Items = new List<PhieuBanGiaoDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<PhieuBanGiaoDto> CreateAsync(PhieuBanGiaoDto input)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVienId = _context.NhanSu.First(_ => _.AccountId == userId).Id;
            var entity = new PhieuBanGiaoEntity
            {
                Id = input.Id,
                CreateTime = DateTime.Now,
                NhanVienId = nhanVienId,
                NhanVienNhan = input.NhanVienNhan,
                Ma = input.Ma,
            };
            _context.PhieuBanGiao.Add(entity);
            await _context.SaveChangesAsync();
            entity.Ma = $"PBG_{entity.Id}";
            _context.PhieuBanGiao.Update(entity);
            var listThietBi = _context.ThongTinChiTietThietBi.Where(_ => input.DanhSachThietBi.Contains(_.Id)).ToList();
            foreach (var item in listThietBi)
            {
                var nhanVien = _context.NhanSu.First(c => c.Id == input.NhanVienNhan);
                item.NhanVienId = input.NhanVienNhan;
                item.KhoaId = nhanVien.KhoaId;
                var lichSu = new LichSuBanGiaoThuHoiEntity
                {
                    Id = 0,
                    ChiTietThietBiId = item.Id,
                    NhanVienId = input.NhanVienNhan,
                    NgayThucHien = DateTime.Now
                };
                _context.LichSuBanGiaoThuHoi.Add(lichSu);

            }
            _context.UpdateRange(listThietBi);
            foreach (var item in input.DanhSachThietBi)
            {
                var chiTiet = new ChiTietPhieuBanGiaoEntity
                {
                    Id = input.Id,
                    PhieuBanGiaoId = entity.Id,
                    ChiTietThietBiId = item
                };
                _context.ChiTietPhieuBanGiao.Add(chiTiet);
            }
            await _context.SaveChangesAsync();
            return input;
        }

        [HttpGet]
        [Route("get-by-id")]
        public PhieuBanGiaoDto GetByIdAsync(int id)
        {
            var entity = _context.PhieuBanGiao.Include(_ => _.ChiTietPhieuBanGiao).ToList().Find(_ => _.Id == id);
            return new PhieuBanGiaoDto
            {
                Id = entity.Id,
                DanhSachThietBi = entity.ChiTietPhieuBanGiao.Select(_ => _.ChiTietThietBiId).ToList(),
                CreateTime = entity.CreateTime,
                NhanVienId = entity.NhanVienId,
                NhanVienNhan = entity.NhanVienNhan
            };
        }

        [HttpGet]
        [Route("get-danh-sach-thiet-bi")]
        public async Task<List<ThongTinChiTietThietBiSelectDto>> GetDanhSachThietBiAsync()
        {
            var thietBiYTe = await _context.ThongTinChiTietThietBi.ToListAsync();
            var items = thietBiYTe
                .Where(_ => _.DaXuat != true)
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