@echo off
echo Switching to master branch...
git checkout master || goto :error

echo Merging Dev into master...
git merge Dev || goto :error

echo Pushing to origin/master...
git push origin master || goto :error

echo.
echo === Push to master successful! ===
goto :end

:error
echo.
echo === ERROR: Operation failed. Please check the above messages. ===

:end
pause
