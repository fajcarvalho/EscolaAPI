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
            CreateMap<CreateAlunoDTO, Aluno>();
            CreateMap<UpdateAlunoDto, Aluno>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapeamento de Endereco
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<UpdateEnderecoDto, Endereco>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
    