# LibrarySystem — учёт библиотеки

Курсовой проект: Windows Forms (.NET Framework 4.7.2) + Microsoft Access (.accdb) через OleDb.

## Тема

Библиотека: книги, читатели, выдачи книг читателям.

## Структура БД (Library.accdb)

Файл базы лежит в `LibrarySystem\LibrarySystem\Library.accdb` и копируется рядом с .exe
при каждой сборке (уже содержит тестовые данные — ничего заполнять не нужно).

**Books** (книги) — главная сущность
| Поле | Тип | Описание |
|---|---|---|
| Id | Counter (PK) | Идентификатор |
| Title | Text | Название |
| Author | Text | Автор |
| Year | Integer | Год издания |
| AddedDate | DateTime | Дата поступления |
| IsAvailable | Yes/No | В наличии |

**Readers** (читатели) — связанная сущность
| Поле | Тип | Описание |
|---|---|---|
| Id | Counter (PK) | Идентификатор |
| FullName | Text | ФИО |
| Phone | Text | Телефон |
| Email | Text | Email |
| RegisteredDate | DateTime | Дата регистрации |

**Loans** (выдачи) — таблица-связка Books <-> Readers (один-ко-многим от каждой)
| Поле | Тип | Описание |
|---|---|---|
| Id | Counter (PK) | Идентификатор |
| BookId | Integer (FK -> Books.Id) | Какая книга |
| ReaderId | Integer (FK -> Readers.Id) | Кому выдана |
| LoanDate | DateTime | Дата выдачи |
| ReturnDate | DateTime, может быть NULL | Дата возврата (NULL — ещё не возвращена) |

В базе уже 14 книг, 12 читателей, 13 выдач тестовых данных.

## Архитектура

```
LibrarySystem.sln
└── LibrarySystem/
    ├── Program.cs              — точка входа, проверка подключения к БД
    ├── MainForm.cs/.Designer   — главное меню (Лаб. 1)
    ├── Data/
    │   └── DbHelper.cs         — строка подключения OleDb, проверка БД (Лаб. 2)
    ├── Models/
    │   ├── BaseEntity.cs       — базовый класс всех сущностей (Лаб. 5)
    │   ├── Book.cs / Reader.cs / Loan.cs
    ├── Repositories/
    │   ├── BaseRepository.cs   — базовый generic-репозиторий (Лаб. 5)
    │   └── BookRepository.cs / ReaderRepository.cs / LoanRepository.cs
    ├── Forms/
    │   ├── BookListForm / BookEditForm     — список, добавление/редактирование/удаление (Лаб. 3, 4)
    │   └── ReaderListForm / ReaderEditForm — то же самое для читателей
    └── Library.accdb           — файл базы данных с тестовыми данными
```

### Почему без ручного SQL

`BaseRepository<T>` пишет вручную только один `SELECT` (его задаёт каждый конкретный
репозиторий в конструкторе). Команды `INSERT`/`UPDATE`/`DELETE` строит
`OleDbCommandBuilder` автоматически по этому `SELECT` — ни в одном репозитории нет
ни одной строки `INSERT INTO`/`UPDATE ... SET`/`DELETE FROM`, написанной руками.

## Инструкция запуска

1. Открыть `LibrarySystem.sln` в Visual Studio 2022.
2. Платформа — **Any CPU** (уже выставлена по умолчанию в проекте).
3. Собрать (Build) и запустить (F5).
4. Файл `Library.accdb` скопируется рядом с .exe автоматически — ничего отдельно
   устанавливать не нужно, драйвер Access нужен только сам (см. ниже).

### Если при запуске ошибка "Microsoft.ACE.OLEDB.12.0 provider is not registered"

Это значит, что на компьютере не установлен (или установлен не той разрядности)
**Microsoft Access Database Engine**. Два варианта:

1. Скачать и установить `AccessDatabaseEngine_X64.exe` (64-бит) с сайта Microsoft —
   подходит для текущей настройки проекта (Any CPU, без `Prefer 32-bit`).
2. Если на компьютере установлен **32-битный** MS Office (а значит и только
   32-битный Access Database Engine) — в свойствах проекта (Project → LibrarySystem
   Properties → Build) выставить **Platform target = x86**, пересобрать, и
   поставить `AccessDatabaseEngine.exe` (32-бит) вместо 64-битного.

Разрядность процесса и разрядность установленного драйвера Access **обязаны
совпадать** — это единственная причина данной ошибки.

## Статус по лабораторным (1–5 готовы)

- **Лаб. 1** — проект, MainForm с навигацией, MenuStrip (Файл/Справка).
- **Лаб. 2** — спроектирована и создана БД Access, таблицы с PK/FK, подключение через OleDb.
- **Лаб. 3** — модели Book/Reader/Loan, репозитории, списки в DataGridView (только чтение).
- **Лаб. 4** — формы добавления/редактирования (BookEditForm/ReaderEditForm) с
  валидацией обязательных полей, удаление с подтверждением.
- **Лаб. 5** — `BaseEntity` и generic `BaseRepository<T>` — общая логика CRUD для
  всех трёх сущностей вынесена в один базовый класс.

Не реализовано (по заданию — следующие лабораторные): поиск/фильтрация и сортировка
в таблицах (Лаб. 6), отображение выдач читателя (Лаб. 6), отчёты с экспортом (Лаб. 8).
