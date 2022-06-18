using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Hubs;
using WebAPI.Sevices;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAPIContext") ?? throw new InvalidOperationException("Connection string 'WebAPIContext' not found.")));

var defaultApp = FirebaseApp.Create(new AppOptions(){ 
    Credential = GoogleCredential.FromFile(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "androidwhatsapp-afa72-firebase-adminsdk-ldyqv-1d65efed13.json"))
});


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSignalR();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtParams:Audience"],
        ValidIssuer = builder.Configuration["JwtParams:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtParams:SecretKey"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
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

app.UseRouting();

app.UseCors("Allow All");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireCors("Allow All");

app.UseHttpLogging();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessagesHub>("/messagesHub").RequireCors("Allow All");
});


app.Run();
