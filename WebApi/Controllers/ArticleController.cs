using Articles.Application.DTOs.Article;
using Articles.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static Articles.Domain.Exceptions.DomainException;

namespace WebApi.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IValidator<CreateArticleDto> _createValidator;
        private readonly IValidator<UpdateArticleDto> _updateValidator;

        public ArticleController(IArticleService articleService, IValidator<CreateArticleDto> createValidator, IValidator<UpdateArticleDto> updateValidator)
        {
            _articleService = articleService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAll()
        {
            try
            {
                var articles = await _articleService.GetAllAsync();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetById(int id)
        {
            try
            {
                var article = await _articleService.GetByIdAsync(id);
                return Ok(article);
            }
            catch (ArticleNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> Create([FromBody] CreateArticleDto dto)
        {
            // Validar con FluentValidation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
            {
                message = "Errores de validación",
                errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
            });
            }
            try
            {
                var article = await _articleService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
            }
            catch (DuplicateArticleNameException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticleDto>> Update(int id, [FromBody] UpdateArticleDto dto)
        {
             // Validar con FluentValidation
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
            {
                message = "Errores de validación",
                errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
            });
            }
            try
            {
                var article = await _articleService.UpdateAsync(id, dto);
                return Ok(article);
            }
            catch (ArticleNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DuplicateArticleNameException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _articleService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArticleNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}
