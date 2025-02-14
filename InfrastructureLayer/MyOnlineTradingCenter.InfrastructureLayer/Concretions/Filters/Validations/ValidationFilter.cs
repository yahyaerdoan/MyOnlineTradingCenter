﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Filters.Validations;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState != null && !context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value != null && x.Value.Errors.Any())
                .ToDictionary(
                    e => e.Key,
                    e => e.Value?.Errors.Select(error => error.ErrorMessage).ToArray() ?? Array.Empty<string>()
                );

            context.Result = new BadRequestObjectResult(errors);
            return;
        }
        await next();
    }
}
