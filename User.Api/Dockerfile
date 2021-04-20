FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["../User.Domain/User.Domain.csproj", "User.Domain/"]
COPY ["../User.Application/User.Application.csproj", "User.Application/"]
COPY ["../User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
COPY ["User.Api/User.Api.csproj", "User.Api/"]
RUN dotnet restore "User.Domain/User.Domain.csproj"
RUN dotnet restore "User.Application/User.Application.csproj"
RUN dotnet restore "User.Infrastructure/User.Infrastructure.csproj"
RUN dotnet restore "User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]
