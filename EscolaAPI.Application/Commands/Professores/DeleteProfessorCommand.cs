using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Professores
{
    public class DeleteProfessorCommand : IRequest<bool>
    {
        public int ProfessorId { get; set; }
        
        public DeleteProfessorCommand(int professorId)
        {
            ProfessorId = professorId;
        }
    }
    
    public class DeleteProfessorCommandHandler : IRequestHandler<DeleteProfessorCommand, bool>
    {
        private readonly IProfessorRepository _professorRepository;
        
        public DeleteProfessorCommandHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }
        
        public async Task<bool> Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(request.ProfessorId);
            if (professor == null) return false;
            
            await _professorRepository.RemoveAsync(professor);
            return await _professorRepository.SaveChangesAsync();
        }
    }
}