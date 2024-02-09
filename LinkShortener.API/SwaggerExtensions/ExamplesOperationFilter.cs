using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;


public class ExamplesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        string relativePath = context.ApiDescription.RelativePath ?? "";
        if (relativePath.Equals("create", StringComparison.OrdinalIgnoreCase))
        {
            // Проверка наличия ключа перед добавлением
            if (!operation.Responses.ContainsKey("400"))
            {
                // operation.Responses.Add("400", new OpenApiResponse { Description = "Неверный запрос" });
            }

            if (!operation.Responses.ContainsKey("500"))
            {
                // operation.Responses.Add("500", new OpenApiResponse { Description = "Ошибка сервера" });
            }


        }
    }
}
