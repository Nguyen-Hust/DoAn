using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.DashBoard;
using WebAPI.Models.Shared;
using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DashBoardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DashBoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-dash-board")]
        public async Task<DashBoardTotalDto> GetDashBoard()
        {
            var phieuNhapXuat = await _context.PhieuNhapXuat.ToListAsync();
            var phieuBaoDuong = await _context.PhieuBaoDuong.ToListAsync();
            var phieuThuHoi = await _context.PhieuThuHoi.ToListAsync();
            var phieuSuaChua = await _context.PhieuSuaChua.ToListAsync();
            var phieuBanGiao = await _context.PhieuBanGiao.ToListAsync();
            var tongTienNhap = 0;
            var tongTienXuat = 0;
            foreach(var item in phieuNhapXuat)
            {
                if(item.LoaiPhieu == 1)
                {
                    tongTienNhap = tongTienNhap + (int)item.TongTien;
                }else {
                    tongTienXuat = tongTienXuat + (int)item.TongTien;
                }
            };
            var result = new DashBoardTotalDto();
            result.TongPhieuBanGiao = phieuBanGiao.Count();
            result.TongPhieuBaoDuong = phieuBaoDuong.Count();
            result.TongPhieuSuaChua = phieuSuaChua.Count();
            result.TongPhieuThuHoi = phieuThuHoi.Count();
            result.TongPhieuXuat = phieuNhapXuat.Where(_ => _.LoaiPhieu == 2).ToList().Count();
            result.TongPhieuNhap = phieuNhapXuat.Where(_ => _.LoaiPhieu == 1).ToList().Count();
            result.TongTienNhap = tongTienNhap;
            result.TongTienXuat = tongTienXuat;
            return result;
        }

        [HttpGet]
        [Route("get-dashbaord-chart")]
        public async Task<List<DashBoardChartDto>> GetDashBoardChart()
        {
            var loaiThietBI = await _context.LoaiThietBi.ToListAsync();
            var thietBi = await _context.ThietBiYTe.ToListAsync();
            var dto = from tb in thietBi
                      join ltb in loaiThietBI on tb.LoaiTTBYT equals ltb.Ma
                      select new ThietBiDto
                      {
                          Id = tb.Id,
                          Ma = tb.Ma,
                          MDRR = tb.MDRR,
                          Ten = tb.Ten,
                          LoaiTTBYT = tb.LoaiTTBYT,
                          TenLoaiTTBYT = ltb.Ten
                      };
            var listData = dto.ToList();
            var result = listData.GroupBy(x => new { x.LoaiTTBYT}).Select(x => new DashBoardChartDto
            {
                Category = x.FirstOrDefault().TenLoaiTTBYT,
                Value = x.Count()
            }).ToList();
            return result;

        }
    }
}

