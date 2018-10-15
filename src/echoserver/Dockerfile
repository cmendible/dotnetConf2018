FROM microsoft/dotnet:2.1-sdk AS builder
COPY . src
WORKDIR src
RUN dotnet restore
RUN dotnet publish -c release

FROM microsoft/dotnet:2.1-aspnetcore-runtime
COPY --from=builder src/bin/release/netcoreapp2.1/publish app
WORKDIR app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "echoserver.dll"]