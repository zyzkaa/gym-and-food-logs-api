$migrationName = "AutoMigration"

dotnet ef migrations add $migrationName

dotnet ef database update