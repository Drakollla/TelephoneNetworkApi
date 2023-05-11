using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi.Domain.Services.Communication
{
    public class RegistrySubscriptionPaymentResponse : BaseResponse
    {
        public RegistrySubscriptionPayment RegistrySubscriptionPayment { get; private set; }

        private RegistrySubscriptionPaymentResponse(bool success, string message, RegistrySubscriptionPayment registrySubscriptionPayment)
            : base(success, message)
        {
            RegistrySubscriptionPayment = registrySubscriptionPayment;
        }

        public RegistrySubscriptionPaymentResponse(RegistrySubscriptionPayment registrySubscriptionPayment)
            : this(true, string.Empty, registrySubscriptionPayment) { }

        public RegistrySubscriptionPaymentResponse(string message)
            : this(false, message, null) { }
    }
}