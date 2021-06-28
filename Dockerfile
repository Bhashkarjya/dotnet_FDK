FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as build-image
WORKDIR /home/app
COPY . .
# RUN dotnet test ./Tests/Tests.csproj
RUN dotnet restore
RUN dotnet publish ./FDK.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /publish
COPY --from=build-image /publish .
ENTRYPOINT ["dotnet", "FDK.dll"]
