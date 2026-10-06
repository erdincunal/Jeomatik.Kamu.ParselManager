$ErrorActionPreference = 'Stop'
$taskProjectRoot = Split-Path $PSScriptRoot -Parent
$taskVsWhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$taskMsBuild = & $taskVsWhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (!$taskMsBuild) { throw 'Visual Studio MSBuild bulunamadı.' }
Push-Location $taskProjectRoot
try {
    & $taskMsBuild ParselManager.csproj /t:Build /p:Configuration=Release /p:BuildProjectReferences=false /verbosity:minimal /nologo
    if ($LASTEXITCODE -ne 0) { throw 'Manager derlemesi başarısız.' }
    & "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /target:exe /out:bin\Release\RegressionTests.exe /reference:bin\Release\Kamu.ParselManager.dll /reference:bin\Release\Kamu.Data.dll /reference:bin\Release\Kamu.Object.dll /reference:System.Windows.Forms.dll /reference:System.Drawing.dll Tests\RegressionTests.cs
    if ($LASTEXITCODE -ne 0) { throw 'Regresyon testleri derlenemedi.' }
    & .\bin\Release\RegressionTests.exe
    if ($LASTEXITCODE -ne 0) { throw 'Regresyon testleri başarısız.' }
}
finally { Pop-Location }
