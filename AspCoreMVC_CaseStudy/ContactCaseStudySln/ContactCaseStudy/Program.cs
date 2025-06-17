var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
//var mycon=builder.Configuration["ConnectionStrings:MyCon"];

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name:"default",pattern:"{controller=Contact}/{action=ShowContacts}/{id?}"); });

//app.MapGet("/", () => "Hello World!");

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync(mycon);
//});

app.Run();
