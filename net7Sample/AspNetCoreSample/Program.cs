﻿var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Map("/", () => "Hello MinimalAPI").AddEndpointFilter<OutputDotNetVersionFilter>();

app.Map("/hhh", () => "Minimal API")
    .AddEndpointFilter(async (context, next) =>
    {
        if (context.HttpContext.Request.QueryString.HasValue)
        {
            return await next(context);
        }
        return Results.Ok(new { Name = "test", Date = DateTime.Today });
    });


var hello = app.MapGroup("/hello");
hello.Map("/test", () => "test");
hello.Map("/test2", () => "test2");

app.MapControllers();

app.Run();

internal sealed class OutputDotNetVersionFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        context.HttpContext.Response.Headers["X-NET-Version"] = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        return await next(context);
    }
}
