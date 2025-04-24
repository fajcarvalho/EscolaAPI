using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using MediatR;

namespace EscolaAPI.Application.Commands.Departamentos
{
    public class CreateDepartamentoCommand : IRequest<int>
    {
        public CreateDepartamentoDto DepartamentoDto { get; set; }
        
        public CreateDepartamentoCommand(CreateDepartamentoDto departamentoDto)
        {
            DepartamentoDto = departamentoDto;
        }
    }
    
    public class CreateDepartamentoCommandHandler : IRequestHandler<CreateDepartamentoCommand, int>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;
        
        public CreateDepartamentoCommandHandler(IDepartamentoRepository departamentoRepository, IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateDepartamentoCommand request, CancellationToken cancellationToken)
        {
            var departamento = _mapper.Map<Departamento>(request.DepartamentoDto);
            
            await _departamentoRepository.AddAsync(departamento);
            await _departamentoRepository.SaveChangesAsync();
            
            return departamento.Id;
        }
    }
}