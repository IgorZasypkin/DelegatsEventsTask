using DelegatsEventsTask;
using DelegatsEventsTask.FileOperations;
using DelegatsEventsTask.FileOperations.Helpers;


    foreach (var image in Url.images)
{
    var imageDownloader = new ImageDownloader();
    imageDownloader.ImageStarted += OnImageStarted;
    imageDownloader.ImageCompleted += OnImageCompleted;

    var task = imageDownloader.Download(image);

    while (!task.IsCompleted)
    {
        Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
        var key = Console.ReadKey();
        Console.WriteLine();
        if (key.Key != ConsoleKey.A)
        {
            Console.WriteLine($"Картинка загружена: {task.IsCompleted}");
        }
        else
        {
            Environment.Exit(0);
        }
    }

    await task;

    static void OnImageStarted()
    {
        Console.WriteLine("Скачивание файла началось");
    }

    static void OnImageCompleted()
    {
        Console.WriteLine("Скачивание файла закончилось");
    }
}