# STAGE 1: BUILD
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy file csproj dulu (cache restore)
COPY ["Mini Project Kredit.csproj", "./"]
RUN dotnet restore "./Mini Project Kredit.csproj" -v minimal

# Copy sisanya
COPY . .

# Publish
RUN dotnet publish "./Mini Project Kredit.csproj" -c Release -o /app/publish /p:UseAppHost=false

# STAGE 2: RUNTIME
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Ganti sesuai assembly kamu (biasanya sama dengan nama csproj tanpa .csproj, tapi cek di bin output)
ENTRYPOINT ["dotnet", "Mini Project Kredit.dll"]
