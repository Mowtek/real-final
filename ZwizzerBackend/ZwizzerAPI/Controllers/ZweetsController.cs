using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ZwizzerAPI.DTO;
using ZwizzerDAL;

namespace ZwizzerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZweetsController : ControllerBase
    {
        ZwizzerContext _context { get; set; }
        public ZweetsController(ZwizzerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZweetDTO>>> GetAllZweets()
        {
            var zweets = await _context.Zweets
                        .Include(z => z.User)
                        .Include(z => z.Likes)
                        .Include(z => z.Comments)
                          .ThenInclude(c => c.User)
                        .Include(z => z.Rezweets)
                        .ToListAsync();

            zweets = zweets.OrderByDescending(z => z.CreationTime).ToList();

            var zweetsDTO = zweets.Select(zweet => new ZweetDTO(zweet));

            return Ok(zweetsDTO);
        }
        [HttpGet("{zweetId:int}")]
        public async Task<ActionResult<ZweetDTO>> GetZweetByZweetId(int zweetId)
        {
            var zweet = await _context.Zweets
                        .Include(z => z.User)
                        .Include(z => z.Likes)
                        .Include(z => z.Comments)
                          .ThenInclude(c => c.User)
                        .Include(z => z.Comments)
                          .ThenInclude(c => c.Likes)
                        .Include(z => z.Rezweets)
                        .FirstOrDefaultAsync(z => z.ZweetId == zweetId);

            if (zweet == null) return NotFound();

            return Ok(new ZweetDTO(zweet));
        }
        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<ZweetDTO>> GetZweetsByUserId(int userId)
        {
            var zweets = await _context.Zweets
                        .Include(z => z.User)
                        .Include(z => z.Likes)
                        .Include(z => z.Comments)
                          .ThenInclude(c => c.User)
                        .Include(z => z.Rezweets)
                        .Where(z => z.UserId == userId)
                        .ToListAsync();
            if (zweets == null) return NotFound();
            var zweetsDTO = zweets.Select(z => new ZweetDTO(z));
            return Ok(zweetsDTO);
        }
        [HttpGet("search/{keyword}")]
        public async Task<ActionResult> SearchForTweets(string keyword)
        {
            var zweets = await _context.Zweets
                        .Include(z => z.User)
                        .Include(z => z.Likes)
                        .Include(z => z.Comments)
                          .ThenInclude(c => c.User)
                        .Include(z => z.Rezweets)
                        .ToListAsync();
            zweets = zweets.Where(z => z.Content.Contains(keyword)).OrderByDescending(z => z.CreationTime).ToList();
            if (zweets.Count == 0) return NotFound();
            var zweetsDTO = zweets.Select(z => new ZweetDTO(z));
            return Ok(zweetsDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ZweetCreationDTO>> CreateZweet(ZweetCreationDTO createdZweet)
        {
            if (createdZweet == null) return BadRequest("Creating new Zweet requires content.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == createdZweet.UserId);
            if (user == null) return BadRequest("No user found. Please login to create new Zweets.");
            Zweet zweet = new Zweet() {
                UserId = createdZweet.UserId,
                User = user,
                Content = createdZweet.Content,
                CreationTime = DateTime.Now,
                Likes = new List<Like>(),
                Comments = new List<Comment>(),
                Rezweets = new List<Rezweet>()
            };
            _context.Zweets.Add(zweet);
            _context.SaveChangesAsync();

            return Created($"/zweets/{zweet.UserId}", createdZweet);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateZweet(ZweetEditDTO editZweet)
        {
            if (editZweet == null) return BadRequest("Please provide Content to edit.");
            if (!ModelState.IsValid) return BadRequest("Invalid Content or Zweet");
            var zweetToEdit = await _context.Zweets.FirstOrDefaultAsync(z => z.ZweetId == editZweet.ZweetId);
            if (zweetToEdit == null) return NotFound("No Zweet found to edit.");
            zweetToEdit.Content = editZweet.Content;
            _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{zweetId:int}")]
        public async Task<ActionResult> DeleteZweet(int zweetId)
        {
            var zweet = await _context.Zweets
                              .Include(z => z.Comments)
                              .Include(z => z.Rezweets)
                              .Include(z => z.Likes)
                              .FirstOrDefaultAsync(z => z.ZweetId == zweetId);
            if (zweet == null) return NotFound("No Zweet found to delete.");

            var rezweetsDelete = _context.Rezweets.Where(z => z.ZweetId == zweetId);
            _context.Rezweets.RemoveRange(rezweetsDelete);

            var commentsDelete = _context.Comments.Where(c => c.ZweetId == zweetId);
            _context.Comments.RemoveRange(commentsDelete);

            var likesDelete = _context.Likes.Where(l => l.ZweetId == zweetId);
            _context.Likes.RemoveRange(likesDelete);

            var commentIdsDelete = zweet.Comments.Select(c => (int?)c.CommentId).ToList();
            var commentLikesDelete = _context.Likes.Where(l => commentIdsDelete.Contains(l.CommentId));
            _context.Likes.RemoveRange(commentLikesDelete);

            _context.Zweets.Remove(zweet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize]
        [HttpPost("comment")]
        public async Task<ActionResult> NewComment(NewCommentDTO newComment)
        {
            var foundZweet = await _context.Zweets.FirstOrDefaultAsync(z => z.ZweetId == newComment.ZweetId);
            if (foundZweet == null) return BadRequest("Zweet not found.");
            var comment = new Comment() { ZweetId = newComment.ZweetId, UserId = newComment.UserId, Content = newComment.Content, CreationTime = DateTime.Now, Likes = new List<Like>() };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Created($"/api/zweets/{newComment.ZweetId}", newComment);
        }
        [Authorize]
        [HttpPost("like/comment")]
        public async Task<ActionResult> ToggleLike(NewLikeDTO newLike)
        {
            var likeFound = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == newLike.UserId && l.CommentId ==  newLike.CommentId);
            if (likeFound == null)
            {
                var newLikeToAdd = new Like()
                {
                    UserId = newLike.UserId,
                    ZweetId = null,
                    CommentId = newLike.CommentId
                };
                _context.Likes.Add(newLikeToAdd);
                await _context.SaveChangesAsync();
                return Created($"/api/zweets/{newLike.UserId}", newLike);
            } else
            {
                _context.Likes.Remove(likeFound);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
        [Authorize]
        [HttpPost("like/post")]
        public async Task<ActionResult> ToggleLikeComment(NewLikeDTO newLike)
        {
            var likeFound = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == newLike.UserId && l.ZweetId == newLike.ZweetId);
            if (likeFound == null)
            {
                var newLikeToAdd = new Like()
                {
                    UserId = newLike.UserId,
                    ZweetId = newLike.ZweetId
                };
                _context.Likes.Add(newLikeToAdd);
                await _context.SaveChangesAsync();
                return Created($"/api/zweets/{newLike.UserId}", newLike);
            }
            else
            {
                _context.Likes.Remove(likeFound);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
        [Authorize]
        [HttpPost("rezweet")]
        public async Task<ActionResult> ToggleRezweet(NewRezweetDTO newRezweet)
        {
            var rezweetFound = await _context.Rezweets.FirstOrDefaultAsync(r => r.UserId == newRezweet.UserId && r.ZweetId == newRezweet.ZweetId);
            if (rezweetFound == null)
            {
                var newRezweetToAdd = new Rezweet()
                {
                    UserId = newRezweet.UserId,
                    ZweetId = newRezweet.ZweetId
                };
                _context.Rezweets.Add(newRezweetToAdd);
                await _context.SaveChangesAsync();
                return Created($"/api/zweets/{newRezweet.ZweetId}", newRezweet);
            } else
            {
                _context.Rezweets.Remove(rezweetFound);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
