# API de Investimentos

Esta API foi desenvolvida como solução cujo objetivo é criar uma calculadora de investimentos pós-fixados com atualização diária baseada em cotações armazenadas em banco de dados.

A aplicação foi construída em .NET 8, seguindo princípios de Arquitetura Hexagonal, com autenticação JWT, logs, testes unitários e CRUD de cotações.



# Tecnologias Utilizadas

.NET 8

C#

SQL Server (optei por substituir o SQLite sugerido)

Entity Framework Core

Arquitetura Hexagonal (Ports & Adapters)

JWT Authentication

xUnit para testes unitários

Serilog para logs 

Swagger 


# Arquitetura

- A solução segue o padrão da Arquitetura Hexagonal, separando responsabilidades em:

Domain
- Regras de negócio, entidades e serviços puros.

Application
- Casos de uso e DTOs.

Infrastructure
- Repositórios, banco de dados, autenticação e adaptadores externos.

API
- Controllers, autenticação e exposição de rotas.

Essa abordagem garante baixo acoplamento, testabilidade e facilidade de evolução.


# Autenticação

- A API utiliza JWT.

- Fluxo para testar as rotas:

- Criar um usuário 

- Fazer login 

Copiar o token retornado

No Swagger, clicar em Authorize e colar o token (Bearer {token})

Agora todas as rotas protegidas podem ser acessadas

Cotações — CRUD Completo

Implementei um CRUD de cotações, permitindo que o usuário possa cadastrar taxas sem depender apenas dos inserts no banco.

Rotas disponíveis:

GET /cotacoes

GET /cotacoes/{id}

POST /cotacoes

PUT /cotacoes/{id}

DELETE /cotacoes/{id}

Assim o fluxo de cálculo fica independente da manipulação direta no banco.

Foi implementado teste unitário utilizando XUnit para garantir a qualidade do código e do calculo

# Como Executar

- git clone <url-do-repositorio>

- Configurar o SQL Server

- Atualize a connection string no appsettings.json.

- Rodar as migrations

- Add-Migration "Nome da sua migration" / Update-Database

- Executar a API

# Acessar o Swagger

https://localhost:7049/swagger

# Endpoints Principais

POST /api/Calculo

# Body:

{
  "valor": 10000,
  "dataAplicacao": "2025-03-13",
  "dataFinal": "2025-03-21"
}