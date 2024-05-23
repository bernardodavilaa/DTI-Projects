# recebimento-integracao-publicador-acl

## Introdução

- Descrição: API genérica para criação de ACL de descida da SAP
- Produto: acl

## Release Notes

- **1.0.0**: API genérica para criação de ACL de descida da SAP

## Funcionalidades

:white_check_mark: WeatherForecast  
ToDo: registrar os recursos da API (controllers)

## Pré-requisitos

:heavy_check_mark: [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
:heavy_check_mark: [Docker](https://aws-dev.localiza.dev/portal/devops/container/containers-windows01.html?q=docker)

## Como executar

1. Por segurança, o arquivo launchSettings.json é excluído no .gitignore. Para realizar a execução do projeto localmente, basta atualizar o arquivo /src/FI.Recebimento.Publicador.ACL.Api/Properties/launchSettings.json (local) com as informaçoes abaixo:
    ```json
    {
      "profiles": {
        "FI.Recebimento.Publicador.ACL.Api": {
          "commandName": "Project",
          "launchBrowser": true,
          "launchUrl": "swagger",
          "environmentVariables": {
            "RABBITMQ_HOST": "localhost",
            "RABBITMQ_USER": "guest",
            "RABBITMQ_PASSWORD": "guest",
            "RABBITMQ_PORT": "5672",
            "RABBITMQ_VHOST": "/",
            "ASPNETCORE_ENVIRONMENT": "Development",
            "AMBIENTE": "dev",
            "LOG_LEVEL": "Debug"
          },
          "applicationUrl": "https://localhost:5001;http://localhost:5000",
          "dotnetRunMessages": true
        }
      },
      "Docker": {
        "commandName": "Docker",
        "launchBrowser": true,
        "launchUrl": "{Scheme}://{ServiceHost}:{ServicePort}/swagger",
        "publishAllPorts": true,
        "useSSL": true
      },
      "$schema": "https://json.schemastore.org/launchsettings.json"
    }
    ```


1. Para executar o projeto localmente, deve-se criar o arquivo docker-compose.yaml no diretório raiz [ (FI.Recebimento.Publicador.ACL) ] com o conteúdo abaixo. Caso ja tenha sido criado, atualizar com as informações do plugin aplicado.
   ```docker-compose
   version: '3.2'
   services:
     rabbitmq:
       image: rabbitmq:3-management-alpine
       container_name: 'rabbitmq'
       ports:
           - 5672:5672
           - 15672:15672
       volumes:
           - ~/.docker-conf/rabbitmq/data/:/var/lib/rabbitmq/
           - ~/.docker-conf/rabbitmq/log/:/var/log/rabbitmq
       networks:
           - rabbitmq_go_net
 


   networks:
     rabbitmq_go_net:
       driver: bridge
   ````

1. Docker compose RabbitMq. (FI.Recebimento.Publicador.ACL\docker-compose.yaml). Comando para subir o docker. Na pasta onde se encontra docker-compose.yaml abrir o Powershell/Cmd/Git Bash executar o comando 
    ```shell 
    docker compose up -d 
    ```
1. Após o container Rabbitmq up podemos acessar através da url http://localhost:15672/#/queues. Username: guest | Password: guest.

1. Exemplo de Producer:
    - Será disponibilizado uma rota exemplo **(POST - /api/v1/weatherforecast/exemplorabbit)** para publicar na fila **dev.queue.dominio.subdominio.tipodado.v1.v1**.
1. Executar o projeto
    - Visual Studio: 
        1. Configurar a solução para inicializar o projeto src/FI.Recebimento.Publicador.ACL.Api/FI.Recebimento.Publicador.ACL.Api.csproj
        1. Build -> Run
    - Comando: 
        ```cmd
        dotnet dev-certs https --trust
        dotnet run --project ./src/FI.Recebimento.Publicador.ACL.Api/FI.Recebimento.Publicador.ACL.Api.csproj
        ```
1. Acessar a url: https://localhost:5001/swagger

## Cenário de negócio atual

- :office: **ToDo**: descrever o Cenário de negócio atual ou remover a seção

## Impactos por não implementação ou fora do ar

- :boom: **ToDo**: descrever os impactos por não implementação ou fora do ar

## Benefício financeiro

- :moneybag: **ToDo**: descrever o benefício financeiro da aplição ou remover a seção

## Objetivo do négocio

- :bar_chart: **ToDo**: descrever objetivo ou remover a seção

## Arquitetura

O diagrama a seguir demonstra em alto nível o fluxo principal da solução de ocorrências:

**ToDo**: incluir referência para o digrama da solução (preferencialmnte para o structurize)

## Api Gateway

- Esta API expõe suas rotas para integrações por meio de uma API Gateway.
- A tecnologia utilizada para este fim é o Sensedia.
- Acesse o link a seguir para visualizar a API na plataforma: [https://manager-localiza.sensedia.com/api-manager/#/apis/overview/**?????**](https://manager-localiza.sensedia.com/api-manager/#/apis/overview/**?????**)
- **ToDo**: verificar se realmente é utilizado Api Gateway e ajustar o link

## Dependências

- RabbitMQ: a aplicação utiliza o RabbitMQ como ferramenta de mensageria.
    - Publicar na fila: queue.dominio.subdominio.tipodado.v1 (consumidor: __??? ToDo: explicar quem consome o dado__)
    - Temos a secret rabbitmq-aws-tribo-financeiro que injeta as variáveis abaixo e os valores de acordo com o ambiente:
        * RABBITMQ_HOST: Endpoint do servidor
        * RABBITMQ_PORT": Porta de acesso ao servidor
        * RABBITMQ_USE_SSL: Se o acesso ao servidor requer o uso de SSL
    - Temos a secret rabbitmq-fi-recebimento-acl que injeta as variáveis abaixo e os valores de acordo com o ambiente:
        * RABBITMQ_VHOST: nome do VHOST que será utilizado para manter as filas
        * RABBITMQ_USER: usuário para acessr o servidor
        * RABBITMQ_PASSWORD: senha para acessr o servidor
- Uso do SDK [BuildingBlock.CorrelationId](https://localiza.visualstudio.com/Arquitetura%20-%20Bibliotecas%20Open%20Source%20Localiza/_git/buildingblockcorrelationid-lib). Gerador de CorrelationId para serviços ASP.NET.  
:white_check_mark: Recebe o cabeçalho `X-Correlation-Id` da solicitação do cliente e gera um `CorrelationId` caso não tenha sido enviado pelo cliente  
:white_check_mark: Retorna o cabeçalho `X-Correlation-Id` na resposta  
:white_check_mark: Ativa o acesso ao `CorrelationId` durante a solicitação HTTP para que você possa fazer um registro personalizado  
- Uso do SDK [Localiza.BuildingBlocks.Logging](https://localiza.visualstudio.com/Arquitetura%20-%20Bibliotecas%20Open%20Source%20Localiza/_git/buildingblocks-logging-netstandard). Gerador de Log para serviços ASP.NET.  
- **ToDo**: incluir os recursos que a aplicação utilizada como banco de dados, mensageria, cache, relatório e apis externas

## Medidas de Sucesso

- **ToDo**: Definir, como nos exemplos abaixo:
- **Confiabilidade/Resiliência**
  - A capacidade do produto de software de manter um nível de desempenho especificado, quando usado em condições especificadas Suas subcaracterísticas são:
    - **Maturidade**: Capacidade do produto de software de evitar falhas decorrentes de defei**tos no software.
    - **Tolerância a Falhas**: Capacidade do produto de software de manter um nível de desempenho especificado em casos de defeitos no software ou de violação de sua interface especificada.
    - **Recuperabilidade**: Capacidade do produto de software de restabelecer seu nível de desempenho especificado e recuperar os dados diretamente afetados no caso de uma falha.
- **Segurança**
  - Capacidade do produto de software de proteger informações e dados, de forma que pessoas ou sistemas não autorizados não possam lê-los nem modificá-los e que não seja negado o acesso às pessoas ou sistemas autorizados.
- **Disponibilidade**
  - Estar disponivel de forma aderente a necessidade do négocio.
    - A disponibilidade está relacionada ao tempo disponivel e a acessibilidade que se tem dos dados e sistemas.
- **Documentação**
  - O sistema deve conter sua documentação de maneira simples, pratica e sempre atualizada em seu repositório.
    - A documentação da solução, software e codigo fonte devem estar no repositório do projeto.

## Premissas e Restrições

- **ToDo**: incluir premissas e/ou restrições da aplicação.

---

## Referências

- [api](/arte/chapters/integracao/habilitadores/tecnicas/api-rest)
- [rest](/arte/Engenharia/habilitadores/tecnicas/rest)
- [.net](/arte/chapters/engenharia/docs/habilitadores/frameworks/netcore6)
- [entrypoints](/arte/Engenharia/base-cientifica/definicoes-arquiteturais/plataforma/entrypoint/)
- [Segurança de API](/arte/chapters/cybersec/docs/base-cientifica/seguranca-rede-comunicacao/protecao-aplicativos/api/)
- [OWASP API Security](/arte/architectures/tribo-cyber-seguranca/docs/appsec/owasp_rest_security/)