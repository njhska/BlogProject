using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BlogProject.Service.Models
{
    public class ExceptionMessageHandler: DelegatingHandler
    {
        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            HttpError error = null;
            if(response.TryGetContentValue<HttpError>(out error))
            {
                
            }
            return response;
        }
    }
}