FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["treni_contact/treni_contact.csproj", "treni_contact/"]
RUN dotnet restore "treni_contact/treni_contact.csproj"
COPY . .
WORKDIR "/src/treni_contact"
RUN dotnet build "treni_contact.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "treni_contact.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "treni_contact.dll"]
