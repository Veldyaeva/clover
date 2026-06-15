param(
    [string]$RootPath = (Join-Path $PSScriptRoot "..\SplashImages"),
    [int]$JpegQuality = 85,
    [int]$MinSizeBytes = 307200
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Drawing

function Get-EncoderInfo([string]$mimeType) {
    [System.Drawing.Imaging.ImageCodecInfo]::GetImageEncoders() |
        Where-Object { $_.MimeType -eq $mimeType } |
        Select-Object -First 1
}

$jpegEncoder = Get-EncoderInfo "image/jpeg"
if (-not $jpegEncoder) {
    throw "JPEG encoder not found."
}

$encoderParams = New-Object System.Drawing.Imaging.EncoderParameters 1
$encoderParams.Param[0] = New-Object System.Drawing.Imaging.EncoderParameter(
    [System.Drawing.Imaging.Encoder]::Quality,
    [long]$JpegQuality
)

$converted = 0
$skipped = 0
$beforeBytes = 0
$afterBytes = 0

Get-ChildItem -Path $RootPath -Recurse -File | ForEach-Object {
    $file = $_
    $extension = $file.Extension.ToLowerInvariant()

    if ($extension -eq ".gif" -or $extension -eq ".jpg" -or $extension -eq ".jpeg") {
        $skipped++
        return
    }

    if ($file.Length -lt $MinSizeBytes) {
        $skipped++
        return
    }

    if ($extension -ne ".png" -and $extension -ne ".bmp") {
        $skipped++
        return
    }

    $beforeBytes += $file.Length
    $targetPath = [System.IO.Path]::ChangeExtension($file.FullName, ".jpg")

    $image = $null
    $stream = $null
    try {
        $stream = [System.IO.File]::OpenRead($file.FullName)
        $image = [System.Drawing.Image]::FromStream($stream)
        $image.Save($targetPath, $jpegEncoder, $encoderParams)
        $afterBytes += (Get-Item $targetPath).Length
        $converted++
    }
    finally {
        if ($image) {
            $image.Dispose()
        }
        if ($stream) {
            $stream.Dispose()
        }
    }

    Remove-Item $file.FullName -Force
}

Write-Host "Converted: $converted"
Write-Host "Skipped: $skipped"
if ($converted -gt 0) {
    $savedMb = [math]::Round(($beforeBytes - $afterBytes) / 1MB, 1)
    Write-Host "Saved: $savedMb MB"
}
