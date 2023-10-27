using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.PhongBan;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PhongBanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PhongBanController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<KhoaDto>>> GetList(SearchListDto input)
        {
            var phongBan = await _context.Khoa.ToListAsync();
            if(phongBan.Count > 0)
            {
                var totalCount = phongBan.Count;
                var items = phongBan.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter) || _.Ten.Contains(input.Filter)).Skip(input.SkipCount).Take(input.MaxResultCount)
                    .Select(_ => new KhoaDto
                    {
                        Id = _.Id,
                        Ma = _.Ma,
                        Ten = _.Ten,
                        Email = _.Email,
                        SDT = _.SDT,
                        TruongKhoaId = _.TruongKhoaId
                    }).ToList();
                return new PagedResultDto<KhoaDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };

            }
            return new PagedResultDto<KhoaDto>
            {
                Items = new List<KhoaDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<KhoaDto> CreateAsync(KhoaDto product)
        {
            var entity = new KhoaEntity()
            {
                Id = product.Id,
                Ma = product.Ma,
                Ten = product.Ten,
                Email = product.Email,
                SDT = product.SDT,
                TruongKhoaId = product.TruongKhoaId
            };
            _context.Khoa.Add(entity);
            await _context.SaveChangesAsync();
            return product;
        }


        [HttpGet]
        [Route("get-by-id")]
        public async Task<KhoaDto> GetByIdAsync(int id)
        {
            var khoa = await _context.Khoa.FindAsync(id);
            return new KhoaDto
            {
                Id = khoa.Id,
                Ma = khoa.Ma,
                Ten = khoa.Ten,
                Email = khoa.Email,
                SDT = khoa.SDT,
                TruongKhoaId = khoa.TruongKhoaId
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var product = await _context.Khoa.FindAsync(id);
            if (product == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            _context.Khoa.Remove(product);
            await _context.SaveChangesAsync();
            return new CommonResultDto<int>(id);
        }

        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<KhoaDto>> Update(int id, KhoaDto khoa)
        {
            if (id != khoa.Id)
            {
                return new CommonResultDto<KhoaDto>("bad request");
            }
            var khoaEntity = await _context.Khoa.FindAsync(id);
            if (khoaEntity == null)
            {
                return new CommonResultDto<KhoaDto>("Not found");
            }
            khoaEntity.Email = khoa.Email;
            khoaEntity.Ma = khoa.Ma;
            khoaEntity.SDT = khoa.SDT;
            khoaEntity.Ten = khoa.Ten;
            khoaEntity.TruongKhoaId = khoa?.TruongKhoaId;
            await _context.SaveChangesAsync();
            return new CommonResultDto<KhoaDto>(khoa);
        }
    }
}

