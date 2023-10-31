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
            var listThietBi = _context.ThongTinChiTietThietBi.Where(_ => input.DanhSachThietBi.Contains(_.Id)).ToList();
            foreach (var item in listThietBi)
            {
                item.NhanVienId = input.NhanVienNhan;
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

        /*[HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var entity = await _context.PhieuBanGiao.FindAsync(id);
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
        }*/

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
                    NhanVienId = _.NhanVienId
                }).ToList();
            return items;

        }
    }
}