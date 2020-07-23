rd /s /q "DataAccess.ContextSetup/Migrations"
dotnet ef database drop --context DataContext --project DataAccess.ContextSetup\DataAccess.ContextSetup.csproj
dotnet ef migrations add InitialEntity --context DataContext --project DataAccess.ContextSetup\DataAccess.ContextSetup.csproj
dotnet ef database update --context DataContext --project DataAccess.ContextSetup\DataAccess.ContextSetup.csproj
PAUSE