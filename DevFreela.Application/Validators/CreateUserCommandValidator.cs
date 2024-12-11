using System.Text.RegularExpressions;
using DevFreela.Application.Commands.CreateUser;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(p => p.Email)
			.EmailAddress()
			.WithMessage("E-mail não válido");

		RuleFor(p => p.Password)
			.Must(ValidPassword)
			.WithMessage(
				"Senha deve conter pelo menos 8 caracteres, 1 número, 1 letra maiscúla, 1 letra minuscúla e 1 caractere especial (@$!%*?&)");

		RuleFor(p => p.FullName)
			.NotEmpty()
			.NotNull()
			.WithMessage("Nome é obrigatório");
	}

	public bool ValidPassword(string password)
	{
		var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
		return regex.IsMatch(password);
	}
}