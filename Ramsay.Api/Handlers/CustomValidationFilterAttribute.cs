using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ramsay.Common.Exceptions;
using Ramsay.Domain.DTOs;
using System.Text;

namespace Ramsay.Api.Handlers
{
    public class CustomValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                HttpResponseException responseException = new HttpResponseException()
                {
                    Status = StatusCodes.Status400BadRequest
                };

                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in context.ModelState)
                {
                    string errorMessage = $" - [{item.Key}]: {item.Value.Errors.Select(x => x.ErrorMessage).FirstOrDefault()}";
                    stringBuilder.AppendLine(errorMessage);
                }

                ResponseDto response = new ResponseDto()
                {
                    Message = stringBuilder.ToString(),
                    Result = new UnprocessableEntityObjectResult(context.ModelState).Value!
                };

                context.Result = new ObjectResult(responseException)
                {
                    StatusCode = responseException.Status,
                    Value = response
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }

}
