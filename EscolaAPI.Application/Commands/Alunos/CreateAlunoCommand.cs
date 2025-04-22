using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using AutoMapper;
using MediatR;

namespace EscolaAPI.Application.Commands.Alunos 
{
    public class CreateAlunoCommand : IRequest<int>
    {
        public CreateAlunoDto AlunoDto { get; set; }

        public CreateAlunoCommand(CreateAlunoDto alunoDto) {
            AlunoDto = alunoDto;
        }
    }

    public class CreateAlunoCommandHandler : IRequestHandler<CreateAlunoCommand, int> 
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;

        public CreateAlunoCommandHandler(IAlunoRepository alunoRepository, IMapper mapper) 
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAlunoCommand request, CancellationToken cancellationToken) 
        {
            // Mapear DTO para entidade
            var aluno = _mapper.Map<Aluno>(request.AlunoDto);

            // Adicionar aluno ao repositório
            await _alunoRepository.AddAsync(aluno);
            await _alunoRepository.SaveChangesAsync();

            return aluno.Id;
        }
    }
}
