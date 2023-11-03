using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.LoaiThietBi;
using WebAPI.Models.PhongBan;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class LoaiThietBiController
    {
        private readonly ApplicationDbContext _context;
        public LoaiThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get-list")]
        public async Task<ActionResult<PagedResultDto<LoaiThietBiDto>>> GetList(SearchListDto input)
        {
            var loaiThietBi = await _context.LoaiThietBi.ToListAsync();
            if (loaiThietBi.Count > 0)
            {
                var totalCount = loaiThietBi.Count;
                var items = loaiThietBi.Where(_ => string.IsNullOrEmpty(input.Filter) || _.Ma.Contains(input.Filter) || _.Ten.Contains(input.Filter))
                    .Skip(input.SkipCount??0).Take(input.MaxResultCount??1000).Select(_ => new LoaiThietBiDto
                    {
                        Id = _.Id,
                        Ten = _.Ten,
                        Ma = _.Ma
                    }).ToList();
                return new PagedResultDto<LoaiThietBiDto>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            return new PagedResultDto<LoaiThietBiDto>
            {
                Items = new List<LoaiThietBiDto>(),
                TotalCount = 0
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<CommonResultDto<LoaiThietBiDto>> CreateAsync(LoaiThietBiDto product)
        {
            var temp = _context.LoaiThietBi.FirstOrDefault(_ => _.Ma == product.Ma);
            if (temp != null) return new CommonResultDto<LoaiThietBiDto>("Mã bị trùng");
            var entity = new LoaiThietBiEntity
            {
                Id = product.Id,
                Ma = product.Ma,
                Ten = product.Ten,
            };
            _context.LoaiThietBi.Add(entity);
            await _context.SaveChangesAsync();
            return new CommonResultDto<LoaiThietBiDto>(product);
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<LoaiThietBiDto> GetByIdAsync(int id)
        {
            var loaiThietBi = await _context.LoaiThietBi.FindAsync(id);
            return new LoaiThietBiDto
            {
                Id = loaiThietBi.Id,
                Ten = loaiThietBi.Ten,
                Ma = loaiThietBi.Ma,
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<CommonResultDto<int>> DeleteAsync(int id)
        {
            var loaiThietBi = await _context.LoaiThietBi.FindAsync(id);
            if (loaiThietBi == null)
            {
                return new CommonResultDto<int>("Not found");
            }
            _context.LoaiThietBi.Remove(loaiThietBi);
            await _context.SaveChangesAsync();
            return new CommonResultDto<int>(id);
        }
        [HttpPost]
        [Route("update")]
        public async Task<CommonResultDto<LoaiThietBiDto>> Update(int id, LoaiThietBiDto loaiThietBi)
        {
            if (id != loaiThietBi.Id)
            {
                return new CommonResultDto<LoaiThietBiDto>("bad request");
            }
            var entity = await _context.LoaiThietBi.FindAsync(id);
            if (entity == null)
            {
                return new CommonResultDto<LoaiThietBiDto>("Not found");
            }
            var temp = _context.LoaiThietBi.FirstOrDefault(_ => _.Ma == loaiThietBi.Ma);
            if (temp != null && temp?.Id != id) return new CommonResultDto<LoaiThietBiDto>("Mã bị trùng");
            entity.Ma = loaiThietBi.Ma;
            entity.Ten = loaiThietBi.Ten;
            _context.LoaiThietBi.Update(entity);
            await _context.SaveChangesAsync();
            return new CommonResultDto<LoaiThietBiDto>(loaiThietBi);
        }
    }
}
