using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AdminWeb.Services
{
    public interface IAPISKichCo
    {
        Task<PagedResult<KichCo>> GetKichCoPagings();
        Task<ApiResult<KichCo>> CreateKichCo(KichCo request);
        Task<ApiResult<KichCo>> DeleteKichCo(int id);
        Task<ApiResult<KichCo>> Update(KichCo rq);
        Task<GetByIdVm<KichCo>> GetByIdLoaiKichCo(int id);
    }
    public class APISKichCo : IAPISKichCo
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APISKichCo(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<KichCo>> CreateKichCo(KichCo request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-size", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<ApiSuccessResult<KichCo>>(body);
            return kichco;
        }

        public async Task<ApiResult<KichCo>> DeleteKichCo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.DeleteAsync
                ($"/api/v1/delete-size/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<ApiSuccessResult<KichCo>>(body);
            return kichco;
        }

        public async Task<GetByIdVm<KichCo>> GetByIdLoaiKichCo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/size/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<GetByIdVm<KichCo>>(body);
            return kichco;
        }

        public async Task<PagedResult<KichCo>> GetKichCoPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/size");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<PagedResult<KichCo>>(body);
            return kichco;
        }

        public async Task<ApiResult<KichCo>> Update(KichCo rq)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(rq);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/update-size", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kich = JsonConvert.DeserializeObject<ApiSuccessResult<KichCo>>(body);
            return kich;
        }
    }
}
