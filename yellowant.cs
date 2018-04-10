using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace yellowantSDK
{
    public class yellowant
    {
        public HttpClient client = new HttpClient();
        public string AppKey, AppSecret, AccessToken, RedirectURI;
        public string TokenType, OAuthVersion, APIVersion;
        public static string APIUrl = "https://api.yellowant.com/api/";

        public yellowant(string AppKey = "", string AppSecret = "", string RedirectURI = "", string AccessToken = "")
        {
            this.AppSecret = AppSecret;
            this.AppKey = AppKey;
            this.RedirectURI = RedirectURI;
            this.AccessToken = AccessToken;
            this.TokenType = "bearer";
            this.OAuthVersion = "2";
            this.APIVersion = "0.0.1";
        }

        /*Private method Get request to yellowant API. Returns String result.  */
        private async Task<Object> GetRequest(string EndPoint)
        {
            client.BaseAddress = new Uri(APIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            
            var response = await client.GetAsync(EndPoint);
           
            HttpContent content = response.Content;
            Task<String> result = content.ReadAsAsync<String>();
            var res = JsonConvert.DeserializeObject(result.Result) ;
            return res;
        }

        /*Private Post method. For url-encode, except while getting code all data to 
        be sent is passed as json string */
        private async Task<Object> PostRequest(string Endpoint, string Data = "",
                                Dictionary<string, string> dict = null, string ContentType = "json")
        {
            client.BaseAddress = new Uri(APIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            if (ContentType == "json")
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                                                            "Bearer", AccessToken);
                var dataToSend = new StringContent(Data);
                //ReadAsAsync<Product>()
                var response = await client.PostAsync(Endpoint, dataToSend).ConfigureAwait(continueOnCapturedContext:false);
                HttpContent content = response.Content;
                Task<String> result = content.ReadAsAsync<String>();
                var res = result.Result;
                return res;

            }
            else
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
                {
                    Content = new FormUrlEncodedContent(dict)
                };
                var response = client.SendAsync(request).Result;
                HttpContent content = response.Content;
                Task<String> result = content.ReadAsStringAsync();
                Object res = JsonConvert.DeserializeObject(result.Result);
                return res;
            }

        }

        public object GetAccessToken(string GrantCode)
        {

            Dictionary<string, string> valuedict = new Dictionary<string, string>();
            valuedict.Add("grant_type", "authorization_code");
            valuedict.Add("client_id", AppKey);
            valuedict.Add("client_secret", AppSecret);
            valuedict.Add("code", GrantCode);
            valuedict.Add("redirect_uri", RedirectURI);

            var result = PostRequest("oauth2/token/", dict: valuedict, ContentType: "url-encode").Result;
            return result;
        }

        public object GetUserProfile()
        {
            var result = GetRequest("user/profile");
            return result;
        }

        public object CreateUserIntegration()
        {
            var result = PostRequest("user/integration/", ContentType: "json").Result;
            return result;
        }

        public object SendMessage(int IntegratonID, MessageClass message)
        {
            string Data = JsonConvert.SerializeObject(message);
            var response = PostRequest("user/message/", Data: Data, ContentType: "json");
            return response;

        }

        public object SendWebhookMessage(int IntegratonID, int WebHookSubscriptionID, MessageClass message)
        {
            string Data = JsonConvert.SerializeObject(message);
            string url = String.Format("user/application/webhook/{0}/", WebHookSubscriptionID);
            // payload = {'webhook_id'=> webhook_subscription_id, 'requester_application'=> integration_id}
            object response = PostRequest(url, Data: Data);
            return response;
        }

        public string DeleteIntegration(int IntegratonID)
        {
            client.BaseAddress = new Uri(APIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                                                        "Bearer", AccessToken);
            string url = String.Format("user/integration/{0}/", IntegratonID);
            var response = client.DeleteAsync(url).Result;
            HttpContent content = response.Content;
            Task<String> result = content.ReadAsStringAsync();
            var res = result.Result;
            return res;
        }


    }
}
