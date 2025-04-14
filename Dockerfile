FROM alpine/git as version
WORKDIR /src
COPY . /src
RUN echo $(git describe --tags --always 2>/dev/null) > /version

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
COPY . /build
COPY --from=version /version /build/version
WORKDIR /build
RUN apt-get update -yq ;\
	apt-get install curl gnupg -yq ;\
	curl -sL https://deb.nodesource.com/setup_20.x | bash - ;\
	apt-get install -y nodejs
	
RUN sed -i -e "s/<Version>0-develop<\/Version>/<Version>$(cat version | cut -c2- )<\/Version>/g" src/Heimdallr.Host/Heimdallr.Host.csproj;\
    dotnet restore -s https://api.nuget.org/v3/index.json; \
    dotnet build --no-restore -c Release; \    
    dotnet publish ./src/Heimdallr.Host/Heimdallr.Host.csproj -c Release -o /app --no-build; \
    dotnet nuget locals http-cache --clear;\
    dotnet nuget locals temp --clear


######## Heimdallr Host
FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim as Heimdallr
COPY --from=build /app /Heimdallr
WORKDIR /Heimdallr
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80
ENTRYPOINT ["dotnet", "Heimdallr.Host.dll"]
