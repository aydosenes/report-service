using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IRestService
    {
        Task<string> Get(string url, string token);
        Task<HttpResponseMessage> Get(string url);
        Task<string> Post<T>(string urlBase, string url, T instance) where T : class, new();
        Task<string> Post<T>(string url, T instance) where T : class, new();
        Task<string> Post(string url, object instance, Dictionary<string, string> headers);
        Task<string> Delete(string url, string instance, string token);
        string CreateToken(string username, string password);

    }
}
