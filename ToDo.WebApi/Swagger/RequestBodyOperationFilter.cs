using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ToDo.WebApi.Swagger
{
    public class RequestBodyOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(operation.RequestBody != null)
            {
                operation.RequestBody.Required = true;
            }
        }
    }
}
