

using Borrar_BCP_CL_2.Context;
using Microsoft.EntityFrameworkCore;

using Borrar_BCP_CL_2.Models;
using Borrar_BCP_CL_2.Services;
using Borrar_BCP_CL_2.Services.Interface;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<BcpCl2Context>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLchain"))
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAutorizationService,AuthorizationService>();

var key = builder.Configuration.GetValue<string>("JwtSetting:key");
var keyBites = Encoding.ASCII.GetBytes(key);


builder.Services.AddAuthentication(
    confing => { 
        confing.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        confing.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
    confing => {
        confing.RequireHttpsMetadata = false;
        confing.SaveToken = true;
        confing.TokenValidationParameters = new TokenValidationParameters { 
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =  new SymmetricSecurityKey(keyBites),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
);

builder.Services.AddScoped<IEmailService,EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//string wwwroot = app.Environment.WebRootPath;
//RotativaConfiguration.Setup(wwwroot, "Rotativa");


app.Run();
