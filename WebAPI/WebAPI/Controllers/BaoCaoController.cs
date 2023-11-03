using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebAPI.Data;
using WebAPI.Models.Shared;
using WebAPI.Models.PhieuNhapXuat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.ThongTinChiTietThietBi;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class BaoCaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private class ExportexcelDto
        {
            public string Ma { get; set; }
            public DateTime NgayNhapXuat { get; set; }
            public string NhaCungCap { get; set; }
            public string NguoiDaiDien { get; set; }
            public int NhanVienId { get; set; }
            public int? SoLuong { get; set; }
            public decimal? TongTien { get; set; }
            public int LoaiPhieu { get; set; }
            public string GhiChu { get; set; }
        }
        public BaoCaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("bao-cao-nhap")]
        public async Task<IActionResult> ExportExcel(SearchListDto input)
        {
            var entity = _context.PhieuNhapXuat.ToList();
            var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                .Where(x => x.LoaiPhieu == 1)
                .Select(_ => new ExportexcelDto
                {
                    Ma = _.Ma,
                    NgayNhapXuat = _.NgayNhapXuat,
                    NhaCungCap = _.NhaCungCap,
                    NguoiDaiDien = _.NguoiDaiDien,
                    NhanVienId = _.NhanVienId,
                    SoLuong = _.SoLuong,
                    TongTien = _.TongTien,
                    LoaiPhieu = _.LoaiPhieu,
                    GhiChu = _.GhiChu,
                }).ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var maxColumn = 0;
            using var excelPackage = new ExcelPackage(stream);
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "Báo cáo Nhập";
                worksheet.Cells["A2"].Value = "Mã";
                worksheet.Cells["B2"].Value = "Ngày sản xuất";
                worksheet.Cells["C2"].Value = "Nhà cung cấp";
                worksheet.Cells["D2"].Value = "Người đại diện";
                worksheet.Cells["E2"].Value = "Nhân viên";
                worksheet.Cells["F2"].Value = "Số lượng";
                worksheet.Cells["G2"].Value = "Tổng tiền";
                worksheet.Cells["H2"].Value = "Loại phiếu";
                worksheet.Cells["I2"].Value = "Ghi chú";
                maxColumn = Math.Max(maxColumn, 9);
                int rowIndex = 3;
                foreach (var dto in items)
                {
                    var columnIndex = 1;
                    foreach (var property in typeof(ExportexcelDto).GetProperties())
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = property.GetValue(dto);
                        columnIndex++;
                    }
                    rowIndex++;
                }

                for (int i = 1; i <= maxColumn; i++)
                {
                    worksheet.Column(i).AutoFit();
                }

                await excelPackage.SaveAsync();
            }
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
        }

        [HttpGet]
        [Route("export-pdf-phieu-nhap")]
        public async Task<IActionResult> ExportDetaiPhieuXuat(int id)
        {
            var phieuNhapXuat = await _context.PhieuNhapXuat.FindAsync(id);
            var dto = new PhieuNhapXuatDto()
            {
                Id = phieuNhapXuat.Id,
                Ma = phieuNhapXuat.Ma,
                NgayNhapXuat = phieuNhapXuat.NgayNhapXuat,
                NhaCungCap = phieuNhapXuat.NhaCungCap,
                NguoiDaiDien = phieuNhapXuat.NguoiDaiDien,
                NhanVienId = phieuNhapXuat.NhanVienId,
                SoLuong = phieuNhapXuat.SoLuong,
                TongTien = phieuNhapXuat.TongTien,
                GhiChu = phieuNhapXuat.GhiChu,
                LoaiPhieu = phieuNhapXuat.LoaiPhieu,
            };
            dto.ThongTinChiTietThietBiDtos = new List<ThongTinChiTietThietBiDto>();
            var listChiTietPhieu = await _context.ChiTietPhieuNhapXuat.Where(_ => _.PhieuNhapXuatId == id).ToListAsync();
            foreach (var item in listChiTietPhieu)
            {
                var thongTinThietBi = await _context.ThongTinChiTietThietBi.FirstOrDefaultAsync(_ => _.Id == item.ChiTietThietBiId);
                if (thongTinThietBi != null)
                {
                    var thongTinThietBiDto = new ThongTinChiTietThietBiDto()
                    {
                        Id = thongTinThietBi.Id,
                        Ma = thongTinThietBi.Ma,
                        ThietBiYTeId = thongTinThietBi.ThietBiYTeId,
                        NgayNhap = thongTinThietBi.NgayNhap,
                        XuatXu = thongTinThietBi.XuatXu,
                        NamSX = thongTinThietBi.NamSX,
                        HangSanXuat = thongTinThietBi.HangSanXuat,
                        TinhTrang = thongTinThietBi.TinhTrang,
                        KhoaId = thongTinThietBi?.KhoaId,
                        NhanVienId = thongTinThietBi?.NhanVienId,
                        Serial = thongTinThietBi.Serial,
                        Model = thongTinThietBi.Model,
                        GiaTien = thongTinThietBi.GiaTien,
                        ThoiGianBaoDuong = thongTinThietBi.ThoiGianBaoDuong
                    };
                    dto.ThongTinChiTietThietBiDtos.Add(thongTinThietBiDto);
                }
            }
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var excelPackage = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "Báo cáo chi tiết nhập";
                worksheet.Cells["A1:N1"].Merge = true;
                worksheet.Cells["A2"].Value = "Mã:";
                worksheet.Cells["B2"].Value = dto.Ma;
                worksheet.Cells["E2"].Value = "Ngày sản xuất:";
                worksheet.Cells["F2"].Value = dto.NgayNhapXuat;

                worksheet.Cells["A3"].Value = "Người đại diện:";
                worksheet.Cells["B3"].Value = dto.NguoiDaiDien;
                worksheet.Cells["E3"].Value = "Nhà cung cấp:";
                worksheet.Cells["F3"].Value = dto.NhaCungCap;

                worksheet.Cells["A4"].Value = "Nhân viên:";
                worksheet.Cells["B4"].Value = dto.NhanVienId;
                worksheet.Cells["E4"].Value = "Số lượng:";
                worksheet.Cells["F4"].Value = dto.SoLuong;

                worksheet.Cells["A5"].Value = "Tổng tiền: ";
                worksheet.Cells["B5"].Value = dto.TongTien;
                worksheet.Cells["E5"].Value = "Ghi chú:";
                worksheet.Cells["F5"].Value = dto.GhiChu;

                worksheet.Cells["A6"].Value = "Danh sách thông tin thiết bị";


                worksheet.Cells["B7"].Value = "Mã";
                worksheet.Cells["C7"].Value = "Thiết bị y tế ";
                worksheet.Cells["D7"].Value = "Ngày nhập";
                worksheet.Cells["E7"].Value = "Xuất xứ";
                worksheet.Cells["F7"].Value = "Năm sản xuất";
                worksheet.Cells["G7"].Value = "Hãng sản xuất";
                worksheet.Cells["H7"].Value = "Tình trạng";
                worksheet.Cells["I7"].Value = "Khoa";
                worksheet.Cells["J7"].Value = "Nhân viên";
                worksheet.Cells["K7"].Value = "Serial";
                worksheet.Cells["L7"].Value = "Model";
                worksheet.Cells["M7"].Value = "Giá tiền";
                worksheet.Cells["N7"].Value = "Thời gian bảo dưỡng";
                int rowIndex = 8;
                foreach (var item in dto.ThongTinChiTietThietBiDtos)
                {
                    var columnIndex = 1;
                    foreach (var property in typeof(ThongTinChiTietThietBiDto).GetProperties())
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = property.GetValue(item);
                        columnIndex++;
                    }
                    rowIndex++;
                }
                await excelPackage.SaveAsync();
            }
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");

        }

        [HttpPost]
        [Route("bao-cao-xuat")]
        public async Task<IActionResult> ExportExcelXuat(SearchListDto input)
        {
            var entity = _context.PhieuNhapXuat.ToList();
            var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                .Where(x => x.LoaiPhieu == 2)
                .Select(_ => new ExportexcelDto
                {
                    Ma = _.Ma,
                    NgayNhapXuat = _.NgayNhapXuat,
                    NhaCungCap = _.NhaCungCap,
                    NguoiDaiDien = _.NguoiDaiDien,
                    NhanVienId = _.NhanVienId,
                    SoLuong = _.SoLuong,
                    TongTien = _.TongTien,
                    LoaiPhieu = _.LoaiPhieu,
                    GhiChu = _.GhiChu,
                }).ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var maxColumn = 0;
            using var excelPackage = new ExcelPackage(stream);
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "Báo cáo ";
                worksheet.Cells["A2"].Value = "Mã";
                worksheet.Cells["B2"].Value = "Ngày sản xuất";
                worksheet.Cells["C2"].Value = "Nhà cung cấp";
                worksheet.Cells["D2"].Value = "Người đại diện";
                worksheet.Cells["E2"].Value = "Nhân viên";
                worksheet.Cells["F2"].Value = "Số lượng";
                worksheet.Cells["G2"].Value = "Tổng tiền";
                worksheet.Cells["H2"].Value = "Loại phiếu";
                worksheet.Cells["I2"].Value = "Ghi chú";
                maxColumn = Math.Max(maxColumn, 9);
                int rowIndex = 3;
                foreach (var dto in items)
                {
                    var columnIndex = 1;
                    foreach (var property in typeof(ExportexcelDto).GetProperties())
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = property.GetValue(dto);
                        columnIndex++;
                    }
                    rowIndex++;
                }
                for (int i = 1; i <= maxColumn; i++)
                {
                    worksheet.Column(i).AutoFit();
                }
                await excelPackage.SaveAsync();
            }
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
        }
    }
}
