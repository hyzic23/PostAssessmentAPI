using Microsoft.AspNetCore.Mvc;
using PostAssessmentAPI.IService;
using PostAssessmentAPI.Model;
using PostAssessmentAPI.Utils;

namespace PostAssessmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostAssessmentController : ControllerBase
	{
		private readonly IPostAssessmentService _service;

		public PostAssessmentController(IPostAssessmentService service)
		{
			_service = service;
		}

        
        [HttpGet]
        [Route("Posts")]
		public async Task<IActionResult> Posts(string tags, string? sortBy = "id", string? direction = "asc")
		{
            
            if (String.IsNullOrEmpty(tags))
            {
                return BadRequest("tags parameter is required");
            }
            else if (!PostUtils.IsSortByValid(sortBy))
            {
                return BadRequest("sortBy parameter is invalid");
            }
            else if (!PostUtils.IsDirectionByValid(direction))
            {
                return BadRequest("direction parameter is invalid");
            }


            PostRequest request = new PostRequest()
			{
				Tags = tags.Trim(),
				SortBy = sortBy.Trim(),
				Direction = direction.Trim()
			};

            var response = await _service.GetPost(request);
			return Ok(response);
		}

	}
}

