using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using UncleApp.Models.ViewModel;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace UncleApp.Models
{
    public class JsonTokenGenerator : IJsonHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        public JsonTokenGenerator(IConfiguration config,UserManager<IdentityUser> userManager)
        {
            _configuration = config;
            _userManager = userManager;
        }
        public async Task<string> GenerateJsonTokenAsync(LoginViewModel login)
        {
            var q=await this.checkloginAsync(login.UserName,login.Password);
            string ok=_configuration.GetValue<string>("Jwt:Key");
            byte[] b=Encoding.ASCII.GetBytes(ok);
            Claim[] a = await produceArrayAsync(login);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokendecriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(a),
                Expires=DateTime.UtcNow.AddMinutes(50),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(b),SecurityAlgorithms.HmacSha256Signature)
            };
            var ai=tokenHandler.CreateToken(tokendecriptor);
            return tokenHandler.WriteToken(ai);
        }
        public async Task<bool> checkloginAsync(string username, string password)
        {
            var user=await _userManager.FindByNameAsync(username);
            return await _userManager.CheckPasswordAsync(user,password);
        }
        public async Task<Claim[]> produceArrayAsync(LoginViewModel model)
        {
            var user=await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,model.UserName),
                new Claim(ClaimTypes.Name,model.UserName)
            };
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
               claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else if(await _userManager.IsInRoleAsync(user, "Shopkeeper"))
            {
               claims.Add(new Claim(ClaimTypes.Role, "Shopkeeper"));
            }
            return claims.ToArray();
        }
    }
}
