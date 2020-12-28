#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Armut.Messaging.Api/Armut.Messaging.Api.csproj", "src/Armut.Messaging.Api/"]
COPY ["src/Armut.Messaging.Infrastructure/Armut.Messaging.Infrastructure.csproj", "src/Armut.Messaging.Infrastructure/"]
COPY ["src/Armut.Messaging.Application/Armut.Messaging.Application.csproj", "src/Armut.Messaging.Application/"]
COPY ["src/Armut.Messaging.Core/Armut.Messaging.Core.csproj", "src/Armut.Messaging.Core/"]
RUN dotnet restore "src/Armut.Messaging.Api/Armut.Messaging.Api.csproj"
COPY . .
WORKDIR "/src/src/Armut.Messaging.Api"
RUN dotnet build "Armut.Messaging.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Armut.Messaging.Api.csproj" -c Release -o /app/publish

ENV TZ=Europe/Istanbul
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Armut.Messaging.Api.dll"]