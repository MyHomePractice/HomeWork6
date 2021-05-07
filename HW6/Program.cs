using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace HW6
{
    class Program
    {
        public static int GetN()
        {
            string nText = File.ReadAllText(Directory.GetCurrentDirectory()+@"\N.txt");
            int n = 0;
            int.TryParse(nText, out n);
            return n;
        }
        public static int[] Arr(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            return arr;
        }
        //public static int GCF(int a, int b) //Алгоритм Евклида
        //{
        //    while (a != b)
        //    {
        //        if (a > b)
        //            a -= b;
        //        else
        //            b -= a;
        //    }
        //    return a;
        //}

        public static string GroupFinal(int ngr, int n)
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            int j = 1;
            for (int k = 0; k < ngr; k++)
            {
                while (Math.Log2(i) < j && i <= n)
                {
                    sb.Append($"{i}  ");
                    i++;
                }
                j++;
                sb.AppendLine();
                sb.AppendLine();
            }
            string sb1 = sb.ToString();
            return sb1;
        }

        public static void moreThen100_000_000(int ngr, int n)
        {
            int i = 1;
            int j = 1;
            using (StreamWriter sr = new StreamWriter($"groupsOfNumbTo{n}.txt"))
            {
                for (int k = 0; k < ngr; k++)
                {
                    StringBuilder sb = new StringBuilder();
                    while (Math.Log2(i) < j && i <= n)
                    {
                        sr.Write($"{i} ");
                        //sb.Append($"{i}  ");
                        i++;
                    }
                    j++;
                    //sb.AppendLine();
                    //sb.AppendLine();
                    sr.WriteLine();
                    sr.WriteLine();
                    //sr.WriteLine(sb);
                }
            }
        }

        public static int GroupsCount(int n)
        {
            int count = (int)Math.Log(n, 2) + 1;
            return count;
        }

        static void Main(string[] args)
        {
            int n = GetN();
            int ngr = GroupsCount(n);
            int choice;
            char key = 'y';
            char key2 = 'н';
            string menu = $"Число из файла = {n}\n" +
                          $"1 - рассчитать количество групп\n" +
                          "2 - показать группы в консоли\n" +
                          "3 - записать группы в файл\n" +
                          "4 - архивировать полученный файл (для архивации сначала необходимо записать файл на диск)";
            do
            {
                Console.Clear();
                Console.WriteLine(menu);
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine($"Количество групп на которые будет разбита последовательность: {ngr}");
                        Console.WriteLine("Выйти в меню? y/n");
                        key = Console.ReadKey(true).KeyChar;
                        key2 = key;
                        break;
                    case 2:
                        Console.Clear();
                        if (n <= 100_000_000)
                            Console.WriteLine(GroupFinal(ngr, n));
                        else
                        {
                            moreThen100_000_000(ngr, n);
                        
                        Console.WriteLine("Лично у меня не хватило оперативной памяти для обработки n-числа больше," +
                                          "поэтому сразу будет вестись запись на диск");
                        }
                        Console.WriteLine("Выйти в меню? y/n");
                        key = Console.ReadKey(true).KeyChar;
                        key2 = key;
                        break;
                    case 3:
                        TimeSpan timetodo = new TimeSpan();
                        Console.Clear();
                        if (n <= 100_000_000)
                        {
                            DateTime nowDateTime = new DateTime();
                            nowDateTime = DateTime.Now;
                            using (StreamWriter sr = new StreamWriter($"groupsOfNumbTo{n}.txt"))
                            {
                                Console.WriteLine("Запись на диск начата");

                                sr.WriteLine(GroupFinal(ngr, n));

                                Console.Write("Запись на диск закончена. Путь к файлу: ");
                                FileInfo pathToFinalGroups = new FileInfo($"groupsOfNumbTo{n}.txt");
                                Console.WriteLine(pathToFinalGroups.DirectoryName);
                            }

                            DateTime nowDateTime1 = new DateTime();
                            nowDateTime1 = DateTime.Now;
                            timetodo = nowDateTime1 - nowDateTime;
                            Console.WriteLine($"Времени ушло на обработку: {timetodo}");
                        }
                        else
                        {
                            DateTime nowDateTime = new DateTime();
                            nowDateTime = DateTime.Now;
                            moreThen100_000_000(ngr, n);
                            DateTime nowDateTime1 = new DateTime();
                            nowDateTime1 = DateTime.Now;
                            timetodo = nowDateTime1 - nowDateTime;
                            Console.WriteLine($"Времени ушло на обработку: {timetodo}");
                        }

                        Console.WriteLine("Выйти в меню? y/n");
                        key = Console.ReadKey(true).KeyChar;
                        key2 = key;
                        break;
                    case 4:
                        using (FileStream read = new FileStream($"groupsOfNumbTo{n}.txt", FileMode.Open))
                        {
                            using (FileStream writer = File.Create($"groupsOfNumbTo{n}.zip"))
                            {
                                using (GZipStream zip = new GZipStream(writer,
                                    CompressionMode.Compress))
                                {
                                    read.CopyTo(zip);
                                    Console.WriteLine($"Занимало место на диске: {read.Length} б");
                                    Console.WriteLine($"После архивации: {writer.Length} б");
                                }
                            }
                        }
                        Console.WriteLine("Выйти в меню? y/n");
                        key = Console.ReadKey(true).KeyChar;
                        key2 = key;
                        break;

                }
            } while (char.ToLower(key) == 'y' || char.ToLower(key2) == 'н');

        }
    }
}