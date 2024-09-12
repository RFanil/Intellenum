param($verbosity = "minimal", $buildConfig = "Release") #quite|q, minimal|m, normal|n, detailed|d

$artifacts = ".\artifacts"
$localPackages = ".\local-global-packages"

function WriteStage([string]$message)
{
    Write-Host "############################################" -ForegroundColor Cyan
    Write-Host "**** " $message -ForegroundColor Cyan
    Write-Host "############################################" -ForegroundColor Cyan
    Write-Output ""
}

function Get999VersionWithUniquePatch()
{
    $date1 = Get-Date("2022-10-17");
    $date2 = Get-Date;
    $patch = [int64]((New-TimeSpan -Start $date1 -End $date2)).TotalSeconds
    return "999.9." + $patch;
}

<#
.SYNOPSIS
  Taken from psake https://github.com/psake/psake
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

WriteStage("Building $buildConfig version of Intellenum...")

if(Test-Path $artifacts) { Remove-Item $artifacts -Force -Recurse }

New-Item -Path $artifacts -ItemType Directory

New-Item -Path $localPackages -ItemType Directory -ErrorAction SilentlyContinue

if(Test-Path $localPackages) { Remove-Item $localPackages\intellenum.* -Force -ErrorAction SilentlyContinue }

WriteStage("Cleaning, restoring, and building $buildConfig version of Intellenum...")

WriteStage("... clean ...")
exec { & dotnet clean Intellenum.sln -c $buildConfig --verbosity $verbosity}

WriteStage("... restore ...")
exec { & dotnet restore Intellenum.sln --no-cache --verbosity $verbosity }

exec { & dotnet build Intellenum.sln -c $buildConfig -p Thorough=true --no-restore --verbosity $verbosity}

# run the analyzer tests
WriteStage("Running analyzer tests...")
exec { & dotnet test tests/AnalyzerTests/AnalyzerTests.csproj -c $buildConfig --no-build -l trx -l "GitHubActions;report-warnings=false" --verbosity $verbosity }

WriteStage("Running unit tests...")
exec { & dotnet test tests/Intellenum.Tests/Intellenum.Tests.csproj -c $buildConfig --no-build -l trx -l "GitHubActions;report-warnings=false" --verbosity $verbosity }
    
# Run the end to end tests. The tests can't have project references to Intellenum. This is because, in Visual Studio, 
# it causes conflicts caused by the difference in runtime; VS uses netstandard2.0 to load and run the analyzers, but the 
# test project uses a variety of runtimes. So, it uses NuGet to reference the Intellenum analyzer. To do this, this script first 
# builds and packs Intellenum using a ridiculously high version number and then restores the tests NuGet dependencies to use that
# package. This will allow you run and debug these tests in VS, but to use any new code changes in the analyzer, you 
# need to rerun this script to force a refresh of the package. 

WriteStage("Building NuGet for local version of Intellenum that will be used to run end to end tests and samples...")

$version = Get999VersionWithUniquePatch

# Build **just** Intellenum first to generate the NuGet package. In the next step,
# we'll build the consumers of package, namely the e2e tests and samples projects.

# **NOTE** - we don't want these 999.9.9.x packages ending up in %userprofile%\.nuget\packages because it'll polute it.

exec { & dotnet restore Intellenum.sln --packages $localPackages --no-cache --verbosity $verbosity }
exec { & dotnet build Intellenum.sln -c Debug --no-restore --verbosity $verbosity}
exec { & dotnet pack ./src/Intellenum.Pack.csproj -c Release -o:$localPackages /p:ForceVersion=$version --include-symbols --version-suffix:dev --no-restore --verbosity $verbosity }

WriteStage("Cleaning and building consumers (tests and samples)")

exec { & dotnet restore Consumers.sln --no-cache --verbosity $verbosity }
exec { & dotnet clean Consumers.sln -c $buildConfig --verbosity $verbosity}


# Restore the project using the custom config file, restoring packages to a local folder
exec { & dotnet restore Consumers.sln -p UseLocallyBuiltPackage=true --force --no-cache --packages $localPackages --configfile ./nuget.private.config --verbosity $verbosity }

exec { & dotnet build Consumers.sln -c Debug --no-restore --verbosity $verbosity }

WriteStage("Running end to end tests with the local version of the NuGet package:" +$version)

exec { & dotnet test ./tests/ConsumerTests -c Debug --no-build --no-restore --verbosity $verbosity }


WriteStage("Building samples using the local version of the NuGet package...")


exec { & dotnet run --project samples/Intellenum.Examples/Intellenum.Examples.csproj -c Debug --no-build --no-restore }


WriteStage("Finally, packing the $buildConfig version into " + $artifacts)

exec { & dotnet pack src/Intellenum.Pack.csproj -c $buildConfig -o $artifacts --no-build --verbosity $verbosity }

WriteStage("Done! Package generated at " + $artifacts)

