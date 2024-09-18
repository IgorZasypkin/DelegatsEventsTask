using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DelegatsEventsTask.FileOperations
{
    public class ImageDownloader
    {
        public event Action? ImageStarted;
        public event Action? ImageCompleted;

        public async Task Download(string url)
        {
            var fileName = "bigimage.jpg";
            var webClient = new WebClient();

            webClient.DownloadFileCompleted += OnDownloadFileCompleted(fileName, url);
            Console.WriteLine("Качаю \"{0}\" из \"{1}\"...\n", fileName, url);
            ImageStarted?.Invoke();
            await webClient.DownloadFileTaskAsync(url, fileName);
        }

        private AsyncCompletedEventHandler OnDownloadFileCompleted(string fileName, string url)
        {
            Action<object, AsyncCompletedEventArgs> action = (_, e) =>
            {
                if (e.Cancelled)
                {
                    Console.WriteLine("Загрузка файла отменена.");
                }
                else if (e.Error != null)
                {
                    throw e.Error;
                }
                else
                {
                    Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, url);
                    ImageCompleted?.Invoke();
                }
            };
            return new AsyncCompletedEventHandler(action);
        }
    }
}
