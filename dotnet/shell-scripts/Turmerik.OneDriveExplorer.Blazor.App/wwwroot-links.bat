
cd C:\
F:
cd "F:\X\TD\SRC\turmerik\dotnet\Turmerik.OneDriveExplorer.Blazor.App\wwwroot"

cd js
mklink /J common "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\js"

cd ../css
mklink /J common "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\css"

cd ../Shared
mklink /h MainLayout.razor.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.Blazor.Core\Pages\Shared\MainLayoutBase.css"

cd ../Pages
mklink /h Files.razor.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.Blazor.Core\Pages\FilesBase.css"

