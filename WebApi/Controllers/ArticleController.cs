using System.ComponentModel.DataAnnotations;
using Articles.Application.DTOs.Article;
using Articles.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Articles.Domain.Exceptions.DomainException;

namespace WebApi.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
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
            try
            {
                var article = await _articleService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
            }
            catch (DuplicateArticleNameException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Errores de validación",
                    errors = ex.Message.Split(';').Select(e => e.Trim()).ToArray()
                });
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
            try
            {
                var article = await _articleService.UpdateAsync(id, dto);
                return Ok(article);
            }
            catch (ArticleNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Errores de validación",
                    errors = ex.Message.Split(';').Select(e => e.Trim()).ToArray()
                });
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
