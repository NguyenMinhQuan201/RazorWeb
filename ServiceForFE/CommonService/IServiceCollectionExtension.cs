using AdminWeb.Services;
using RazorWeb.Models;
using RazorWeb.Services;

namespace ServiceForFE.CommonService
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection Services(this IServiceCollection services)
        {
            services.AddTransient<IAPILoaiSanPham, APILoaiSanPham>();
            services.AddTransient<IAPIChiTietSanPham, APIChiTietSanPham>();
            services.AddTransient<IAPISKichCo, APISKichCo>();
            services.AddTransient<IAPIMauSac, APIMauSac>();
            services.AddTransient<IAPITinTuc, APITinTuc>();
            services.AddTransient<IAPIHoaDons, APIHoaDon>();
            services.AddTransient<IAPISanPham, APISanPham>();
            services.AddTransient<IEventsModels, EventsModels>();
            return services;
        }
    }
}
