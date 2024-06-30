# Will create a json string representation of the debug launch arguments for a specified dll file
# Usage: debug_args_dll.ps1 -dllPath <path to dll> -cwd <path to working directory> -debugargs <args to pass to "args">

param(
    [string]$dllPath,
    [string]$cwd,
    [string]$debugargs
)

# Get absolute path of the dll
$dllPath = (Resolve-Path $dllPath) -replace '\\', '/'

# Get absolute path of the working directory
$cwd = (Resolve-Path $cwd) -replace '\\', '/'

# Create the json string
$launchArgs = @"
{"type":"coreclr","request":"launch","program":"$dllPath","cwd":"$cwd","just_my_code":false,"args":$debugargs}
"@

# url encode the json string
function URLEncode {
    param([string]$String)
    $String = [System.Web.HttpUtility]::UrlEncode($String)
    $String = $String.Replace("+", "%20")
    return $String
}

$url = $launchArgs
$encodedUrl = URLEncode $url
Write-Host $encodedUrl