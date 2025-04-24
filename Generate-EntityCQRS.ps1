param (
    [Parameter(Mandatory=$true)]
    [string]$EntityName
)

$EntityNameLower = $EntityName.ToLower()
$EntityNamePlural = $EntityName + "s"
$SolutionDir = $PSScriptRoot

# Verificar se os diretórios existem e criar se necessário
if (-not (Test-Path "$SolutionDir\EscolaAPI.Application\Commands\$EntityNamePlural")) {
    New-Item -Path "$SolutionDir\EscolaAPI.Application\Commands\$EntityNamePlural" -ItemType Directory -Force
}

if (-not (Test-Path "$SolutionDir\EscolaAPI.Application\Queries\$EntityNamePlural")) {
    New-Item -Path "$SolutionDir\EscolaAPI.Application\Queries\$EntityNamePlural" -ItemType Directory -Force
}

# Gerar a interface do repositório
Write-Host "Gerando interface do repositório para $EntityName..."
Set-Location "$SolutionDir\EscolaAPI.Application\Interfaces"
dotnet new cqrs-repo-interface --name $EntityName

# Gerar os commands
Write-Host "Gerando commands para $EntityName..."
Set-Location "$SolutionDir\EscolaAPI.Application\Commands\$EntityNamePlural"
dotnet new cqrs-create-command --name $EntityName

# Gerar as queries
Write-Host "Gerando queries para $EntityName..."
Set-Location "$SolutionDir\EscolaAPI.Application\Queries\$EntityNamePlural"
dotnet new cqrs-get-by-id-query --name $EntityName

# Gerar o controller
Write-Host "Gerando controller para $EntityName..."
Set-Location "$SolutionDir\EscolaAPI\Controllers"
dotnet new cqrs-controller --name $EntityName

Write-Host "Gerando implementação do repositório..."
$repoContent = @"
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Infrastructure.Repositories
{
    public class ${EntityName}Repository : Repository<$EntityName>, I${EntityName}Repository
    {
        public ${EntityName}Repository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<$EntityName> Get${EntityName}WithDepartamentoAsync(int id)
        {
            return await _context.${EntityNamePlural}
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<$EntityName> Get${EntityName}ByEmailAsync(string email)
        {
            return await _context.${EntityNamePlural}
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<$EntityName>> Get${EntityNamePlural}ByDepartamentoAsync(int departamentoId)
        {
            return await _context.${EntityNamePlural}
                .Where(p => p.DepartamentoId == departamentoId)
                .ToListAsync();
        }
    }
}
"@

Set-Content -Path "$SolutionDir\EscolaAPI.Infrastructure\Repositories\${EntityName}Repository.cs" -Value $repoContent

Write-Host "Scaffolding para $EntityName completado com sucesso!"