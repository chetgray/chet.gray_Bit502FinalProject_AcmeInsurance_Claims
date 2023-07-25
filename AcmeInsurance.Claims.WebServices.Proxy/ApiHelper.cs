using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AcmeInsurance.Claims.WebServices.Proxy
{
    internal static class ApiHelper
    {
        public static HttpClient AsyncClient { get; } = new HttpClient();

        /// <summary>
        ///     Instantiates a new <see cref="WebClient"/> and initializes it to accept JSON
        ///     responses.
        /// </summary>
        /// <returns>
        ///     A new <see cref="WebClient"/> instance.
        /// </returns>
        public static WebClient GetSyncClient()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            return client;
        }

        /// <summary>
        ///     Initializes the <see cref="AsyncClient"/> to accept JSON responses.
        /// </summary>
        public static void InitializeAsyncClient()
        {
            AsyncClient.DefaultRequestHeaders.Accept.Clear();
            AsyncClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
    }
}
