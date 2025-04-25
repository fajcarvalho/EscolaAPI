using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Disciplinas 
{
    public class DeleteDisciplinaCommand : IRequest<bool>
    {
        public int DisciplinaId { get; set; }
        
        public DeleteDisciplinaCommand(int disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }
    }

    public class DeleteDisciplinaCommandHandler : IRequestHandler<DeleteDisciplinaCommand, bool>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        
        public DeleteDisciplinaCommandHandler(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }
        
        public async Task<bool> Handle(DeleteDisciplinaCommand request, CancellationToken cancellationToken)
        {
            var disciplina = await _disciplinaRepository.GetDisciplinaWithAllDetailsAsync(request.DisciplinaId);
            if (disciplina == null) return false;
            
            // Verificar se a disciplina tem turmas
            if (disciplina.Turmas?.Count > 0)
                throw new ApplicationException("Não é possível excluir a disciplina porque existem turmas vinculadas a ela.");
                
            // Verificar se a disciplina é pré-requisito de outras disciplinas
            if (disciplina.DisciplinasQuePreRequisitamEsta?.Count > 0)
                throw new ApplicationException("Não é possível excluir a disciplina porque ela é pré-requisito de outras disciplinas.");
                
            await _disciplinaRepository.RemoveAsync(disciplina);
            return await _disciplinaRepository.SaveChangesAsync();
        }
    }
}
