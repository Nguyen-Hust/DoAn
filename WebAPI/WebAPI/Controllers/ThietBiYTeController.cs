﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ThietBiYTeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ThietBiYTeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<ThietBiYTeDto>>> GetList(SearchListDto input)
        {
            var thietBiYTe = await _context.ThietBiYTe.ToListAsync();
            if (thietBiYTe.Count > 0)
            {
                var totalCount = thietBiYTe.Count;
                var items = thietBiYTe.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter) || _.Ten.Contains(input.Filter))
                    .Skip(input.SkipCount).Take(input.MaxResultCount).Select(_ => new ThietBiYTeDto
                    {
                        Id = _.Id,
                        Ten = _.Ten,
                        Ma = _.Ma,
                        LoaiTTBYT = _.LoaiTTBYT,
                        MDRR = _.MDRR,
                        SoLuong = _.SoLuong,
                    }).ToList();
                return new PagedResultDto<ThietBiYTeDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<ThietBiYTeDto>
            {
                Items = new List<ThietBiYTeDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<ThietBiYTeDto> CreateAsync(ThietBiYTeDto product)
        {
            _context.ThietBiYTe.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<ThietBiYTeDto> GetByIdAsync(int id)
        {
            var thietBiYTe = await _context.ThietBiYTe.FindAsync(id);
            return new ThietBiYTeDto
            {
                Id = thietBiYTe.Id,
                Ten = thietBiYTe.Ten,
                Ma = thietBiYTe.Ma,
                LoaiTTBYT = thietBiYTe.LoaiTTBYT,
                MDRR = thietBiYTe.MDRR,
                SoLuong = thietBiYTe.SoLuong,
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var product = await _context.ThietBiYTe.FindAsync(id);
            if (product == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            _context.ThietBiYTe.Remove(product);
            await _context.SaveChangesAsync();
            return new CommonResultDto<int>(id);
        }
        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<ThietBiYTeDto>> Update(int id, ThietBiYTeDto thietBiYTe)
        {
            if (id != thietBiYTe.Id)
            {
                return new CommonResultDto<ThietBiYTeDto>("bad request");
            }
            var thietBiYTeEntity = await _context.ThietBiYTe.FindAsync(id);
            if (thietBiYTeEntity == null)
            {
                return new CommonResultDto<ThietBiYTeDto>("Not found");
            }
            thietBiYTeEntity.LoaiTTBYT = thietBiYTe.LoaiTTBYT;
            thietBiYTeEntity.Ma = thietBiYTe.Ma;
            thietBiYTeEntity.MDRR = thietBiYTe.MDRR;
            thietBiYTeEntity.Ten = thietBiYTe.Ten;
            await _context.SaveChangesAsync();
            return new CommonResultDto<ThietBiYTeDto>(thietBiYTe);
        }
    }
}
