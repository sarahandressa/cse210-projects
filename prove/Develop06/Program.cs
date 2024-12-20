using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; private set; }
    public int Points { get; private set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetProgress();

}

class SimpleGoal : Goal
{
   private bool completed;

   public SimpleGoal(string name, int points) : base(name, points)
   {
        completed = false;

   }

    public override void RecordEvent()
    {
        completed = true;
    }

    public override bool IsComplete()
    {
        return completed;
    }

    public override string GetProgress()
    {
        return completed ? "[X]" : "[]";
    }
}

class EternalGoal : Goal
{
    public int TimesRecorded { get; private set; }
    public EternalGoal(string name, int points) : base(name, points)
    {
        TimesRecorded = 0;
    }
    public override void RecordEvent()
    {
        TimesRecorded++;
    }

    public override bool IsComplete()
    {
        return false;
    }
    public override string GetProgress()
    {
        return $"Completed {TimesRecorded} times";
    }
}

class ChecklistGoal : Goal
{
    public int Target { get; private set; }
    public int Current { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, int points, int target, int bonusPoints) : base(name, points)
    {
        Target = target;
        Current = 0;
        BonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        if (Current < Target)
        {
            Current++;
        }
    }

    public override bool IsComplete()
    {
        return Current >= Target;
    }

    public override string GetProgress()
    {
        return $"Completed {Current}/{Target} times";
    }
}

class EternalQuestProgram
{
    private List<Goal> goals = new List<Goal>();
    private int totalScore = 0;

    public void Run()
    {
        string choice;

        do
        {
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record an event");
            Console.WriteLine("3. Show goals");
            Console.WriteLine("4. Save goals");
            Console.WriteLine("5. Load goals");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Choose an option: ");

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    ShowGoals();
                    break;
                case "4":
                    SaveGoals();
                    break;
                case "5":
                    LoadGoals();
                    break; 
            }

            Console.WriteLine();
        
        }while (choice !="6");   
    }

    private void CreateGoal()
    {
        Console.WriteLine("Choose a goal type:" );
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("Enter Choice: ");

        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, points));
                break;
            case "3":
                Console.Write("Enter target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonuspoints = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, points, target, bonuspoints));
                break;
        }

        Console.WriteLine("Goal created!");

    }

    private void RecordEvent()
    {
        ShowGoals();
        Console.Write("Enter the number of the goal to record: ");
        int index = int.Parse(Console.ReadLine()) -1;

        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordEvent();
            totalScore += goals[index].Points;

            if (goals[index] is ChecklistGoal checklist && checklist.IsComplete())
            {
                totalScore += checklist.BonusPoints;
            }

            Console.WriteLine("Event recorded!");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    private void ShowGoals()
    {
        Console.WriteLine("YourGoals:");
        for (int i = 0; i < goals.Count; i++)
        {
            var goal = goals[i];
            Console.WriteLine($"{i + 1}. {goal.GetProgress()} {goal.Name}");

        }

        Console.WriteLine($"Total Score: {totalScore}");
    }

    private void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(totalScore);
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.GetType().Name);
                writer.WriteLine(goal.Name);
                writer.WriteLine(goal.Points);
                if (goal is EternalGoal eternal)
                {
                    writer.WriteLine(eternal.TimesRecorded);

                }
                else if (goal is ChecklistGoal checklist)
                {
                    writer.WriteLine(checklist.Current);
                    writer.WriteLine(checklist.Target);
                    writer.WriteLine(checklist.BonusPoints);
                }   
            }
        }

        Console.WriteLine("Goals saved!");
    }

    private void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            goals.Clear();
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                totalScore = int.Parse(reader.ReadLine());

                while (!reader.EndOfStream)
                {
                    string type = reader.ReadLine();
                    string name = reader.ReadLine();
                    int points = int.Parse(reader.ReadLine());

                    if (type == "EternalGoal")
                    {
                        int TimesRecorded = int.Parse(reader.ReadLine());
                        EternalGoal eternal = new EternalGoal (name, points);
                        for (int i = 0; i < TimesRecorded; i++)
                        {
                            eternal.RecordEvent();
                        }
                        goals.Add(eternal);
                    }
                    else if (type == "ChecklistGoal")
                    {
                        int current = int.Parse(reader.ReadLine());
                        int target = int.Parse(reader.ReadLine());
                        int bonusPoints = int.Parse(reader.ReadLine());
                        ChecklistGoal checklist = new ChecklistGoal (name, points, target, bonusPoints);
                        for (int i = 0; i < current; i++)
                        {
                            checklist.RecordEvent();
                        }
                        goals.Add(checklist);
                    }
                    else if (type == "SimpleGoal")
                    {
                        SimpleGoal simple = new SimpleGoal(name, points);
                        simple.RecordEvent();
                        goals.Add(simple);
                    }
                }
            }

            Console.WriteLine("Goals loaded!");
        }

        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }

    static void Main(string[] args)
    {
        EternalQuestProgram program = new EternalQuestProgram();
        program.Run();
    }

}