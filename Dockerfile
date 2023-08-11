# Use a Red Hat UBI base image for .NET
FROM registry.access.redhat.com/ubi8/dotnet-70-runtime

ADD HelloWorldApp/bin/Release/net7.0/publish/ .

CMD ["dotnet", "HelloWorldApp.dll"]
