using BatteriesAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BatteriesAPI.Filters
{
    public class UnificationFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is not FileResult)
            {
                var data = context.Result is ObjectResult objRes ? objRes.Value : null;

                var statusCode = context.Result is IStatusCodeActionResult scRes
                    ? scRes.StatusCode
                    : StatusCodes.Status200OK;

                var resp = new Response<object>
                {
                    IsSuccess = statusCode >= 200 && statusCode < 300,
                    Data = data,
                };

                context.Result = new ObjectResult(resp) { StatusCode = statusCode };
            }
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}
