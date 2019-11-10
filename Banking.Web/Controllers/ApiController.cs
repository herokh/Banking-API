//using Banking.Application.DTOs.Interfaces;
//using Banking.Application.Models.Interfaces;
//using Banking.Infrastructure.Repositories.Interfaces;
//using FluentValidation;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Banking.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public abstract class ApiController<TEntity, TDto, TValidator, TRepository> : ControllerBase
//        where TEntity : class, IEntity
//        where TDto : IDto<TEntity>
//        where TValidator : IValidator<TDto>
//        where TRepository : IRepository<TEntity>
//    {
//        private readonly TRepository _repository;
//        private readonly TValidator _validator;

//        public ApiController(TRepository repository, TValidator validator)
//        {
//            _repository = repository;
//            _validator = validator;
//        }

//        // GET: api/[controller]
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
//        {
//            var entities = await _repository.GetAll();
//            return Ok(entities);
//        }

//        // GET: api/[controller]/x
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TEntity>> Get(int id)
//        {
//            var entity = await _repository.Get(id);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            return Ok(entity);
//        }

//        // PUT: api/[controller]/x
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Put(int id, TEntity entity)
//        {
//            if (id != entity.Id)
//            {
//                return BadRequest();
//            }
//            await _repository.Update(entity);
//            return NoContent();
//        }

//        // POST: api/[controller]
//        [HttpPost]
//        public async Task<ActionResult<TEntity>> Post(TDto dto)
//        {
//            var validationResult = _validator.Validate<TDto>(dto);

//            if (!validationResult.IsValid)
//            {
//                return ValidationProblem(validationResult.Errors.First().ErrorMessage);
//            }

//            var entity = dto.ConvertToEntity();

//            await _repository.Add(entity);
//            return CreatedAtAction("Get", new { id = entity.Id }, entity);
//        }

//        // DELETE: api/[controller]/x
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<TEntity>> Delete(int id)
//        {
//            var entity = await _repository.Delete(id);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            return Ok(entity);
//        }

//    }
//}
