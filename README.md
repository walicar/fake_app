# fake_app
- `dotnet` is required to run/build this project
- using .NET 7.0
- can take in a `.json` file as input
- writes out a `.txt` file

## Usage
Change directory into project folder
- Run console app: `dotnet run <count> [-f appinput.json]`
    - example: `dotnet run 2 -- -f appinput.json`
- Build console app .exe: `dotnet publish -c Release -r win-x64 --self-contained true`
- Build console app bin: `dotnet publish -c Release -r linux-x64 --self-contained true`
## Notes
- `--self-contained true` means you don't need .net runtime to be installed to start the app
- `input.json` must follow the properties of the `AppInput` class