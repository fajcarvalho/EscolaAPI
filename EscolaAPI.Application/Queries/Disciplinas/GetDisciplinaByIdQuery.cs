using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Disciplinas 
{
    public class GetDisciplinaByIdQuery : IRequest<DisciplinaDto>
    {
        public int Id { get; set; }
        
        public GetDisciplinaByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetDisciplinaByIdQueryHandler : IRequestHandler<GetDisciplinaByIdQuery, DisciplinaDto>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IMapper _mapper;
        
        public GetDisciplinaByIdQueryHandler(IDisciplinaRepository disciplinaRepository, IMapper mapper)
        {
            _disciplinaRepository = disciplinaRepository;
            _mapper = mapper;
        }
        
        public async Task<DisciplinaDto> Handle(GetDisciplinaByIdQuery request, CancellationToken cancellationToken)
        {
            var disciplina = await _disciplinaRepository.GetDisciplinaWithAllDetailsAsync(request.Id);
            return _mapper.Map<DisciplinaDto>(disciplina);
        }
    }
}
