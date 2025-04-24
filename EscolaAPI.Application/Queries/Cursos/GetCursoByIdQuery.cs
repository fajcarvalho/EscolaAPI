using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Cursos
{
    public class GetCursoByIdQuery : IRequest<CursoDto>
    {
        public int Id { get; set; }
        
        public GetCursoByIdQuery(int id)
        {
            Id = id;
        }
    }
    
    public class GetCursoByIdQueryHandler : IRequestHandler<GetCursoByIdQuery, CursoDto>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;
        
        public GetCursoByIdQueryHandler(ICursoRepository cursoRepository, IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }
        
        public async Task<CursoDto> Handle(GetCursoByIdQuery request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.GetCursoWithDepartamentoAsync(request.Id);
            return _mapper.Map<CursoDto>(curso);
        }
    }
}