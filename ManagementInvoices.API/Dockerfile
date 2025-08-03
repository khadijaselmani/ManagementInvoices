# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln ./
COPY ManagementInvoices.API/*.csproj ManagementInvoices.API/
COPY ManagementInvoices.Application/*.csproj ManagementInvoices.Application/
COPY ManagementInvoices.Domain/*.csproj ManagementInvoices.Domain/
COPY ManagementInvoices.Infrastructure/*.csproj ManagementInvoices.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY . .

# Publish the app to the out directory
RUN dotnet publish ManagementInvoices.API/ManagementInvoices.API.csproj -c Release -o out

# Use runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ManagementInvoices.API.dll"]
