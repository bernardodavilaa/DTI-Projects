# cdcworker-agaddressen-pub

## Introdução

- Descrição: Solução para captura de registros em tabelas e publicação em tópicos corporativos
- Produto: cdcworker-agaddressen-pub

## Release Notes

- **1.0.0**: Solução para captura de registros em tabelas e publicação em tópicos corporativos

## Funcionalidades

:white_check_mark: **ToDo**: descrever as funcionalidades do worker

## Pré-requisitos

:heavy_check_mark: [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
:heavy_check_mark: [Docker](https://aws-dev.localiza.dev/portal/devops/container/containers-windows01.html?q=docker)

## Como executar

1. Realizar clone do [projeto](https://dev.azure.com/localiza/Tribo%20Tech%20Products/_git/cdcworker-agaddressen-pub)
    ```cmd
    git clone https://localiza@dev.azure.com/localiza//_git/cdcworker-agaddressen-pub
    ```
1. Adicionar as variáveis de ambiente no arquivo /src/Labs.IntegracoesDigitais.Cdc.Worker/Properties/launchSettings.json
    ```json
    {
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "LOCALIZA_ENVIRONMENT": "dev"
      }
    }
    ```
1. Executar o projeto
    - Visual Studio: 
        1. Configurar a solução para inicializar o projeto src/Labs.IntegracoesDigitais.Cdc.Worker/Labs.IntegracoesDigitais.Cdc.Worker.csproj
        1. Build -> Run
    - Comando: 
        ```cmd
        dotnet run --project ./src/Labs.IntegracoesDigitais.Cdc.Worker/Labs.IntegracoesDigitais.Cdc.Worker.csproj
        ```

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

## Dependências

- Uso do SDK [BuildingBlock.CorrelationId](https://localiza.visualstudio.com/Arquitetura%20-%20Bibliotecas%20Open%20Source%20Localiza/_git/buildingblockcorrelationid-lib). Gerador de CorrelationId para serviços ASP.NET.  
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

- [worker](/arte/Engenharia/habilitadores/tecnicas/workers)
- [.net](/arte/chapters/engenharia/docs/habilitadores/frameworks/netcore6)
- [entrypoints](/arte/Engenharia/base-cientifica/definicoes-arquiteturais/plataforma/entrypoint/)