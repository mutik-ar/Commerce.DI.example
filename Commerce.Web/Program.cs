using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Model.Interfaces;
using Model.Parameters;
using Model.Results;
using Model.CommandsServices;
using Repository.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

public static class RootComposite
{
    public static readonly IUnitOfWork Database = new UnitOfWork(@"Server=(localdb)\mssqllocaldb;Database=Commerce;Trusted_Connection=True");
    public static readonly ICommandService<InsertProductParameter, Result> InsertProductService = new InsertProductService(Database.Products);
    public static readonly ICommandService<NullParameter, GetAllProductsResult> GetAllProductService = new GetAllProductService(Database.Products);
    public static readonly ICommandService<IdProductParameter, Result> DeleteProductService = new DeleteProductService(Database.Products);
}

