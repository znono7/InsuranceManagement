
using InsuranceManagement.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;
using Container = InsuranceManagement.API.Container;

var builder = WebApplication.CreateBuilder(args);

// Access the App Configuration through the API instance

Container.Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Add DapperDbContext service to the container.
builder.Services.AddScoped<DapperDbContext>();

// Add EFDbContext service to the container.
builder.Services.AddDbContext<EFDbContext>(option =>
                option.UseSqlServer(Container.Configuration.GetConnectionString("DefaultConnection")));

// AddIdentity adds cookie based authentication
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()

    //Adds UserStore and RoleStore from this context
    .AddEntityFrameworkStores<EFDbContext>()

    // Adds a provider that generates unique keys and hashes for things like
    // forgot password links, phone number verification codes etc...
    .AddDefaultTokenProviders();

// Add JWT Authentication for Api clients
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        //Set validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,

            // Set issuer
            ValidIssuer = Container.Configuration["Jwt:Issuer"],
            // Set audience
            ValidAudience = Container.Configuration["Jwt:Audience"],

            // Set signing key
            IssuerSigningKey = new SymmetricSecurityKey(
            // Get our secret key from configuration
                            Encoding.UTF8.GetBytes(Container.Configuration["Jwt:SecretKey"])),
        };
    });

// Change password policy
builder.Services.Configure<IdentityOptions>(options =>
    {
        // Make really weak passwords possible
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    // Redirect to /login 
    options.LoginPath = "/login";

    // Change cookie timeout to expire in 15 seconds
    options.ExpireTimeSpan = TimeSpan.FromSeconds(1500);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Access the service provider through the API instance
var serviceProvider = app.Services;

//Store instance of the DI service provider so the application can access it anywhere
Container.Provider = (ServiceProvider)serviceProvider;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Setup Identity
app.UseAuthentication();

app.MapControllers();

app.Run();
