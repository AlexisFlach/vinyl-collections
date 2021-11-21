using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VinylCollection.Core;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator
{
    public class List
    {
        public class Query : IRequest<Result<List<Vinyl>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Vinyl>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Result<List<Vinyl>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Vinyl>>.Success(await _context.Vinyls.ToListAsync());
            }
        }
    }
}