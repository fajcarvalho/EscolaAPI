# EscolaAPI

EscolaAPI é uma API RESTful para gerenciamento escolar desenvolvida em C# .NET. O projeto implementa a arquitetura CQRS (Command Query Responsibility Segregation) com MediatR e foco em boas práticas de desenvolvimento.

## 📋 Sumário

- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Principais Recursos](#-principais-recursos)

## 🚀 Tecnologias

- **ASP.NET Core 8.0**
- **Entity Framework Core 9.0**
- **SQL Server** (via Docker)
- **MediatR** (implementação CQRS)
- **AutoMapper**
- **FluentValidation**
- **Swagger/OpenAPI**
- **xUnit** (testes)

## 🏗 Arquitetura

O projeto segue uma arquitetura em camadas baseada em Clean Architecture e CQRS:

- **Domain**: Entidades e regras de negócio
- **Application**: Casos de uso, DTOs, validações e mapeamentos
- **Infrastructure**: Persistência de dados e serviços externos
- **API**: Controllers e configuração da aplicação web

### Padrão CQRS

- **Commands**: Operações que modificam o estado do sistema (Create, Update, Delete)
- **Queries**: Operações que apenas leem dados (Get, GetAll, GetBy...)
- **MediatR**: Implementa o padrão Mediator para comunicação entre camadas

## 📁 Estrutura do Projeto

```
EscolaAPI.sln
├── EscolaAPI                         # Projeto principal (API)
├── EscolaAPI.Domain                  # Camada de domínio
├── EscolaAPI.Application             # Camada de aplicação
├── EscolaAPI.Infrastructure          # Camada de infraestrutura
├── EscolaAPI.Tests                   # Testes unitários
└── EscolaAPI.IntegrationTests        # Testes de integração
```

## ✨ Principais Recursos

- **Gestão de entidades escolares**:
  - Alunos
  - Professores
  - Departamentos
  - Cursos
  - Disciplinas
  - Turmas
  - Matrículas
  - Avaliações
  - Notas
  - etc.

- **Implementação de todos os tipos de relacionamentos**:
  - 1:1 (ex: Aluno-Endereço)
  - 1:N (ex: Curso-Disciplinas)
  - N:N (ex: Disciplina-Professores, Disciplina-Pré-requisitos)

- **Operações CRUD completas para todas as entidades**

- **Validações com FluentValidation**

- **Mapeamento automático com AutoMapper**


## 🚧 Status do Projeto

O projeto encontra-se em desenvolvimento ativo, com as seguintes funcionalidades já implementadas:

- ✅ CRUD completo para Alunos
- ✅ CRUD completo para Professores
- ✅ CRUD completo para Departamentos
- ✅ CRUD completo para Cursos
- ✅ CRUD completo para Disciplinas
