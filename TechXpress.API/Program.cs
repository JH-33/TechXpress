using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TechXpress.API.Controllers;
using TechXpress.BLL.Manger;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TechXpressDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TechXpressDBContext>();
builder.Services.AddScoped<ICategoryManger, CategoryManger>();
builder.Services.AddScoped<ICategoryrepo, categoryrepo>();
builder.Services.AddScoped<IOrderManger,OrderManger>();
builder.Services.AddScoped<IOrderrepo, orderrepo>();
builder.Services.AddScoped<IpaymentManger, PaymentManger>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
builder.Services.AddScoped<IProductManger, ProductManger>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IReviewManger, ReviewManger>();
builder.Services.AddScoped<IReviewRepo,ReviewRepo>();
builder.Services.AddScoped<IShoppingManager, ShoppingManger>();
builder.Services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();
builder.Services.AddScoped<ICouponManager, CouponManager>();
builder.Services.AddScoped<ICouponRepo, CouponRepo>();


builder.Services.AddScoped<IAccountManger, AccountManger>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "jwt";
    option.DefaultChallengeScheme = "jwt";
})
    .AddJwtBearer("samira", options =>
{
    var securitykeystring = builder.Configuration.GetSection("SecretKey").Value;
    var securtykeyByte = Encoding.ASCII.GetBytes(securitykeystring);
    SecurityKey securityKey = new SymmetricSecurityKey(securtykeyByte);

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = securityKey,
        //ValidAudience = "url" ,
        //ValidIssuer = "url",
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 


app.UseAuthorization();

app.MapControllers();

app.Run();
