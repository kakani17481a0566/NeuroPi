@echo off
setlocal EnableDelayedExpansion

:menu
cls
echo ================================================
echo              GIT TOOL - INTERACTIVE MENU
echo ================================================
echo  1.  Show git status
echo  2.  Show git log
echo  3.  List all local branches
echo  4.  List all remote branches
echo  5.  Switch to the last branch
echo  6.  Checkout a branch
echo  7.  Create a new branch
echo  8.  Delete a local branch
echo  9.  Delete a remote branch
echo 10.  Merge a branch into current
echo 11.  Pull latest from origin
echo 12.  Pull from selected remote branch into current local branch
echo 13.  Push current branch to origin
echo 14.  Push current branch to selected remote branch
echo 15.  Push current branch to all remote branches
echo 16.  Commit and push local changes to current branch
echo 17.  Stash changes
echo 18.  Exit
echo ================================================
set /p choice="Select an option (1-18): "

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
if "%choice%"=="18" goto end
goto menu

:status
echo.
git status
goto pause_return

:log
echo.
git log --oneline --graph --decorate --all
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
set /p branch="Enter the branch name to checkout: "
git checkout %branch% || goto error
goto pause_return

:create_branch
echo.
set /p newBranch="Enter the name of the new branch: "
git checkout -b %newBranch% || goto error
echo Switched to new branch %newBranch%
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

:merge
echo.
set /p mergeBranch="Enter the branch to merge into current: "
git merge %mergeBranch% || goto error
goto pause_return

:pull_origin
echo.
git pull || goto error
goto pause_return

:pull_remote_branch
echo.
echo === Remote Branches ===
git branch -r
echo.
set /p remoteBranch="Enter the remote branch name to pull from (e.g. origin/feature-xyz): "
echo Pulling from remote branch...
call :progress_bar
git pull origin %remoteBranch% || goto error
goto pause_return

:push
echo.
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo Pushing current branch %currentBranch% to origin...
call :progress_bar
git push origin %currentBranch% || goto error
goto pause_return

:push_selected_branch
echo.
echo === Remote Branches ===
git branch -r
echo.
set /p selectedBranch="Enter the remote branch name to push the current branch to: "
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo Pushing to remote branch %selectedBranch%...
call :progress_bar
git push origin %currentBranch%:%selectedBranch% || goto error
goto pause_return

:push_all_branches
echo.
for /f %%b in ('git branch --show-current') do set currentBranch=%%b
echo Pushing current branch to all remote branches...
echo === Remote Branches ===
git branch -r
echo.
call :progress_bar
:: Loop over remote branches and push
for /f "tokens=*" %%a in ('git branch -r') do (
    set "remoteBranch=%%a"
    if not "!remoteBranch!"=="origin/HEAD" (
        call :push_to_remote !remoteBranch!
    )
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

git add . || goto error
git commit -m "%msg%" || goto error
del tmp_status.txt

goto push

:stash
echo.
git stash push || goto error
echo Changes have been stashed.
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


:progress_bar
@echo off
setlocal enabledelayedexpansion

:: Total steps for progress bar
set "total=50"
set "percent=0"
set "bar="
set "width=50"
set "delay=50"

:: ANSI color codes
for /f %%A in ('echo prompt $E ^| cmd') do set "ESC=%%A"
set "GREEN=%ESC%[32m"
set "CYAN=%ESC%[36m"
set "YELLOW=%ESC%[33m"
set "RESET=%ESC%[0m"

:: Characters for the spinner animation
set "chars=⠋⠙⠹⠸⠼⠴⠦⠧⠇⠏"

:: Loop to create the animated progress bar
for /L %%i in (1,1,%total%) do (
    set /a "percent=(%%i*100)/%total%"
    
    :: Calculate spinner position
    set /a "spinner_pos=%%i %% 10"
    call set "spinner=%%chars:~!spinner_pos!,1%%"
    
    :: Build the progress bar with different colors
    set "bar="
    for /L %%j in (1,1,%%i) do (
        set /a "color_zone=%%j*100/%width%"
        if !color_zone! lss 30 (
            set "bar=!bar!%GREEN%▓%RESET%"
        ) else if !color_zone! lss 70 (
            set "bar=!bar!%YELLOW%▓%RESET%"
        ) else (
            set "bar=!bar!%CYAN%▓%RESET%"
        )
    )
    
    :: Display the progress bar with spinner
    <nul set /p="%CYAN%!spinner!%RESET% [%GREEN%!bar!%RESET%] %percent%%% %CYAN%Working...%RESET%"
    
    :: Add slight delay for animation
    ping 127.0.0.1 -n 1 -w !delay! > nul
    
    :: Clear the line for next update (using proper ANSI escape codes)
    <nul set /p="%ESC%[2K%ESC%[1G"
)

:: Finish with a newline
echo.
endlocal
goto :eof