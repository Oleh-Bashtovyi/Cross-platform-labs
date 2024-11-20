## Lab3
### Запуск програми

Щоб запустити Lab3, треба спочатку встановити бібліотеку класів Lab3.Library. Виконайте наступні команди з кореневої папки Lab3:
```bash
cd Lab3.Library

dotnet build -c Release

#Creating a NuGet package from the Lab3.Library project...
dotnet pack Lab3.Library.csproj -c Release -o ./nupkg

cd ..
cd Lab3

#Installing the NuGet package into the Lab3.Runner project...
dotnet add package OBashtovyi_Lab3 --version 1.0.0 --source ../Lab3.Library/nupkg

#Restoring all project dependencies...
dotnet restore

cd ..

#Executing the Lab3.Runner project...
dotnet run --project Lab3/Lab3.csproj
```

### Запуск тестів
Без проміжних результатів
```bash
dotnet test
```
З проміжними результатами
```bash
dotnet test --logger "console;verbosity=detailed"
```

### Результати

Вхідні дані беруться з файлу `INPUT.TXT`, результати записуються в файл `OUTPUT.TXT`

# Варіант 44

Дано матрицю A розміром N×N, заповнену невід'ємними цілими числами. 
Відстань між двома елементами Ai j та Ap q визначено як | i - p | + | j - q |. 
Потрібно замінити кожен нульовий елемент матриці найближчим ненульовим. 
Якщо є два або більше найближчих ненульових осередків, нуль має бути залишений.

## Вхідні дані

У першому рядку вхідного файлу INPUT.TXT міститься натуральне число N (N ≤ 200). 
Потім йдуть N рядків N чисел, розділених пробілами і являють собою матрицю. 
Елементи матриці не перевищують значення 106.

## Вихідні дані

У вихідний файл OUTPUT.TXT виведіть N рядків N чисел, розділених пробілами, - модифіковану матрицю.

## Приклади

| №  | INPUT.TXT        | OUTPUT.TXT  |
|----|------------------|-------------|
| 1  | 3                |             |
|    | 0 0 0            |  1 0 2      |
|    | 1 0 2            |  1 0 2      |
|    | 0 3 0            |  0 3 0      |
