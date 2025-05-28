using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;
using System.Text.Json;

namespace Shared.Extensions.Validation
{
    public static class FailedAnnotationValidationResponse
    {
        public static IActionResult MakeValidationResponse(ActionContext context)
        {
            var validationProblemDetails = new ValidationProblemDetails(context.ModelState);

            Dictionary<string, string> validationErrors = new Dictionary<string, string>();

            foreach (var error in validationProblemDetails.Errors)
            {
                validationErrors.Add(error.Key, error.Value.First());
            }

            throw new BadRequest(JsonSerializer.Serialize(validationErrors));
        }
    }
}
