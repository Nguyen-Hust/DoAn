using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.PhongBan;
using WebAPI.Models.Shared;
using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DanhSachThietBiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DanhSachThietBiController(ApplicationDbContext context)
		{
            _context = context;
		}

        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<ThongTinChiTietThietBiDto>>> GetList(SearchListDto input)
        {
            var phongBan = await _context.ThongTinChiTietThietBi.ToListAsync();
            if (phongBan.Count > 0)
            {
                var totalCount = phongBan.Count;
                var items = phongBan.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter)).Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000)
                    .Select(_ => new ThongTinChiTietThietBiDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        ThietBiYTeId = _.ThietBiYTeId,
                        NgayNhap = _.NgayNhap,
                        XuatXu = _.XuatXu,
                        NamSX = _.NamSX,
                        HangSanXuat = _.HangSanXuat,
                        TinhTrang = _.TinhTrang,
                        KhoaId = _.KhoaId,
                        NhanVienId = _.NhanVienId,
                        Serial = _.Serial,
                        Model = _.Model,
                        GiaTien = _.GiaTien,
                        ThoiGianBaoDuong = _.ThoiGianBaoDuong
                    }).ToList();
                return new PagedResultDto<ThongTinChiTietThietBiDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };

            }
            return new PagedResultDto<ThongTinChiTietThietBiDto>
            {
                Items = new List<ThongTinChiTietThietBiDto>(),
                TotalCount = 0
            };
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<ThongTinChiTietThietBiDto> GetByIdAsync(int id)
        {
            var _ = await _context.ThongTinChiTietThietBi.FindAsync(id);
            return new ThongTinChiTietThietBiDto
            {
                Id = _.Id,
                Ma = _.Ma,
                ThietBiYTeId = _.ThietBiYTeId,
                NgayNhap = _.NgayNhap,
                XuatXu = _.XuatXu,
                NamSX = _.NamSX,
                HangSanXuat = _.HangSanXuat,
                TinhTrang = _.TinhTrang,
                KhoaId = _.KhoaId,
                NhanVienId = _.NhanVienId,
                Serial = _.Serial,
                Model = _.Model,
                GiaTien = _.GiaTien,
                ThoiGianBaoDuong = _.ThoiGianBaoDuong
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var product = await _context.ThongTinChiTietThietBi.FindAsync(id);
            if (product == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            _context.ThongTinChiTietThietBi.Remove(product);
            await _context.SaveChangesAsync();
            return new CommonResultDto<int>(id);
        }

        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<ThongTinChiTietThietBiDto>> Update(int id, ThongTinChiTietThietBiDto thietBi)
        {
            try
            {
                if (id != thietBi.Id)
                {
                    return new CommonResultDto<ThongTinChiTietThietBiDto>("bad request");
                }
                var entity = await _context.ThongTinChiTietThietBi.FindAsync(id);
                if (entity == null)
                {
                    return new CommonResultDto<ThongTinChiTietThietBiDto>("Not found");
                }
                var _ = thietBi;
                entity.Ma = _.Ma;
                entity.NgayNhap = _.NgayNhap;
                entity.XuatXu = _.XuatXu;
                entity.NamSX = _.NamSX;
                entity.HangSanXuat = _.HangSanXuat;
                entity.TinhTrang = _.TinhTrang;
                entity.KhoaId = _.KhoaId;
                entity.NhanVienId = _.NhanVienId;
                entity.Serial = _.Serial;
                entity.Model = _.Model;
                entity.GiaTien = _.GiaTien;
                entity.ThoiGianBaoDuong = _.ThoiGianBaoDuong;
                await _context.SaveChangesAsync();
                return new CommonResultDto<ThongTinChiTietThietBiDto>(thietBi);
            } catch(Exception ex)
            {
                return null;
            }
        }
    }
}

