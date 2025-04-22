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
    public class GetAllAlunosQuery : IRequest<IEnumerable<AlunoDto>>
    {
    }

    public class GetAllAlunosQueryHandler : IRequestHandler<GetAllAlunosQuery, IEnumerable<AlunoDto>> 
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;

        public GetAllAlunosQueryHandler(IAlunoRepository alunoRepository, IMapper mapper) {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlunoDto>> Handle(GetAllAlunosQuery request, CancellationToken cancellationToken) 
        {
            var alunos = await _alunoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AlunoDto>>(alunos);
        }
    }
}
