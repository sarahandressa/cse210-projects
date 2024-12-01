using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>

/// </summary>
public class Entry
{
    public string Prompt { get; set; } 
    public string Response { get; set; } 
    public string Date { get; set; } 
    public string Category { get; set; } // A category for the entry (e.g., "Happiness", "Reflection")

    /// <summary>
    
    /// </summary>
    public Entry(string prompt, string response, string date, string category)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        Category = category;
    }

    /// <summary>
    
    /// </summary>
    public override string ToString()
    {
        return $"{Date} | {Category} | {Prompt} | {Response}";
    }
}

/// <summary>

/// </summary>
public class Journal
{
    
    private List<Entry> entries = new List<Entry>();

    
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    /// <summary>
    
    /// </summary>
    public void WriteEntry()
    {
        
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Console.Write("Enter a category for this entry (e.g., Happiness, Reflection): ");
        string category = Console.ReadLine();

        
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        
        entries.Add(new Entry(prompt, response, date, category));

        Console.WriteLine("Entry saved successfully!");
    }

    /// <summary>
    
    /// </summary>
    public void DisplayEntries()
    {
        Console.WriteLine("\nJournal Entries:");
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries found."); 
        }
        else
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }
    }

    /// <summary>
    /// Saves the journal entries to a file in JSON format.
    /// </summary>
    public void SaveToFile(string filename)
    {
        try
        {
            // Serialize the entries list to JSON and save it to a file
            string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);
            Console.WriteLine("Journal saved successfully!");
            Console.WriteLine("Take a moment to reflect on your day!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads journal entries from a file in JSON format.
    /// </summary>
    public void LoadFromFile(string filename)
    {
        try
        {
            if (File.Exists(filename))
            {
                // Read the file and deserialize the JSON into the entries list
                string json = File.ReadAllText(filename);
                entries = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
                Console.WriteLine("Journal loaded successfully!");
                Console.WriteLine("Now's a great time to write something new!");
            }
            else
            {
                Console.WriteLine("File not found. No entries loaded.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }
}

/// <summary>
/// The main program class that provides the user interface and menu for interacting with the journal.
/// Exceeds requirements by including additional functionality such as categories for entries and JSON storage.
/// </summary>
class Program
{
    static void Main()
    {
        Journal journal = new Journal(); 
        bool running = true;

        while (running)
        {
            
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter filename to save (e.g., journal.json): ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.Write("Enter filename to load (e.g., journal.json): ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    running = false; // Exit the program
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        
        Console.WriteLine("Thank you for using the Journal App. Goodbye!");
    }
}