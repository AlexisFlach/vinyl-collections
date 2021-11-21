using FluentValidation;
using VinylCollection.Entities;

namespace VinylCollection.Mediator.Vinyls
{
    public class VinylValidator : AbstractValidator<Vinyl>
    {
        public VinylValidator() {
            RuleFor(x => x.Artist).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}