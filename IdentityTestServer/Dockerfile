FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80 5000

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY IdentityTestServer.sln ./
COPY IdentityTestServer/IdentityTestServer.csproj IdentityTestServer/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/IdentityTestServer
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityTestServer.dll"]
