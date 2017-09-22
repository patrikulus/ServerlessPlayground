$lambdaDir = Join-Path $PSScriptRoot ".\LambdaFunctions"
$functions = Get-ChildItem $lambdaDir -Exclude "*.Tests"

foreach ($function in $functions) {
    cd $function
    dotnet lambda deploy-function 
}