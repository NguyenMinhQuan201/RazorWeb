using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Common;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AdminWeb.Services
{
    public interface IAPIChiTietSanPham
    {

        Task<PagedResult<ChiTietSanPhamVM>> GetChiTietSanPhamPagings();
        Task<ApiResult<ChiTietSanPhamVM>> CreateChiTietSanPham(ChiTietSanPhamVM request);
        Task<ApiResult<ChiTietSanPhamVM>> UpdateChiTietSanPham(ChiTietSanPhamVM request);
        Task<GetByIdVm<ChiTietSanPhamVM>> GetByIdChiTietSanPham(int id);
        Task<PagedResult<ChiTietSanPham>> GetByIdSanPham(int id);
        Task<ApiResult<ChiTietSanPham>> DeleteChiTietSanPham(int id);
        Task<GetByIdVm<ChiTietSanPham>> GetChiTietSanPhamByConditionID_MS_KC_SL(CombineConditionChiTietSanPham request);
        Task<PagedResult<ChiTietSanPham>> getMauSacByKichCo(MauSacByKichCo request);
    }
    public class APIChiTietSanPham : IAPIChiTietSanPham
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APIChiTietSanPham(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<ChiTietSanPhamVM>> CreateChiTietSanPham(ChiTietSanPhamVM request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-product-details", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<ChiTietSanPhamVM>>(body);
            return loaisanpham;
        }

        public async Task<ApiResult<ChiTietSanPham>> DeleteChiTietSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.DeleteAsync
                ($"/api/v1/delete-product-type/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var loaisanpham = JsonConvert.DeserializeObject<ApiSuccessResult<ChiTietSanPham>>(body);
            return loaisanpham;
        }

        public async Task<GetByIdVm<ChiTietSanPhamVM>> GetByIdChiTietSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product-details/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetByIdVm<ChiTietSanPhamVM>>(body);
            return result;
        }

        public async Task<PagedResult<ChiTietSanPham>> GetByIdSanPham(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product-details-by-idsanpham/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PagedResult<ChiTietSanPham>>(body);
            return result;
        }

        public async Task<PagedResult<ChiTietSanPhamVM>> GetChiTietSanPhamPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/product-details");
            var body = await response.Content.ReadAsStringAsync();
            var chitietsanpham = JsonConvert.DeserializeObject<PagedResult<ChiTietSanPhamVM>>(body);
            return chitietsanpham;
        }

        public async Task<ApiResult<ChiTietSanPhamVM>> UpdateChiTietSanPham(ChiTietSanPhamVM request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/update-product-details", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiSuccessResult<ChiTietSanPhamVM>>(body);
            return result;
        }
        public async Task<GetByIdVm<ChiTietSanPham>> GetChiTietSanPhamByConditionID_MS_KC_SL(CombineConditionChiTietSanPham request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/product-details-by-condition", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetByIdVm<ChiTietSanPham>>(body);
            return result;
        }

        public async Task<PagedResult<ChiTietSanPham>> getMauSacByKichCo(MauSacByKichCo request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/colorbysize", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PagedResult<ChiTietSanPham>>(body);
            return result;
        }
    }
}
