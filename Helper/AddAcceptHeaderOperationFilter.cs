using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AddAcceptHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("application/json;v=1.0"),
                    new OpenApiString("application/json;v=2.0")
                },
                Default = new OpenApiString("application/json;v=1.0")
            },
            Description = "حدد نسخة الـ API (application/json;v=1.0 أو application/json;v=2.0)"
        });
    }
}
