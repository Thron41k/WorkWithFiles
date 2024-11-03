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
    // Вызываем метод GetDirectorySize класса DirectorySizeCalculator, передавая ему путь к директории
    // Этот метод рассчитывает размер директории и возвращает его в виде long
    var result = DirectorySizeCalculator.GetDirectorySize(path);

    // Выводим размер директории в консоль
    Console.WriteLine($"Размер директории {path} {result} байт");
}
catch (Exception e)
{
    // Если во время выполнения метода GetDirectorySize возникает исключение, выводим его в консоль
    Console.WriteLine(e);
}
