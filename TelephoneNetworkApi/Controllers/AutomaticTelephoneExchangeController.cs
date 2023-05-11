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
    public class AutomaticTelephoneExchangeController : ControllerBase
    {
        private readonly IAutomaticTelephoneExchangeService _automaticTelephoneExchangeService;
        private readonly IMapper _mapper;

        public AutomaticTelephoneExchangeController(IAutomaticTelephoneExchangeService automaticTelephoneExchangeService, IMapper mapper)
        {
            _automaticTelephoneExchangeService = automaticTelephoneExchangeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AutomaticTelephoneExchangeResourse>> GetAllAsync()
        {
            var ats = await _automaticTelephoneExchangeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<AutomaticTelephoneExchange>, IEnumerable<AutomaticTelephoneExchangeResourse>>(ats);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAutomaticTelephoneExchangeResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ats = _mapper.Map<SaveAutomaticTelephoneExchangeResourse, AutomaticTelephoneExchange>(resource);
            var result = await _automaticTelephoneExchangeService.SaveAsync(ats);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<AutomaticTelephoneExchange, AutomaticTelephoneExchangeResourse>(result.AutomaticTelephoneExchange);
            return Ok(categoryResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAutomaticTelephoneExchangeResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveAutomaticTelephoneExchangeResourse, AutomaticTelephoneExchange>(resource);
            var result = await _automaticTelephoneExchangeService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<AutomaticTelephoneExchange, AutomaticTelephoneExchangeResourse>(result.AutomaticTelephoneExchange);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _automaticTelephoneExchangeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var atsResource = _mapper.Map<AutomaticTelephoneExchange, AutomaticTelephoneExchangeResourse>(result.AutomaticTelephoneExchange);
            return Ok(atsResource);
        }
    }
}