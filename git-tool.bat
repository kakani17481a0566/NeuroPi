@echo off
setlocal EnableDelayedExpansion

:: Initialize ANSI color codes
for /f %%A in ('echo prompt $E ^| cmd') do set "ESC=%%A"
set "BLUE=%ESC%[94m"
set "GREEN=%ESC%[92m"
set "YELLOW=%ESC%[93m"
set "RED=%ESC%[91m"
set "CYAN=%ESC%[96m"
set "RESET=%ESC%[0m"

:menu
cls
echo ================================================
echo           %CYAN%GIT TOOL - INTERACTIVE MENU%RESET%
echo ================================================
echo  1.  Show git status
echo  2.  Show git log (last 20 commits)
echo  3.  List all local branches
echo  4.  List all remote branches
echo  5.  Switch to the last branch
echo  6.  Checkout a branch
echo  7.  Create a new branch
echo  8.  Delete a local branch
echo  9.  Delete a remote branch
echo 10.  Merge a branch into current
echo 11.  Pull latest from origin
echo 12.  Pull from selected remote branch
echo 13.  Push current branch to origin
echo 14.  Push to selected remote branch
echo 15.  Push to all remote branches
echo 16.  Commit and push changes (with filenames)
echo 17.  Stash changes
echo 18.  Delete local changes (reset working directory)
echo 19.  Create Pull Request
echo 20.  Backup master/main branch
echo 21.  Exit
echo ================================================
set /p choice="%YELLOW%Select an option (1-21): %RESET%"

if "%choice%"=="1" goto status
if "%choice%"=="2" goto log
if "%choice%"=="3" goto list_local
if "%choice%"=="4" goto list_remote
if "%choice%"=="5" goto last_branch
if "%choice%"=="6" goto checkout
if "%choice%"=="7" goto create_branch
if "%choice%"=="8" goto delete_local_branch
if "%choice%"=="9" goto delete_remote_branch
if "%choice%"=="10" goto merge
if "%choice%"=="11" goto pull_origin
if "%choice%"=="12" goto pull_remote_branch
if "%choice%"=="13" goto push
if "%choice%"=="14" goto push_selected_branch
if "%choice%"=="15" goto push_all_branches
if "%choice%"=="16" goto commit_push
if "%choice%"=="17" goto stash
if "%choice%"=="18" goto delete_local_changes
if "%choice%"=="19" goto create_pull_request
if "%choice%"=="20" goto backup_master
if "%choice%"=="21" goto end
goto menu

:status
echo.
git status
goto pause_return

:log
echo.
git log --oneline --graph --decorate --all -n 20
goto pause_return

:list_local
echo.
git branch
goto pause_return

:list_remote
echo.
git branch -r
goto pause_return

:last_branch
echo.
git checkout - || goto error
goto pause_return

:checkout
echo.
set /p branch="Enter branch name to checkout: "
git checkout %branch% || goto error
goto pause_return

:create_branch
echo.
set /p newBranch="Enter name for new branch: "
git checkout -b %newBranch% || goto error
echo Switched to new branch %newBranch%
goto pause_return

:delete_local_branch
echo.
set /p deleteBranch="Enter local branch to delete: "
git branch -d %deleteBranch% || goto error
echo Local branch %deleteBranch% deleted.
goto pause_return

:delete_remote_branch
echo.
set /p deleteRemoteBranch="Enter remote branch to delete: "
git push origin --delete %deleteRemoteBranch% || goto error
echo Remote branch %deleteRemoteBranch% deleted.
goto pause_return

:merge
echo.
set /p mergeBranch="Enter branch to merge into current: "
call :progress_line "Merging %mergeBranch%"
git merge %mergeBranch% || goto error
goto pause_return

:pull_origin
echo.
call :progress_line "Pulling from origin"
git pull || goto error
goto pause_return

:pull_remote_branch
echo.
echo === Remote Branches ===
git branch -r
echo.
set /p remoteBranch="Enter remote branch to pull from (e.g. origin/main): "
call :progress_line "Pulling %remoteBranch%"
git pull origin %remoteBranch% || goto error
goto pause_return

:push
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
call :progress_line "Pushing %currentBranch% to origin"
git push origin %currentBranch% || goto error
goto pause_return

:push_selected_branch
echo.
echo === Remote Branches ===
git branch -r
echo.
set /p selectedBranch="Enter remote branch to push to: "
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
call :progress_line "Pushing to %selectedBranch%"
git push origin %currentBranch%:%selectedBranch% || goto error
goto pause_return

:push_all_branches
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo === Remote Branches ===
git branch -r
echo.
call :progress_line "Pushing to all remotes"
for /f "tokens=*" %%a in ('git branch -r') do (
    set "remoteBranch=%%a"
    setlocal enabledelayedexpansion
    if not "!remoteBranch!"=="origin/HEAD" (
        set "branchName=!remoteBranch:origin/=!"
        echo !branchName! | findstr /i "^backup_" >nul
        if errorlevel 1 (
            git push origin %currentBranch%:!branchName! || echo !RED!Failed to push to !branchName!!RESET!
        ) else (
            echo !YELLOW!Skipping backup branch: !branchName!!RESET!
        )
    )
    endlocal
)
goto pause_return


:: Commit and push changes (without tmp files)
:commit_push
echo.
set "msg=Updated:"
setlocal EnableDelayedExpansion
set fileCount=0
for /f "delims=" %%f in ('git diff --name-only --cached') do (
    set /a fileCount+=1
    if !fileCount! leq 5 (
        set "file=%%~nxf"
        set "msg=!msg! !file!"
    )
)
if !fileCount! gtr 5 (
    set /a remaining=fileCount - 5
    set "msg=!msg! and !remaining! more"
)
endlocal & set msg=%msg%

call :progress_line "Committing changes"
git add . || goto error
git commit -m "%msg%" || goto error

for /f %%b in ('git branch --show-current') do set currentBranch=%%b
call :progress_line "Pushing %currentBranch%"
git push origin %currentBranch% || goto error
goto pause_return

:stash
echo.
call :progress_line "Stashing changes"
git stash push || goto error
echo Changes have been stashed.
goto pause_return

:delete_local_changes
echo.
echo Are you sure you want to delete all local changes? This action cannot be undone.
set /p confirm="Type 'yes' to confirm, or anything else to cancel: "
if /i not "%confirm%"=="yes" goto pause_return

echo.
call :progress_line "Resetting working directory"
git reset --hard || goto error
echo Local changes have been deleted.
goto pause_return

:create_pull_request
echo.
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
for /f %%d in ('git symbolic-ref refs/remotes/origin/HEAD') do set defaultBranch=%%d
set defaultBranch=!defaultBranch:refs/remotes/origin/=!

for /f "delims=" %%r in ('git config --get remote.origin.url') do set REPO_URL=%%r
set "REPO_URL=!REPO_URL:git@github.com:=https://github.com/!"
set "REPO_URL=!REPO_URL:.git=!"
set "REPO_URL=!REPO_URL::=/!"

echo Creating Pull Request from !currentBranch! to !defaultBranch!
echo Please visit:
echo !REPO_URL!/compare/!currentBranch!...!defaultBranch!
goto pause_return

:backup_master
echo.
call :progress_line "Backing up master/main branch"
git checkout master || git checkout main || goto error
git pull origin master || git pull origin main || goto error
set backupBranch=backup_master_!date:/=-!
git checkout -b !backupBranch! || goto error
git push origin !backupBranch! || goto error
echo Backup branch !backupBranch! created and pushed to origin.
goto pause_return

:error
echo.
echo %RED%Error occurred!%RESET%
goto pause_return

:pause_return
pause
goto menu

:progress_line
echo
