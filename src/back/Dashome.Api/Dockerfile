FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Dashome/Dashome.csproj", "Dashome/"]
RUN dotnet restore "Dashome/Dashome.csproj"
COPY . .
WORKDIR "/src/Dashome"
RUN dotnet build "Dashome.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dashome.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dashome.dll"]
