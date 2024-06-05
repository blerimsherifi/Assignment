using Assignment.Application.Common.Interfaces;
using Assignment.Application.Common.Security;

namespace Assignment.Application.TodoLists.Queries.GetTodos;

[Authorize]
public record GetTodosQuery : IRequest<IList<DTOs.TodoListDto>>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IList<DTOs.TodoListDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<DTOs.TodoListDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoLists
                .AsNoTracking()
                .ProjectTo<DTOs.TodoListDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}
