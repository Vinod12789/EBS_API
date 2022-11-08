using EBS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        public static CustomerDto c = new CustomerDto();
        public static Customer customer = new Customer();

        private readonly IConfiguration _configuration;
        private readonly EBSContext _context;
        public AuthController(IConfiguration configuration, EBSContext eBSContext)
        {
            _configuration = configuration;
            _context = eBSContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(CustomerDto request)
        {


            using var hmac = new HMACSHA512();
            var user = new Customer
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerMobile = request.CustomerMobile,
                CustomerAddress = request.CustomerAddress,

                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.CustomerPassword)),
                PasswordSalt = hmac.Key
            };

            _context.Customers.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(Login login)
        {
            String number = (login.CustomerEmail);
            
          var result = await _context.Customers
                       .SingleOrDefaultAsync(x => x.CustomerEmail == number);

            
            if (result == null)
            {
                return BadRequest("User not found.");
            }
            User us = new User
            {
                CustomerName = result.CustomerName,
                CustomerMobile = result.CustomerMobile,
                PasswordHash = result.PasswordHash,
                PasswordSalt = result.PasswordSalt,
                CustomerEmail=result.CustomerEmail, 
                CustomerAddress = result.CustomerAddress

            };
            if (!VerifyPasswordHash(us, login))
            {
                return BadRequest("Wrong Password");
            }
            

            string token = CreateToken(login);
            await _context.SaveChangesAsync();
            return Ok(token);

            //return Ok("MY CRAZY TOKEN");
        }

        private string CreateToken(Login user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.CustomerEmail)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(User user, Login login)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != user.PasswordHash[i])
                {
                    return false;
                }

            }
            return true;
        }

    }
}

