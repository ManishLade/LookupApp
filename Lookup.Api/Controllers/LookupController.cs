using Lookup.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lookup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateLookupController : ControllerBase
    {
        private ILookupService _lookupService;

        public StateLookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpPost]
        [Route("/api/states/lookup/{zipCode}")]
        public IActionResult Get(string zipCode)
        {
            try
            {
                return Ok(_lookupService.GetStateByZipCode(zipCode));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }
    }
}
