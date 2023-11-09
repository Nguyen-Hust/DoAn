using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using WebAPI.Data;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UploadDanhSachThietBiControoller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadDanhSachThietBiControoller(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
            _context = context;
			_webHostEnvironment = webHostEnvironment;
		}

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> Upload(CancellationToken ct)
        {
            if (Request.Form.Files.Count == 0) return NoContent();

            var file = Request.Form.Files[0];
            var filePath = SaveFile(file);

            // load product requests from excel file
            var productRequests = ExcelHelper.Import<ThongTinChiTietThietBiEntity>(filePath);

            // save product requests to database
            foreach (var productRequest in productRequests)
            {
                var product = new ThongTinChiTietThietBiEntity
                {
                    Id = 0,
                    Ma  = productRequest.Ma,
                    ThietBiYTeId = productRequest.ThietBiYTeId,
                    NgayNhap = productRequest.NgayNhap,
                    XuatXu = productRequest.XuatXu,
                    NamSX = productRequest.NamSX,
                    HangSanXuat = productRequest.HangSanXuat,
                    TinhTrang = productRequest.TinhTrang,
                    KhoaId = productRequest.KhoaId,
                    NhanVienId = productRequest.NhanVienId,
                    Serial = productRequest.Serial,
                    Model = productRequest.Model,
                    GiaTien = productRequest.GiaTien,
                    ThoiGianBaoDuong = productRequest.ThoiGianBaoDuong,
                };
                await _context.AddAsync(product, ct);
            }
            await _context.SaveChangesAsync(ct);

            return Ok();
        }

        // save the uploaded file into wwwroot/uploads folder
        private string SaveFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new BadHttpRequestException("File is empty.");
            }

            var extension = Path.GetExtension(file.FileName);

            var webRootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var folderPath = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Guid.NewGuid()}.{extension}";
            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return filePath;
        }
    }
}

