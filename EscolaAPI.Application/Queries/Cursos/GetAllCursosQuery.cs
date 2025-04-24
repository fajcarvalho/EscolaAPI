using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Application.Queries.Cursos
{
    public class GetAllCursosQuery : IRequest<IEnumerable<CursoDto>>
    {
    }
    
    public class GetAllCursosQueryHandler : IRequestHandler<GetAllCursosQuery, IEnumerable<CursoDto>>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;
        
        public GetAllCursosQueryHandler(ICursoRepository cursoRepository, IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CursoDto>> Handle(GetAllCursosQuery request, CancellationToken cancellationToken)
        {
            var cursos = await _cursoRepository.GetAllWithDepartamentoAsync();
            return _mapper.Map<IEnumerable<CursoDto>>(cursos);
        }
    }
}