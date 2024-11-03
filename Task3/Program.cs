using Task1;
using Task2;

// Объявляем переменную path для хранения пути к директории
string? path;

// Проверяем, переданы ли аргументы командной строки (args)
if (args.Length == 0)
{
    // Если аргументы не переданы, выводим сообщение, запрашивающее путь к директории
    Console.WriteLine("Пожалуйста, введите путь к директории.");

    // Читаем путь к директории из консоли
    path = Console.ReadLine();
}
else
{
    // Если аргументы переданы, присваиваем первый аргумент переменной path
    path = args[0];
}

try
{
    // Рассчитываем исходный размер директории, вызывая метод GetDirectorySize класса DirectorySizeCalculator
    var startSize = DirectorySizeCalculator.GetDirectorySize(path);

    // Выводим исходный размер директории в консоль
    Console.WriteLine($"Исходный размер папки: {startSize} байт");

    // Вызываем метод CleanDirectory класса DirectoryCleaner, передавая ему путь к директории
    // Этот метод удаляет ненужные файлы и освобождает место на диске
    DirectoryCleaner.CleanDirectory(path);

    // Рассчитываем конечный размер директории после очистки, вызывая метод GetDirectorySize класса DirectorySizeCalculator
    var endSize = DirectorySizeCalculator.GetDirectorySize(path);

    // Выводим количество освобожденного места в консоль
    Console.WriteLine($"Освобождено: {startSize - endSize} байт");

    // Выводим конечный размер директории в консоль
    Console.WriteLine($"Текущий размер папки: {endSize} байт");
}
catch (Exception e)
{
    // Если во время выполнения кода возникает исключение, выводим его в консоль
    Console.WriteLine(e);
}