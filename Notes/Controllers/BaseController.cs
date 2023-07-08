using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Service.Abstract;

namespace Notes.Controllers
{
    public class BaseController<Dto, Entity> : Controller
    {
        private readonly IGenericService<Dto, Entity> _genericService;
        protected readonly IMapper Mapper;

        public BaseController(IGenericService<Dto, Entity> genericService, IMapper mapper)
        {
            this._genericService = genericService;
            this.Mapper = mapper;
        }

        [HttpGet("GetAllNotes")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _genericService.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            if(result.Response is null)
            {
                return NoContent();
            }

            return StatusCode(201, result);
        }

        [NonAction]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _genericService.GetByIdAsync(id);

            if (result.Success)
            {
                return StatusCode(201, result);
            }

            return BadRequest(result.Message);
        }

        [NonAction]
        public async Task<IActionResult> CreateAsync([FromBody] Dto entity)
        {
            var result = await _genericService.InsertAsync(entity);

            if (result.Success)
            {
                return StatusCode(201, result);
            }

            return BadRequest(result.Message);
        }

        [NonAction]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Dto entity)
        {
            var result = await _genericService.UpdateAsync(id, entity);

            if (result.Success)
            {
                return StatusCode(201, result);
            }

            return BadRequest(result.Message);
        }

        [NonAction]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _genericService.RemoveAsync(id);

            if (result.Success)
            {
                return StatusCode(201, result);
            }

            return BadRequest(result.Message);
        }
    }
}
