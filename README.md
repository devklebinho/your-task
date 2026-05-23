# 📋 Desafio Fullstack - Gerenciador de Tarefas

Este projeto é um sistema gerenciador de tarefas completo, composto por uma API  desenvolvida em **.NET**, um banco de dados relacional **SQL Server** e uma interface reativa em **Angular**.



# 🚀 Como Executar o Projeto

Para facilitar a avaliação, o projeto pode ser executado de duas formas:

* **Execução Tradicional (Local)** — recomendada para ambientes Windows.
* **Execução via Docker** — utilizada durante o desenvolvimento do projeto.


# 🖥️ Opção 1: Execução Tradicional (Recomendado para Windows)

## Pré-requisitos

Certifique-se de possuir instalado:

* [.NET SDK 9.0+](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)
* [Node.js v18+ e npm](https://nodejs.org/)
* [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)


## 1. Configurar o Banco de Dados

Abra o arquivo:

```bash
backend/appsettings.json
```



E configure a `ConnectionString` para apontar para sua instância local do SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TarefaDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## 2. Executar o Back-end (.NET)

No terminal, navegue até a pasta do back-end:

```bash
cd backend
```

Execute as migrações para criar automaticamente o banco de dados e as tabelas:

```bash
dotnet ef database update
```

Depois, inicie a aplicação:

```bash
dotnet run
```

A API estará disponível em:

```bash
http://localhost:5071
```

---

## 3. Executar o Front-end (Angular)

Abra outro terminal e navegue até a pasta do front-end:

```bash
cd frontend
```

Instale as dependências:

```bash
npm install
```

Inicie o servidor Angular:

```bash
ng serve
```

O front-end estará disponível em:

```bash
http://localhost:4200
```

---

# 🐳 Opção 2: Execução via Docker

Durante o desenvolvimento deste projeto foi utilizada a distribuição **Zorin OS (Linux)**. Como o SQL Server não possui uma instalação nativa simples no ecossistema Linux, optou-ei pela utilização do Docker.

O Docker permitiu:

* Isolar o banco de dados em um contêiner leve;
* Garantir maior portabilidade;
* Evitar conflitos de ambiente;
* Manter o sistema operacional limpo;
* Facilitar a inicialização rápida do banco de dados.


## Executando o SQL Server com Docker

Caso possua Docker instalado (Linux ou Docker Desktop no Windows), execute:

```bash
docker run -e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=SuaSenhaForte123!" \
-p 1433:1433 \
--name sqlserver-desafio \
-d mcr.microsoft.com/mssql/server:2022-latest
```

> ⚠️ **Importante:**
> Ao utilizar o contêiner Docker, lembre-se de ajustar a `ConnectionString` no `appsettings.json` para utilizar:
>
> * Usuário: `sa`
> * Senha: `SuaSenhaForte123!`

---

# 🛠️ Tecnologias Utilizadas

## Front-end

* Angular
* Standalone Components
* HttpClient
* Experimental Zoneless Change Detection

## Back-end

* .NET Web API
* Entity Framework Core (Code First)

## Banco de Dados

* Microsoft SQL Server

## Ambiente de Desenvolvimento

* Zorin OS (Linux)
* Docker

---

# 👨‍💻 Desenvolvido por

**Kleber Silva** @devklebinho
