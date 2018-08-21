using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/env")]
    public class EnvController : ControllerBase
    {
    
        private readonly CloudFoundryInfo _cfInfo;

        public EnvController(CloudFoundryInfo cfInfo)
        {
            _cfInfo = cfInfo;
        }


        [HttpGet]
        public CloudFoundryInfo Get() => _cfInfo;
    }
}
