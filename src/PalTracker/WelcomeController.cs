using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {

        private readonly string _message;

        public WelcomeController(WelcomeMessage welcomeMessage)
        {
            _message = welcomeMessage.Message;
        }


        [HttpGet]
        public string SayHello() => _message;
    }
}
