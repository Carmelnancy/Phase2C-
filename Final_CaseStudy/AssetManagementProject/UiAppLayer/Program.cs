var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient("ApiClient", client =>
//{
//    client.BaseAddress = new Uri("http://localhost:5239/");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
//});

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5239/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
