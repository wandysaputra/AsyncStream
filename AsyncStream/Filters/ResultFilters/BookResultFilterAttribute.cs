using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AsyncStream.Filters.ResultFilters;

public class BookResultFilterAttribute : ResultFilterAttribute
{
    private readonly IMapper _mapper;

    // the constructor injection won't works if we implement this as attribute [BookResultFilter] as it expects parameters-less constructor
    // , instead uses [TypeFilter(typeof(BookResultFilterAttribute))]
    public BookResultFilterAttribute(IMapper mapper) 
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var contextResult = context.Result;
        var resultFromAction = contextResult as ObjectResult;
        if (resultFromAction?.Value == null
            || resultFromAction.StatusCode < 200
            || resultFromAction.StatusCode >= 300)
        {
            await next();
            return;
        }

        // Manually inject the service from HttpContext if implement this as attribute [BookResultFilter]
        // var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

        await next();
    }
}