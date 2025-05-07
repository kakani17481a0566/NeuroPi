@echo off
:: This batch script will push Dev branch to all remote branches except master

:: Fetch the latest changes
git fetch

:: Get the list of remote branches, excluding master
for /f "tokens=*" %%a in ('git branch -r ^| findstr /v "origin/master"') do (
    set branch=%%a
    setlocal enabledelayedexpansion

    :: Remove the 'origin/' prefix from the branch name
    set branch_name=!branch:origin/=!

    :: Push Dev to each branch
    echo Pushing Dev to !branch_name!
    git push origin Dev:!branch_name!

    endlocal
)

echo Done pushing to all branches except master.
pause
