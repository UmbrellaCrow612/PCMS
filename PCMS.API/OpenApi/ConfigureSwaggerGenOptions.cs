using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PCMS.API.OpenApi
{
    public class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider = apiVersionDescriptionProvider;

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                var OpenApiInfo = new OpenApiInfo
                {
                    Title = $"PCMS.API v{description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                };

                options.SwaggerDoc(description.GroupName, OpenApiInfo);
            }
        }
    }
}
