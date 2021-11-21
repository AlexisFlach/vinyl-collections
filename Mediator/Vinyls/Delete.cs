using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VinylCollection.Core;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var vinyl = await _context.Vinyls.FindAsync(request.id);

                //  if(vinyl is null) return null;
               
                _context.Remove(vinyl);

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed to delete vinyl");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}