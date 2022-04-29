using Microsoft.Win32;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Kursach.http
{
    public class MyHttpClient
    {
        private string url;

        HttpClient httpClient;
        public MyHttpClient(string baseUri ="")
        {
            httpClient = new HttpClient();
            url = baseUri;
            httpClient.BaseAddress = new Uri("http://localhost:3000/");
            string token = GetToken();
            if (token != "")
            {
                httpClient.DefaultRequestHeaders.Add("authorization", token);
            }
        }

        private string GetToken()
        {
            return Registry.CurrentUser.OpenSubKey("courses")?.GetValue("token") as string ?? "";
        }

        public async Task<HttpData<R>> Get<R>(string uri)
        {
            var response = await httpClient.GetAsync(url+uri);
            return await ConstructHttpResponse<R>(response);
        }

        public async Task<HttpData<R>> Post<R, T>(string uri, T body)
        {
            var response = await httpClient.PostAsJsonAsync<T>(url+uri, body);
            return await ConstructHttpResponse<R>(response);
        }

        public async Task<HttpData<R>> Delete<R>(string uri)
        {
            var response = await httpClient.DeleteAsync(url+uri);
            return await ConstructHttpResponse<R>(response);
        }

        public async Task<HttpData<R>> Put<R, T>(string uri, T body)
        {
            var response = await httpClient.PutAsJsonAsync(url+uri, body);
            return await ConstructHttpResponse<R>(response);
        }

        private async Task<HttpData<T>> ConstructHttpResponse<T>(HttpResponseMessage responseMessage)
        {
            HttpData<T> data = new HttpData<T>();
            if (!responseMessage.IsSuccessStatusCode)
            {
                data.IsError = true;
                data.HttpError = await responseMessage.Content.ReadFromJsonAsync<HttpError>();
            }
            else
            {
                data.IsSucessfull = true;
                data.Data = await responseMessage.Content.ReadFromJsonAsync<T>();
            }
            return data;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
