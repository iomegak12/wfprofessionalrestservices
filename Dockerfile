FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

COPY . /app

WORKDIR /app

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

COPY --from=build /app/WebApplication1/out ./

ENTRYPOINT ["dotnet", "CRMSystemHostingv2.dll"]