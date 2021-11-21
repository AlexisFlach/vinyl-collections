using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Vinyl vinyl { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var vinyl = await _context.Vinyls.FindAsync(request.vinyl.Id);

                _mapper.Map(request.vinyl, vinyl);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}