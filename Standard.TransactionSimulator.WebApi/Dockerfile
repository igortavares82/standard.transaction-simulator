#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Standard.TransactionSimulator.WebApi/Standard.TransactionSimulator.WebApi.csproj", "Standard.TransactionSimulator.WebApi/"]
COPY ["Standard.TransactionSimulator.Applicator/Standard.TransactionSimulator.Applicator.csproj", "Standard.TransactionSimulator.Applicator/"]
RUN dotnet restore "Standard.TransactionSimulator.WebApi/Standard.TransactionSimulator.WebApi.csproj"
COPY . .
WORKDIR "/src/Standard.TransactionSimulator.WebApi"
RUN dotnet build "Standard.TransactionSimulator.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Standard.TransactionSimulator.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Standard.TransactionSimulator.WebApi.dll"]
