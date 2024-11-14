### ������ ��������
��� ������� �������� ������� �������� �� ����� `Lab4/Lab4`
```bash
cd Lab4/Lab4
```

������� �������:
```bash
dotnet run -- run lab1 --input ./inputs/INPUT.txt -o OUTPUT.txt
```

### ���� �������
������� ������ ������:
```bash
dotnet run -- -h
```
�������� ����� ���������� `LAB_PATH` �������� ����� ��� ������� �� �������������:
```bash
dotnet run -- set-path -p {path}
```
����� �� �����:
```bash
dotnet run -- version
```

### ������ ���������� �����
��� ��������� ���������� ����� ������� ���� Virtual Box �� Vagrant. �������� �������� � ����� `Lab4`:
```bash
cd Lab4
```
�� ���������:
```bash
vagrant up
```
ϳ��� ����� �� ���� ����������� � ���� ���� dotnet 8.0 � ������������ nuget �������.

��� ��������������� ����� ��� ��������� �������:
```bash
OBashtovyi run lab{lab_number} -i {input_file_path} -o {output_file_path}
```

�������� �� ���� �������� ��� MacOS ����� ��������� ��������� ��������� ��������� ������.