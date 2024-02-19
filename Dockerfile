FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
RUN apt-get update && apt-get -qq install clang zlib1g-dev
WORKDIR /source

COPY . ./
RUN dotnet restore -r linux-x64 rinha-2024-q1.sln

COPY . .
RUN dotnet publish -r linux-x64 -o /app api-rinha-2024-q1/api-rinha-2024-q1.csproj
RUN rm /app/*.dbg /app/*.Development.json

FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
ENTRYPOINT ["./api-rinha-2024-q1"]