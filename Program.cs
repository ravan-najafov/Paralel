using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Paralel;
FileStatistics ProcessFile(string filePath)
{
    string text = File.ReadAllText(filePath);
    FileStatistics statistics = new FileStatistics();

    statistics.Characters = text.Length;
    statistics.Words = Regex.Matches(text, @"\b\w+\b").Count;
    statistics.Lines = File.ReadAllLines(filePath).Length;
    statistics.Errors = Regex.Matches(text, @"\berror\b", RegexOptions.IgnoreCase).Count;
    return statistics;
}

string folderPath = @"C:\Users\NITRO\Desktop\Finalka\Paralel\Files";

if (!Directory.Exists(folderPath))
{
    Console.WriteLine("Papka ne naydena: " + folderPath);
    return;
}

string[] files = Directory.GetFiles(folderPath, "*.txt");
FileStatistics[] results = new FileStatistics[files.Length];

Parallel.ForEach(
    Enumerable.Range(0, files.Length),
    i =>
    {
        results[i] = ProcessFile(files[i]);

        Console.WriteLine(Path.GetFileName(files[i]));
        Console.WriteLine($"Characters: {results[i].Characters}");
        Console.WriteLine($"Words: {results[i].Words}");
        Console.WriteLine($"Lines: {results[i].Lines}");
        Console.WriteLine($"Errors: {results[i].Errors}");
        Console.WriteLine();
    });

int totalCharacters = 0;
int totalWords = 0;
int totalLines = 0;
int totalErrors = 0;

foreach (FileStatistics statistics in results)
{
    totalCharacters += statistics.Characters;
    totalWords += statistics.Words;
    totalLines += statistics.Lines;
    totalErrors += statistics.Errors;
}

Console.WriteLine("===== TOTAL =====");
Console.WriteLine();
Console.WriteLine($"Files: {files.Length}");
Console.WriteLine($"Characters: {totalCharacters}");
Console.WriteLine($"Words: {totalWords}");
Console.WriteLine($"Lines: {totalLines}");
Console.WriteLine($"Errors: {totalErrors}");

