using Demo3MVC.DAL.Models;
using Demo3MVC.PL.Helpers;
using Demo3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo3MVC.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, 
               SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        //there are all action in this controller

        #region Register
        // to Reatch This Action ==> BaseUrl/Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    FName = model.FName,
                    LName = model.LName,
                };
                var Result = await _userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user is not null)
                {
                    //Login
                   var Result = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (Result)
                    {
                        //Login
                       var LoginResult = await _signInManager.PasswordSignInAsync(user,model.Password, model.RememberMe,false);
                        if(LoginResult.Succeeded)
                            return RedirectToAction("index","Home");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Passwors Is InCorrect");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email Is Not Exists");
            }
            return View(model);
        }
        #endregion

        #region Sign Out
        public new async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
		#endregion

		#region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User =await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(User);
                    // This Token Is Valid For Only One time For This User
                    // h ttps://localhost:44351//Account/ResetPassword?email=mohammedyassen728@gmail.com
                    //Request.Scheme = h ttps://localhost:44351 by Dynamic if any one
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new {email = User.Email , Token = token},Request.Scheme);

                    //Send Email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Is Not Exists");
                }
            }
                return View("ForgetPassword", model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var User = await _userManager.FindByEmailAsync(email);
                var Result = await _userManager.ResetPasswordAsync(User, token,model.NewPassword);

                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
			}
            return View(model);
        }

        #endregion


    }
}
