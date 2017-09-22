function Get-FunctionName([string]$path) {
    $splitted = $path.Split('\')
    return $splitted[$splitted.Count -1]
}

$lambdaDir = Join-Path $PSScriptRoot ".\LambdaFunctions"
$functions = Get-ChildItem $lambdaDir -Exclude "*.Tests"

foreach ($function in $functions) {
    cd $function
    $functionName = Get-FunctionName($function)
    dotnet lambda deploy-function --name $functionName
}