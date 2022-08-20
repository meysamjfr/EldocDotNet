using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Project.Application;
using Project.Application.Filters;
using Project.Application.Middlewares;
using Project.Infrastructure;
using Project.Persistence;
using Project.Web.Experts;
using System.IO.Compression;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddSingleton<HtmlEncoder>(
     HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                                                           UnicodeRanges.All}));

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes =
        ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "image/svg+xml" });
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/account/accessdenied";
        options.Cookie.Name = "ProjectCookie";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LogoutPath = "/account/logout";
        options.LoginPath = "/account";
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //o.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});

builder.Services.AddCors(o =>
{
    var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");
    var allowedOriginsList = allowedOrigins.Split(',');
    o.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://localhost:3000", "https://localhost:3001", "http://eldoc.ir", "https://eldoc.ir")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services
    .AddMvc(option =>
    {
        option.EnableEndpointRouting = false;
        option.Filters.Add(typeof(ModelStateCheckFilter));
    }).AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });

builder.Services.AddSignalR();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/page", "?code={0}");

app.UseHttpsRedirection();
app.UseResponseCompression();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    context.Context.Response.Headers.Add("Cache-Control", "public, max-age=2592000")
});

app.UseCookiePolicy();

app.UseAuthentication();

app.UseSession();

app.Use(async (context, next) =>
{
    string path = context.Request.Path;

    if (path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".jpg") || path.EndsWith(".jpeg") || path.EndsWith(".png"))
    {
        //Set css and js files to be cached for 7 days
        TimeSpan maxAge = new(7, 0, 0, 0);     //7 days
        context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
    }
    else
    {
        //Request for views fall here.
        context.Response.Headers.Append("Cache-Control", "no-cache");
        context.Response.Headers.Append("Cache-Control", "private, no-store");
    }
    await next();
});

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.UseMvc(routes =>
{
    routes.MapRoute(
      name: "areas",
      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapHub<ChatHub>("/chatHub");

app.Run();