using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    protected int duration;
    protected string activityName;
    protected string activityDescription;

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine($"Starting {activityName}...");
        Console.WriteLine(activityDescription);
        Console.WriteLine("Please enter the duration for the activity (in seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get Ready!");
        ShowCountdown(3);
    }

    public void EndActivity()
    {
        Console.WriteLine("Good job! You've completed the activity.");
        ShowCountdown(3);

    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Starting in {i}...");
            Thread.Sleep(1000);
        }
    }

    public abstract void PerformActivity();

}

public class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        activityName = "Breathing Activity";
        activityDescription = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        
    }

    public override void PerformActivity()
    {
        Console.Clear();
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(4);
            Console.WriteLine("Breathe out...");
            ShowCountdown(4);
        }
        EndActivity();
    }
}

public class ReflectionActivity : Activity
{
    private List<string> reflectionPrompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "hink of a time when you did something truly selfless."
    };

    private List<string> reflectionsQuestions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        activityName = "Reflection Activity";
        activityDescription = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";

    }

    public override void PerformActivity()
    {
        Console.Clear();
        DateTime endTime = DateTime.Now.AddSeconds (duration);
        Random random = new Random();

        string prompt = reflectionPrompts[random.Next(reflectionPrompts.Count)];
        Console.WriteLine(prompt);
        ShowCountdown(5);

        while (DateTime.Now < endTime)
        {
            string question = reflectionsQuestions[random.Next(reflectionsQuestions.Count)];
            Console.WriteLine(question);
            ShowCountdown(5);
        }

        EndActivity();
    }
}

public class ListingActivity : Activity
{
    private List<string> listingprompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        activityName = "Listing Activity";
        activityDescription = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void PerformActivity()
    {
        Console.Clear();
        Random random = new Random();
        string prompt = listingprompts[random.Next(listingprompts.Count)];
        Console.WriteLine(prompt);

        ShowCountdown(5);

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Type an item (or press Enter to Stop):");
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
            {
                items.Add(item);
            }
            else
            {
                break;
            }
        }

        Console.WriteLine($"You listed {items.Count} items");
        EndActivity();
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Activities");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");


            Console.WriteLine("Choose an Activity: ");
            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                
                case "2":
                    activity = new ReflectionActivity();
                    break;
                
                case "3":
                    activity = new ListingActivity();
                    break;
                
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;

            }

            activity.StartActivity();
            activity.PerformActivity();
            

        }
    }
}