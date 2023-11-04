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
using WebAPI.Models.ExportImport;
using WebAPI.Models.PhieuSuaChua;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class BaoCaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public BaoCaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("bao-cao-nhap")]
        public async Task<IActionResult> ExportExcel(SearchListDto input)
        {
            var entity = _context.PhieuNhapXuat.ToList();
            var nhanVienEntity = _context.NhanSu.ToList();
            var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                .Where(x => x.LoaiPhieu == 1)
                .Select(_ => new ExportPhieuNhapXuatDto
                {
                    Ma = _.Ma,
                    NgayNhapXuat = _.NgayNhapXuat.ToString("dd/MM/yyyy"),
                    NhaCungCap = _.NhaCungCap,
                    NguoiDaiDien = _.NguoiDaiDien,
                    NhanVien = nhanVienEntity.FirstOrDefault(x => x.Id == _.NhanVienId).Ten,
                    SoLuong = _.SoLuong,
                    TongTien = _.TongTien,
                    LoaiPhieu = _.LoaiPhieu == 1 ? "Phiếu nhập" : "Phiếu xuất",
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
                    foreach (var property in typeof(ExportPhieuNhapXuatDto).GetProperties())
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
        [Route("export-pdf-phieu-nhap-xuat")]
        public async Task<IActionResult> ExportDetaiPhieuNhapXuat(int id)
        {
            var phieuNhapXuat = await _context.PhieuNhapXuat.FindAsync(id);
            var dto = new ExportDetailNhapXuatDto()
            {
                Ma = phieuNhapXuat.Ma,
                NgayNhapXuat = phieuNhapXuat.NgayNhapXuat.ToString("dd/MM/yyyy"),
                NhaCungCap = phieuNhapXuat.NhaCungCap,
                NguoiDaiDien = phieuNhapXuat.NguoiDaiDien,
                NhanVienId = phieuNhapXuat.NhanVienId,
                SoLuong = phieuNhapXuat.SoLuong,
                TongTien = phieuNhapXuat.TongTien,
                GhiChu = phieuNhapXuat.GhiChu,
                LoaiPhieu = phieuNhapXuat.LoaiPhieu == 1 ? "Phiếu nhập" : "Phiếu xuất",
            };
            dto.ExportThongTinThietBiDetailDtos = new List<ExportThongTinThietBiDetailDto>();
            var listChiTietPhieu = await _context.ChiTietPhieuNhapXuat.Where(_ => _.PhieuNhapXuatId == id).ToListAsync();
            foreach (var item in listChiTietPhieu)
            {
                var thongTinThietBi = await _context.ThongTinChiTietThietBi.FirstOrDefaultAsync(_ => _.Id == item.ChiTietThietBiId);
                if (thongTinThietBi != null)
                {
                    var thongTinThietBiDto = new ExportThongTinThietBiDetailDto()
                    {
                        Ma = thongTinThietBi.Ma,
                        ThietBiYTeId = thongTinThietBi.ThietBiYTeId,
                        NgayNhap = thongTinThietBi.NgayNhap.ToString("dd/MM/yyyy"),
                        XuatXu = thongTinThietBi.XuatXu,
                        NamSX = thongTinThietBi.NamSX,
                        HangSanXuat = thongTinThietBi.HangSanXuat,
                        TinhTrang = thongTinThietBi.TinhTrang,
                        Serial = thongTinThietBi.Serial,
                        Model = thongTinThietBi.Model,
                        GiaTien = thongTinThietBi.GiaTien,
                        ThoiGianBaoDuong = thongTinThietBi.ThoiGianBaoDuong
                    };
                    dto.ExportThongTinThietBiDetailDtos.Add(thongTinThietBiDto);
                }
            }
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var excelPackage = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                var titleExcel = "Báo cáo chi tiết nhập";
                if (dto.LoaiPhieu == "Phiếu xuất")
                {
                    titleExcel = "Báo cáo chi tiết xuất";
                }
                worksheet.Cells["A1"].Value = titleExcel;
                worksheet.Cells["A1:K1"].Merge = true;
                worksheet.Cells["A2"].Value = "Mã:";
                worksheet.Cells["B2"].Value = dto.Ma;
                worksheet.Cells["E2"].Value =  "Ngày sản xuất";
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


                worksheet.Cells["A7"].Value = "Mã";
                worksheet.Cells["B7"].Value = "Thiết bị y tế ";
                worksheet.Cells["C7"].Value = dto.LoaiPhieu == "Phiếu " ? "Ngày xuất:" : "Ngày nhập: ";
                worksheet.Cells["D7"].Value = "Xuất xứ";
                worksheet.Cells["E7"].Value = "Năm sản xuất";
                worksheet.Cells["F7"].Value = "Hãng sản xuất";
                worksheet.Cells["G7"].Value = "Tình trạng";
                worksheet.Cells["H7"].Value = "Serial";
                worksheet.Cells["I7"].Value = "Model";
                worksheet.Cells["J7"].Value = "Giá tiền";
                worksheet.Cells["K7"].Value = "Thời gian bảo dưỡng";
                int rowIndex = 8;
                foreach (var item in dto.ExportThongTinThietBiDetailDtos)
                {
                    var columnIndex = 1;
                    foreach (var property in typeof(ExportThongTinThietBiDetailDto).GetProperties())
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = property.GetValue(item);
                        columnIndex++;
                    }
                    rowIndex++;
                }
                worksheet.Cells.AutoFitColumns();
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
            var nhanVienEntity = await _context.NhanSu.ToListAsync();
            var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                .Where(x => x.LoaiPhieu == 2)
                .Select(_ => new ExportPhieuNhapXuatDto
                {
                    Ma = _.Ma,
                    NgayNhapXuat = _.NgayNhapXuat.ToString("dd/MM/yyyy"),
                    NhaCungCap = _.NhaCungCap,
                    NguoiDaiDien = _.NguoiDaiDien,
                    NhanVien = nhanVienEntity.FirstOrDefault(x => x.Id == _.NhanVienId).Ten,
                    SoLuong = _.SoLuong,
                    TongTien = _.TongTien,
                    LoaiPhieu = _.LoaiPhieu == 1 ? "Phiếu nhập" : "Phiếu xuất",
                    GhiChu = _.GhiChu,
                }).ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var maxColumn = 0;
            using var excelPackage = new ExcelPackage(stream);
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value =  "Báo cáo phiếu xuất";
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
                    foreach (var property in typeof(ExportPhieuNhapXuatDto).GetProperties())
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


        [HttpPost]
        [Route("bao-cao-bao-duong")]
        public async Task<IActionResult> ExportExcelBaoDuong(PhieuBaoDuongInputDto input)
        {
            var entity = await _context.PhieuBaoDuong.Include(_ => _.ChiTietPhieuBaoDuong).ToListAsync();
            var nhanVienEntity = await _context.NhanSu.ToListAsync();
            var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Where(_ => !input.Date.HasValue || input.Date.Value.Date == _.CreateTime.Value.Date)
                .Select(_ => new ExportPhieuBaoDuongDto
                {
                    CreateTime = _.CreateTime?.ToString("dd/MM/y"),
                    NhanVien = nhanVienEntity.FirstOrDefault(x => x.Id == _.NhanVienId).Ten,
                    TrangThai = _.TrangThai,
                    TongThietBi = _.ChiTietPhieuBaoDuong.Count,
                    Ma = _.Ma
                }).ToList();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var maxColumn = 0;
            using var excelPackage = new ExcelPackage(stream);
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "Báo cáo bảo dưỡng";
                worksheet.Cells["A1:E1"].Merge = true;
                worksheet.Cells["A2"].Value = "Mã";
                worksheet.Cells["B2"].Value = "Nhân viên";
                worksheet.Cells["C2"].Value = "Trạng thái";
                worksheet.Cells["D2"].Value = "Ngày tạo phiếu";
                worksheet.Cells["E2"].Value = "Tổng thiết bị";
                maxColumn = Math.Max(maxColumn, 5);
                int rowIndex = 3;
                foreach (var dto in items)
                {
                    var columnIndex = 1;
                    foreach (var property in typeof(ExportPhieuBaoDuongDto).GetProperties())
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


        [HttpPost]
        [Route("bao-cao-sua-chua")]
        public async Task<IActionResult> ExportExcelSuaChua(PhieuSuaChuaInputDto input)
        {
            var phieuSuaChua = await _context.PhieuSuaChua.ToListAsync();
            var thietBiEntity = await _context.ThietBiYTe.ToListAsync();
            var chiTietThietBi = await _context.ThongTinChiTietThietBi.ToListAsync();
            var nhanVienEntity = await _context.NhanSu.ToListAsync();
            var listDto = (from p in phieuSuaChua
                        join chitiet in chiTietThietBi on p.ChiTietThietBiId equals chitiet.Id
                        join tb in thietBiEntity on chitiet.ThietBiYTeId equals tb.Id
                        select new ExportPhieuSuaDto()
                        {
                           ChiTietThietBi = tb.Ten,
                           CreateTime = p.CreateTime,
                           LyDo = p.LyDo,
                           NhanVien = nhanVienEntity.FirstOrDefault(x => x.Id == p.NhanVienId).Ten,
                           TrangThai = p.TrangThai,
                           Ma = p.Ma,
                        }).Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                           .Where(_ => !input.Date.HasValue || input.Date.Value.Date == _.CreateTime.Value.Date);
            var list = listDto.ToList();
            var items = list.Select(_ => new ExportPhieuSuaChuaDto
            {
                CreateTime = _.CreateTime?.ToString("dd/MM/yyyy"),
                NhanVien = _.NhanVien,
                TrangThai = _.TrangThai,
                Ma = _.Ma,
                ChiTietThietBi = _.ChiTietThietBi,
                LyDo = _.LyDo
            });
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var maxColumn = 0;
            using var excelPackage = new ExcelPackage(stream);
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "Báo cáo sửa chữa";
                worksheet.Cells["A2"].Value = "Mã";
                worksheet.Cells["B2"].Value = "Nhân viên";
                worksheet.Cells["C2"].Value = "Chi tiết thiết bị";
                worksheet.Cells["D2"].Value = "Trạng thái";
                worksheet.Cells["E2"].Value = "Lý do";
                worksheet.Cells["F2"].Value = "Ngày tạo phiếu";
                maxColumn = Math.Max(maxColumn, 6);
                int rowIndex = 3;
                foreach (var dto in items)
                {
                    var columnIndex = 1;
                    foreach (var property in typeof(ExportPhieuSuaChuaDto).GetProperties())
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
