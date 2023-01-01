using AdminWeb.Models;
using AdminWeb.Models.VM;
using Data.Common;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdminWeb.Services
{
    public interface IAPIMauSac
    {
        Task<PagedResult<MauSac>> GetPagings();
        Task<ApiResult<MauSac>> Create(MauSac request);
        Task<ApiResult<MauSac>> Delete(int id);
        Task<ApiResult<MauSac>> Update(MauSac rq);
        Task<GetByIdVm<MauSac>> GetById(int id);
        Task<GetByIdVm<MauSac>> GetColorbyname(string request);
    }
    public class APIMauSac : IAPIMauSac
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public APIMauSac(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<MauSac>> Create(MauSac request)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PostAsync
                ($"/api/v1/create-color", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<ApiSuccessResult<MauSac>>(body);
            return kichco;
        }

        public async Task<ApiResult<MauSac>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.DeleteAsync
                ($"/api/v1/delete-color/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<ApiSuccessResult<MauSac>>(body);
            return kichco;
        }

        public async Task<GetByIdVm<MauSac>> GetById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/color/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<GetByIdVm<MauSac>>(body);
            return kichco;
        }

        public async Task<GetByIdVm<MauSac>> GetColorbyname(string name)
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var request = new MauSacByName
            {
                MauSacSP = name,
            };
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/colorbyname", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<GetByIdVm<MauSac>>(body);
            return kichco;
        }

        public async Task<PagedResult<MauSac>> GetPagings()
        {
            /*var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");*/
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.GetAsync
                ($"/api/v1/color");
            var body = await response.Content.ReadAsStringAsync();
            var kichco = JsonConvert.DeserializeObject<PagedResult<MauSac>>(body);
            return kichco;
        }

        public async Task<ApiResult<MauSac>> Update(MauSac rq)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var json = JsonConvert.SerializeObject(rq);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);*/
            var response = await client.PutAsync
                ($"/api/v1/update-color", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var kich = JsonConvert.DeserializeObject<ApiSuccessResult<MauSac>>(body);
            return kich;
        }
    }
}
