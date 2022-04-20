using System;
using System.IO;
using System.Threading.Tasks;
using FluentFTP;
using Microsoft.VisualBasic;

namespace FtpGazApp
{
    class AsynchronousFtpUpLoader
    {
        private static async Task Main(string[] args)
        {
            int k = 0;

            // read settings file or create it if not exists
            Settings settings = IoClass.ReadSettingsFile();

            if (settings == null)
            {
                Console.ReadKey();
                return;
            }

            // create client            
            FtpClient ftpClient = new FtpClient(settings.HostUrl, settings.UserName, settings.Password);

            while (true)
            {
                string currentFile = IoClass.GetFilePathOrNull(settings.LocalPath);
                if (currentFile == null)
                {
                    Console.WriteLine($"{settings.LocalPath} contains 0 photos. Will be trying again in 30 seconds.");
                    System.Threading.Thread.Sleep(30000);
                    continue;
                }

                ftpClient = new FtpClient(settings.HostUrl, settings.UserName, settings.Password);
                if (!ftpClient.IsConnected)
                {
                    Console.WriteLine($"Connecting to url: '{settings.HostUrl}'...");
                    try
                    {
                        await ftpClient.ConnectAsync();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception.Message}");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine($"Connected to '{settings.HostUrl}'!");
                }

                string currentFileName = Path.GetFileName(currentFile);
                string remotePath = String.Concat(settings.RemotePath, "/", currentFileName);

                Console.WriteLine($"Uploading file {currentFileName}");

                FtpStatus status = await ftpClient.UploadFileAsync(currentFile, remotePath);

                if (status == FtpStatus.Failed)
                {
                    Console.WriteLine("Upload failed. Will be trying again in 30 seconds.");
                    System.Threading.Thread.Sleep(30000);
                    continue;
                }

                if (status == FtpStatus.Skipped)
                {
                    Console.WriteLine("File already exists in remote path... " +
                                      "\nRenaming file... " +
                                      "\nWill be trying again in 30 seconds.");

                    try
                    {
                        FileSystem.Rename(currentFile, RandomNameGenerator.GetRandomNameSamePathAndExtension(currentFile));
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception.Message}");
                        Console.ReadKey();
                        return;
                    }

                    System.Threading.Thread.Sleep(30000);
                    continue;
                }

                if (status == FtpStatus.Success)
                {
                    Console.WriteLine($"File {Path.GetFileName(currentFile)} uploaded successfully!");

                    string fileName = Path.GetFileName(currentFile);
                    string targetPath = Path.Combine(settings.LocalPathUploaded, fileName);

                    Console.WriteLine($"Moving file {fileName} to {settings.LocalPathUploaded}");
                    try
                    {
                        File.Move(currentFile, targetPath, true);
                        Console.WriteLine($"File moved successfully!");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception.Message}");
                        Console.ReadKey();
                        return;
                    }

                    System.Threading.Thread.Sleep(30000);
                }
            }
        }
    }
}