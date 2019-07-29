FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic

WORKDIR /app
COPY ManheimData/. .
RUN dotnet restore
RUN dotnet build

RUN apt-get update
RUN apt-get install -y --allow-unauthenticated --fix-missing libc6-dev libgdiplus libx11-dev
RUN rm -rf /var/lib/apt/lists/*

RUN dotnet run