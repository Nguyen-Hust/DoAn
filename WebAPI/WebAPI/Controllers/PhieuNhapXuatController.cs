using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.NhanVien;
using WebAPI.Models.PhieuBaoDuong;
using WebAPI.Models.PhieuNhapXuat;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.Shared;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhieuNhapXuatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PhieuNhapXuatController(ApplicationDbContext context)
		{
            _context = context;
        }

        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<PhieuNhapXuatDto>>> GetList(SearchListDto input)
        {
            var entity = await _context.PhieuNhapXuat.ToListAsync();
            if (entity.Count > 0)
            {
                var totalCount = entity.Count;
                var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000).Select(_ => new PhieuNhapXuatDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        NhanVienId = _.NhanVienId,
                        NgayNhapXuat = _.NgayNhapXuat,
                        NguoiDaiDien = _.NguoiDaiDien,
                        SoLuong = _.SoLuong,
                        TongTien = _.TongTien,
                        GhiChu = _.GhiChu,
                        LoaiPhieu = _.LoaiPhieu,
                    }).ToList();
                return new PagedResultDto<PhieuNhapXuatDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<PhieuNhapXuatDto>
            {
                Items = new List<PhieuNhapXuatDto>(),
                TotalCount = 0
            };
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
                    NhanVienId = _.NhanVienId
                }).ToList();
            return items;

        }

        [HttpGet]
        [Route("get-danh-sach-nhan-vien")]
        public async Task<List<NhanSuDto>> GetDanhSachNhanVienAsync()
        {
            var thietBiYTe = await _context.NhanSu.ToListAsync();
            var items = thietBiYTe
                .Select(_ => new NhanSuDto
                {
                    Id = _.Id,
                    Ma = _.Ma,
                    Ten = _.Ten
                }).ToList();
            return items;

        }
    }
}

