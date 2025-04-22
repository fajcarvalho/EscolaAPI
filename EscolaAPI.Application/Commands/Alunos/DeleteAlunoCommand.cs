using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using EscolaAPI.Application.Interfaces;

namespace EscolaAPI.Application.Commands.Alunos 
{
    public class DeleteAlunoCommand : IRequest<bool>
    {
        public int AlunoId { get; set; }
        public DeleteAlunoCommand(int alunoId) {
            AlunoId = alunoId;
        }
    }

    public class DeleteAlunoCommandHandler : IRequestHandler<DeleteAlunoCommand, bool>
    {
        private readonly IAlunoRepository _alunoRepository;

        public DeleteAlunoCommandHandler(IAlunoRepository alunoRepository) 
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<bool> Handle(DeleteAlunoCommand request, CancellationToken cancellationToken) 
        {
            // Buscar aluno
            var aluno = await _alunoRepository.GetByIdAsync(request.AlunoId);
            if (aluno == null) {
                return false;
            }
            // Remove aluno
            await _alunoRepository.RemoveAsync(aluno);
            return await _alunoRepository.SaveChangesAsync();
        }

    }
}
