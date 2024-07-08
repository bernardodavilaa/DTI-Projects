# recebimento-integracao-publicador-acl

![RabbitMQ](docs/imagens/RabbitMQ.png)
![DotNet](docs/imagens/DotNet.png)
![SonarCloud](docs/imagens/SonarCloud.png)
![DataDog](docs/imagens/DataDog.png)

- Descrição: API genérica para criação de ACL de descida da SAP
- Produto: acl

## Pré-requisitos

:heavy_check_mark: [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
:heavy_check_mark: [Docker]

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
- **ToDo**: incluir os recursos que a aplicação utilizada como banco de dados, mensageria, cache, relatório e apis externas

## Referências
---

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops).  
You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)