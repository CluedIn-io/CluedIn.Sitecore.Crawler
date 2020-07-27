# TODO This needs to be replaced by using nuget packages.
# In the meantime it helps coping the DLLs on to cluedin

$root = "$PSScriptRoot"
$configuration="Debug"
$destination = "$root\..\..\cluedin\.artifacts\Cluedin\ServerComponent"
$projects = @("Crawling","Provider")

$patterns = @(
	"*Sitecore.*",
	"AutoMapper.*",
	"System.Collections*",
	"System.Diagnostics.*",
	"System.Interactive.Async*",
	"System.Runtime*",
	"System.Threading.*",
	"System.ValueTuple*"
	)

$projects | % {
	$project = $_
	$bin="$root\src\Sitecore.$project\bin\$configuration"
	$set = New-Object System.Collections.Generic.HashSet[System.IO.DirectoryInfo]
	
	$patterns | % {
		$pattern = $_
		gci "$bin\$pattern" | % {
			[void] $set.Add($_.FullName)
		}
	}
	$set | % {
		$file = $_
		Write-Output "Copying $($_.FullName)"
		Copy-Item $file $destination
	}
}
