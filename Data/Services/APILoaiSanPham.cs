using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RazorWeb.Models;
using RazorWeb.Models.VM;
using System.Text;

namespace RazorWeb.Services
{

    public interface IAPILoaiSanPham
    {
        Task<PagedResult<LoaiSanPhamVm>> GetLoaiSanPhamPagings();
        Task<ApiResult<LoaiSanPham>> CreateLoaiSanPham(LoaiSanPham request);
        Task<ApiResult<LoaiSanPham>> UpdateLoaiSanPham(LoaiSanPham request);
        Task<GetByIdVm<LoaiSanPham>> GetByIdLoaiSanPham(int id);
        Task<ApiResult<LoaiSanPham>> DeleteLoaiSanPham(int id);
    }
    public class APILoaiSanPham : IAPILoaiSanPham
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APILoaiSanPham(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<LoaiSanPham>> CreateLoaiSanPham(LoaiSanPham request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-product-type", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<LoaiSanPham>>(body);
            return loaisanpham;
        }

        public async Task<ApiResult<LoaiSanPham>> DeleteLoaiSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.DeleteAsync
                ($"/api/v1/delete-product-type/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<LoaiSanPham>>(body);
            return loaisanpham;
        }

        public async Task<GetByIdVm<LoaiSanPham>> GetByIdLoaiSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product-type/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<GetByIdVm<LoaiSanPham>>(body);
            return loaisanpham;
        }

        public async Task<PagedResult<LoaiSanPhamVm>> GetLoaiSanPhamPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product-type");
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<PagedResult<LoaiSanPhamVm>>(body);
            return loaisanpham;
        }

        public async Task<ApiResult<LoaiSanPham>> UpdateLoaiSanPham(LoaiSanPham request)
        {
            request.ID = request.IdloaiSanPham;
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/update-product-type", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<LoaiSanPham>>(body);
            return loaisanpham;
        }
    }
}
