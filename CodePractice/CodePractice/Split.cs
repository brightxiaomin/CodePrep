char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

string text = "one\ttwo :,five six seven";
System.Console.WriteLine($"Original text: '{text}'");

string[] words = text.Split(delimiterChars,System.StringSplitOptions.RemoveEmptyEntries);
System.Console.WriteLine($"{words.Length} words in text:");

foreach (var word in words)
{
    System.Console.WriteLine($"<{word}>");
}


var arr = lower.Split(new[] {' ', ',', '.', ':', '!', '?', '\''} ,  StringSplitOptions.RemoveEmptyEntries).Where(k=>toys.Contains(k));