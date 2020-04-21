
:: [Change] Set your target executable name (typically [projectname].exe) your DLL name and your merged file Name

SET APP_NAME=unwrapped.exe
SET DLL_NAME=Gma.System.MouseKeyHook.dll
SET MERGED_NAME=wrappedPayload.exe

:: [Change] Set the path to the folder where the executable and the DLL is:
SET PATH_TO_EXE_FOLDER= C:\YOUR\PATH\TO\PROJECT\NAMEOF\PROJECT\bin\Release

:: [Change] set your NuGet ILMerge Version (check on Visual Studio)
SET ILMERGE_VERSION=3.0.29

:: the full ILMerge should be found here:

SET ILMERGE_PATH=%USERPROFILE%\.nuget\packages\ilmerge\%ILMERGE_VERSION%\tools\net452
SET FINAL_PATH=%PATH_TO_EXE_FOLDER%\%APP_NAME%
SET DLL_PATH=%PATH_TO_EXE_FOLDER%\%DLL_NAME%

"%ILMERGE_PATH%"\ILMerge.exe %FINAL_PATH%  ^
/lib:Bin\%ILMERGE_BUILD%\ ^
/out:%MERGED_NAME%  ^
%DLL_PATH%

:Done
set /p asd="DONE. Merge was completed!"