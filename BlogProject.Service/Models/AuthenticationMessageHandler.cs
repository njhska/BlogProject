using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BlogProject.Service.Models
{
    public class AuthenticationMessageHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CookieHeaderValue cookie = request.Headers.GetCookies("session-id").FirstOrDefault();
            if (cookie != null)
            {
                var cookieValue = cookie["session-id"].Value;
                if(cookieValue=="123")
                {
                    SetPrincipal(new GenericPrincipal(new GenericIdentity("manager"),null));
                }
            }
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}