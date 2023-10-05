using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Web.Http.Results;

namespace Core.Validation
{
    public class ValidationMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = ex.Errors.Select(x => new ValidationError() 
                    {
                        Message = x.ErrorMessage,
                        PropertyName = x.PropertyName 
                    }).ToList()
                };

            await context.Response.WriteAsync(JsonSerializer.Serialize(validationFailureResponse));
            }
        }
    }
}
