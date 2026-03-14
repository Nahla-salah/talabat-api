# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات الـ csproj لكل الطبقات لعمل Restore
COPY ["E-CommerceCorevera.Api/E-CommerceCorevera.Api.csproj", "E-CommerceCorevera.Api/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["ServicesAbstraction/ServicesAbstraction.csproj", "ServicesAbstraction/"]
COPY ["ServicesImplementation/ServicesImplementation.csproj", "ServicesImplementation/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "E-CommerceCorevera.Api/E-CommerceCorevera.Api.csproj"

# نسخ باقي الكود وعمل Build
COPY . .
WORKDIR "/src/E-CommerceCorevera.Api"
RUN dotnet build "E-CommerceCorevera.Api.csproj" -c Release -o /app/build

# عمل Publish
FROM build AS publish
RUN dotnet publish "E-CommerceCorevera.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# تشغيل التطبيق النهائي
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-CommerceCorevera.Api.dll"]