using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZwizzerAPI.DTO;
using ZwizzerDAL;

namespace ZwizzerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ZwizzerContext _context;
        IConfiguration _Configuration;
        public UsersController(IConfiguration configuration, ZwizzerContext context)
        {
            _context = context;
            _Configuration = configuration;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            if (_context.Users.Count() > 0) { 
                var users = _context.Users.Select(u => new UserDTO(u));
                return Ok(users);
            }
            return BadRequest("No users found.");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{userId:int}")]
        public async Task<ActionResult> GetUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound($"No user with ID: {userId} found.");
            return Ok(new UserDTO(user));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]
        public async Task<ActionResult> EditUser(EditUserDTO editUserInfo)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.UserId == editUserInfo.UserId);
            if (userToEdit == null) return NotFound($"No user found with ID: {editUserInfo.UserId}");
            userToEdit.Email = editUserInfo.Email;
            userToEdit.Bio = editUserInfo.Bio;
            userToEdit.UserRole = editUserInfo.UserRole;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("profile/{userId:int}")]
        public async Task<ActionResult> GetUserProfile(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound("No user found.");
            var userProfile = await _context.Users
                              .Include(u => u.Zweets)
                                .ThenInclude(zs => zs.Rezweets)
                              .Include(u => u.Zweets)
                                .ThenInclude(zs => zs.Likes)
                              .Include(u => u.Zweets)
                                .ThenInclude(zs => zs.Comments)
                              .Include(u => u.Comments)
                                .ThenInclude(c => c.Zweet)
                                .ThenInclude(c => c.User)
                              .Include(u => u.Comments)
                                .ThenInclude(c => c.Likes)
                              .Include(u => u.Likes)
                                .ThenInclude(l => l.Zweet)
                                .ThenInclude(l => l.User)
                                .ThenInclude(l => l.Comments)
                              .Include(u => u.Rezweets)
                                .ThenInclude(r => r.Zweet)
                                .ThenInclude(r => r.User)
                              .FirstOrDefaultAsync(u => u.UserId == userId);
            return Ok(new UserProfileDTO(userProfile));
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterNewAccount(RegisterDTO info)
        {
            var userFound = await _context.Users.FirstOrDefaultAsync(u => u.UserName == info.UserName);
            if (userFound != null) return BadRequest("Username already exists. Please pick another one.");
            if (info == null) return BadRequest("Please fill out the forms.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userToInsert = new User()
            {
                UserName = info.UserName,
                Email = info.Email,
                HashedPassword = info.Password,
                UserRole = UserRole.User,
                ProfileImagePath = "/ProfileImages/default.jpg",
                BackgroundImagePath = "/BackgroundImages/default.jpg",
                Bio = info.Bio == null ? info.Bio : null,
                Zweets = new List<Zweet>(),
                Comments = new List<Comment>(),
                Likes = new List<Like>(),
                Rezweets = new List<Rezweet>()
            };
            var passHasherDTO = new PasswordHashDTO(userToInsert);
            var passHasher = new PasswordHasher<PasswordHashDTO>();
            var hashedPassword = passHasher.HashPassword(passHasherDTO, passHasherDTO.Password);
            userToInsert.HashedPassword = hashedPassword;
            _context.Users.Add(userToInsert);
            await _context.SaveChangesAsync();
            var newUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == info.UserName);
            return Created($"/api/users/{newUser.UserId}", new { User = new UserProfileDTO(newUser), token = GenerateJwtToken(newUser) });
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(PasswordHashDTO info)
        {
            if (info == null) return BadRequest("Please fill out the form before submitting.");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == info.UserName.ToLower());
            if (user == null) return Unauthorized("Invalid Username or Password.");
            var userInDbPH = new PasswordHashDTO(user);
            var passHasher = new PasswordHasher<PasswordHashDTO>();
            var result = passHasher.VerifyHashedPassword(userInDbPH, userInDbPH.Password, info.Password);
            if (result == PasswordVerificationResult.Failed) return Unauthorized("Invalid Username or Password.");
            return Ok(new { token = GenerateJwtToken(user) });
        }

        string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _Configuration["Jwt:Issuer"],
                audience: _Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpGet("checkJwt")]
        public ActionResult CheckJWT()
        {
            return Ok();
        }
    }
}
