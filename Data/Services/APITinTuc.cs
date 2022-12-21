using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AdminWeb.Services
{
    public interface IAPITinTuc
    {
        Task<PagedResult<TinTuc>> GetPagings();
        Task<ApiResult<TinTuc>> Create(TinTuc request);
        Task<ApiResult<TinTuc>> Delete(int id);
        Task<ApiResult<TinTuc>> Update(TinTuc rq);
        Task<GetByIdVm<TinTuc>> GetById(int id);
    }
    public class APITinTuc : IAPITinTuc
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APITinTuc(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<TinTuc>> Create(TinTuc request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-news", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<ApiSuccessResult<TinTuc>>(body);
            return kichco;
        }

        public async Task<ApiResult<TinTuc>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.DeleteAsync
                ($"/api/v1/delete-news/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<ApiSuccessResult<TinTuc>>(body);
            return kichco;
        }

        public async Task<GetByIdVm<TinTuc>> GetById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/news/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<GetByIdVm<TinTuc>>(body);
            return kichco;
        }

        public async Task<PagedResult<TinTuc>> GetPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/news");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<PagedResult<TinTuc>>(body);
            return kichco;
        }

        public async Task<ApiResult<TinTuc>> Update(TinTuc rq)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(rq);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/update-news", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kich = JsonConvert.DeserializeObject<ApiSuccessResult<TinTuc>>(body);
            return kich;
        }
    }
}
