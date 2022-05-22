start "Rating App" dotnet run --project .\WebApplication2
start "WebApi server" dotnet run --project .\RatingApp\RatingApp
start "NodeJS react" npm start --prefix .\WebApplication2\ClientApp
