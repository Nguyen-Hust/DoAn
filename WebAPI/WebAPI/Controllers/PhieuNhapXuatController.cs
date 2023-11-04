using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Enums;
using WebAPI.Models.LoaiThietBi;
using WebAPI.Models.NhanVien;
using WebAPI.Models.PhieuBaoDuong;
using WebAPI.Models.PhieuNhapXuat;
using WebAPI.Models.PhieuSuaChua;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;
using WebAPI.Models.ThongTinChiTietThietBi;

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
                        NhaCungCap = _.NhaCungCap,
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

        [HttpPost]
        [Route("get-list-phieu-nhap")]
        public async Task<ActionResult<PagedResultDto<PhieuNhapXuatDto>>> GetListPhieuNhap(SearchListDto input)
        {
            var entity = await _context.PhieuNhapXuat.ToListAsync();
            if (entity.Count > 0)
            {
                var totalCount = entity.Count;
                var items = entity.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter))
                    .Where(x => x.LoaiPhieu == 1)
                    .Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000).Select(_ => new PhieuNhapXuatDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        NhanVienId = _.NhanVienId,
                        NgayNhapXuat = _.NgayNhapXuat,
                        NguoiDaiDien = _.NguoiDaiDien,
                        NhaCungCap = _.NhaCungCap,
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

        [HttpPost]
        [Route("get-list-phieu-xuat")]
        public async Task<ActionResult<PagedResultDto<PhieuNhapXuatDto>>> GetListPhieuXuat(SearchListDto input)
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
                        NhaCungCap = _.NhaCungCap,
                        SoLuong = _.SoLuong,
                        TongTien = _.TongTien,
                        GhiChu = _.GhiChu,
                        LoaiPhieu = _.LoaiPhieu,
                    }).ToList();
                return new PagedResultDto<PhieuNhapXuatDto>
                {
                    Items = items.Where(x => x.LoaiPhieu == 2).ToList(),
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<PhieuNhapXuatDto>
            {
                Items = new List<PhieuNhapXuatDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<PhieuNhapXuatDto> CreateAsync(PhieuNhapXuatDto product)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var nhanVienId = _context.NhanSu.First(_ => _.AccountId == userId).Id;
                var entity = new PhieuNhapXuatEntity
                {
                    Id = product.Id,
                    Ma = product.Ma,
                    NgayNhapXuat = product.NgayNhapXuat,
                    NhaCungCap = product.NhaCungCap,
                    NguoiDaiDien = product.NguoiDaiDien,
                    NhanVienId = nhanVienId,
                    SoLuong = product.SoLuong,
                    TongTien = product.TongTien,
                    GhiChu = product.GhiChu,
                    LoaiPhieu = product.LoaiPhieu,
                };
                _context.PhieuNhapXuat.Add(entity);
                await _context.SaveChangesAsync();
                if (product.ThongTinChiTietThietBiDtos != null && product.ThongTinChiTietThietBiDtos.Count > 0)
                {
                    foreach (var item in product.ThongTinChiTietThietBiDtos)
                    {
                        var entityThietBi = new ThongTinChiTietThietBiEntity
                        {
                            Id = item.Id,
                            Ma = item.Ma,
                            ThietBiYTeId = item.ThietBiYTeId,
                            NgayNhap = item.NgayNhap,
                            XuatXu = item.XuatXu,
                            NamSX = item.NamSX,
                            HangSanXuat = item.HangSanXuat,
                            TinhTrang = item.TinhTrang,
                            KhoaId = item.KhoaId,
                            NhanVienId = item?.NhanVienId,
                            Serial = item.Serial,
                            Model = item.Model,
                            GiaTien = item.GiaTien,
                            ThoiGianBaoDuong = item.ThoiGianBaoDuong
                        };
                        _context.ThongTinChiTietThietBi.Add(entityThietBi);
                        await _context.SaveChangesAsync();

                        var entityChiTietPhieu = new ChiTietPhieuNhapXuatEntity
                        {
                            PhieuNhapXuatId = entity.Id,
                            ChiTietThietBiId = entityThietBi.Id,
                            GiaTien = item.GiaTien
                        };
                        _context.ChiTietPhieuNhapXuat.Add(entityChiTietPhieu);
                    }
                }
                await _context.SaveChangesAsync();
                return product;
            }catch(Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<PhieuNhapXuatDto>> Update(int id, PhieuNhapXuatDto product)
        {
            if (id != product.Id)
            {
                return new CommonResultDto<PhieuNhapXuatDto>("bad request");
            }
            var entity = await _context.PhieuNhapXuat.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<PhieuNhapXuatDto>("Not found");
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVienId = _context.NhanSu.First(_ => _.AccountId == userId).Id;
            entity.Ma = product.Ma;
            entity.NgayNhapXuat = product.NgayNhapXuat;
            entity.NhaCungCap = product.NhaCungCap;
            entity.NguoiDaiDien = product.NguoiDaiDien;
            entity.NhanVienId = nhanVienId;
            entity.SoLuong = product.SoLuong;
            entity.TongTien = product.TongTien;
            entity.GhiChu = product.GhiChu;
            entity.LoaiPhieu = product.LoaiPhieu;
            var temp = _context.ChiTietPhieuNhapXuat.Where(_ => _.PhieuNhapXuatId == id);
            _context.ChiTietPhieuNhapXuat.RemoveRange(temp);
            if (product.ThongTinChiTietThietBiDtos != null && product.ThongTinChiTietThietBiDtos.Count > 0)
            {
                foreach (var item in product.ThongTinChiTietThietBiDtos)
                {
                    var etntitThongTinTB = await _context.ThongTinChiTietThietBi.FirstOrDefaultAsync(_ => _.Id == item.Id);
                    if(etntitThongTinTB != null) {
                        etntitThongTinTB.Ma = item.Ma;
                        etntitThongTinTB.ThietBiYTeId = item.ThietBiYTeId;
                        etntitThongTinTB.NgayNhap = item.NgayNhap;
                        etntitThongTinTB.XuatXu = item.XuatXu;
                        etntitThongTinTB.NamSX = item.NamSX;
                        etntitThongTinTB.HangSanXuat = item.HangSanXuat;
                        etntitThongTinTB.TinhTrang = item.TinhTrang;
                        etntitThongTinTB.KhoaId = item.KhoaId;
                        etntitThongTinTB.NhanVienId = item?.NhanVienId;
                        etntitThongTinTB.Serial = item.Serial;
                        etntitThongTinTB.Model = item.Model;
                        etntitThongTinTB.GiaTien = item.GiaTien;
                        etntitThongTinTB.ThoiGianBaoDuong = item.ThoiGianBaoDuong;

                        var entityChiTietPhieu = new ChiTietPhieuNhapXuatEntity
                        {
                            PhieuNhapXuatId = entity.Id,
                            ChiTietThietBiId = item.Id,
                            GiaTien = item.GiaTien
                        };
                        _context.ChiTietPhieuNhapXuat.Add(entityChiTietPhieu);
                    }else
                    {
                        var entityThietBi = new ThongTinChiTietThietBiEntity
                        {
                            Id = item.Id,
                            Ma = item.Ma,
                            ThietBiYTeId = item.ThietBiYTeId,
                            NgayNhap = item.NgayNhap,
                            XuatXu = item.XuatXu,
                            NamSX = item.NamSX,
                            HangSanXuat = item.HangSanXuat,
                            TinhTrang = item.TinhTrang,
                            KhoaId = item.KhoaId,
                            NhanVienId = item?.NhanVienId,
                            Serial = item.Serial,
                            Model = item.Model,
                            GiaTien = item.GiaTien,
                            ThoiGianBaoDuong = item.ThoiGianBaoDuong
                        };
                        _context.ThongTinChiTietThietBi.Add(entityThietBi);
                        await _context.SaveChangesAsync();

                        var entityChiTietPhieu = new ChiTietPhieuNhapXuatEntity
                        {
                            PhieuNhapXuatId = entity.Id,
                            ChiTietThietBiId = entityThietBi.Id,
                            GiaTien = item.GiaTien
                        };
                        _context.ChiTietPhieuNhapXuat.Add(entityChiTietPhieu);
                    }
                   
                }
            }

            await _context.SaveChangesAsync();
            return new CommonResultDto<PhieuNhapXuatDto>(product);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var product = await _context.PhieuNhapXuat.FindAsync(id);
            if (product == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            _context.PhieuNhapXuat.Remove(product);
            var tmp = _context.ChiTietPhieuNhapXuat.Where(_ => _.PhieuNhapXuatId == id);
            _context.ChiTietPhieuNhapXuat.RemoveRange();
            await _context.SaveChangesAsync();
            return new CommonResultDto<int>(id);
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<PhieuNhapXuatDto> GetByIdAsync(int id)
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
            foreach(var item in listChiTietPhieu)
            {
                var thongTinThietBi = await _context.ThongTinChiTietThietBi.FirstOrDefaultAsync(_ => _.Id == item.ChiTietThietBiId);
                if(thongTinThietBi != null)
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
            return dto;
        }

        [HttpGet]
        [Route("get-danh-sach-thiet-bi")]
        public async Task<List<ThietBiYTeDto>> GetDanhSachThietBiAsync()
        {
            var thietBiYTe = await _context.ThietBiYTe.ToListAsync();
            var items = thietBiYTe
                .Select(_ => new ThietBiYTeDto
                {
                    Id = _.Id,
                    Ma = _.Ma,
                    Ten = _.Ten
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

