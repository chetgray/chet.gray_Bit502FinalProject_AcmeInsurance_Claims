using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using AcmeInsurance.Claims.Models;

using Newtonsoft.Json;

namespace AcmeInsurance.Claims.WebServices.Proxy
{
    public class ClaimsProxy
    {
        private readonly HttpClient _asyncClient;
        private readonly Uri _baseUri;
        private readonly JsonSerializerSettings _serializerSettings;

        public ClaimsProxy()
        {
            _asyncClient = ApiHelper.AsyncClient;
            _baseUri = new Uri(ConfigurationManager.AppSettings["BaseUri"]);
            _serializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        public int AddClaim(IClaimModel claim)
        {
            Uri requestUri = new Uri(_baseUri, "Claims");
            string requestJsonString = JsonConvert.SerializeObject(claim, _serializerSettings);
            using (WebClient client = ApiHelper.GetSyncClient())
            {
                string responseString = client.UploadString(requestUri, requestJsonString);
                int id = int.Parse(responseString);

                return id;
            }
        }

        public async Task<int> AddClaimAsync(IClaimModel claim)
        {
            Uri requestUri = new Uri(_baseUri, "Claims");
            string requestJsonString = JsonConvert.SerializeObject(claim, _serializerSettings);
            using (
                HttpResponseMessage response = await _asyncClient.PostAsync(
                    requestUri,
                    new StringContent(requestJsonString)
                )
            )
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }

                string responseString = await response.Content.ReadAsStringAsync();
                int id = int.Parse(responseString);

                return id;
            }
        }

        public ClaimStatus GetClaimStatus(int id)
        {
            Uri requestUri = new Uri(_baseUri, $"Claims/{id}/ClaimStatus");
            using (WebClient client = ApiHelper.GetSyncClient())
            {
                string responseString = client.DownloadString(requestUri);
                ClaimStatus claimStatus = JsonConvert.DeserializeObject<ClaimStatus>(
                    responseString,
                    _serializerSettings
                );

                return claimStatus;
            }
        }

        public async Task<ClaimStatus> GetClaimStatusAsync(int id)
        {
            Uri requestUri = new Uri(_baseUri, $"Claims/{id}/ClaimStatus");
            using (HttpResponseMessage response = await _asyncClient.GetAsync(requestUri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }

                string responseString = await response.Content.ReadAsStringAsync();
                ClaimStatus claimStatus = JsonConvert.DeserializeObject<ClaimStatus>(
                    responseString,
                    _serializerSettings
                );

                return claimStatus;
            }
        }
    }
}
