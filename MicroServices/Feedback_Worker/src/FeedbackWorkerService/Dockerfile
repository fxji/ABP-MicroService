FROM mcr.microsoft.com/dotnet/aspnet:7.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/FeedbackWorkerService/FeedbackWorkerService.csproj", "src/FeedbackWorkerService/"]
RUN dotnet restore "src\FeedbackWorkerService\FeedbackWorkerService.csproj"
COPY . .
WORKDIR "/src/src/FeedbackWorkerService"
RUN dotnet build "FeedbackWorkerService.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "FeedbackWorkerService.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeedbackWorkerService.dll"]
