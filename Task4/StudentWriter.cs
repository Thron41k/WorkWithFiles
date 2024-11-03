namespace Task4;

/// <summary>
/// Класс для записи списка студентов на рабочий стол.
/// </summary>
internal static class StudentWriter
{
    /// <summary>
    /// Записывает список студентов на рабочий стол, сгруппированных по их группе.
    /// </summary>
    /// <param name="students">Список объектов Student для записи на рабочий стол.</param>
    /// <exception cref="ArgumentException">Вызывается, если список студентов равен null или пуст.</exception>
    public static void WriteStudentsToDesktop(List<Student>? students)
    {
        // Проверяем, является ли входной список пустым или равным null. Если да, генерируем исключение ArgumentException с описательным сообщением.
        if (students == null || students.Count == 0)
        {
            throw new ArgumentException("Список студентов пустой");
        }

        // Получаем путь к папке рабочего стола с помощью метода Environment.GetFolderPath.
        // Параметр Environment.SpecialFolder.Desktop указывает, что мы хотим получить путь к рабочему столу.
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Создаем новый путь к папке, объединяя путь к рабочему столу со строкой "Students".
        // Это будет папка, в которой мы будем записывать данные о студентах.
        var studentsDirectoryPath = Path.Combine(desktopPath, "Students");

        // Создаем папку для студентов, если она еще не существует.
        // Метод Directory.CreateDirectory не генерирует исключение, если папка уже существует.
        Directory.CreateDirectory(studentsDirectoryPath);

        // Группируем студентов по их группе с помощью метода LINQ GroupBy.
        // Лямбда-выражение s => s?.Group указывает, что мы хотим группировать по свойству Group каждого объекта Student.
        var studentGroups = students.GroupBy(s => s.Group);

        // Перебираем каждую группу студентов с помощью цикла foreach.
        foreach (var group in studentGroups)
        {
            // Используем метод string.Join, чтобы объединить данные о студентах в одну строку.
            // Параметр Environment.NewLine указывает, что мы хотим использовать символ новой строки для разделения данных каждого студента.
            // Лямбда-выражение s => $"{s?.Name}, {s?.DateOfBirth}, {s?.AverageScore}" указывает, что мы хотим включить свойства Name, DateOfBirth и AverageScore каждого объекта Student.
            var studentInfo = string.Join(
                Environment.NewLine,
                group.Select(s => $"{s.Name}, {s.DateOfBirth}, {s.AverageScore}"));

            // Создаем путь к файлу, объединяя путь к папке для студентов с ключом группы (который является названием группы) и расширением ".txt".
            var filePath = Path.Combine(studentsDirectoryPath, $"{group.Key}.txt");

            // Записываем данные о студентах в файл с помощью метода File.WriteAllText.
            File.WriteAllText(filePath, studentInfo);
        }
    }
}