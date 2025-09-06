using BatteriesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BatteriesAPI.Filters
{
    public class UnificationFilter : IResultFilter
    {
        private const string ValidationErrorCode = "validation";

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is FileResult) return;

            var objResult = context.Result as ObjectResult;
            var data = objResult?.Value;

            var statusCode = context.Result is IStatusCodeActionResult scRes
                ? scRes.StatusCode ?? StatusCodes.Status200OK
                : StatusCodes.Status200OK;

            var isSuccess = statusCode >= 200 && statusCode < 300;
            var resp = new Response<object>
            {
                IsSuccess = isSuccess,
                Data = isSuccess ? data : null,
                Error = isSuccess ? null : BuildError(context, objResult)
            };

            context.Result = new ObjectResult(resp) { StatusCode = statusCode };
        }

        public void OnResultExecuted(ResultExecutedContext context) { }

        private static Error? BuildError(ResultExecutingContext context, ObjectResult? objRes)
        {
            var state = (context.Controller as ControllerBase)?.ModelState;
            if (state?.IsValid == false)
            {
                var details = state
                    .Where(kvp => kvp.Value?.Errors?.Count > 0)
                    .SelectMany(kvp => kvp.Value!.Errors.Select((e, i) =>
                        new ErrorDetails(
                            Id: string.IsNullOrEmpty(kvp.Key) ? $"__model__:{i}" : kvp.Key,
                            Msg: string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Validation error" : e.ErrorMessage)))
                    .ToList();

                return new Error
                {
                    Code = ValidationErrorCode,
                    Msg = "One or more validation errors occurred.",
                    Details = details
                };
            }

            if (objRes?.Value is ValidationProblemDetails vpd)
            {
                var details = vpd.Errors
                    .SelectMany(kvp => kvp.Value.Select((msg, i) =>
                        new ErrorDetails(
                            Id: string.IsNullOrEmpty(kvp.Key) ? $"__model__:{i}" : kvp.Key,
                            Msg: string.IsNullOrWhiteSpace(msg) ? "Validation error" : msg)))
                    .ToList();

                return new Error
                {
                    Code = ValidationErrorCode,
                    Msg = string.IsNullOrWhiteSpace(vpd.Title) ? "Validation failed." : vpd.Title,
                    Details = details
                };
            }

            if (objRes?.Value is ProblemDetails pd)
            {
                return new Error
                {
                    Code = string.IsNullOrWhiteSpace(pd.Type) ? "problem" : pd.Type,
                    Msg = string.IsNullOrWhiteSpace(pd.Detail) ? pd.Title : pd.Detail,
                    Details = []
                };
            }

            return new Error
            {
                Code = "error",
                Msg = "Request failed.",
                Details = []
            };
        }
    }
}
