FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Curso.Udemy/Curso.Udemy.csproj Curso.Udemy/
RUN dotnet restore Curso.Udemy/Curso.Udemy.csproj
COPY . .
WORKDIR /src/Curso.Udemy
RUN dotnet build Curso.Udemy.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Curso.Udemy.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Curso.Udemy.dll"]
