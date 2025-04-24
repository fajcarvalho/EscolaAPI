using System;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Departamentos
{
    public class DeleteDepartamentoCommand : IRequest<bool>
    {
        public int DepartamentoId { get; set; }
        
        public DeleteDepartamentoCommand(int departamentoId)
        {
            DepartamentoId = departamentoId;
        }
    }
    
    public class DeleteDepartamentoCommandHandler : IRequestHandler<DeleteDepartamentoCommand, bool>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        
        public DeleteDepartamentoCommandHandler(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }
        
        public async Task<bool> Handle(DeleteDepartamentoCommand request, CancellationToken cancellationToken)
        {
            var departamento = await _departamentoRepository.GetByIdAsync(request.DepartamentoId);
            if (departamento == null) return false;
            
            // Verificar se o departamento tem professores
            var departamentoDetalhado = await _departamentoRepository.GetDepartamentoWithProfessoresAsync(request.DepartamentoId);
            if (departamentoDetalhado.Professores?.Count > 0)
                throw new ApplicationException("Não é possível excluir o departamento porque existem professores vinculados a ele.");
            
            // Verificar se o departamento tem cursos
            var departamentoComCursos = await _departamentoRepository.GetDepartamentoWithCursosAsync(request.DepartamentoId);
            if (departamentoComCursos.Cursos?.Count > 0)
                throw new ApplicationException("Não é possível excluir o departamento porque existem cursos vinculados a ele.");
            
            await _departamentoRepository.RemoveAsync(departamento);
            return await _departamentoRepository.SaveChangesAsync();
        }
    }
}