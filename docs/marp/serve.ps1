
$action=$args[0]


$files = Get-ChildItem ..\adr\0*.md

$allContent = Get-Content -Path "header.md" -Raw

foreach($file in $files) {
    $content = Get-Content -Path $file -Raw
    if (!($content -match "==marp==")) { Write-Host "skipping empty marp part in $file"; continue;}

    $split = ($content -split "==marp==" | select-object -index 1).Trim()
    Write-Host "Adding content from file $file"
    $allContent = $allContent + "`r`n---`r`n" + $split;
}

$footer = Get-Content -Path "footer.md" -Raw
$allContent = $allContent + "`r`n---`r`n" + $footer

Set-Content -Path adr.marp.md $allContent

if ($action -eq "pdf") {
    docker run --rm --init -v ${PWD}:/home/marp/app/ -e LANG=$LANG marpteam/marp-cli adr.marp.md --pdf
} elseif ($action -eq "serve") {
    docker run --rm --init -v ${PWD}:/home/marp/app/ -e LANG=$LANG -p 8080:8080 -p 37717:37717 marpteam/marp-cli -s .
    start http://localhost:8080/adr.marp.md
}
