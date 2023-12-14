param(
    [Parameter(Position = 0, Mandatory = $True)]
    [ValidateSet(
        "run",
        "build",
        "test"
    )]
    [string]$Command,

    [Parameter(Position=1, ValueFromRemainingArguments=$true)]
    $Rest
)

function Invoke-Run {
    Push-Location $PSScriptRoot/src/AOC23Console
    Invoke-Expression "dotnet run -- $Rest"
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
