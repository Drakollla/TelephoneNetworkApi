using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Domain.Services;
using TelephoneNetworkApi.Extensions;
using TelephoneNetworkApi.Resourse;

namespace TelephoneNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrySubscriptionPaymentController : ControllerBase
    {
        private readonly IRegistrySubscriptionPaymentService _registrySubscriptionPaymentService;
        private readonly IMapper _mapper;

        public RegistrySubscriptionPaymentController(IRegistrySubscriptionPaymentService registrySubscriptionPaymentService, IMapper mapper)
        {
            _registrySubscriptionPaymentService = registrySubscriptionPaymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RegistrySubscriptionPaymentResourse>> GetAllAsync()
        {
            var payment = await _registrySubscriptionPaymentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<RegistrySubscriptionPayment>, IEnumerable<RegistrySubscriptionPaymentResourse>>(payment);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRegistrySubscriptionPaymentResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var payment = _mapper.Map<SaveRegistrySubscriptionPaymentResourse, RegistrySubscriptionPayment>(resource);
            var result = await _registrySubscriptionPaymentService.SaveAsync(payment);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentResource = _mapper.Map<RegistrySubscriptionPayment, RegistrySubscriptionPaymentResourse>(result.RegistrySubscriptionPayment);
            return Ok(paymentResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRegistrySubscriptionPaymentResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var payment = _mapper.Map<SaveRegistrySubscriptionPaymentResourse, RegistrySubscriptionPayment>(resource);
            var result = await _registrySubscriptionPaymentService.UpdateAsync(id, payment);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentResource = _mapper.Map<RegistrySubscriptionPayment, RegistrySubscriptionPaymentResourse>(result.RegistrySubscriptionPayment);
            return Ok(paymentResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _registrySubscriptionPaymentService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentesource = _mapper.Map<RegistrySubscriptionPayment, RegistrySubscriptionPaymentResourse>(result.RegistrySubscriptionPayment);
            return Ok(paymentesource);
        }
    }
}