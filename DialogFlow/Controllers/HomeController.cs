using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DialogFlow.Models;
using Google.Cloud.Dialogflow.V2;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Grpc.Auth;
using Grpc.Core;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace DialogFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment env;
        private readonly IHostApplicationLifetime lifetime;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env,IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            this.env = env;
            this.lifetime = lifetime;
        }

        public IActionResult Index()
        {

            
            var cred = GoogleCredential.GetApplicationDefault();
            
            var channel = new Channel(
                SessionsClient.DefaultEndpoint.ToString(), cred.ToChannelCredentials());
            var Session = SessionsClient.Create(channel);
            
            var InputQueryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = "Hello",
                    LanguageCode = "en_US",

                }
            };
           
            var request =  Session.DetectIntent(new SessionName("ProjectId", "Test"), InputQueryInput);
            return Ok(request);
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
          
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

     
        
    }
}
