namespace Task2;

/// <summary>
/// Класс для расчета размера директории.
/// </summary>
public static class DirectorySizeCalculator
{
    /// <summary>
    /// Рассчитывает общий размер директории и всего её содержимого.
    /// </summary>
    /// <param name="directoryPath">Путь к директории, размер которой нужно рассчитать.</param>
    /// <returns>Общий размер директории в байтах.</returns>
    public static long GetDirectorySize(string? directoryPath)
    {
        // **Проверка входных данных**
        // Проверяем, не является ли путь к директории пустым или null
        // Это базовая проверка корректности входных данных
        if (string.IsNullOrEmpty(directoryPath))
        {
            // Если путь неверен, выбрасываем исключение ArgumentException с описанием ошибки
            // Это исключение будет выброшено, если вызывающий метод передаст null или пустую строку
            throw new ArgumentException("Путь к директории не может быть пустым", nameof(directoryPath));
        }

        // **Проверка существования директории**
        // Проверяем, существует ли директория по указанному пути
        // Это еще одна проверка корректности входных данных
        if (!Directory.Exists(directoryPath))
        {
            // Если директория не существует, выбрасываем исключение DirectoryNotFoundException с описанием ошибки
            // Это исключение будет выброшено, если директория отсутствует или была удалена
            throw new DirectoryNotFoundException($"Директория '{directoryPath}' не найдена");
        }

        try
        {
            // **Расчет размера директории**
            // Используем метод Directory.EnumerateFiles для получения перечисления всех файлов в директории и ее поддиректориях
            // Используем маску "*" для совпадения со всеми файлами, и SearchOption.AllDirectories для включения поддиректорий
            // Этот метод возвращает перечисление строк, содержащее пути всех файлов в директории
            var files = Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories);

            // Используем метод Sum для расчета общего размера всех файлов в директории
            // Для каждого файла создаем новый объект FileInfo и получаем его свойство Length (которое представляет размер файла в байтах)
            // Метод Sum возвращает общий размер всех файлов в байтах
            return files.Sum(file => new FileInfo(file).Length);
        }
        catch (UnauthorizedAccessException ex)
        {
            // **Обработка исключения доступа**
            // Если во время доступа к директории возникает исключение UnauthorizedAccessException, выбрасываем новое исключение с описанием ошибки
            throw new UnauthorizedAccessException($"Нет доступа к директории '{directoryPath}'", ex);
        }
        catch (IOException ex)
        {
            // **Обработка исключения ввода/вывода**
            // Если во время чтения директории возникает исключение IOException, выбрасываем новое исключение с описанием ошибки
            throw new IOException($"Ошибка при чтении директории '{directoryPath}'", ex);
        }
    }
}