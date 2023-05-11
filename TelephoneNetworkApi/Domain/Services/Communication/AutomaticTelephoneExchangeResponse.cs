using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi.Domain.Services.Communication
{
    public class AutomaticTelephoneExchangeResponse : BaseResponse
    {
        public AutomaticTelephoneExchange AutomaticTelephoneExchange{ get; private set; }

        private AutomaticTelephoneExchangeResponse(bool success, string message, AutomaticTelephoneExchange automaticTelephoneExchange) 
            : base(success, message)
        {
            AutomaticTelephoneExchange = automaticTelephoneExchange;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>

        public AutomaticTelephoneExchangeResponse(AutomaticTelephoneExchange automaticTelephoneExchange) 
            : this(true, string.Empty, automaticTelephoneExchange) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AutomaticTelephoneExchangeResponse(string message) 
            : this(false, message, null) { }
    }
}