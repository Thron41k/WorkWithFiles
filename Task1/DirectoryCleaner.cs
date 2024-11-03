namespace Task1;

/// <summary>
/// Класс для очистки директорий.
/// </summary>
public static class DirectoryCleaner
{
    // Константа, определяющая время, в течение которого файлы и поддиректории не должны использоваться для удаления
    private const int NoAcccessForDeleteTime = 3;

    /// <summary>
    /// Очищает директорию, удаляя файлы и поддиректории, которые не использовались более NoAcccessForDeleteTime минут.
    /// </summary>
    /// <param name="directoryPath">Путь к директории, которую нужно очистить.</param>
    public static void CleanDirectory(string? directoryPath)
    {
        // Проверяем, является ли путь к директории пустым или null
        if (string.IsNullOrEmpty(directoryPath))
        {
            // Если путь неверен, выводим сообщение об ошибке и выходим из функции
            Console.WriteLine("Ошибка: передан некорректный путь.");
            return;
        }

        // Проверяем, существует ли директория
        if (!Directory.Exists(directoryPath))
        {
            // Если директории не существует, выводим сообщение об ошибке и выходим из функции
            Console.WriteLine("Ошибка: папка по заданному адресу не существует.");
            return;
        }

        try
        {
            // Получаем информацию о директории
            var directoryInfo = new DirectoryInfo(directoryPath);

            // Получаем список файлов в директории
            var files = directoryInfo.GetFiles();

            // Получаем список поддиректорий в директории
            var subdirectories = directoryInfo.GetDirectories();

            // Перебираем файлы в директории
            foreach (var file in files)
            {
                // Проверяем, использовался ли файл в течение последних NoAcccessForDeleteTime минут
                if (!((DateTime.Now - file.LastAccessTime).TotalMinutes > NoAcccessForDeleteTime)) continue;

                try
                {
                    // Пытаемся удалить файл
                    File.Delete(file.FullName);

                    // Если файл удален успешно, выводим сообщение об успехе
                    Console.WriteLine($"Файл {file.Name} удален.");
                }
                catch (UnauthorizedAccessException)
                {
                    // Если есть исключение несанкционированного доступа, выводим сообщение об ошибке
                    Console.WriteLine($"Ошибка: нет прав доступа к файлу {file.Name}.");
                }
                catch (Exception ex)
                {
                    // Если есть любое другое исключение, выводим сообщение об ошибке с подробностями исключения
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            // Перебираем поддиректории в директории
            foreach (var subdirectory in subdirectories)
            {
                // Проверяем, использовалась ли поддиректория в течение последних NoAcccessForDeleteTime минут
                if (!((DateTime.Now - subdirectory.LastAccessTime).TotalMinutes > NoAcccessForDeleteTime)) continue;

                try
                {
                    // Пытаемся удалить поддиректорию и все ее содержимое
                    Directory.Delete(subdirectory.FullName, true);

                    // Если поддиректория удалена успешно, выводим сообщение об успехе
                    Console.WriteLine($"Папка {subdirectory.Name} удалена.");
                }
                catch (UnauthorizedAccessException)
                {
                    // Если есть исключение несанкционированного доступа, выводим сообщение об ошибке
                    Console.WriteLine($"Ошибка: нет прав доступа к папке {subdirectory.Name}.");
                }
                catch (Exception ex)
                {
                    // Если есть любое другое исключение, выводим сообщение об ошибке с подробностями исключения
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Если есть исключение несанкционированного доступа на уровне директории, выводим сообщение об ошибке
            Console.WriteLine("Ошибка: нет прав доступа к папке.");
        }
        catch (Exception ex)
        {
            // Если есть любое другое исключение, выводим сообщение об ошибке с подробностями исключения
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}