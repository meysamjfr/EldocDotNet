using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Post;
using Project.Application.DTOs.PostCategory;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostCategoryService _postCategoryService;

        public PostsController(IPostService postService, IPostCategoryService postCategoryService)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
        }
        
        [HttpGet]
        [Produces(typeof(Response<List<PostDTO>>))]
        public async Task<JsonResult> GetAll(FilterPosts input)
        {
            var res = await _postService.FilterPaginate(input);

            return new Response<List<PostDTO>>(res).ToJsonResult();
        }

        [HttpGet("categories")]
        [Produces(typeof(Response<List<PostCategoryDTO>>))]
        public async Task<JsonResult> Categories()
        {
            var res = await _postCategoryService.GetAll();

            return new Response<List<PostCategoryDTO>>(res).ToJsonResult();
        }

        [HttpGet("{id}")]
        [Produces(typeof(Response<PostDTO>))]
        public async Task<JsonResult> Get(int id)
        {
            var res = await _postService.GetPost(id);

            return new Response<PostDTO>(res).ToJsonResult();
        }
    }
}
