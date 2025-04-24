using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Departamentos
{
    public class GetAllDepartamentosQuery : IRequest<IEnumerable<DepartamentoDto>>
    {
    }
    
    public class GetAllDepartamentosQueryHandler : IRequestHandler<GetAllDepartamentosQuery, IEnumerable<DepartamentoDto>>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;
        
        public GetAllDepartamentosQueryHandler(IDepartamentoRepository departamentoRepository, IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<DepartamentoDto>> Handle(GetAllDepartamentosQuery request, CancellationToken cancellationToken)
        {
            var departamentos = await _departamentoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartamentoDto>>(departamentos);
        }
    }
}