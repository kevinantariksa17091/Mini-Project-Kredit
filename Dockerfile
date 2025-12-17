# build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# copy csproj and restore
COPY ["Mini_Project_Kredit/Mini_Project_Kredit.csproj", "Mini_Project_Kredit/"]
RUN dotnet restore "Mini_Project_Kredit/Mini_Project_Kredit.csproj"

# copy the rest and publish
COPY . .
WORKDIR "/src/Mini_Project_Kredit"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# run on http inside container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Mini_Project_Kredit.dll"]
