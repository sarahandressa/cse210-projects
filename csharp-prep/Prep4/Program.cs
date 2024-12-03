using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        
        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());

            if (number == 0)
            {
                break; 
            }

            numbers.Add(number);
        }

        // Core Requirements
        
        int sum = numbers.Sum();

        
        double average = numbers.Average();

        
        int max = numbers.Max();

        
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenges
        // 1. Find the smallest positive number
        int? smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty().Min();

        if (smallestPositive.HasValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive.Value}");
        }

        // 2. Sort the numbers and display them
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
