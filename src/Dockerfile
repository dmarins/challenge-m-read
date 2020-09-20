#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["M.Challenge.Read.Api/M.Challenge.Read.Api.csproj", "M.Challenge.Read.Api/"]
COPY ["M.Challenge.Read.Infrastructure/M.Challenge.Read.Infrastructure.csproj", "M.Challenge.Read.Infrastructure/"]
COPY ["M.Challenge.Read.Domain/M.Challenge.Read.Domain.csproj", "M.Challenge.Read.Domain/"]
COPY ["M.Challenge.Read.Application/M.Challenge.Read.Application.csproj", "M.Challenge.Read.Application/"]
RUN dotnet restore "M.Challenge.Read.Api/M.Challenge.Read.Api.csproj"
COPY . .
WORKDIR "/src/M.Challenge.Read.Api"
RUN dotnet build "M.Challenge.Read.Api.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "M.Challenge.Read.Api.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "M.Challenge.Read.Api.dll", "--environment=Production"]