using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VinylCollection.Core;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Details
    {
        public class Query : IRequest<Result<Vinyl>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Vinyl>>
        {   
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Result<Vinyl>> Handle(Query request, CancellationToken cancellationToken)
            {
                var vinyl = await _context.Vinyls.FindAsync(request.Id);
                return Result<Vinyl>.Success(vinyl);
            }
        }
    }
}