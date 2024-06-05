using Assignment.Application.Common.Interfaces;
using Assignment.Application.Common.Security;

namespace Assignment.Application.TodoLists.Queries.CheckTodoTitle;

[Authorize]
public record CheckTodoTitleQuery : IRequest<bool> { public string Title { get; set; } = string.Empty; }

public class CheckTitleQueryHandler : IRequestHandler<CheckTodoTitleQuery, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CheckTitleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CheckTodoTitleQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoLists
                      .AsNoTracking()
                      .AnyAsync(t => t.Title == request.Title, cancellationToken);
    }
}
