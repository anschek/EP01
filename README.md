# УП.01
Учебная практика по профессиональному модулю 01 "Разработка модулей ПО для КС"
## неделя 1
Все деятельность по УП в период с 01.03 по 07.03 
### 1.1. Анализ предметной области
Ветка: [first](https://github.com/anschek/EP01/tree/first)

Все документы данного этапа размещены в папке [docs](https://github.com/anschek/EP01/tree/first/docs)
1. Диаграмма прецедентов

![image](https://github.com/user-attachments/assets/a3060684-42b0-4ca6-aa37-bbfc7d798e38)

3. [Структуризация импорта данных](https://github.com/anschek/EP01/blob/first/docs/total_import.xlsx)
4. ER-диаграмма

![ER-диаграмма](https://github.com/anschek/EP01/blob/first/docs/db/er-diagram.png)

4. [Словарь данных](https://github.com/anschek/EP01/blob/first/docs/data_dictionary.xlsx)
5. [Скрипт создания таблиц БД](https://github.com/anschek/EP01/blob/first/docs/db/dll_script.sql)
6. [Скрипт заполнения таблиц БД](https://github.com/anschek/EP01/blob/first/docs/db/data_export.sql)

### 1.2. Разработка настольного приложения
Ветка: [first](https://github.com/anschek/EP01/tree/first)

[Настольное приложение](https://github.com/anschek/EP01/tree/first/ConferencesSystem) разработано для проведения конференций и включает работы с такими группами пользователей:
- незарегистрированные пользователи
- участники
- модераторы
- жюри
- организаторы

Основные страницы:
1. Список мероприятия с возможностью фильтрации по дате и направлению

![image](https://github.com/user-attachments/assets/fb045d16-d420-4074-a326-642b766112d0)

2. Авторизация

![image](https://github.com/user-attachments/assets/56bc12d9-828d-4635-aa37-80248f1c8e80)

3. Профиль пользователя

![image](https://github.com/user-attachments/assets/5fa22b6c-8df9-4623-b214-0b0af2305459)

4. Регистрация модератора/жюри (страница доступна только организатору)

![image](https://github.com/user-attachments/assets/3cfeeed1-933d-49f9-a1a6-125875de9e08)

Для приложения написано [руководство пользователя](https://github.com/anschek/EP01/blob/first/docs/%D0%A0%D1%83%D0%BA%D0%BE%D0%B2%D0%BE%D0%B4%D1%81%D1%82%D0%B2%D0%BE%20%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D1%8F%2001.docx), описывающее всю функциональность для каждой роли.

### 1.3. Разработка DLL и тестирование
Ветка: [first-session2](https://github.com/anschek/EP01/tree/first-session2)

[Библиотека классов](https://github.com/anschek/EP01/blob/first-session2/REG_MARK_LIB/RegMarkService.cs) предназначена для работы с регистрационными знаками и предоставляет методы для:
- проверки валидности знака
- определения следующего знака
- определения следующего знака в указанном диапазоне
- определения количества знаков между двумя другими

[Проект модульных тестов](https://github.com/anschek/EP01/blob/first-session2/REG_MARK_LIB.Test/RegMarkService_Test.cs) состоит из 18 юнит-тестов (10 позитивных, 8 негативных), которые покрывают возможные сценарии работы с библиотекой.

![Тесты](https://github.com/anschek/EP01/blob/first-session2/docs/test_result.png)

[Тест-кейсы](https://github.com/anschek/EP01/blob/first-session2/docs/test_cases.docx) для проведения ручного тестирования содержат описание пяти случаев проверки функции регистрации моедратора/жюри приложения, описанного в пункте 1.2.

### 1.4. Оценивание приложения другого студента по критериям демоэкзамена
Ветка: [main](https://github.com/anschek/EP01/tree/main)

[Проверяемый проект другого студента](https://github.com/Damir1528/GazizovUpGolubeva/tree/master)

[Таблица с критериями и выстановленными баллами](https://github.com/anschek/EP01/blob/main/docs/desktop_app_assessment.xlsx) - общий результат: 37.6/50 - 75.13%

Разработанная программа была проверена на соответствие требованиям по следующим основным аспектам:
 -	Системный анализ и проектирование
 -	Разработка программного обеспечения
 -	Стандарты разработки программного обеспечения
 -	Документирование программных решений
