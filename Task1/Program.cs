﻿using Task1;

// Объявляем переменную для хранения пути к директории
string? path;

// Проверяем, есть ли аргументы командной строки
if (args.Length == 0)
{
    // Если аргументы отсутствуют, запрашиваем ввод пути от пользователя
    Console.WriteLine("Пожалуйста, введите путь к директории.");

    // Читаем ввод пользователя и сохраняем его в переменной path
    path = Console.ReadLine();
}
else
{
    // Если аргументы командной строки присутствуют, сохраняем первый аргумент в переменной path
    path = args[0];
}
// Вызываем метод CleanDirectory класса DirectoryCleaner для очистки директории
DirectoryCleaner.CleanDirectory(path);