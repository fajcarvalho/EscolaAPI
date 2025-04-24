using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Application.Mappings 
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Mapeamento de Aluno
            CreateMap<Aluno, AlunoDto>().ReverseMap();
            CreateMap<CreateAlunoDto, Aluno>();
            CreateMap<UpdateAlunoDto, Aluno>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapeamento de Endereco
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<UpdateEnderecoDto, Endereco>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapeamentos para Departamento
            CreateMap<Departamento, DepartamentoDto>();
            CreateMap<CreateDepartamentoDto, Departamento>();
            CreateMap<UpdateDepartamentoDto, Departamento>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            // Mapeamentos para Professor
            CreateMap<Professor, ProfessorDto>()
                .ForMember(dest => dest.DepartamentoNome, opt => opt.MapFrom(src => src.Departamento != null ? src.Departamento.Nome : null));
            CreateMap<CreateProfessorDto, Professor>();
            CreateMap<UpdateProfessorDto, Professor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            
        }
    }
}
    