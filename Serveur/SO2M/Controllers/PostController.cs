// par Sabrine Matoui
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SO2M.Data;
using SO2M.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SO2M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostStatus([FromForm] string content)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
            {
                return Unauthorized("Utilisateur non connecté.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("ID utilisateur invalide.");
            }

            var utilisateur = _context.Utilisateurs.Find(userId);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            var post = new Post
            {
                UserId = userId,
                Content = content,
                Username = utilisateur.Username,
                DateCreated = DateTime.Now,
                Utilisateur = utilisateur
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpGet("posts")]
        public IActionResult GetPosts()
        {
            var posts = _context.Posts.ToList();
            return Ok(posts);
        }

        [HttpDelete("post/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok("Post supprimé avec succès");
        }
    }
}