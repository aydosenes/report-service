using Application.Interfaces.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RestService : IRestService
    {
        static readonly string baseUrl = "http://test.twalletgate.com/";
        public async Task<string> Get(string url, string token)
        {
            try
            {

                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));

                    var response = client.GetAsync(url);
                    response.Wait();
                    return await response.Result.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {

                var _ex = ex.Message;
                return null;
            }

        }
        public async Task<string> Post<T>(string urlBase, string url, T instance) where T : class, new()
        {
            try
            {

                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                using (var client = new HttpClient())
                {

                    var httpContent = new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(urlBase);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync(url, httpContent);
                    response.Wait();
                    return await response.Result.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {

                var _ex = ex.Message;
                return null;
            }

        }
        public async Task<string> Post<T>(string url, T instance) where T : class, new()
        {
            try
            {

                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                using (var client = new HttpClient())
                {

                    var httpContent = new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync(url, httpContent);
                    response.Wait();
                    return await response.Result.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {

                var _ex = ex.Message;
                return null;
            }

        }
        public async Task<string> Post(string url, object instance, Dictionary<string, string> headers)
        {
            try
            {

                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                using (var client = new HttpClient())
                {

                    var httpContent = new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    foreach (var value in headers)
                    {
                        client.DefaultRequestHeaders.Add(value.Key, value.Value);
                    }
                    var response = client.PostAsync(url, httpContent);
                    response.Wait();
                    return await response.Result.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {

                var _ex = ex.Message;
                return null;
            }
        }
        public async Task<string> Delete(string url, string instance, string token)
        {
            try
            {

                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                using (var client = new HttpClient())
                {

                    var httpContent = new StringContent(instance, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));


                    var response = client.PostAsync(url, httpContent);
                    response.Wait();
                    return await response.Result.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {

                var _ex = ex.Message;
                return null;
            }
        }
        public string CreateToken(string username, string password)
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and Password required!");
            }
            var token = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            return token;
        }

    }
}
