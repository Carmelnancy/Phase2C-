var builder = WebApplication.CreateBuilder(args);

// Register MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Enable developer exception page (optional, for debug mode)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Use static files (for Bootstrap, CSS, JS)
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Map controller routes (conventional routing)
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();