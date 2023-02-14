using AsyncStream.Models.ApiDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AsyncStream.Filters.ResultFilters;

// since ResultFilterAttribute implement IAsyncResultFilter and we can call it via TypeFilter, we can directly provide our own function
public class BookResultFilter : IAsyncResultFilter
{
    private readonly IMapper _mapper;
    
    public BookResultFilter(IMapper mapper) 
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
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

        resultFromAction.Value = _mapper.Map<BookDto>(resultFromAction.Value);
        
        await next();
    }
}