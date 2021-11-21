using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid id { get; set; }
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
                var vinyl = await _context.Vinyls.FindAsync(request.id);
                _context.Remove(vinyl);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}