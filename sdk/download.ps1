$url = "http://www.dev-c.com/files/ScriptHookV_SDK_1.0.617.1a.zip"
$zip = "sdk\\scripthookv.zip"
$out = "sdk\\scripthookv"
$headers = @{
    "Referer" = "http://www.dev-c.com"
}

if (!(Test-Path $out))
{
    # Create the directory
    New-Item -Path "sdk" -Name "scripthookv" -ItemType "directory"
    
    # If the SDK ZIP doesn't exists, download it
    if (!(Test-Path $zip))
    {
        Invoke-WebRequest -Uri $url -OutFile $zip -MaximumRedirection 0 -Headers $headers
    }
    
    # Extract the files
    Expand-Archive -Path $zip -DestinationPath $out
    
    # Once everything is done, do some cleanup
    Remove-Item $zip -ErrorAction Ignore
    Remove-Item "$($out)\\inc\\nativeCaller.h" -ErrorAction Ignore
    Remove-Item "$($out)\\inc\\natives.h" -ErrorAction Ignore
}
