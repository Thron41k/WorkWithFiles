using System.Text;

namespace Task4;

/// <summary>
/// Класс для чтения базы данных студентов из бинарного файла.
/// </summary>
internal static class StudentReader
{
    /// <summary>
    /// Считывает список студентов из бинарного файла.
    /// </summary>
    /// <param name="binaryFilePath">Путь к бинарному файлу.</param>
    /// <returns>Список студентов, или null, если файл пустой.</returns>
    /// <exception cref="ArgumentException">Вызывается, если путь к файлу пуст или null.</exception>
    /// <exception cref="FileNotFoundException">Вызывается, если файл не найден.</exception>
    public static List<Student> ReadBinaryFile(string? binaryFilePath)
    {
        // Проверяем, что путь к файлу не пустой и не null
        if (string.IsNullOrEmpty(binaryFilePath))
        {
            // Если путь к файлу пустой или null, бросаем исключение ArgumentException
            throw new ArgumentException("Путь к файлу не может быть пустым", nameof(binaryFilePath));
        }

        // Проверяем, что файл существует
        if (!File.Exists(binaryFilePath))
        {
            // Если файл не найден, бросаем исключение FileNotFoundException
            throw new FileNotFoundException($"Файл '{binaryFilePath}' не найден");
        }

        // Создаем пустой список студентов
        var students = new List<Student>();

        // Открываем FileStream для чтения
        using var fileStream = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read);

        // Создаем BinaryReader для чтения данных из файла
        using var binaryReader = new BinaryReader(fileStream, Encoding.UTF8, true);

        // Читаем данные из файла пока не достигнем конца файла
        while (fileStream.Position < fileStream.Length)
        {
            // Создаем новый объект Student
            var student = new Student
            {
                // Читаем строку из файла и присваиваем ее свойству Name
                Name = binaryReader.ReadString(),
                // Читаем строку из файла и присваиваем ее свойству Group
                Group = binaryReader.ReadString(),
                // Читаем целое число из файла, преобразуем его в DateTime и присваиваем его свойству DateOfBirth
                DateOfBirth = DateTime.FromBinary(binaryReader.ReadInt64()),
                // Читаем десятичное число из файла и присваиваем его свойству AverageScore
                AverageScore = binaryReader.ReadDecimal()
            };

            // Добавляем студента в список
            students.Add(student);
        }

        // Возвращаем список студентов
        return students;
    }
}