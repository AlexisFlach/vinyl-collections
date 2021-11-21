using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Create
    {
        public class Command : IRequest
        {
            public Vinyl Vinyl {get; set;}
        }

        public class Handler : IRequestHandler<Command>
        {   
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Vinyls.Add(request.Vinyl);

                await _context.SaveChangesAsync();

                return Unit.Value;

            }
        }
    }
}