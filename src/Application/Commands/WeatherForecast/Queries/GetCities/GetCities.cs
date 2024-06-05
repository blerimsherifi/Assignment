using Assignment.Application.Common.Interfaces;
using Assignment.Application.Common.Security;
using Assignment.Application.DTOs;

namespace Assignment.Application.WeatherForecast.Queries.GetCities;

[Authorize]
public record GetCitiesQuery : IRequest<IList<CityDto>>
{
    public int CountryID { get; set; }
}

public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IList<CityDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Cities
                .Where(c => c.CountryID == request.CountryID)
                .AsNoTracking()
                .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.CityName)
                .ToListAsync(cancellationToken);
    }
}
