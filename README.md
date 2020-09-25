# Challenge M Read

API que simula a consulta do Censo Demogr�fico de uma regi�o.

# Tecnologias Usadas

- ASP.NET Core 3.1
- OData
- AutoFac
- Serilog
- MongoDB
- Docker
- XUnit
- AutoFixture
- NSubstitute
- FluentAssertions
- REST

# Principais pontos

- O desafio foi fragmentado em 2 APIs (escrita e leitura) para reduzir a complexidade, simplificar a manutebilidade, permitir maior escala e tolerar falhas
- Protocolo OData (Open Data Protocol): `https://www.odata.org/getting-started/understand-odata-in-6-steps/`
- V�rias possibilidades de consultas individuais e combinadas: `https://www.odata.org/getting-started/basic-tutorial/#queryData`
- Code Coverage em 92%
- HealthCheck para conex�o com a base de dados
- Autentica��o por X-API-KEY com roles
- Log dos requests e responses
- Tratamento para exce��es inesperadas
- "Dockerizada"

# Executando a aplica��o localmente

- baixe e instale o SDK 3.1.402 do .NET Core: `https://dotnet.microsoft.com/download/dotnet-core/3.1`
- clone o reposit�rio: `https://github.com/dmarins/challenge-m-read.git`
- avance para o diret�rio: `challenge-m-read`
- avance para o diret�rio: `src`
- execute o comando de compila��o em seu terminal: `dotnet build`
- execute o comando de execu��o de todos os testes: `dotnet test`
- execute o comando de execu��o da aplica��o: `dotnet run --project "M.Challenge.Read.Api"`
- use a collection do postman para verificar o healthcheck e consultar dados: `https://www.getpostman.com/collections/bb9d3904da75a6961338`
- os logs registrados poder�o ser visualizados em seu terminal
- pare a execu��o atr�ves do comando CTRL + C

# Executando a aplica��o em container na sua m�quina windows

- baixe e instale o Docker Desktop para Windows
- execute ele
- clone o reposit�rio: `https://github.com/dmarins/challenge-m-read.git`
- avance para o diret�rio: `challenge-m-read`
- avance para o diret�rio: `src`
- execute o comando de gera��o de imagem em seu terminal: `docker build -t challenge-m-read:latest .`
- execute o comando de execu��o do docker em seu terminal: `docker run -p 5002:80 challenge-m-read:latest`
- use a collection do postman para verificar o healthcheck e cadastrar dados: `https://www.getpostman.com/collections/bb9d3904da75a6961338`
- os logs registrados poder�o ser visualizados em seu terminal
- saia da execu��o do container atr�ves do comando CTRL + C
- para saber o id do container em execu��o da imagem "challenge-m-read:latest" execute o comando: `docker ps -a`
- copie seu id
- para parar o container execute o comando: `docker stop {id do container}`
