using System;
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
        private readonly IDepartamentoRepository _departamentoRepository;
        
        public DeleteProfessorCommandHandler(
            IProfessorRepository professorRepository,
            IDepartamentoRepository departamentoRepository)
        {
            _professorRepository = professorRepository;
            _departamentoRepository = departamentoRepository;
        }
        
        public async Task<bool> Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
        {
            // Buscar o professor
            var professor = await _professorRepository.GetByIdAsync(request.ProfessorId);
            if (professor == null) return false;
            
            // Verificar se o professor é coordenador de algum departamento
            var departamentos = await _departamentoRepository.GetAllDepartamentosWithDetailsAsync();
            foreach (var departamento in departamentos)
            {
                if (departamento.CordenadorId == request.ProfessorId)
                {
                    throw new ApplicationException(
                        $"Não é possível excluir o professor porque ele é coordenador do departamento {departamento.Nome}.");
                }
            }
            
            // Verificar se o professor tem turmas
            var professorComTurmas = await _professorRepository.GetProfessorWithDepartamentoAsync(request.ProfessorId);
            if (professorComTurmas.Turmas?.Count > 0)
            {
                throw new ApplicationException(
                    "Não é possível excluir o professor porque ele possui turmas associadas.");
            }
            
            // Excluir o professor
            await _professorRepository.RemoveAsync(professor);
            return await _professorRepository.SaveChangesAsync();
        }
    }
}