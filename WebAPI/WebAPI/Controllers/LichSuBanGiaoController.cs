using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.LichSuBanGIao;
using WebAPI.Models.PhongBan;
using WebAPI.Models.Shared;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class LichSuBanGiaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LichSuBanGiaoController(ApplicationDbContext context)
		{
            _context = context;
		}
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<LichSuBanGiaoDto>>> GetList(SearchListDto input)
        {
            var lichSu = await _context.LichSuBanGiaoThuHoi.ToListAsync();
            if (lichSu.Count > 0)
            {
                var totalCount = lichSu.Count;
                var items = lichSu.Skip(input.SkipCount ?? 0).Take(input.MaxResultCount ?? 1000)
                    .Select(_ => new LichSuBanGiaoDto
                    {
                        Id = _.Id,
                        NhanVienId = _.NhanVienId,
                        ChiTietThietBiId = _.ChiTietThietBiId,
                        NgayThucHien = _.NgayThucHien
                    }).ToList();
                return new PagedResultDto<LichSuBanGiaoDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };

            }
            return new PagedResultDto<LichSuBanGiaoDto>
            {
                Items = new List<LichSuBanGiaoDto>(),
                TotalCount = 0
            };
        }
    }
}

