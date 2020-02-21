#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MeetingApp.Website/MeetingApp.Website.csproj", "MeetingApp.Website/"]
COPY ["MeetingApp.Domain/MeetingApp.Domain.csproj", "MeetingApp.Domain/"]
COPY ["MeetingApp.Models/MeetingApp.Models.csproj", "MeetingApp.Models/"]
COPY ["MeetingApp.Application/MeetingApp.Application.csproj", "MeetingApp.Application/"]
COPY ["MeetingApp.Database/MeetingApp.Database.csproj", "MeetingApp.Database/"]
RUN dotnet restore "MeetingApp.Website/MeetingApp.Website.csproj"
COPY . .
WORKDIR "/src/MeetingApp.Website"
RUN dotnet build "MeetingApp.Website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetingApp.Website.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetingApp.Website.dll"]