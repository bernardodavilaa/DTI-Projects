FROM registryll.azurecr.io/ti/container/images/netcore8/x-build:stable as build
ARG ARTIFACTSTOKENNUGET
RUN dotnet nuget add source https://localiza.pkgs.visualstudio.com/_packaging/LocalizaAtivos%40Local/nuget/v3/index.json -n LocalizaAtivos -u myUsername -p $ARTIFACTSTOKENNUGET --store-password-in-clear-text
WORKDIR /src
COPY src .
WORKDIR /src/Labs.IntegracoesDigitais.Cdc.Worker
RUN dotnet publish Labs.IntegracoesDigitais.Cdc.Worker.csproj -c Release -o /app/publish

FROM registryll.azurecr.io/ti/container/images/netcore8/x-runtime/debian:stable
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_HTTP_PORTS=80 
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Labs.IntegracoesDigitais.Cdc.Worker.dll"]