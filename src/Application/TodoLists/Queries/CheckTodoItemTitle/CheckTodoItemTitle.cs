using Assignment.Application.Common.Interfaces;
using Assignment.Application.Common.Security;

namespace Assignment.Application.TodoLists.Queries.CheckTodoItemTitle;

[Authorize]
public record CheckTodoItemTitleQuery : IRequest<bool> { public string Title { get; set; } = string.Empty; }

public class CheckTodoItemTitleQueryHandler : IRequestHandler<CheckTodoItemTitleQuery, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CheckTodoItemTitleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CheckTodoItemTitleQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
                      .AsNoTracking()
                      .AnyAsync(t => t.Title == request.Title, cancellationToken);
    }
}
