using System;
using System.IO;
using System.Net;


namespace ConvertIpToDecimal
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = @"d:\temp\ip\ipList.txt"; // путь к входному текстовому файлу
            string outputFilePath = @"d:\temp\ip\output_ipList.txt"; // путь к выходному текстовому файлу

            try
            {
                // Читаем все строки из входного файла
                string[] lines = File.ReadAllLines(inputFilePath);

                using (StreamWriter writer = new StreamWriter(outputFilePath)) // Открываем StreamWriter для записи в выходной файл
                {
                    // Записываем заголовок в выходной файл
                    writer.WriteLine("IP-адрес\tДесятичное представление");
                    writer.WriteLine(new string('-', 70));

                    foreach (var line in lines)
                    {
                        
                            string ip = line.Trim(); // Получаем третий элемент и удаляем лишние пробелы

                            if (IPAddress.TryParse(ip, out IPAddress ipAddress))
                            {
                                // Получаем байты IP-адреса
                                byte[] bytes = ipAddress.GetAddressBytes();

                                // Преобразуем в десятичное значение с учетом порядка байтов
                                uint decimalIp = (uint)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);

                                // Формируем новую строку с добавленным десятичным значением
                                string resultLine = $"{line}|{decimalIp}";                            // Записываем результат в файл
                                writer.WriteLine(resultLine);

                            }
                            else
                            {
                                writer.WriteLine($"{line}\t{ip}\tНеверный IP");
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

        }
    
    }
}
