using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Disciplinas 
{
    public class GetDisciplinasByCursoQuery : IRequest<IEnumerable<DisciplinaDto>>
    {
        public int CursoId { get; set; }
        
        public GetDisciplinasByCursoQuery(int cursoId)
        {
            CursoId = cursoId;
        }
    }

    public class GetDisciplinasByCursoQueryHandler : IRequestHandler<GetDisciplinasByCursoQuery, IEnumerable<DisciplinaDto>>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IMapper _mapper;
        
        public GetDisciplinasByCursoQueryHandler(IDisciplinaRepository disciplinaRepository, IMapper mapper)
        {
            _disciplinaRepository = disciplinaRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<DisciplinaDto>> Handle(GetDisciplinasByCursoQuery request, CancellationToken cancellationToken)
        {
            var disciplinas = await _disciplinaRepository.GetDisciplinasByCursoAsync(request.CursoId);
            return _mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas);
        }
    }
}
