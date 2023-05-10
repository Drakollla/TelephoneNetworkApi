using restAPI.Models;

namespace TelephoneNetworkApi.Services.Communication
{
    public class SaveSubscriberResponse : BaseResponse
    {
        public Subscriber Subscriber { get; private set; }
        public SaveSubscriberResponse(bool success, string message, Subscriber subscriber) : base(success, message)
        {
            Subscriber = subscriber;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        public SaveSubscriberResponse(Subscriber subscriber) : this(true, string.Empty, subscriber)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveSubscriberResponse(string message) : this(false, message, null)
        { }
    }
}
