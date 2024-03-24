using System.Net.Http.Headers;
using System.Net;
using TestSite.Models;

#pragma warning disable CS8603
#pragma warning disable CS8625
namespace TestSite.Utils
{
    enum HttpRequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class APIManager
    {
        private static HttpClient client = null;
        private static readonly string API_URL = "http://192.168.0.105:5016/api/Medicines/";

        public APIManager()
        {
            client = new();
            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<bool> canConnectToAPI()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("checkStatus");
                return true;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private async Task<object> executeRequest<T>(HttpRequestType requestType, string request, Medicine medicine = null)
        {
            if (!await canConnectToAPI())
                return null;

            HttpResponseMessage response = requestType switch
            {
                HttpRequestType.GET => await client.GetAsync(request),
                HttpRequestType.POST => await client.PostAsJsonAsync(request, medicine),
                HttpRequestType.PUT => await client.PutAsJsonAsync(request, medicine),
                HttpRequestType.DELETE => await client.DeleteAsync(request),
                _ => throw new NotImplementedException(),
            };

            if (requestType != HttpRequestType.GET)
            {
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return null;
        }

        public async Task<Medicine> getMedicine(int index)
        {
            object result = await executeRequest<Medicine>(HttpRequestType.GET, $"getMedicine?index={index}");
            return result != null ? (Medicine)result : null;
        }

        public async Task<List<Medicine>> getMedicines()
        {
            object result = await executeRequest<List<Medicine>>(HttpRequestType.GET, "getMedicines");
            return result != null ? (List<Medicine>)result : null;
        }

        public async Task<HttpStatusCode> createMedicine(Medicine medicine)
        {
            object result = await executeRequest<HttpStatusCode>(HttpRequestType.POST, "addMedicine", medicine);
            return result != null ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
        }

        public async Task<HttpStatusCode> deleteMedicine(int index)
        {
            object result = await executeRequest<HttpStatusCode>(HttpRequestType.DELETE, $"deleteMedicine?index={index}");
            return result != null ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
        }

        public async Task<HttpStatusCode> updateMedicine(int index, Medicine medicine)
        {
            object result = await executeRequest<HttpStatusCode>(HttpRequestType.PUT, $"updateMedicine?index={index}", medicine);
            return result != null ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
        }
    }
}
