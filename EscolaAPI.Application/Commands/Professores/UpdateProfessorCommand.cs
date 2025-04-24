using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Professores
{
    public class UpdateProfessorCommand : IRequest<bool>
    {
        public UpdateProfessorDto ProfessorDto { get; set; }
        
        public UpdateProfessorCommand(UpdateProfessorDto professorDto)
        {
            ProfessorDto = professorDto;
        }
    }
    
    public class UpdateProfessorCommandHandler : IRequestHandler<UpdateProfessorCommand, bool>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        
        public UpdateProfessorCommandHandler(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(request.ProfessorDto.Id);
            if (professor == null) return false;
            
            _mapper.Map(request.ProfessorDto, professor);
            
            await _professorRepository.UpdateAsync(professor);
            return await _professorRepository.SaveChangesAsync();
        }
    }
}