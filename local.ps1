param(
    [Parameter(Position=0, Mandatory=$True)]
    [ValidateSet("run", "test")]
    [string]$Command
)

function Invoke-Run {
    pushd $PSScriptRoot/src/AOC23Console
        dotnet run
    popd
}

function Invoke-Test() {
    dotnet test $PSScriptRoot/AOC23.sln
}

switch ($Command) {
    "run" { Invoke-Run }
    "test" { Invoke-Test }
}