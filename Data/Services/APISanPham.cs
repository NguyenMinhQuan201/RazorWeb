using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AdminWeb.Services
{
    public interface IAPISanPham
    {
        Task<PagedResult<SanPham>> GetSanPhamPagings();
        Task<ApiResult<SanPham>> CreateSanPham(SanPham request);
        Task<ApiResult<SanPham>> UpdateSanPham(SanPham request);
        Task<GetByIdVm<SanPham>> GetByIdSanPham(int id);
        Task<ApiResult<SanPham>> DeleteSanPham(int id);
        Task<PagedResult<SanPham>> GetSanPhamPagingsByIdLoaiSanPham(int id);
    }
    public class APISanPham : IAPISanPham
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APISanPham(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<SanPham>> CreateSanPham(SanPham request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-product", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var rs = JsonConvert.DeserializeObject<ApiSuccessResult<SanPham>>(body);
            return rs;
        }

        public async Task<ApiResult<SanPham>> DeleteSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.DeleteAsync
                ($"/api/v1/delete-product/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var rs = JsonConvert.DeserializeObject<ApiSuccessResult<SanPham>>(body);
            return rs;
        }

        public async Task<GetByIdVm<SanPham>> GetByIdSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var rs = JsonConvert.DeserializeObject<GetByIdVm<SanPham>>(body);
            return rs;
        }

        public async Task<PagedResult<SanPham>> GetSanPhamPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            try
            {
                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product");
            var body = await response.Content.ReadAsStringAsync();
            var sanpham = JsonConvert.DeserializeObject<PagedResult<SanPham>>(body);
            return sanpham;
        }

        public async Task<PagedResult<SanPham>> GetSanPhamPagingsByIdLoaiSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/list-product/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var rs = JsonConvert.DeserializeObject<PagedResult<SanPham>>(body);
            return rs;
        }

        public async Task<ApiResult<SanPham>> UpdateSanPham(SanPham request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/update-product", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var rs = JsonConvert.DeserializeObject<ApiSuccessResult<SanPham>>(body);
            return rs;
        }
    }
}
