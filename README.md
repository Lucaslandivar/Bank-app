<h1>7Bank API</h1>

<h2>Description of the project</h2>
<p>API REST criada em .NET 9, seguindo arquitetura Clean, com camadas de Model, Repository, Service e Controllers.
O objetivo é simular as principais operações de um banco digital, como gerenciamento de usuários, contas e transações (PIX).</p> 

<h2>Why i make this project?</h2>
<p>I make this project so i can learn more things and functions in JavaScript, also to improve my coding skills by doing something i like to do.</p>

<h2>How does this project work?</h2>
<P>The user has to correctly write the text displayed at the top, once the user has finished writing the text, the time in seconds that the user took will appear at the bottom, as well as a table of the time that the user took a while to write the other sentences. At the top there is also a button to change the theme to a darker or lighter one.</p>

<ul>Tecnologies
  <li>.NET 9 / ASP.NET Core Web API</li>
  <li>Entity Framework Core</li>
  <li>SQL Server</li>
  <li>Migrations</li>
  <li>Dependency Injection</li>
  <li>Repository Pattern</li>
  <li>DTOs</li>
  <li>Postman (testes)</li>
  <li>Angular</li>
</ul>

<h2>Arquitetura da Aplicação</h2>
7Bank.Api/
│
├── Controllers/        → Endpoints da API
├── Models/             → Classes de domínio (Users, Account, Transaction)
├── DTOs/               → Objetos de transferência de dados
├── Services/           → Regras de negócio
├── Repositories/       → Acesso ao banco de dados
├── Data/               → DbContext + Configurações
└── Migrations/         → Histórico do EF

<h2>Como executar o Projeto</h2>

<p>Inserir a seguinte conexão no appsettings.json: "{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=7Bank;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}"</p>
<br>
<p>Aplicar as migrations: "dotnet ef database update"</p>
<p>Rodar a API: "dotnet run"</p>

<h2>Rotas e Endpoints</h2>
<h3>👤 Users</h3>
Método	Rota	Descrição
POST	/api/users	Criar usuário + conta automática
POST	/api/users/login	Validar login
GET	/api/users/{id}	Buscar usuário por ID
GET	/api/users/email/{email}	Buscar por Email
GET	/api/users/cpf/{cpf}	Buscar por CPF
GET	/api/users	Listar usuários

<h3>🏦 Accounts</h3>
Método	Rota	Descrição
GET	/api/account/{id}	Buscar conta por ID
GET	/api/account/user/{userId}	Buscar conta por usuário
GET	/api/account/saldo/{accountId}	Ver saldo
GET	/api/account	Listar todas
PUT	/api/account	Atualizar conta
DELETE	/api/account/{accountId}	Excluir se não houver transações
POST	/api/account/inativar/{accountId}	Inativar conta

<h3>Transactions / PIX</h3>
Método	Rota	Descrição
POST	/api/transactions/pix/{fromUserId}	Realizar PIX
POST	/api/transactions/pix	PIX versão DTO
GET	/api/transactions/user/{userId}	Histórico por usuário
GET	/api/transactions/last3months/{userId}	Últimos 3 meses
GET	/api/transactions	Todas transações

<h2>💳 Como funciona o PIX</h2>
<p>Enviar: "{
  "fromUserId": 1,
  "identifier": "12345678901",
  "identifierType": "cpf",
  "amount": 50
}
" Retorno: "{
  "success": true,
  "message": "Transferência realizada com sucesso!"
}
"</p>

<ul>📘 Regras de Negócio Atendidas
<li>✔ Usuário só pode ter 1 conta</li>
<li>✔ Conta é criada automaticamente ao criar usuário</li>
<li>✔ Conta não pode ser excluída se tiver movimentações</li>
<li>✔ Caso tenha transações → somente inativar</li>
<li>✔ PIX só funciona:</li>
<li>para destinatário cadastrado</li>
<li>saldo suficiente</li>
<li>valor > 0</li>
<li>✔ Busca por transações:</li>
<li>todas</li>
<li>do usuário</li>
<li>últimos 3 meses</li>
</ul>

<h2>Author</h2>
<p>Lucas Landivar de Morais</p>
