using EscolaAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using AutoMapper;
using System.Threading;

namespace EscolaAPI.Application.Queries.Alunos 
{
    public class GetAlunoByMatriculaQuery : IRequest<AlunoDto>
    {
        public string Matricula { get; set; }

        public GetAlunoByMatriculaQuery(string matricula) 
        {
            Matricula = matricula;
        }
    }

    public class GetAlunoByMatriculaQueryHandler : IRequestHandler<GetAlunoByMatriculaQuery, AlunoDto> 
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;
        public GetAlunoByMatriculaQueryHandler(IAlunoRepository alunoRepository, IMapper mapper) 
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }
        public async Task<AlunoDto> Handle(GetAlunoByMatriculaQuery request, CancellationToken cancellationToken) 
        {
            var aluno = await _alunoRepository.GetAlunoByMatriculaAsync(request.Matricula);
            return _mapper.Map<AlunoDto>(aluno);
        }
    }
}
