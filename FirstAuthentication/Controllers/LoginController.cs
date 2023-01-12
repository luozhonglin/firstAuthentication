using FirstAuthentication.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstAuthentication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult UserLog()
        {
            return View();
        }

        public async void ActionPost(string userName, string passWord)
        {
            var u = UserDatas.CheckUser(userName, passWord);
            if (u != null)
            {
                Claim[] cs = new Claim[]
                {
                new Claim(ClaimTypes.Name, u.UserName!),
                new Claim("level", u.Level.ToString())  //注意这里，收集重要情报
                };
                ClaimsIdentity id = new(cs, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal p = new(id);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);
                HttpContext.Response.Redirect("/Home/Index");
            }
        }
    }
}
