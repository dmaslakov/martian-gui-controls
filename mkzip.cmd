@echo off
if [%1] == [] (
	echo Usage: %~n0 RELEASE
	exit /b 1
)

set cfg_version=1.0
set cfg_release=%1

zip -9 -j martian-gui-controls-%cfg_version%-%cfg_release%.zip VirtualTreeView-TestApp\bin\Release\VirtualTreeViewTestApp.exe VirtualTreeView-TestApp\bin\Release\VirtualTreeViewTestApp.exe.config VirtualTreeView-TestApp\bin\Release\VirtualTreeViewTestApp.pdb MartianGuiControls\bin\Release\MartianGuiControls.dll MartianGuiControls\bin\Release\MartianGuiControls.pdb license.txt
