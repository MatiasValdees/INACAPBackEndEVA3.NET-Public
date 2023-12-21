using EVA3.Models;
using EVA3.Models.ViewsModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EVA3.Controllers
{
    public class AuthController : Controller

    {
        private readonly AppDbContext db;

        public AuthController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (db.TblUser.ToList().Count == 0)
            {
                var usuario = new User();
                usuario.Rol = "SuperAdministrador";
                usuario.Username = "SuperAdministrador";
                usuario.Email = "Admin@midominio.cl";
                usuario.Name = "SuperAdministrador";
                usuario.IsActive = true;
                CreatePasswordHash("SuperAdministrador", out byte[] hash, out byte[] salt);
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;
                db.TblUser.Add(usuario);
                db.SaveChanges();
            }
            var user = db.TblUser.FirstOrDefault(x=>x.Username==model.username);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario No Encontrado");
                return View(model);
            }
            if (VerifyPasswordHash(model.password, user.PasswordSalt, user.PasswordHash))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,user.Rol)

                };//Datos del carnet
                var Identity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Carnet
                var Principal = new ClaimsPrincipal(Identity);//Billetera
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,Principal,
                    new AuthenticationProperties { IsPersistent=true});
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Password Incorrecta");
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }



         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }     
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordhash)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordhash);
            }
        }

        public async Task<RedirectToActionResult> LogOut()
        {
           await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
