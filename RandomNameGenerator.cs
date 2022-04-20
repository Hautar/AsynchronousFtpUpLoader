using System;
using System.IO;
using System.Text;

namespace FtpGazApp
{
    public static class RandomNameGenerator
    {
        public static string GetRandomNameSamePathAndExtension(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar;

            string[] split = filePath.Split('.');
            string extension = split[split.Length - 1];
            
            Random random = new Random();
            StringBuilder result = new StringBuilder(directory);
            
            for (int k = 0; k < 16; k++)
            {
                double randomDouble = random.NextDouble();
                int randomInt = Convert.ToInt32(Math.Floor(randomDouble * 25));
                char letter = Convert.ToChar(randomInt + 65);
                result.Append(letter);
            }

            result.Append(".");
            result.Append(extension);
            return result.ToString();
        }
    }
}