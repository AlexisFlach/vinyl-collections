using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using VinylCollection.Core;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Mediator.Vinyls
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Vinyl Vinyl {get; set;}
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() {
                RuleFor(x => x.Vinyl).SetValidator(new VinylValidator());
            }
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
                _context.Vinyls.Add(request.Vinyl);

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed to create vinyl");

                return Result<Unit>.Success(Unit.Value);

            }
        }
    }
}