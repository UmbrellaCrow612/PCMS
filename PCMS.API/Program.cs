using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Auth;
using PCMS.API.Models;
using PCMS.API.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


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


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(option =>
    {
        option.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

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

            option.SwaggerEndpoint(url,name);
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

