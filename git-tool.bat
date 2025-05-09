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
echo 6. Push current branch to selected remote branch
echo 7. Push current branch to all remote branches
echo 8. Pull latest from origin
echo 9. Show git status
echo 10. Commit and push local changes to current branch
echo 11. Create a new branch
echo 12. Delete a local branch
echo 13. Delete a remote branch
echo 14. Stash changes
echo 15. Show git log
echo 16. Switch to the last branch
echo 17. Exit
echo ================================================
set /p choice="Select an option (1-17): "

if "%choice%"=="1" goto list_local
if "%choice%"=="2" goto list_remote
if "%choice%"=="3" goto checkout
if "%choice%"=="4" goto merge
if "%choice%"=="5" goto push
if "%choice%"=="6" goto push_selected_branch
if "%choice%"=="7" goto push_all_branches
if "%choice%"=="8" goto pull
if "%choice%"=="9" goto status
if "%choice%"=="10" goto commit_push
if "%choice%"=="11" goto create_branch
if "%choice%"=="12" goto delete_local_branch
if "%choice%"=="13" goto delete_remote_branch
if "%choice%"=="14" goto stash
if "%choice%"=="15" goto log
if "%choice%"=="16" goto last_branch
if "%choice%"=="17" goto end
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
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo Pushing current branch (%currentBranch%) to origin...
git push origin %currentBranch% || goto error
goto pause_return

:push_selected_branch
echo.
echo === Remote Branches ===
git branch -r
echo.
set /p selectedBranch="Enter the remote branch name to push the current branch to (or press Enter to select a branch from the list): "
if "%selectedBranch%"=="" (
    echo No remote branch selected. Please type the name of the remote branch.
    goto pause_return
)
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo Pushing current branch (%currentBranch%) to remote branch %selectedBranch%...
git push origin %currentBranch%:%selectedBranch% || goto error
goto pause_return

:push_all_branches
echo.
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo Pushing current branch (%currentBranch%) to all remote branches...

for /f "tokens=*" %%a in ('git branch -r') do (
    set "remoteBranch=%%a"
    call :push_to_remote !remoteBranch!
)
goto pause_return

:push_to_remote
setlocal EnableDelayedExpansion
set "branchName=%~1"
set "branchName=!branchName:origin/=!"
echo Pushing to !branchName!...
git push origin %currentBranch%:!branchName! || echo Failed to push to !branchName!
endlocal
exit /b

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
git diff --name-only > tmp_status.txt
set msg=Updated:
setlocal EnableDelayedExpansion
for /f "usebackq delims=" %%f in ("tmp_status.txt") do (
    set "file=%%~nxf"
    set "msg=!msg! !file!"
)
endlocal

if not exist tmp_status.txt (
    echo No changes to commit.
    goto pause_return
)

echo Adding all changes...
git add . || goto error

echo Committing with message: "%msg%"
git commit -m "%msg%" || goto error
del tmp_status.txt

goto push

:create_branch
echo.
set /p newBranch="Enter the name of the new branch: "
git checkout -b %newBranch% || goto error
echo Switched to new branch %newBranch%.
goto pause_return

:delete_local_branch
echo.
set /p deleteBranch="Enter the name of the local branch to delete: "
git branch -d %deleteBranch% || goto error
echo Local branch %deleteBranch% deleted.
goto pause_return

:delete_remote_branch
echo.
set /p deleteRemoteBranch="Enter the name of the remote branch to delete: "
git push origin --delete %deleteRemoteBranch% || goto error
echo Remote branch %deleteRemoteBranch% deleted.
goto pause_return

:stash
echo.
echo Stashing changes...
git stash push || goto error
echo Changes have been stashed.
goto pause_return

:log
echo.
echo Showing git log...
git log --oneline --graph --decorate --all || goto error
goto pause_return

:last_branch
echo.
echo Switching to the last branch...
git checkout - || goto error
goto pause_return

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
