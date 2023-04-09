using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OlağanWeb.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using WebSite.Application.Abstractions.TokenAbs;

namespace OlağanWeb.Controllers;

public class AdminLoginController : Controller
{
    readonly IConfiguration _configuration;
    readonly ITokenHandler _tokenHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminLoginController(ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _tokenHandler = tokenHandler;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public IActionResult Login()
    {

        return View();
    }


    [HttpPost]
    public IActionResult Login(LoginModel model)
    {

        if (model.UserName == "admin" && model.Password == "123456")
        {
            var token = _tokenHandler.CreateToken(7);
            return RedirectToAction("addtext", "admin");

        }

        else
        {

            ViewBag.hata = "kullanıcı adı yada şifre yanlış";
            return StatusCode(204);
        }

    }
}

