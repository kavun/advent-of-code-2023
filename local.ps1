param(
    [Parameter(Position = 0, Mandatory = $True)]
    [ValidateSet(
        "run",
        "build",
        "test"
    )]
    [string]$Command
)

function Invoke-Run {
    Push-Location $PSScriptRoot/src/AOC23Console
    & dotnet run
    Pop-Location
}

function Invoke-Test() {
    & dotnet test $PSScriptRoot/AOC23.sln
}

function Invoke-Build() {
    & dotnet build $PSScriptRoot/AOC23.sln
}

switch ($Command) {
    "run" { Invoke-Run }
    "test" { Invoke-Test }
    "build" { Invoke-Build }
}