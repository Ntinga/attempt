using RaceDay.MVC.Services;

namespace RaceDay.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
                ?? throw new InvalidOperationException("ApiSettings:BaseUrl is not configured.");

            builder.Services.AddHttpClient("RaceDayApi", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IEnrolmentService, EnrolmentService>();

            builder.Services.AddScoped<IResultService, ResultService>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IResultService, ResultService>();
            builder.Services.AddScoped<IEventService, EventService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
