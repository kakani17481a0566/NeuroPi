@echo off
setlocal EnableDelayedExpansion

:menu
cls
echo ================================================
echo            GIT TOOL - INTERACTIVE MENU          
echo ================================================
echo 1. List all local branches
echo 2. List all remote branches
echo 3. Checkout a branch
echo 4. Merge a branch into current
echo 5. Push current branch to origin
echo 6. Pull latest from origin
echo 7. Show git status
echo 8. Commit and push local changes to current branch
echo 9. Exit
echo ================================================
set /p choice="Select an option (1-9): "

if "%choice%"=="1" goto list_local
if "%choice%"=="2" goto list_remote
if "%choice%"=="3" goto checkout
if "%choice%"=="4" goto merge
if "%choice%"=="5" goto push
if "%choice%"=="6" goto pull
if "%choice%"=="7" goto status
if "%choice%"=="8" goto commit_push
if "%choice%"=="9" goto end
goto menu

:list_local
echo.
echo === Local Branches ===
git branch
goto pause_return

:list_remote
echo.
echo === Remote Branches ===
git branch -r
goto pause_return

:checkout
echo.
set /p branch="Enter the branch name to checkout: "
git checkout %branch% || goto error
goto pause_return

:merge
echo.
set /p mergeBranch="Enter the branch to merge into current: "
git merge %mergeBranch% || goto error
goto pause_return

:push
echo.
git branch --show-current > tmp_branch.txt
set /p currentBranch=<tmp_branch.txt
del tmp_branch.txt
echo Pushing current branch (%currentBranch%) to origin...
git push origin %currentBranch% || goto error
goto pause_return

:pull
echo.
git pull || goto error
goto pause_return

:status
echo.
git status
goto pause_return

:commit_push
echo.
git status --porcelain > tmp_status.txt
setlocal EnableDelayedExpansion
set msg=Updated:
for /f "tokens=2 delims= " %%f in (tmp_status.txt) do (
    set msg=!msg! %%~nxf
)
if not defined msg (
    echo No changes to commit.
    del tmp_status.txt
    goto pause_return
)
echo Adding all changes...
git add . || goto error
echo Committing with message: "!msg!"
git commit -m "!msg!" || goto error
del tmp_status.txt
goto push

:error
echo.
echo === ERROR: Operation failed. Please check the above output. ===
goto pause_return

:pause_return
echo.
pause
goto menu

:end
echo.
echo Exiting Git Tool...
exit /b
