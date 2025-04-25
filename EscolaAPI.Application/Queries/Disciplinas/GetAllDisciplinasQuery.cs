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
    public class GetAllDisciplinasQuery : IRequest<IEnumerable<DisciplinaDto>>
    {
    }

    public class GetAllDisciplinasQueryHandler : IRequestHandler<GetAllDisciplinasQuery, IEnumerable<DisciplinaDto>>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IMapper _mapper;
        
        public GetAllDisciplinasQueryHandler(IDisciplinaRepository disciplinaRepository, IMapper mapper)
        {
            _disciplinaRepository = disciplinaRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<DisciplinaDto>> Handle(GetAllDisciplinasQuery request, CancellationToken cancellationToken)
        {
            var disciplinas = await _disciplinaRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas);
        }
    }
}
