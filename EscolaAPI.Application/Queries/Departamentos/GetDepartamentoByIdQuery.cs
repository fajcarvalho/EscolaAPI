using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Queries.Departamentos
{
    public class GetDepartamentoByIdQuery : IRequest<DepartamentoDto>
    {
        public int Id { get; set; }
        
        public GetDepartamentoByIdQuery(int id)
        {
            Id = id;
        }
    }
    
    public class GetDepartamentoByIdQueryHandler : IRequestHandler<GetDepartamentoByIdQuery, DepartamentoDto>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;
        
        public GetDepartamentoByIdQueryHandler(IDepartamentoRepository departamentoRepository, IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }
        
        public async Task<DepartamentoDto> Handle(GetDepartamentoByIdQuery request, CancellationToken cancellationToken)
        {
            var departamento = await _departamentoRepository.GetDepartamentoWithCordenadorAsync(request.Id);
            return _mapper.Map<DepartamentoDto>(departamento);
        }
    }
}