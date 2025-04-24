using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Infrastructure.Data;
using EscolaAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Registrar o DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            // Aqui você pode registrar outros serviços de infraestrutura
            // como repositories, etc.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAlunoRepository, AlunoRepository>(); // Registrar o repositório de Aluno
            services.AddScoped<IProfessorRepository, ProfessorRepository>(); // Registrar o repositório de Professor
	        services.AddScoped<IDepartamentoRepository, DepartamentoRepository>(); // Registrar o repositório de Departamentos
            services.AddScoped<ICursoRepository, CursoRepository>(); // Registrar o repositório de Curso


            return services;
        }
    }
}