using Commons.Dto;
using Commons.Entities;
using Commons.Services;
using Microsoft.AspNetCore.Mvc;

namespace Commons.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<Entity, Response> : ControllerBase
        where Entity : BaseEntity
        where Response : BaseDto
    {
        protected readonly IBaseService<Entity, Response> _service;

        public BaseController(IBaseService<Entity, Response> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Response>>> GetAll()
        {
            var entities = await _service.GetAll();
            return Ok(entities);
        }

        [HttpGet("{Id}")]
        public virtual async Task<ActionResult<Response>> GetById(Guid Id)
        {
            var entity = await _service.GetById(Id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ActionResult<Response>> Create([FromBody] Entity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            var createdEntity = await _service.Create(entity, cancellationToken);
            return Created(string.Concat(Request.Path, "/", createdEntity.Id), entity);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] Entity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            var existingEntity = await _service.GetById(entity.Id);

            if (existingEntity == null)
                return NotFound();

            await _service.Update(entity, cancellationToken);
            return Ok(entity);
        }


        [HttpDelete("{Id}")]
        public virtual async Task<IActionResult> Delete(Guid Id, CancellationToken cancellationToken)
        {
            var entity = await _service.GetById(Id);

            if (entity == null)
                return NotFound();

            await _service.Delete(Id, cancellationToken);
            return Ok();
        }
    }
}
