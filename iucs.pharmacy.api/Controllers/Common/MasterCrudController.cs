using iucs.pharmacy.application.Services.CommonCurdService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers.Common
{
    [ApiController]
    public abstract class MasterCrudController<TDto, TEntity> : ControllerBase
    {
        protected readonly ICrudService<TDto, TEntity> _service;

        protected MasterCrudController(ICrudService<TDto, TEntity> service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public virtual async Task<IActionResult> Update(Guid id, [FromBody] TDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
