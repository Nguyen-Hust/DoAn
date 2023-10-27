using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.NhanVien;
using WebAPI.Models.PhongBan;
using WebAPI.Models.Shared;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class NhanVienController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public NhanVienController(ApplicationDbContext context)
		{
            _context = context;
        }

        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<NhanSuDto>>> GetList(SearchListDto input)
        {
            var nhanSu = await _context.NhanSu.ToListAsync();
            if (nhanSu.Count > 0)
            {
                var totalCount = nhanSu.Count;
                var items = nhanSu.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter) || _.Ten.Contains(input.Filter)).Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000)
                    .Select(_ => new NhanSuDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        Ten = _.Ten,
                        Email = _.Email,
                        SDT = _.SDT,
                        KhoaId = _.KhoaId,
                        DiaChi = _.DiaChi,
                        LaTruongKhoa = _.LaTruongKhoa,
                        LaQuanLyThietBi = _.LaQuanLyThietBi,
                        AccountId = _.AccountId,
                    }).ToList();
                return new PagedResultDto<NhanSuDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };

            }
            return new PagedResultDto<NhanSuDto>
            {
                Items = new List<NhanSuDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<NhanSuDto> CreateAsync(NhanSuDto product)
        {
            try
            {
                var entity = new NhanSuEntity()
                {
                    Id = product.Id,
                    Ma = product.Ma,
                    Ten = product.Ten,
                    Email = product.Email,
                    SDT = product.SDT,
                    KhoaId = product.KhoaId,
                    DiaChi = product.DiaChi,
                    AccountId = product.AccountId,
                    LaTruongKhoa = product.LaTruongKhoa,
                    LaQuanLyThietBi = product.LaQuanLyThietBi
                };
                _context.NhanSu.Add(entity);
                await _context.SaveChangesAsync();
                return product;
            }catch(Exception e)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("get-by-id")]
        public async Task<NhanSuDto> GetByIdAsync(int id)
        {
            var nhanSu = await _context.NhanSu.FindAsync(id);
            return new NhanSuDto
            {
                Id = nhanSu.Id,
                Ma = nhanSu.Ma,
                Ten = nhanSu.Ten,
                Email = nhanSu.Email,
                SDT = nhanSu.SDT,
                KhoaId = nhanSu.KhoaId,
                DiaChi = nhanSu.DiaChi,
                LaTruongKhoa = nhanSu.LaTruongKhoa,
                LaQuanLyThietBi = nhanSu.LaQuanLyThietBi
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var product = await _context.NhanSu.FindAsync(id);
            if (product == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            _context.NhanSu.Remove(product);
            await _context.SaveChangesAsync();
            return new CommonResultDto<int>(id);
        }

        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<NhanSuDto>> Update(int id, NhanSuDto nhanSu)
        {
            if (id != nhanSu.Id)
            {
                return new CommonResultDto<NhanSuDto>("bad request");
            }
            var nhanSuEntity = await _context.NhanSu.FindAsync(id);
            if (nhanSuEntity == null)
            {
                return new CommonResultDto<NhanSuDto>("Not found");
            }
            nhanSuEntity.Email = nhanSu.Email;
            nhanSuEntity.Ma = nhanSu.Ma;
            nhanSuEntity.SDT = nhanSu.SDT;
            nhanSuEntity.Ten = nhanSu.Ten;
            nhanSuEntity.KhoaId = nhanSu.KhoaId;
            nhanSuEntity.DiaChi = nhanSu.DiaChi;
            nhanSuEntity.LaTruongKhoa = nhanSu.LaTruongKhoa;
            nhanSuEntity.LaQuanLyThietBi = nhanSu.LaQuanLyThietBi;
            await _context.SaveChangesAsync();
            return new CommonResultDto<NhanSuDto>(nhanSu);
        }

        [HttpPost]
        [Route("Get-all-khoa")]
        public async Task<List<KhoaDto>> GetAllKhoa()
        {
            var khoa = await _context.Khoa.ToListAsync();
            var listData = khoa.Select(_ => new KhoaDto
            {
                Id = _.Id,
                Ma = _.Ma,
                Ten = _.Ten,
                Email = _.Email,
                SDT = _.SDT,
            });
            return listData.ToList();
        }
    }
}

