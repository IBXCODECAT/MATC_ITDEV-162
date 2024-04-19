using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly DataContext context;

        public PostsController(DataContext context)
        {
            this.context = context;
        }

        //Get api/posts
        [HttpGet(Name = "GetPosts")]
        public ActionResult<List<Post>> Get()
        {
            return context.posts.ToList();
        }

        /// <summary>
        /// Get API/Posts/[id]
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>A single post matching the specified id</returns>
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public ActionResult<Post> GetPost(Guid id)
        {
            Post? post = this.context.posts.Find(id);

            if (post is null)
            {
                return new ObjectResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new ObjectResult(post) { StatusCode = StatusCodes.Status200OK};
        }

        [HttpPost(Name = "Create")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.Created)]
        public ActionResult<Post> Create([FromBody] Post request)
        {
            Post post = new Post
            {
                Id = request.Id,
                Title = request.Title,
                Body = request.Body,
                Date = request.Date
            };

            context.posts.Add(post);

            bool success = context.SaveChanges() > 0;

            if (success)
            {
                return new ObjectResult(post) { StatusCode = StatusCodes.Status201Created };
            }

            throw new Exception("Error creating post");
        }

        [HttpPut("{id}", Name = "Update")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.Created)]
        public ActionResult<Post> Update([FromBody] Post request)
        {
            Post? postToUpdate = context.posts.Find(request.Id);

            if (postToUpdate is null)
            {
                Post newPost = new Post()
                {
                    Id = request.Id,
                    Title = request.Title,
                    Body = request.Body,
                    Date = request.Date
                };

                context.posts.Add(newPost);

                bool success = context.SaveChanges() > 0;

                return success ? new ObjectResult(newPost) { StatusCode = StatusCodes.Status201Created } : new ObjectResult(null) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            else
            {
                postToUpdate.Title = request.Title;
                postToUpdate.Body = request.Body;
                postToUpdate.Date = request.Date;

                bool success = context.SaveChanges() > 0;

                return success ? new ObjectResult(postToUpdate) { StatusCode = StatusCodes.Status200OK } : throw new Exception("Error creating or updating resource.");
            }
        }

        [HttpDelete("id", Name = "Delete")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public ObjectResult Delete(Guid id)
        {
            Post? postToDelete = context.posts.Find(id);

            if (postToDelete is null)
            {
                return new ObjectResult(postToDelete) { StatusCode = StatusCodes.Status404NotFound };   
            }

            context.posts.Remove(postToDelete);

            bool success = context.SaveChanges() > 0;

            if (!success)
            {
                return new ObjectResult(null) {StatusCode = StatusCodes.Status500InternalServerError};
                throw new Exception("Error deleting resource.");
            }
            else
            {
                return new ObjectResult(null) { StatusCode = StatusCodes.Status204NoContent };
            }
        }
    }
}
