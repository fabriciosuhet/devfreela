using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
	private readonly IProjectRepository _projectRepository;

	public DeleteProjectCommandHandler(IProjectRepository projectRepository)
	{
		_projectRepository = projectRepository;
	}
	public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
	{
		await _projectRepository.RemoveProjectAsync(request.Id);
		return Unit.Value;
	}
}