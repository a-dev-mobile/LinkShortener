using LinkShortener.API.Controllers.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LinkShortener.API.SwaggerExtensions
{
    public class CustomSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(CreateLinkRequest))
            {
                schema.Example = new OpenApiObject
                {
                    ["originalUrl"] = new OpenApiString("https://example.com/original-url")
                };
            }
        }
    }
}
