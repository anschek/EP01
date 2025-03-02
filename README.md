# УП.01
Учебная практика по профессиональному модулю 01 "Разработка модулей ПО для КС"
## неделя 1
Все деятельность этого периода размещена в ветке [first](https://github.com/anschek/EP01/tree/first)
### 1.1. Анализ предметной области
На данном этапе происходит анализ входных данных, разработка UML-диаграмм, модификация данных для импорта, реализация БД и прочее документирование
#### 1.1.1. Диаграмма вариантов использования
Для согласования процесса разработки с заказчиком разрабатывается диаграмма прецедентов (Use Case) для основных пользователей системы. Диаграмма выгружена в двух форматах: [PDF](https://github.com/anschek/EP01/blob/first/docs/use_case_conferences.pdf) и [VSDX](https://github.com/anschek/EP01/blob/first/docs/use_case_conferences.vsdx)
![use case диаграмма](https://github.com/user-attachments/assets/9f5395fd-b7d9-4b8a-9ab2-229a3842f36c)
#### 1.1.2. Импорт данных
Данные из 9 исходных excel-таблиц были нормализованы, приведены к общим идентификаторам и упорядочены. Итоговый импорт находится в [данном документе](https://github.com/anschek/EP01/blob/first/docs/total_import.xlsx)
![итоговый импорт](https://github.com/user-attachments/assets/819c12b3-9cdb-413b-b9da-ad91ab4efb4b)
#### 1.1.3. Разработка БД
По описанию предметной области и данным импорта спроектирована, разработана БД, а также заполненна данными. Вся докумнтация по данному разделу находится в папке [docs/db](https://github.com/anschek/EP01/tree/first/docs/db)
- [er-диаграмма](https://github.com/anschek/EP01/blob/first/docs/db/er-diagram.png)
- [скрипт описания таблиц БД](https://github.com/anschek/EP01/blob/first/docs/db/dll_script.sql)
- [скрипт заполнения БД](https://github.com/anschek/EP01/blob/first/docs/db/data_export.sql)
![er-диаграмма](https://github.com/anschek/EP01/blob/first/docs/db/er-diagram.png)
#### 1.1.4. Создание словаря данных
Также разработан [словарь данных](https://github.com/anschek/EP01/blob/first/docs/data_dictionary.xlsx) -набор информации, описывающий, какой тип данных хранится в базе данных, их формат, структуру и способы использования данных
![data dictionary](https://github.com/user-attachments/assets/2c0f068c-4165-4a54-b67a-11d3fe7b2f88)
