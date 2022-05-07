
cd C:\

F:

cd "F:\X\TD\SRC\turmerik\dotnet\Turmerik.LocalDiskExplorer.Blazor.App\wwwroot"

cd "F:\X\TD\SRC\turmerik\dotnet\Turmerik.OneDriveExplorer.Blazor.App\wwwroot"

cd "F:\X\TD\SRC\turmerik\dotnet\Turmerik.MusicalKeyboard.Blazor.App\wwwroot"


cd js

mklink /J common "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\js"

cd bootstrap

mklink /h bootstrap.bundle.min.js "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\bootstrap\js\bootstrap.bundle.min.js"

mklink /h bootstrap.bundle.min.js.map "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\bootstrap\js\bootstrap.bundle.min.js.map"


cd ../../css

mklink /J common "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\css"

cd bootstrap

mklink /h bootstrap.min.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\bootstrap\css\bootstrap.min.css"

mklink /h bootstrap.min.css.map "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\bootstrap\css\bootstrap.min.css.map"

rem cd ../fontawesome/css
rem 
rem mklink /h fontawesome.all.min.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\fontawesome\css\fontawesome.all.min.css"
rem 
rem mklink /h fontawesome.min.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\fontawesome\css\fontawesome.min.css"

cd ../

mklink /J webfonts "F:\X\TD\SRC\turmerik\dotnet\Turmerik.AspNetCore\wwwroot\fontawesome\webfonts"


cd ../../Shared

mklink /h MainLayout.razor.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.Blazor.Core\Pages\Shared\MainLayoutBase.css"


cd ../Pages

mklink /h Files.razor.css "F:\X\TD\SRC\turmerik\dotnet\Turmerik.Blazor.Core\Pages\FilesBase.css"

