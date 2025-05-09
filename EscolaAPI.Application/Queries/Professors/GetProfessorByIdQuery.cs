using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Professores
{
    public class GetProfessorByIdQuery : IRequest<ProfessorDto>
    {
        public int Id { get; set; }
        
        public GetProfessorByIdQuery(int id)
        {
            Id = id;
        }
    }
    
    public class GetProfessorByIdQueryHandler : IRequestHandler<GetProfessorByIdQuery, ProfessorDto>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        
        public GetProfessorByIdQueryHandler(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }
        
        public async Task<ProfessorDto> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetProfessorWithDepartamentoAsync(request.Id);
            return _mapper.Map<ProfessorDto>(professor);
        }
    }
}