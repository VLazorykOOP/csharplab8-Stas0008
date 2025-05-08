using System.Text.RegularExpressions;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

void Task1()
{
	try
	{
        string inputPath = "task1.txt";
        string outputPath = "output.txt";

        string text = File.ReadAllText(inputPath);

        Console.WriteLine(text);

        string pattern = @"\b[\w.-]+(?:\.[\w.-]+)*\.com\b";

        // Пояснення:
        // \b — межа слова (щоб не включати символів перед або після адреси);
        // [\w.-]+ — слово, що містить літери, цифри, підкреслення, крапки, дефіси;
        // (?:\.[\w.-]+)* — дозволяє піддомени (news.bbc, my.site);
        // \.com — кінець адреси;
        // \b — межа слова після .com.

        MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

        List<string> addresses = new List<string>();
        foreach (Match match in matches)
            addresses.Add(match.Value);

        Console.WriteLine("\nЗнайдені адреси: ");
        foreach (var ad in addresses)
            Console.WriteLine(ad);

        File.WriteAllLines(outputPath, addresses);

        Console.WriteLine($"\nЗнайдено {addresses.Count} .com адрес.");

        Console.Write("Введи адресу для заміни: ");
        string toReplace = Console.ReadLine();

        Console.Write("Введи нову адресу: ");
        string replacement = Console.ReadLine();

        text = Regex.Replace(text, Regex.Escape(toReplace), replacement, RegexOptions.IgnoreCase);

        Console.WriteLine("\nОновнений текст: ");
        Console.WriteLine(text);

        File.WriteAllText("updated.txt", text);
    }
	catch (Exception ex)
	{
        Console.WriteLine("Помилка: " + ex.Message);
    }
}
void Task2()
{
    string inputFile = "task2.txt";
    string outputFile = "output.txt";

    string vowelPattern = @"\b[АЕЄИІЇОУЮЯаеиієїоуюя]\w*\b";

    try
    {
        string text = File.ReadAllText(inputFile);

        Console.WriteLine(text);

        string result = Regex.Replace(text, vowelPattern, match =>
        {
            return char.IsUpper(match.Value[0]) ? "" : match.Value;
        });

        Console.WriteLine("\nРезультат: ");
        Console.WriteLine(result);

        File.WriteAllText(outputFile, result);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка: " + ex.Message);
    }
}
void Task3()
{
    string file1 = "task3_1.txt";
    string file2 = "task3_2.txt";
    string output = "output.txt";

    try
    {
        string text1 = File.ReadAllText(file1);
        string text2 = File.ReadAllText(file2);

        string[] words1 = Regex.Split(text1.ToLower(), @"[\W\d]+");
        string[] words2 = Regex.Split(text2.ToLower(), @"[\W\d]+");

        HashSet<string> set1 = new HashSet<string>(words1);
        HashSet<string> set2 = new HashSet<string>(words2);

        Console.WriteLine("Перший: ");
        foreach (var s in set1)
            Console.Write($"{s} ");

        Console.WriteLine("\nДругий: ");
        foreach (var s in set2)
            Console.Write($"{s} ");

        set1.IntersectWith(set2);

        Console.WriteLine("\nРезультат: ");
        foreach (var s in set1)
            Console.Write($"{s} ");

        File.WriteAllText(output, string.Join(" ", set1));

    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка: " + ex.Message);
    }
}
void Task4()
{
    string fileName = "task4.dat";

    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
    {
        for (int i = 0; i < 10; i++)
        {
            int power = (int)Math.Pow(3, i);
            writer.Write(power);
        }
    }

    Console.WriteLine("Елементи з парними номерами:");
    using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
    {
        int index = 0;
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            int number = reader.ReadInt32();
            if (index % 2 == 0)
            {
                Console.WriteLine($"[{index}] = {number}");
            }
            index++;
        }
    }
}
void Task5()
{
    string basePath = @"D:\temp";
    string dir1 = Path.Combine(basePath, "Antimichuk1");
    string dir2 = Path.Combine(basePath, "Antimichuk2");
    string allDir = Path.Combine(basePath, "ALL");

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    Directory.CreateDirectory(dir1);
    Directory.CreateDirectory(dir2);

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    string t1Path = Path.Combine(dir1, "t1.txt");
    File.WriteAllText(t1Path, "Stas Antmichuk, 2003 року народження, місце проживання м. Чернівці.");

    string t2Path = Path.Combine(dir1, "t2.txt");
    File.WriteAllText(t2Path, "Mercedes-Benz SL55 AMG , 2008 року, знаходиться у м. Київ.");

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    string t3Path = Path.Combine(dir2, "t3.txt");
    string text1 = File.ReadAllText(t1Path);
    string text2 = File.ReadAllText(t2Path);
    File.WriteAllText(t3Path, text1 + Environment.NewLine + text2);

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    Console.WriteLine("Інформація про файли в Antimichuk1:");
    PrintFileInfo(t1Path);
    PrintFileInfo(t2Path);

    Console.WriteLine("\nІнформація про файл t3.txt у Antimichuk2:");
    PrintFileInfo(t3Path);

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    string newT2Path = Path.Combine(dir2, "t2.txt");
    File.Move(t2Path, newT2Path);

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    string newT1Path = Path.Combine(dir2, "t1.txt");
    File.Copy(t1Path, newT1Path);

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    if (Directory.Exists(allDir)) Directory.Delete(allDir, true);
    Directory.Move(dir2, allDir);
    Directory.Delete(dir1, true);

    Console.WriteLine("\nPress any key to continue...\n");
    ReadKey();

    Console.WriteLine("\nФайли у папці ALL:");
    string[] files = Directory.GetFiles(allDir);
    foreach (string file in files)
    {
        PrintFileInfo(file);
    }
}

static void PrintFileInfo(string path)
{
    FileInfo fi = new FileInfo(path);
    Console.WriteLine($"Файл: {fi.Name}");
    //Console.WriteLine($"  Повний шлях: {fi.FullName}");
    Console.WriteLine($"  Розмір: {fi.Length} байт");
    Console.WriteLine($"  Дата створення: {fi.CreationTime}");
    //Console.WriteLine($"  Дата останнього доступу: {fi.LastAccessTime}");
    //Console.WriteLine($"  Дата останньої модифікації: {fi.LastWriteTime}");
    Console.WriteLine();
}

Task5();