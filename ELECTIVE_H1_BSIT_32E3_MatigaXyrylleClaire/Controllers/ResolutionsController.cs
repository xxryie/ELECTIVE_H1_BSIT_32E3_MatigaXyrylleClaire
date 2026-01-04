using ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Models;
using ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Controllers
{
    [Route("api/resolutions")]  
    [ApiController]
    public class ResolutionsController : ControllerBase  
    {
        private readonly ResolutionService _service;

        public ResolutionsController(ResolutionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? isDone, [FromQuery] string? title)
        {
            bool? isDoneValue = null;
            if (!string.IsNullOrEmpty(isDone))
            {
                if (bool.TryParse(isDone, out bool parsed))
                {
                    isDoneValue = parsed;
                }
                else
                {
                    return BadRequest(new ErrorResponse
                    {
                        Error = "BadRequest",
                        Message = "Invalid query parameter.",
                        Details = new List<string> { "isDone must be 'true' or 'false'" }
                    });
                }
            }

            var resolutions = _service.GetAll(isDoneValue, title);
            return Ok(new ResolutionListResponse { Items = resolutions });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Invalid id.",
                    Details = new List<string> { "id must be greater than 0" }
                });
            }

            var resolution = _service.GetById(id);
            if (resolution == null)
            {
                return NotFound(new ErrorResponse
                {
                    Error = "NotFound",
                    Message = "Resolution not found.",
                    Details = new List<string> { $"No resolution with id: {id}" }
                });
            }

            return Ok(resolution);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateResolutionDto? dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Validation failed.",
                    Details = new List<string> { "title is required" }
                });
            }

            var resolution = _service.Create(dto.Title);
            return CreatedAtAction(nameof(GetById), new { id = resolution.Id }, resolution);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateResolutionDto? dto)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Invalid id.",
                    Details = new List<string> { "route id must be greater than 0" }
                });
            }

            if (dto == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Validation failed.",
                    Details = new List<string> { "request body is required" }
                });
            }

            if (id != dto.Id)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Route id does not match body id.",
                    Details = new List<string>
                    {
                        $"route id: {id}",
                        $"body id: {dto.Id}"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Validation failed.",
                    Details = new List<string> { "title is required" }
                });
            }

            var resolution = _service.Update(id, dto);
            if (resolution == null)
            {
                return NotFound(new ErrorResponse
                {
                    Error = "NotFound",
                    Message = "Resolution not found.",
                    Details = new List<string> { $"No resolution with id: {id}" }
                });
            }

            return Ok(resolution);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "BadRequest",
                    Message = "Invalid id.",
                    Details = new List<string> { "id must be greater than 0" }
                });
            }

            var deleted = _service.Delete(id);
            if (!deleted)
            {
                return NotFound(new ErrorResponse
                {
                    Error = "NotFound",
                    Message = "Resolution not found.",
                    Details = new List<string> { $"No resolution with id: {id}" }
                });
            }

            return NoContent();
        }
    }
}