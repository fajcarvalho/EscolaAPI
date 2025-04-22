using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;


namespace EscolaAPI.Application.Queries.Alunos 
{
    public class GetAlunoByIdQuery : IRequest<AlunoDto>
    {
        public int Id { get; set; }

        public GetAlunoByIdQuery(int id) {
            Id = id;
        }
    }

    public class GetAlunoByIdQueryHandler : IRequestHandler<GetAlunoByIdQuery, AlunoDto>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;
        
        public GetAlunoByIdQueryHandler(IAlunoRepository alunoRepository, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }
        
        public async Task<AlunoDto> Handle(GetAlunoByIdQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.GetAlunoWithEnderecoAsync(request.Id);
            return _mapper.Map<AlunoDto>(aluno);
        }
    }
}
