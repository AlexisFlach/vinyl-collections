using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator
{
    public class List
    {
        public class Query : IRequest<List<Vinyl>> { }

        public class Handler : IRequestHandler<Query, List<Vinyl>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<List<Vinyl>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Vinyls.ToListAsync();
            }
        }
    }
}