@echo off
echo ========================================
echo   GitHub Artifacts Cleanup Tool
echo ========================================
echo.
echo This tool will delete ALL artifacts from:
echo Repository: kakani17481a0566/NeuroPi
echo.
echo ========================================
echo   Get Your GitHub Token First:
echo ========================================
echo 1. Open: https://github.com/settings/tokens
echo 2. Click "Generate new token" -^> "Classic"
echo 3. Name it: "Delete Artifacts"
echo 4. Check ONLY: repo (Full control)
echo 5. Click "Generate token"
echo 6. Copy the token (starts with ghp_)
echo.
echo ========================================
echo.

set /p GITHUB_TOKEN="Paste your GitHub token here: "

if "%GITHUB_TOKEN%"=="" (
    echo.
    echo ERROR: No token provided!
    pause
    exit /b 1
)

echo.
echo Token received. Starting cleanup...
echo.

set REPO=kakani17481a0566/NeuroPi

powershell -NoProfile -ExecutionPolicy Bypass -Command "$token = '%GITHUB_TOKEN%'; $repo = '%REPO%'; $headers = @{ Authorization = \"token $token\"; Accept = \"application/vnd.github.v3+json\" }; Write-Host 'Fetching artifacts...' -ForegroundColor Yellow; try { $response = Invoke-RestMethod -Uri \"https://api.github.com/repos/$repo/actions/artifacts?per_page=100\" -Headers $headers; $artifacts = $response.artifacts; if ($artifacts.Count -eq 0) { Write-Host ''; Write-Host 'No artifacts found! Storage is already clean.' -ForegroundColor Green; exit 0; }; Write-Host ''; Write-Host \"Found $($artifacts.Count) artifacts. Deleting...\" -ForegroundColor Cyan; Write-Host ''; $count = 0; $success = 0; $failed = 0; foreach ($artifact in $artifacts) { $count++; Write-Host \"[$count/$($artifacts.Count)] $($artifact.name)\" -NoNewline; try { Invoke-RestMethod -Uri \"https://api.github.com/repos/$repo/actions/artifacts/$($artifact.id)\" -Method Delete -Headers $headers | Out-Null; Write-Host ' - DELETED' -ForegroundColor Green; $success++; } catch { Write-Host ' - FAILED' -ForegroundColor Red; $failed++; }; }; Write-Host ''; Write-Host '======================================' -ForegroundColor Cyan; Write-Host \"  SUCCESS: $success deleted\" -ForegroundColor Green; if ($failed -gt 0) { Write-Host \"  FAILED: $failed\" -ForegroundColor Red }; Write-Host '======================================' -ForegroundColor Cyan; } catch { Write-Host ''; Write-Host 'ERROR: Failed to connect to GitHub' -ForegroundColor Red; Write-Host \"Details: $($_.Exception.Message)\" -ForegroundColor Red; Write-Host ''; Write-Host 'Possible issues:'; Write-Host '  - Invalid token'; Write-Host '  - Token lacks repo permissions'; Write-Host '  - Network connection problem'; exit 1; }"

echo.
echo ========================================
echo   Cleanup Complete!
echo ========================================
echo.
echo You can now push your code to GitHub.
echo The workflow should succeed now!
echo.
pause
