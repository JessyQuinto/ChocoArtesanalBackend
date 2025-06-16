using ChocoArtesanal.Application.Dtos;
using FluentValidation;

namespace ChocoArtesanal.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
	public RegisterRequestValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
	}
}