using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using MundoDisney.ViewModels.Autenticacion;
using MundoDisney.ViewModels.Autenticacion.Login;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MundoDisney.Controllers
{
    [ApiController]
    [Route(template: "auth")]
    
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _sigInManager;
        private readonly IMailService _mailService;

        public AuthenticationController(UserManager<Usuario> userManager, SignInManager<Usuario> sigInManager, IMailService mailService)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _mailService = mailService;
        }

        [HttpPost]
        [Route(template: "register-admin")]
        public async Task<IActionResult> RegistroAdmin(RegisterRequestViewModel model)
        {

            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }


            var user = new Usuario
            {

                UserName = model.UserName,
                Email = model.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Message = $"Error al crear el usuario: {string.Join(", ", result.Errors.Select(x => x.Description))}"
                    });
            }

            return Ok(new
            {
                Status = "operacion exitosa",
                Message = "El usuario fue creado con exito!"
            });
        }

        [HttpPost]
        [Route(template:"register")]
        public async Task<IActionResult> Registro(RegisterRequestViewModel model)
        {
            
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            
            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

           
            var user = new Usuario
            {

                UserName = model.UserName,
                Email = model.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Message = $"Error al crear el usuario: {string.Join(", ",result.Errors.Select(x => x.Description))}"
                    });
            }
            await _mailService.SendMail(user);
            return Ok(new
            {
                Status = "operacion exitosa",
                Message = "El usuario fue creado con exito!"
            });
        }
        
        [HttpPost]
        [Route(template: "login")]
        public async Task<IActionResult> Login(LoginRequestViewModel model)
        {
           
           var result = await _sigInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            
            if(result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.UserName);
                if(currentUser.IsActive)
                {
                   

                    return Ok(await GetToken(currentUser));
                }
            }
            return StatusCode(StatusCodes.Status401Unauthorized,
                new
                {
                    Status = "Error",
                    Message = $"El usuario {model.UserName} no esta autorizado"
                });


        }

        
        private async Task<LoginResponseViewModel> GetToken(Usuario currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSignedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeAUTORIZACION"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignedKey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
