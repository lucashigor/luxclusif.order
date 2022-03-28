#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["luxclusif.order.webapi/luxclusif.order.webapi.csproj", "luxclusif.order.webapi/"]
RUN dotnet restore "luxclusif.order.webapi/luxclusif.order.webapi.csproj"
COPY . .
WORKDIR "/src/luxclusif.order.webapi"
RUN dotnet build "luxclusif.order.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "luxclusif.order.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "luxclusif.order.webapi.dll"]