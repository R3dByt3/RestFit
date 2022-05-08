using Microsoft.OpenApi.Models;
using RestFit.API.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestFit.API.Middleware
{
    public class SwaggerQueryParameterAttributeFilter : IOperationFilter, IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.ParameterInfo == null) return;

            var customAttributes = context.ParameterInfo.GetCustomAttributes(true);

            ApplySwaggerParameterAttribute(parameter, customAttributes);
        }

        private static void ApplySwaggerParameterAttribute(OpenApiParameter parameter, object[] customAttributes)
        {
            var swaggerParameterAttribute = customAttributes
                .OfType<QueryParameterBaseAttribute>()
                .FirstOrDefault();

            if (swaggerParameterAttribute == null) return;

            var newParameter = CreateParameter(swaggerParameterAttribute);
            if (!string.IsNullOrWhiteSpace(newParameter.Name)) parameter.Name = newParameter.Name;
            parameter.Description = newParameter.Description;
            parameter.In = newParameter.In;
            parameter.Required = newParameter.Required;
            parameter.Schema = newParameter.Schema;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<QueryParameterBaseAttribute>();

            if (attributes == null) return;
            foreach (var attribute in attributes) operation.Parameters.Add(CreateParameter(attribute));
        }

        private static OpenApiParameter CreateParameter(QueryParameterBaseAttribute attributeBase)
        {
            var baseParameter = CreateBaseParameter(attributeBase);
            return baseParameter;
        }

        private static OpenApiParameter CreateBaseParameter(QueryParameterBaseAttribute attributeBase) => new()
        {
            Name = attributeBase.Name,
            Description = attributeBase.Description,
            In = attributeBase.ParameterType,
            Required = attributeBase.Required,
            Schema = new OpenApiSchema { Type = attributeBase.DataType, Format = attributeBase.Format ?? string.Empty },
        };
    }
}
