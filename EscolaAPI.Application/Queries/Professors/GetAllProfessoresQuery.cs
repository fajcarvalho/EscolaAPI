using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Professores
{
    public class GetAllProfessoresQuery : IRequest<IEnumerable<ProfessorDto>>
    {
    }
    
    public class GetAllProfessoresQueryHandler : IRequestHandler<GetAllProfessoresQuery, IEnumerable<ProfessorDto>>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        
        public GetAllProfessoresQueryHandler(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ProfessorDto>> Handle(GetAllProfessoresQuery request, CancellationToken cancellationToken)
        {
            var professores = await _professorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfessorDto>>(professores);
        }
    }
}