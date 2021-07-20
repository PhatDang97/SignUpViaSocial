using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;

[assembly: OwinStartup(typeof(SignUpViaGoogle.App_Start.Startup))]

namespace SignUpViaGoogle.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Home/Index"),
                SlidingExpiration = true
            });
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "137750893632-ek3n827krshg2obueusqkemm1c0jqgv1.apps.googleusercontent.com",
                ClientSecret = "DOX3mSg2YUPBSetJLC18zyWy"
            });
            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions()
            {
                ClientId = "59e4935f-a0db-4321-838e-8cc98da5284b",
                ClientSecret = "-Ox.7nS85joRzq~j_.Atm1x~5QGj51Avg3"
            });
        }
    }
}
