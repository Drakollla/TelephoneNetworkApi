using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restAPI.Models;
using TelephoneNetworkApi.Services;

namespace TelephoneNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet]
        public async Task<IEnumerable<Subscriber>> GetAllAsync()
        {
            var subscribers = await _subscriberService.ListAsync();
            return subscribers;
        }
    }
}