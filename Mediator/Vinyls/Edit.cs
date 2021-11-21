using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using VinylCollection.Core;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Vinyl Vinyl { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() {
                RuleFor(x => x.Vinyl).SetValidator(new VinylValidator());
            }
        }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var vinyl = await _context.Vinyls.FindAsync(request.Vinyl.Id);
                if(vinyl is null) return null;

                _mapper.Map(request.Vinyl, vinyl);

                var result = await _context.SaveChangesAsync() > 0;

                 if(!result) return Result<Unit>.Failure("Failed to update vinyl");

                return Result<Unit>.Success(Unit.Value);   

            }
        }
    }
}