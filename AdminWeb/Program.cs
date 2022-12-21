using AdminWeb.CommonService;
using AdminWeb.Services;
using Data.Models;
using Data.Services;
using RazorWeb.Models;
using RazorWeb.Services;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyOrigin()
                                .AllowAnyMethod();
        });
});
/*builder.Services.AddCors();*/
builder.Services.AddHttpClient();
//CommonService
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//LoaiSanPhamService
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAPILoaiSanPham, APILoaiSanPham>();
builder.Services.AddTransient<IAPIChiTietSanPham, APIChiTietSanPham>();
builder.Services.AddTransient<IAPISKichCo, APISKichCo>();
builder.Services.AddTransient<IAPIMauSac, APIMauSac>();
builder.Services.AddTransient<IAPITinTuc, APITinTuc>();
builder.Services.AddTransient<IAPIHoaDons, APIHoaDon>();
builder.Services.AddTransient<IAPISanPham, APISanPham>();
builder.Services.AddTransient<IEventsModels, EventsModels>();
builder.Services.AddTransient<IAPINguoiDung, APINguoiDung>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
/*app.UseCors(builder =>
{
    builder
          .WithOrigins("http://localhost:3000", "https://localhost:3000")
          .SetIsOriginAllowedToAllowWildcardSubdomains()
          .AllowAnyHeader()
          .AllowCredentials()
          .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
          .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));

}
);*/
app.Run();
