FROM microsoft/dotnet AS builder
COPY . src
WORKDIR src
RUN dotnet restore
RUN dotnet publish -c release

FROM microsoft/dotnet
COPY --from=builder src/bin/release/netcoreapp2.0/publish app
WORKDIR app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "echoserver.dll"]