using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic;
using PCMS.API.Data;
using PCMS.API.Filters;
using PCMS.API.Models;
using PCMS.API.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(option =>
{
    option.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingActionFilter>();
    options.Filters.Add<ExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
    option.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new QueryStringApiVersionReader());
})
.AddMvc()
.AddApiExplorer(option =>
{
    option.GroupNameFormat = "'v'V";
    option.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<UserValidationFilter>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<ICaseActionService, CaseActionService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IEvidenceService, EvidenceService>();

var app = builder.Build();

// app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            option.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();

try
{
    Log.Information("Starting App");

    await DatabaseSeeder.SeedDatabase(app.Services);

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}
finally
{
    await Log.CloseAndFlushAsync();
}