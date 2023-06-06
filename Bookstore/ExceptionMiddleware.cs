using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }
    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (IsbnDublicateException)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = String.Empty,
                Instance = "",
                Title = "Isbn already Exists",
                Type = ""
            };

            var problemDetailsJson = JsonConvert.SerializeObject(problemDetails);

            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (AuthorNotFoundException)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = String.Empty,
                Instance = "",
                Title = "Author not found",
                Type = ""
            };

            var problemDetailsJson = JsonConvert.SerializeObject(problemDetails);

            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (BookNotFoundException)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = String.Empty,
                Instance = "",
                Title = "Book not found.",
                Type = ""
            };

            var problemDetailsJson = JsonConvert.SerializeObject(problemDetails);

            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = JsonConvert.SerializeObject(ex.Error),
                Instance = "",
                Title = "Isbn already Exists",
                Type = ""
            };

            var problemDetailsJson = JsonConvert.SerializeObject(problemDetails);

            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (Exception ex)
        {

            context.Response.ContentType = "appplication/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = "",
                Title = "Internal Server Erorr - something went wrong",
                Type = ""
            };

            var problemDetailsJson = JsonConvert.SerializeObject(problemDetails);

            await context.Response.WriteAsync(problemDetailsJson);



        }
    }
}