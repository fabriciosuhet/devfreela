using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
	private readonly IAuthService _authService;
	private readonly IUserRepository _userRepository;

	public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
	{
		_authService = authService;
		_userRepository = userRepository;
	}

	public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		// utilizar o mesmo algoritmo para criar o hash da senha
		var passwordHash = _authService.ComputeSha256Hash(request.Password);
		
		// buscar no meu banco de dados um User que tenha meu e-mail e minha senha em formato hash
		var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
		
		// se nao exisitr, erro no login
		if (user == null)
		{
			return null;
		}
		
		// se exisit gerar o token com os dados do usuario
		var token = _authService.GenerateJwtToken(user.Email, user.Role);
		return new LoginUserViewModel(user.Email, token);
	}
}