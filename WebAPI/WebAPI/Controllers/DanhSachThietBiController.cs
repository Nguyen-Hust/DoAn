using System.Data;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using System.Text;
using ExcelDataReader;
using Hangfire.MemoryStorage.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.Dtos;
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
                        GiaTien = _.GiaTien ?? 0,
                        ThoiGianBaoDuong = _.ThoiGianBaoDuong ?? 0,
                        DaXuat = _.DaXuat
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

        [HttpPost]
        [Route("get-list-by-user")]
        public async Task<ActionResult<PagedResultDto<ThongTinChiTietThietBiDto>>> GetListByUse(SearchListDto input)
        {
            var phongBan = await _context.ThongTinChiTietThietBi.ToListAsync();
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVienId = _context.NhanSu.First(_ => _.AccountId == userId).Id;
            if (phongBan.Count > 0)
            {
                var totalCount = phongBan.Count;
                var items = phongBan.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Where(x => x.NhanVienId == nhanVienId)
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000)
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
                        GiaTien = _.GiaTien ?? 0,
                        ThoiGianBaoDuong = _.ThoiGianBaoDuong ?? 0,
                        DaXuat = _.DaXuat
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
        [Route("get-all-ma")]
        public List<string> GetAllMa()
        {
            return _context.ThongTinChiTietThietBi.Select(_ => _.Ma).ToList();
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
                GiaTien = _.GiaTien ?? 0,
                ThoiGianBaoDuong = _.ThoiGianBaoDuong ?? 0
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
                entity.NgayNhap = _.NgayNhap??DateTime.Now;
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

        [HttpPost]
        [Route("create-list")]
        public async Task<CommonResultDto<bool>> CreateListAsync(List<ThongTinThietBiReaderDto> input)
        {
            try
            {
                foreach (var _ in input)
                {
                    var entityThietBi = new ThongTinChiTietThietBiEntity()
                    {
                        Id = 0,
                        Ma = _.Ma,
                        ThietBiYTeId = _.ThietBiYTeId,
                        NgayNhap = _.NgayNhap,
                        XuatXu = _.XuatXu ?? "",
                        NamSX = _.NamSX,
                        HangSanXuat = _.HangSanXuat ?? "",
                        TinhTrang = _.TinhTrang,
                        KhoaId = _.KhoaId > 0 ? _.KhoaId : null,
                        NhanVienId = _.NhanVienId > 0 ? _.NhanVienId : null,
                        Serial = _.Serial ?? "",
                        Model = _.Model ?? "",
                        GiaTien = _.GiaTien,
                        ThoiGianBaoDuong = _.ThoiGianBaoDuong,
                    };
                    _context.ThongTinChiTietThietBi.Add(entityThietBi);
                    await _context.SaveChangesAsync();
                }
                return new CommonResultDto<bool>(true);
            }catch(Exception ex)
            {
                return new CommonResultDto<bool>("Đã có lỗi sảy ra");
            }
        }


        [Route("UploadExcel")]
        [HttpPost]
        public async Task<ReadFileExcelDto<ThongTinThietBiReaderDto>> ExcelUpload(IFormFile files)
        {
            var ret = new ReadFileExcelDto<ThongTinThietBiReaderDto>();
            var excelValues = await ExcelHelper.CommonReadValueImportExcel(files, 0, 3, 13);
            CheckFileIsCorrectTemplate(excelValues);
            excelValues.RemoveAt(0);
            foreach (var values in excelValues)
            {
                var resultDto = await GetDtoAsync(values, ret);
                if (resultDto == null) continue;
                if (resultDto.IsValid)
                {
                    ret.ValidDtos.Add(resultDto);
                }
                else
                {
                    ret.InValidDtos.Add(resultDto);
                }
            }
            return ret;
        }

        protected void CheckFileIsCorrectTemplate(List<List<string>> excelValues)
        {
            var error = new StringBuilder("File excel không đúng mẫu import.");
            var titleRow = excelValues.FirstOrDefault();
            var titleValids = new List<string>()
                {
                    "Mã chi tiết thiết bị (*)", "Mã thiết bị (*)", "Ngày nhập (*)",
                };
            var indexValids = new List<int>()
                {
                    0, 1, 2,
                };
            for (int i = 0; i < indexValids.Count(); i++)
            {
                if (titleRow[indexValids[i]] != titleValids[i])
                {
                    throw new Exception(error.ToString());
                }
            }
        }

        private async Task<ReadFileExcelItemDto<ThongTinThietBiReaderDto>> GetDtoAsync(List<string> values, ReadFileExcelDto<ThongTinThietBiReaderDto> ret)
        {
            var arrValue = values.ToArray();
            List<string> errors = new List<string>();
            var dto = new ThongTinThietBiReaderDto()
            {
                XuatXu = arrValue[3],
                NamSX = tryInt(arrValue[4]),
                HangSanXuat = arrValue[5],
                TinhTrang = arrValue[6],
                KhoaId = await GetKhoa(arrValue[7]),
                NhanVienId = await GetNhanVien(arrValue[8]),
                Serial = arrValue[9],
                Model = arrValue[10],
                GiaTien = tryDecimal(arrValue[11]),
                ThoiGianBaoDuong = tryInt(arrValue[12])
            };

            if (string.IsNullOrEmpty(arrValue[0]))
            {
                errors.Add("Mã chi tiết sản phẩm không được để trống");
            }
            else
            {
                var Ma = await CheckExistMaThietBi(arrValue[0]);
                dto.Ma = arrValue[0];
                if (Ma > 0)
                {
                    errors.Add("Mã chi tiết thiết bị không hợp lệ");
                }
            }

            if (string.IsNullOrEmpty(arrValue[1]))
            {
                errors.Add("Mã thiết bị không được để trống");
            }
            else
            {
                var productUnit = await CheckMaThietBi(arrValue[1]);
                dto.ThietBiYTeId = productUnit;
                if (productUnit == 0)
                {
                    errors.Add("Mã thiết bị không hợp ");
                }
            }

            if (string.IsNullOrEmpty(arrValue[2]))
            {
                errors.Add("Ngày nhập không được để trống");
            }
            else
            {
                var inputDate = ValidateDate(arrValue[2]);
                dto.NgayNhap = (DateTime)inputDate;
                if (!inputDate.HasValue)
                {
                    errors.Add("Ngày nhập không hợp lệ");
                }
            }

            if (errors.Count() > 0)
            {
                return new ReadFileExcelItemDto<ThongTinThietBiReaderDto>()
                {
                    Data = dto,
                    InvalidErrors = errors
                };
            }
            return new ReadFileExcelItemDto<ThongTinThietBiReaderDto>()
            {
                Data = dto,
            };
        }

        private async Task<int?> CheckExistMaThietBi(string code)
        {
            var listChiThietThietBi = await _context.ThongTinChiTietThietBi.ToListAsync();
            var ExistMa = listChiThietThietBi.FirstOrDefault(x => x.Ma == code);
            return ExistMa == null ? 0 : ExistMa.Id;
        }

        private async Task<int?> GetKhoa(string code)
        {
            try
            {
                var listKhoa = await _context.Khoa.ToListAsync();
                var khoa = listKhoa.FirstOrDefault(x => x.Ma == code);
                return khoa == null ? 0 : khoa.Id;
            }catch(Exception ex)
            {
                return null;
            }
        }

        private async Task<int?> GetNhanVien(string code)
        {
            try
            {
                var listNhanVien = await _context.NhanSu.ToListAsync();
                var nhanVien = listNhanVien.FirstOrDefault(x => x.Ma == code);
                return nhanVien == null ? 0 : nhanVien.Id;
            }catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<int> CheckMaThietBi(string code)
        {
            var listThietBi = await _context.ThietBiYTe.ToListAsync();
            var codeConcept = listThietBi.FirstOrDefault(x => x.Ma == code);
            return codeConcept == null ? 0 : codeConcept.Id;
        }

        private int? tryInt(string number)
        {
            if (Int32.TryParse(number, out int b))
            {
                return b;
            }else
            {
                return null;
            }
        }

        private decimal? tryDecimal(string number)
        {
            if (Decimal.TryParse(number, out decimal b))
            {
                return b;
            }
            else
            {
                return null;
            }
        }

        private DateTime? ValidateDate(string dateTime)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(dateTime.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return dt;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
    }
}

