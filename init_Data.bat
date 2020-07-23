dotnet ef migrations add InitialEntity --context DataContext --project DataAccess.ContextSetup\DataAccess.ContextSetup.csproj
dotnet ef database update --context DataContext --project DataAccess.ContextSetup\DataAccess.ContextSetup.csproj
PAUSE