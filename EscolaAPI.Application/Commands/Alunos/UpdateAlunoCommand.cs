using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;
using AutoMapper;

namespace EscolaAPI.Application.Commands.Alunos 
{
    public class UpdateAlunoCommand : IRequest<bool>
    {
        public UpdateAlunoDto AlunoDto { get; set; }
        
        public UpdateAlunoCommand(UpdateAlunoDto alunoDto)
        {
            AlunoDto = alunoDto;
        }
    }

    public class UpdateAlunoCommandHandler : IRequestHandler<UpdateAlunoCommand, bool>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;
        
        public UpdateAlunoCommandHandler(IAlunoRepository alunoRepository, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
        {
            // Buscar aluno existente
            var aluno = await _alunoRepository.GetAlunoWithEnderecoAsync(request.AlunoDto.Id);
            if (aluno == null) return false;
            
            // Atualizar dados do aluno
            _mapper.Map(request.AlunoDto, aluno);
            
            // Atualizar no repositório
            await _alunoRepository.UpdateAsync(aluno);
            return await _alunoRepository.SaveChangesAsync();
        }
    }
}
