using AspRestaurant.Data;
using AspRestaurant.Models;
using AspRestaurant.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AspRestaurantContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("AspRestaurantDB")));

//add identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AspRestaurantContext>()
    .AddDefaultTokenProviders();

//JWT
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };

});



//accountRepository
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
//MenuReposiory
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
//ReviewRepository
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
//Category
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
//cards
builder.Services.AddTransient<ICardRepository, CardRepository>();
//Payment 
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//Adding Stripe configuration
StripeConfiguration.ApiKey = "sk_test_51OlWFUDfRJUn5qMUwhdPj3mt7IueKqmcDeXpVxH3fwHsFpLcjqwnHR88dOTMY2jDSK4BZcJxVeFCFMWYHpXfVzvL009UCxAO7t";


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
