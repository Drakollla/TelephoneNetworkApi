using TelephoneNetworkApi.Models;

namespace TelephoneNetworkApi.Services.Communication
{
    public class SubscriberResponse : BaseResponse
    {
        public Subscriber Subscriber { get; private set; }

        public SubscriberResponse(bool success, string message, Subscriber subscriber) : base(success, message)
        {
            Subscriber = subscriber;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        public SubscriberResponse(Subscriber subscriber) : this(true, string.Empty, subscriber)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SubscriberResponse(string message) : this(false, message, null)
        { }
    }
}
