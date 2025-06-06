using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TechXpress.API.Controllers;
using TechXpress.API.Middlewares;
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
builder.Services.AddScoped<IDiscountManager, DiscountManager>();
builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAccountManger, AccountManger>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "jwt";
    option.DefaultChallengeScheme = "jwt";
})
    .AddJwtBearer("jwt", options =>
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

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddScoped<IMemoryCache, MemoryCache>();

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

app.UseMiddleware<GlobalException>();

app.Run();