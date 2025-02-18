# Utiliza una imagen base de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura las dependencias
COPY ["BrokerAtm.csproj", "./"]
RUN dotnet restore "./BrokerAtm.csproj"

# Copia el resto de los archivos y compila la aplicación
COPY . .
RUN dotnet build "BrokerAtm.csproj" -c Release -o /app/build

# Publica la aplicación
RUN dotnet publish "BrokerAtm.csproj" -c Release -o /app/publish

# Utiliza una imagen base de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto en el que la aplicación escuchará
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "BrokerAtm.dll"]