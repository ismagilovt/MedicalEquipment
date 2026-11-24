#!/usr/bin/env bash
# Сборка portable-версии для Windows (запуск с флешки). Работает на macOS/Linux.
set -euo pipefail
cd "$(dirname "$0")"

if [ ! -d Migrations ]; then
  echo "Нет папки Migrations. Сначала создайте миграцию:"
  echo "  dotnet tool install --global dotnet-ef   # один раз"
  echo "  dotnet ef migrations add InitialCreate"
  exit 1
fi

OUT=dist/MedicalEquipment
rm -rf "$OUT" dist/MedicalEquipment-win-x64.zip
dotnet publish MyMvcApp.csproj -c Release -r win-x64 -o "$OUT"
cp "ПРОЧТИ.txt" "$OUT/"
(cd dist && zip -qr MedicalEquipment-win-x64.zip MedicalEquipment)

echo
echo "Готово: $OUT  (и архив dist/MedicalEquipment-win-x64.zip)"
echo "Скопируйте папку MedicalEquipment на флешку целиком."
