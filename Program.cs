using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;
using SampleTaskApp.Repositories;
using SampleTaskApp.UnitOfWork;
using SampleTaskApp.Utilities;
using System.Data;
using System.Security.Claims;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SampleTaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

builder.Services.AddScoped<IDbConnection>(provider => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
#region JWT 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            NameClaimType = ClaimTypes.NameIdentifier // Ensure that UserId is recognized as the NameIdentifier
        };
    });

#endregion

#region Repository Service


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEfUserInfoRepository<UserInfo>, EfUserInfoRepository>();
builder.Services.AddScoped<IEfDoctorsRepository<Doctors>, EfDoctorsRepository>();
builder.Services.AddScoped<IEfHospitalsRepository<Hospitals>, EfHospitalsRepository>();
builder.Services.AddScoped<IEfBedsRepository<Beds>, EfBedsRepository>();
builder.Services.AddScoped<IEfPatientsRepository<Patients>, EfPatientsRepository>();
builder.Services.AddScoped<IEfBedsAlotementsRepository<BedsAlotements>, EfBedsAlotementsRepository>();
builder.Services.AddScoped<IEfNotificationsRepository<Notifications>, EfNotificationsRepository>();

builder.Services.AddScoped<IDapperUserInfoRepository<UserInfo>, DapperUserInfoRepository>();
builder.Services.AddScoped<IDapperDoctorsRepository<Doctors>, DapperDoctorsRepository>();
builder.Services.AddScoped<IDapperHospitalsRepository<Hospitals>, DapperHospitalsRepository>();
builder.Services.AddScoped<IDapperBedsRepository<Beds>, DapperBedsRepository>();
builder.Services.AddScoped<IDapperPatientsRepository<Patients>, DapperPatientsRepository>();
builder.Services.AddScoped<IDapperBedsAlotementsRepository<BedsAlotements>, DapperBedsAlotementsRepository>();
builder.Services.AddScoped<IDapperNotificationsRepository<Notifications>, DapperNotificationsRepository>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUserPermissionService, EfUserPermissionService>();

builder.Services.AddScoped<IAuthorizationHandler, CustomActionAuthorizationHandler>();
builder.Services.AddHttpContextAccessor();
#endregion
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomActionPolicy", policy =>
        policy.Requirements.Add(new CustomActionAuthorizationRequirement())); // Placeholder
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Ensure that the database is created on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SampleTaskDbContext>();
    dbContext.Database.EnsureCreated();  // This will create the database if it doesn't exist
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // Enable Authentication Middleware
app.UseAuthorization();   // Enable Authorization Middleware

app.MapControllers();

app.Run();
