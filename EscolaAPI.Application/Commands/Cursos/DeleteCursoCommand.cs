using System;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Cursos
{
    public class DeleteCursoCommand : IRequest<bool>
    {
        public int CursoId { get; set; }
        
        public DeleteCursoCommand(int cursoId)
        {
            CursoId = cursoId;
        }
    }
    
    public class DeleteCursoCommandHandler : IRequestHandler<DeleteCursoCommand, bool>
    {
        private readonly ICursoRepository _cursoRepository;
        
        public DeleteCursoCommandHandler(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }
        
        public async Task<bool> Handle(DeleteCursoCommand request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.GetByIdAsync(request.CursoId);
            if (curso == null) return false;
            
            // Verificar se o curso tem disciplinas
            var cursoComDisciplinas = await _cursoRepository.GetCursoWithDisciplinasAsync(request.CursoId);
            if (cursoComDisciplinas.Disciplinas?.Count > 0)
                throw new ApplicationException("Não é possível excluir o curso porque existem disciplinas vinculadas a ele.");
            
            await _cursoRepository.RemoveAsync(curso);
            return await _cursoRepository.SaveChangesAsync();
        }
    }
}