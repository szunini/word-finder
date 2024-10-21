using FluentValidation;
using qubeyond.wordfinder.domain.Contracts.Validators;
using qubeyond.wordfinder.shared.constant;

namespace quebeyond.wordfinder.infraestructure.Validation
{
    public class WordFinderValidator : AbstractValidator<IEnumerable<string>>, IWordFinderValidator
    {
        public WordFinderValidator()
        {
            RuleFor(matrix => matrix)
                .NotNull()
                .WithMessage(ErrorMessages.MATRIX_NULL_OR_EMPTY)
                .NotEmpty()
                .WithMessage(ErrorMessages.MATRIX_NULL_OR_EMPTY);

            RuleForEach(matrix => matrix)
                .Must(row => row.Length <= 64)
                .WithMessage(ErrorMessages.MATRIX_EXCEEDS_MAX_DIMENSION);

            RuleFor(matrix => matrix.Count())
                .LessThanOrEqualTo(64)
                .WithMessage(ErrorMessages.MATRIX_EXCEEDS_MAX_DIMENSION)
                .WithName("Matrix");
        }

        public void Validate(IEnumerable<string> matrix)
        {
            this.ValidateAndThrow(matrix);
        }
    }
}
