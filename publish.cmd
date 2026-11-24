@echo off
chcp 65001 >nul
rem Сборка portable-версии на Windows
cd /d "%~dp0"
if not exist Migrations (
  echo Нет папки Migrations. Сначала: dotnet ef migrations add InitialCreate
  exit /b 1
)
if exist dist\MedicalEquipment rmdir /s /q dist\MedicalEquipment
dotnet publish MyMvcApp.csproj -c Release -r win-x64 -o dist\MedicalEquipment || exit /b 1
copy /y "ПРОЧТИ.txt" dist\MedicalEquipment\ >nul
echo Готово: dist\MedicalEquipment — скопируйте папку на флешку целиком.
