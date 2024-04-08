using ChatApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.Api.Filters
{
    public class ValidateFilterAttribute :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto.Fail(400,errors));

            }
        }

    }
}
