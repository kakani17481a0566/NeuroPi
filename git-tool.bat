@echo off
setlocal EnableDelayedExpansion

:: Initialize ANSI color codes
for /f %%A in ('echo prompt $E ^| cmd') do set "ESC=%%A"
set "BLUE=%ESC%[94m"
set "GREEN=%ESC%[92m"
set "YELLOW=%ESC%[93m"
set "RED=%ESC%[91m"
set "RESET=%ESC%[0m"

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
echo 12.  Pull from selected remote branch
echo 13.  Push current branch to origin
echo 14.  Push to selected remote branch
echo 15.  Push to all remote branches
echo 16.  Commit and push changes (with filenames)
echo 17.  Stash changes
echo 18.  Create Pull Request
echo 19.  Exit
echo ================================================
set /p choice="Select an option (1-19): "

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
if "%choice%"=="18" goto create_pull_request
if "%choice%"=="19" goto end
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
    if not "!remoteBranch!"=="origin/HEAD" (
        set "remoteBranch=!remoteBranch:origin/=!"
        git push origin %currentBranch%:!remoteBranch! || echo !RED!Failed to push to !remoteBranch!!RESET!
    )
)
goto pause_return

:commit_push
echo.
:: Get list of changed/added files (combined staged and unstaged changes)
git diff --name-only --cached > tmp_status.txt
git diff --name-only >> tmp_status.txt
git ls-files --others --exclude-standard >> tmp_status.txt

:: Windows-friendly alternative to 'sort | uniq'
:: First sort the file
sort tmp_status.txt > tmp_status_sorted.txt

:: Then remove duplicates (Windows version)
set "prevLine="
(
    for /f "delims=" %%f in (tmp_status_sorted.txt) do (
        if "%%f" neq "!prevLine!" (
            echo %%f
            set "prevLine=%%f"
        )
    )
) > tmp_status_filtered.txt

:: Check if there are any changes
set "hasChanges="
for /f "usebackq delims=" %%f in ("tmp_status_filtered.txt") do (
    set "hasChanges=1"
    goto :has_changes
)

:has_changes
if not defined hasChanges (
    echo No changes to commit.
    del tmp_status.txt 2>nul
    del tmp_status_sorted.txt 2>nul
    del tmp_status_filtered.txt 2>nul
    goto pause_return
)

:: Build commit message from file names (limited to 5 files for brevity)
set msg=Updated:
setlocal EnableDelayedExpansion
set fileCount=0
for /f "usebackq delims=" %%f in ("tmp_status_filtered.txt") do (
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

:: Cleanup temporary files
del tmp_status.txt 2>nul
del tmp_status_sorted.txt 2>nul
del tmp_status_filtered.txt 2>nul

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

:create_pull_request
echo.
:: Get current branch
for /f %%b in ('git branch --show-current') do set currentBranch=%%b

:: Get default branch (usually main or master)
for /f %%d in ('git symbolic-ref refs/remotes/origin/HEAD') do set defaultBranch=%%d
set defaultBranch=!defaultBranch:refs/remotes/origin/=!

echo Creating Pull Request from !currentBranch! to !defaultBranch!
echo.
set /p prTitle="Enter Pull Request title: "
set /p prDescription="Enter Pull Request description: "

call :progress_line "Creating Pull Request"
git push origin %currentBranch% || goto error

:: Try different methods to open PR URL based on git remote
for /f "delims=" %%r in ('git remote get-url origin') do set repoUrl=%%r
set repoUrl=!repoUrl:git@github.com:=https://github.com/!
set repoUrl=!repoUrl:.git=!
set repoUrl=!repoUrl:https://github.com/=https://github.com/!

:: Create PR URL
set prUrl=!repoUrl!/compare/!defaultBranch!...!currentBranch!?expand=1&title=!prTitle!&body=!prDescription!

echo.
echo !GREEN!Pull Request ready:!RESET!
echo !prUrl!
echo.
echo You can also create it manually:
echo 1. Go to: !repoUrl!/pulls
echo 2. Click "New pull request"
echo 3. Set base: !defaultBranch! and compare: !currentBranch!
echo 4. Enter title and description
echo 5. Click "Create pull request"

:: Try to open browser (works on most Windows systems)
start "" "!prUrl!" 2>nul
goto pause_return

:error
echo.
echo !RED!=== ERROR: Operation failed. Please check the above output. ===!RESET!
goto pause_return

:pause_return
echo.
pause
goto menu

:end
echo.
echo Exiting Git Tool...
exit /b

:progress_line
@echo off
setlocal enabledelayedexpansion

:: Settings
set "width=40"
set "delay=20"
set "steps=30"

:: Get operation description
set "operation=%~1"
if not defined operation set "operation=Processing"

:: Clear line sequence
set "clear_line=%ESC%[2K%ESC%[1G"

:: Animation loop
for /L %%i in (1,1,%steps%) do (
    set /a "percent=(%%i*100/%steps%)"
    set /a "pos=(%%i*%width%/%steps%)"
    
    :: Build progress line
    set "line="
    for /L %%j in (1,1,%width%) do (
        if %%j lss !pos! (
            if !percent! geq 80 (
                set "line=!line!!GREEN!▓!RESET!"
            ) else if !percent! geq 50 (
                set "line=!line!!YELLOW!▒!RESET!"
            ) else (
                set "line=!line!!BLUE!░!RESET!"
            )
        ) else if %%j == !pos! (
            set "line=!line!!BLUE!►!RESET!"
        ) else (
            set "line=!line! "
        )
    )
    
    :: Display progress
    <nul set /p="!clear_line!!BLUE!!operation! !RESET![!line!] !percent!%%"
    
    :: Smooth delay
    >nul ping -n 1 -w %delay% 127.0.0.1
)

:: Completion state
set "line="
for /L %%j in (1,1,%width%) do set "line=!line!!GREEN!▓!RESET!"
echo !clear_line!!BLUE!!operation! !RESET![!line!] 100%% !GREEN!✓!RESET!
endlocal
goto :eof