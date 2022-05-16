using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebAPI.Sevices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/*builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);

});*/

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
).AddCookie(options =>
{
    //options.AccessDeniedPath = "/Users/AccessDenied/"; // probably send back to login?
    //options.LoginPath = "/Users/Login/"; // probably send to home?
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.Cookie.IsEssential = true;
    //options.Cookie.HttpOnly = true;
    //options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.ConsentCookie.IsEssential = true;
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("Allow All");

app.UseHttpsRedirection();

// app.UseSession();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();