using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
{
	private readonly IProjectRepository _projectRepository;

	public FinishProjectCommandHandler(IProjectRepository projectRepository)
	{
		_projectRepository = projectRepository;
	}

	public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
	{
		var project = await _projectRepository.GetByIdAsync(request.Id);
		await _projectRepository.FinishAsync(project);
		return Unit.Value;
	}
}