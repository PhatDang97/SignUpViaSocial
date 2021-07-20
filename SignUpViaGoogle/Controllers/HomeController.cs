using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using SignUpViaGoogle.Models;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SignUpViaGoogle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void SignIn(string ReturnUrl = "/", string type = "")
        {
            if (!Request.IsAuthenticated)
            {
                if (type == "Google")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Home/GoogleLoginCallback" }, "Google");
                }
                
                if(type == "Microsoft")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Home/GoogleLoginCallback" }, "Microsoft");
                }
            }
        }

        [AllowAnonymous]
        public ActionResult GoogleLoginCallback()
        {
            var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;

            var loginInfo = GoogleLoginViewModel.GetLoginInfo(claimsPrincipal);
            if (loginInfo == null)
            {
                return RedirectToAction("Index");
            }


            //WebEntities db = new WebEntities(); //DbContext
            //var user = db.UserAccounts.FirstOrDefault(x => x.Email == loginInfo.emailaddress);

            //if (user == null)
            //{
            //    user = new UserAccount
            //    {
            //        Email = loginInfo.emailaddress,
            //        GivenName = loginInfo.givenname,
            //        Identifier = loginInfo.nameidentifier,
            //        Name = loginInfo.name,
            //        SurName = loginInfo.surname,
            //        IsActive = true
            //    };
            //    db.UserAccounts.Add(user);
            //    db.SaveChanges();
            //}

            var ident = new ClaimsIdentity(
                    new[] { 
									// adding following 2 claim just for supporting default antiforgery provider
									new Claim(ClaimTypes.NameIdentifier, loginInfo.emailaddress),
                                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                                    new Claim(ClaimTypes.Name, loginInfo.name),
                                    new Claim(ClaimTypes.Email, loginInfo.emailaddress),
									// optionally you could add roles if any
									new Claim(ClaimTypes.Role, "User")
                    },
                    CookieAuthenticationDefaults.AuthenticationType);


            HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
            return Redirect("~/");

        }

        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Redirect("~/");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}