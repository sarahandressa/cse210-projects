using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Showing Creativity and Exceeding Requirements - Create a library of scriptures
        var scriptureLibrary = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6), 
                          "Trust in the Lord with all thine heart and lean not unto thine own understanding"),
            new Scripture(new Reference("John", 3, 16), 
                          "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"),
            new Scripture(new Reference("Psalms", 23, 1), 
                          "The Lord is my shepherd I shall not want"),
            new Scripture(new Reference("Isaiah", 40, 31), 
                          "But they that wait upon the Lord shall renew their strength they shall mount up with wings as eagles they shall run and not be weary and they shall walk and not faint"),
            new Scripture(new Reference("Matthew", 5, 16), 
                          "Let your light so shine before men that they may see your good works and glorify your Father which is in heaven")
        };

        // Randomly select a scripture from the library
        Random random = new Random();
        Scripture selectedScripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];

        
        while (!selectedScripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(selectedScripture.GetFormattedScripture());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit:");
            string userInput = Console.ReadLine()?.Trim().ToLower();

            if (userInput == "quit")
                break;

            selectedScripture.HideRandomWords();
        }

        
        Console.Clear();
        Console.WriteLine(selectedScripture.GetFormattedScripture());
        Console.WriteLine("\nAll words are hidden. Program ended.");
    }
}

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = verse;
        EndVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null
            ? $"{Book} {Chapter}:{StartVerse}"
            : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

class Word
{
    private string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string GetDisplayText()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    private Reference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string GetFormattedScripture()
    {
        string scriptureText = string.Join(" ", Words.Select(word => word.GetDisplayText()));
        return $"{Reference}\n{scriptureText}";
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        var visibleWords = Words.Where(word => !word.IsHidden).ToList();

        if (visibleWords.Count > 0)
        {
            int wordsToHide = Math.Min(3, visibleWords.Count);
            for (int i = 0; i < wordsToHide; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(word => word.IsHidden);
    }
}