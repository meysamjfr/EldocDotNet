using Newtonsoft.Json.Serialization;
using Project.Application;
using Project.Application.Filters;
using Project.Application.Middlewares;
using Project.Application.Models;
using Project.Infrastructure;
using Project.Persistence;
using Project.Web.Api.Extensions;
using Project.Web.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services
    .AddControllers(options => options.Filters.Add(typeof(ModelStateCheckFilter)))
    .ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true)
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDoc();
builder.Services.AddSwaggerGenNewtonsoftSupport();


builder.Services.AddCors(o =>
{
    var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");
    var allowedOriginsList = allowedOrigins.Split(',');
    //var allowedOrigins = builder.Configuration.GetValue<string[]>("AllowedOrigins");
    o.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://localhost:3000", "https://localhost:3001", "http://eldoc.ir", "https://eldoc.ir")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
//}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "docs";
    c.DocumentTitle = "Eldoc Api Documentation";
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors("CorsPolicy");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
app.MapControllers();
app.MapFallbackToController("notfoundpage", "error");

app.Run();


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
