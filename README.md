# fake_app
- `dotnet` is required to run/build this project
- using .NET 7.0

## Usage
Change directory into project folder
Run console app: `dotnet run [count]`
Build console app .exe: `dotnet publish -c Release -r win-x64 --self-contained true`
Build console app bin: `dotnet publish -c Release -r linux-x64 --self-contained true`

## Notes
- `--self-contained true` means you don't need .net runtime to be installed to start the app