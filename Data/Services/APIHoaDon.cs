using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AdminWeb.Services
{
    public interface IAPIHoaDons
    {
        Task<PagedResult<HoaDon>> GetPagings();
        Task<GetByIdVm<HoaDon>> Create(HoaDon request);
        Task<ApiResult<ChiTietHoaDon>> CreateCTHD(ChiTietHoaDon request);
        Task<ApiResult<HoaDon>> Delete(int id);
        Task<ApiResult<HoaDon>> Update(HoaDon rq);
        Task<GetByIdVm<HoaDon>> GetById(int id);
        Task<PagedResult<TheYear>> GetYears();
    }
    public class APIHoaDon : IAPIHoaDons
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APIHoaDon(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetByIdVm<HoaDon>> Create(HoaDon request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-bill", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<GetByIdVm<HoaDon>>(body);
            return loaisanpham;
        }

        public async Task<ApiResult<ChiTietHoaDon>> CreateCTHD(ChiTietHoaDon request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-bill-details", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<ChiTietHoaDon>>(body);
            return loaisanpham;
        }

        public Task<ApiResult<HoaDon>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetByIdVm<HoaDon>> GetById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/bill/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetByIdVm<HoaDon>>(body);
            return result;
        }

        public async Task<PagedResult<HoaDon>> GetPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/bill");
            var body = await response.Content.ReadAsStringAsync();
            var hoadon = JsonConvert.DeserializeObject<PagedResult<HoaDon>>(body);
            return hoadon;
        }

        public async Task<PagedResult<TheYear>> GetYears()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/get-year-hoadon");
            var body = await response.Content.ReadAsStringAsync();
            var year = JsonConvert.DeserializeObject<PagedResult<TheYear>>(body);
            return year;
        }

        public Task<ApiResult<HoaDon>> Update(HoaDon rq)
        {
            throw new NotImplementedException();
        }
    }
}
