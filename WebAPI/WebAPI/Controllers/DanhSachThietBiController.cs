using System.Data;
using System.Net;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.Shared;
using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DanhSachThietBiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DanhSachThietBiController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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


        [Route("UploadExcel")]
        [HttpPost]
        public async Task<ActionResult> ExcelUpload(IFormFile files)
        {
           try
           {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", files.Name);
                using(Stream stream = new FileStream(path, FileMode.Create))
                {
                    files.CopyTo(stream);
                    var filePath = SaveFile(files);
                    var productRequests = ExcelHelper.Import<ThongTinThietBiReaderDto>(filePath);
                    foreach (var _ in productRequests)
                    {
                        var entityThietBi = new ThongTinChiTietThietBiEntity()
                        {
                            Id = 0,
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
                            ThoiGianBaoDuong = _.ThoiGianBaoDuong,
                        };
                        _context.ThongTinChiTietThietBi.Add(entityThietBi);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                return StatusCode(StatusCodes.Status201Created);
           }catch(Exception )
           {
                return StatusCode(StatusCodes.Status500InternalServerError);
           }
        }

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

