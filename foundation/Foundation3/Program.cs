using System;
using System.Collections.Generic;

abstract class Activity
{
    private DateTime _date;
    private int _durationMinutes;

    public Activity(DateTime date, int durationMinutes)
    {
        _date = date;
        _durationMinutes = durationMinutes;
    }

    public DateTime Date => _date;
    public int DurationMinutes => _durationMinutes;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} Activity ({_durationMinutes} min): " + 
               $"Distance: {GetDistance() :0.0} miles, Speed: {GetSpeed() :0.0} mph, Pace: {GetPace() :0.0} min per mile";

    }
}

class Running : Activity
{
    private double _distanceMiles;

    public Running(DateTime date, int durationMinutes, double _distanceMiles)
        : base(date, durationMinutes)
        {
            _distanceMiles = _distanceMiles;
        }

        public override double GetDistance() => _distanceMiles;
        public override double GetSpeed() => (GetDistance() / DurationMinutes) * 60;
        public override double GetPace() => DurationMinutes / GetDistance();

    public override string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} Running ({DurationMinutes} min): " + 
               $"Distance: {GetDistance() :0.0} miles, Speed: {GetSpeed() :0.0} mph, Pace: {GetPace() :0.0} min per mile";
    }

}

class Cycling : Activity
{
    private double _speedMph;

    public Cycling(DateTime date, int durationMinutes, double speedMph)
        : base(date, durationMinutes)
    {
        _speedMph = speedMph;
    }

    public override double GetDistance() => (_speedMph * DurationMinutes) / 60;
    public override double GetSpeed() => _speedMph;
    public override double GetPace() => 60 / _speedMph;

    public override string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} Cycling ({DurationMinutes} min): " + 
               $"Distance: {GetDistance() :0.0} miles, Speed: {GetSpeed() :0.0} mph, Pace: {GetPace() :0.0} min per mile";
    }
}

class Swimming : Activity
{
    private int _laps;
    public Swimming(DateTime date, int durationMinutes, int laps)
        : base(date, durationMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50 / 1000.0) * 0.62;
    public override double GetSpeed() => (GetDistance() / DurationMinutes) * 60;
    public override double GetPace() => DurationMinutes / GetDistance();

    public override string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} Swimming ({DurationMinutes} min): " + 
               $"Distance: {GetDistance() :0.0} miles, Speed: {GetSpeed() :0.0} mph, Pace: {GetPace() :0.0} min per mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 40, 15.0),
            new Swimming(new DateTime(2022, 11, 5), 25, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}