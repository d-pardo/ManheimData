FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic

WORKDIR /app
COPY ManheimData/. .
RUN dotnet restore
RUN dotnet build
RUN dotnet publish -c Release -o out

ARG FILES_FOLDER
ENV FILES_FOLDER=$FILES_FOLDER

RUN apt-get update
RUN apt-get install -y --allow-unauthenticated --fix-missing libc6-dev libgdiplus libx11-dev
RUN rm -rf /var/lib/apt/lists/*

ENTRYPOINT [ "dotnet", "out/ManheimData.dll" ] 