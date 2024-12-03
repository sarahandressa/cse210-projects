using System;

class Program
{
    static void Main()
    {
        
        Console.Write("Enter your grade percentage: ");
        int percentage = int.Parse(Console.ReadLine());

        
        string letter = "";
        string sign = "";

        
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        
        if (letter != "F" && percentage >= 60)
        {
            int lastDigit = percentage % 10;

            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        
        if (letter == "A" && sign == "+")
        {
            sign = ""; // No A+
        }

        if (letter == "F")
        {
            sign = ""; // No F+ or F-
        }

       
        Console.WriteLine($"Your grade is {letter}{sign}.");

        
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the class.");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep trying, and you'll succeed next time.");
        }
    }
}