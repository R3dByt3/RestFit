using Microsoft.OpenApi.Models;
using RestFit.API.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RestFit.API.Middleware
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context.Type == null)
                return;

            var excludedProperties = context.Type.GetProperties()
                                         .Where(t =>
                                                t.GetCustomAttribute<SwaggerIgnoreParameter>()
                                                != null);

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.Properties.ContainsKey(excludedProperty.Name))
                    schema.Properties.Remove(excludedProperty.Name);
            }
        }
    }
}
