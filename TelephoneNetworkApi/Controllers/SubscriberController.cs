using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Extensions;
using TelephoneNetworkApi.Resourse;
using TelephoneNetworkApi.Services;

namespace TelephoneNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;

        public SubscriberController(ISubscriberService subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SubscriberResourse>> GetAllAsync()
        {
            var subscribers = await _subscriberService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Subscriber>, IEnumerable<SubscriberResourse>>(subscribers);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriberResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriber = _mapper.Map<SaveSubscriberResource, Subscriber>(resource);

            foreach (var id in resource.AutomaticTelephoneExchangeIds)
            {
                subscriber.AtsSubscribers.Add(new AtsSubscriber()
                {
                    Subscriber = subscriber,
                    AutomaticTelephoneExchangeId = id
                });
            }

            var result = await _subscriberService.SaveAsync(subscriber);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Subscriber, SubscriberResourse>(result.Subscriber);
            return Ok(categoryResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSubscriberResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriber = _mapper.Map<SaveSubscriberResource, Subscriber>(resource);
            var result = await _subscriberService.UpdateAsync(id, subscriber);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Subscriber, SubscriberResourse>(result.Subscriber);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _subscriberService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriberResource = _mapper.Map<Subscriber, SubscriberResourse>(result.Subscriber);
            return Ok(subscriberResource);
        }
    }
}