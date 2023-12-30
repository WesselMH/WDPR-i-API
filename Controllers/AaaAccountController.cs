using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;

namespace WDPR_i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AaaAccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AaaAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        [Route("beheerder/aanmelden")]
        public async Task<ActionResult<IEnumerable<Beheerder>>> RegistreerBeheerder([FromBody] Beheerder beheerder)
        {
            var resultaat = await _userManager.CreateAsync(beheerder, beheerder.Wachtwoord);
            await _roleManager.CreateAsync(new IdentityRole { Name = "beheerder" });
            await _userManager.AddToRoleAsync(beheerder, "beheerder");
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        // [HttpPost("beheerder/login")]
        // public async Task<IActionResult> LoginBeheerder([FromBody] Beheerder beheerder)
        // {
        //     var _user = await _userManager.FindByNameAsync(beheerder.GebruikersNaam);
        //     if (_user != null)
        //     {
        //         await _signInManager.SignInAsync(_user, true);
        //         return Ok();
        //     }

        //     return Unauthorized();
        // }

        [HttpPost]
        [Route("bedrijf/aanmelden")]
        public async Task<ActionResult<IEnumerable<Bedrijf>>> RegistreerBedrijf([FromBody] Bedrijf bedrijf)
        {
            var resultaat = await _userManager.CreateAsync(bedrijf, bedrijf.Wachtwoord);
            await _roleManager.CreateAsync(new IdentityRole { Name = "bedrijf" });
            await _userManager.AddToRoleAsync(bedrijf, "bedrijf");
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [HttpPost]
        [Route("ervaringsdeskundige/aanmelden")]
        public async Task<ActionResult<IEnumerable<ErvaringsDeskundige>>> RegistreerErvaringsDeskundige([FromBody] ErvaringsDeskundige ervaringsDeskundige)
        {
            var resultaat = await _userManager.CreateAsync(ervaringsDeskundige, ervaringsDeskundige.Wachtwoord);
            await _roleManager.CreateAsync(new IdentityRole { Name = "ervaringsDeskundige" });
            await _userManager.AddToRoleAsync(ervaringsDeskundige, "ervaringsDeskundige");
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [HttpPost]
        [Route("google/aanmelden")]
        public async Task<ActionResult<IEnumerable<Google>>> RegistreerGoogle([FromBody] Google googleGebruiker)
        {
            var resultaat = await _userManager.CreateAsync(googleGebruiker, googleGebruiker.sub);
            await _roleManager.CreateAsync(new IdentityRole { Name = "ervaringsDeskundige" });
            await _userManager.AddToRoleAsync(googleGebruiker, "ervaringsDeskundige");
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <remarks>
        /// Voorbeeld request:
        /// 
        /// POST /login
        ///     {
        ///         "id": 0,
        ///         "userName": "string",
        ///         "gebruikersNaam": "string",
        ///         "wachtwoord": "string",
        ///         "email": "string"
        ///     }
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> LoginTest([FromBody] Account account)
        {
            var _user = await _userManager.FindByNameAsync(account.GebruikersNaam);
            if (_user != null)
                Console.WriteLine("test");
            if (await _userManager.CheckPasswordAsync(_user, account.Wachtwoord))
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh98f89uawef9j8aw89hefawef"));

                var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
                var roles = await _userManager.GetRolesAsync(_user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var tokenOptions = new JwtSecurityToken(
                    //hier moeten we wel onze eigen domein zetten
                    issuer: "http://localhost:5155",
                    audience: "http://localhost:5155",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signingCredentials
                );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
            }
            //moet andere fout code nog komen zodat we fouten afhandelen
            return Unauthorized();
        }
    }
}