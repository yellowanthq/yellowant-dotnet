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
    public class Yellowant
    {
        /* 
         * Main Yellowant class 
         */ 


        HttpClient client = new HttpClient();
        public string AppKey, AppSecret, AccessToken, RedirectURI;
        public string TokenType, OAuthVersion, APIVersion;
        public static string APIUrl = "http://api.yellowant.com/api/";

        public Yellowant(string AppKey = "", string AppSecret = "", string RedirectURI = "", string AccessToken = "")
        {
            this.AppSecret = AppSecret;
            this.AppKey = AppKey;
            this.RedirectURI = RedirectURI;
            this.AccessToken = AccessToken;
            TokenType = "bearer";
            OAuthVersion = "2";
            APIVersion = "0.0.1";
            client.BaseAddress = new Uri(APIUrl); 
        }

        /*Private method Get request to yellowant API. Returns String result.  */
        private async Task<String> GetRequest(string EndPoint)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);


            HttpResponseMessage response = await client.GetAsync(EndPoint).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            string content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        /*Private Post method. For url-encode, except while getting code all data to 
        be sent is passed as json string */
        private async Task<String> PostRequest(string Endpoint, string Data = "",
                                Dictionary<string, string> dict = null, string ContentType = "json")
        {
            client.DefaultRequestHeaders.Accept.Clear();
            Console.WriteLine(Endpoint);
            if (ContentType == "json")
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                                                            "Bearer", AccessToken);
                client.DefaultRequestHeaders.ConnectionClose = true;
                var dataToSend = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Endpoint, dataToSend).ConfigureAwait(continueOnCapturedContext:false);
                string content = response.Content.ReadAsStringAsync().Result;
                return content;

            }
            else
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
                {
                    Content = new FormUrlEncodedContent(dict)
                };
                HttpResponseMessage response = client.SendAsync(request).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                return content;
            }

        }

        /* Post method for 'Delete' type request */
        public async Task<String> PostDelete(int IntegratonID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                                                        "Bearer", AccessToken);
            string url = String.Format("user/integration/{0}/", IntegratonID);
            HttpResponseMessage response = await client.DeleteAsync(url);
            string content = response.Content.ReadAsStringAsync().Result;
            return content;
        }


        //Once you get grant code from YellowAnt, use this method to OAuth Access Token
        public object GetAccessToken(string GrantCode)
        {
            Dictionary<string, string> valuedict = new Dictionary<string, string>();
            valuedict.Add("grant_type", "authorization_code");
            valuedict.Add("client_id", AppKey);
            valuedict.Add("client_secret", AppSecret);
            valuedict.Add("code", GrantCode);
            valuedict.Add("redirect_uri", RedirectURI);

            var result = PostRequest("oauth2/token/", dict: valuedict, ContentType: "url-encode").Result;
            Console.WriteLine(result);
            dynamic RObject = JsonConvert.DeserializeObject(result);
            return RObject;
        }

        //Get User Profile Name, Company etc
        public object GetUserProfile()
        {
            var result = GetRequest("user/profile/").Result;
            dynamic RObject = JsonConvert.DeserializeObject(result);
            return RObject;
        }

        //Create a new integration of your application
        public object CreateUserIntegration()
        {
            var result = PostRequest("user/integration/", ContentType: "json").Result;
            dynamic RObject = JsonConvert.DeserializeObject(result);
            return RObject;
        }

        //Send Message to users' console/Messaging app
        public object SendMessage(int IntegratonID, MessageClass message)
        {
            message.RequesterApplication = IntegratonID;
            string DataToSend = JsonConvert.SerializeObject(message);
            var result = PostRequest("user/message/", Data: DataToSend, ContentType: "json").Result;
            dynamic RObject = JsonConvert.DeserializeObject(result);
            return RObject;
            
        }

        //Send Webhook Messages to users' account. 
        public object SendWebhookMessage(int IntegratonID, int WebHookSubscriptionID, MessageClass message)
        {
            string Data = JsonConvert.SerializeObject(message);
            string url = String.Format("user/application/webhook/{0}/", WebHookSubscriptionID);
            var result = PostRequest(url, Data: Data).Result;
            dynamic RObject = JsonConvert.DeserializeObject(result);
            return RObject;
        }

        //Delete a particular integration
       public object DeleteIntegration(int IntegrationID)
        {
            var result = PostDelete(IntegrationID).Result;
            dynamic RObject = JsonConvert.DeserializeObject(result);
            return RObject;
        }
    

    }
}
