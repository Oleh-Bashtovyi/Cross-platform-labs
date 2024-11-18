### Запуск програми
Для запуску програми проекту перейдіть до папки `Lab4/Lab4`
```bash
cd Lab4/Lab4
```

Приклад запуску:
```bash
dotnet run -- run lab1 --input ./inputs/INPUT.txt -o OUTPUT.txt
```

### Інші команди
Вивести перелік команд:
```bash
dotnet run -- -h
```
Присвоїти змінній середовижа `LAB_PATH` значення шляху для читання за замовчуванням:
```bash
dotnet run -- set-path -p {path}
```
Версія та автор:
```bash
dotnet run -- version
```

### Запуск віртуальних машин

Для того, щоб на віртуальних машинах запустився застосунок, у вас має бути запущений приватний репозиторій BaGet на порту 5000. У ньому має бути застоснок як глобальний пакет. Щоб опублікувати пакет, виконайте:
```bash
cd Lab4/Lab4
dotnet pack --configuration Release
dotnet nuget push --source http://localhost:5000/v3/index.json nupkg/OBashtovyi.1.0.0.nupkg
```

Для створення віртуальних машин потрібно мати Virtual Box та Vagrant. Спочатку перейдіть у папку `Lab4`, що містить `Vagrant` файл та пропишіть:
```bash
vagrant up
```
Після цього ВМ буде встановлена і буде мати dotnet 8.0 зі встановленим nuget пакетом.

Щоб використовувати пакет слід прописати команду:
```bash
OBashtovyi run lab{lab_number} -i {input_file_path} -o {output_file_path}
```

Завдання не було виконано для MacOS через відсутність можливості запустити віртуальну машину.