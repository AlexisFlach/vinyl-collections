using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Details
    {
        public class Query : IRequest<Vinyl>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Vinyl>
        {   
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Vinyl> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Vinyls.FindAsync(request.Id);
            }
        }
    }
}