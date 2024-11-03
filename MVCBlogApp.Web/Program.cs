using MVCBlogApp.Web.Repositories;

namespace MVCBlogApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            string connectionString = "Server=DESKTOP-17L4C0E\\SQLEXPRESS;Database=BlogApp;Integrated Security=True;TrustServerCertificate=Yes";
            builder.Services.AddSingleton(new ArticleRepository(connectionString));
            builder.Services.AddSingleton(new AuthorRepository(connectionString));
            builder.Services.AddSingleton(new CategoryRepository(connectionString));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
