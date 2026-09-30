// Lab 3
// Student name: Eeshal Fatima
// Student number: 76020494

using System;
using System.Collections.Generic;

string userName = "Eeshal Fatima";
Console.WriteLine($"CPEN223 Lab 3 for user: {userName}");

//Testing: Write some test cases to test well all methods you are to implement    
//         This is to demonstrates what test cases you have considered
//TODO 
bool actual = SensorAnalyzer.IsUsableReading(21.5, 0.0, 50.0);
Console.WriteLine($"Expected: True, Actual: {actual}");

actual = SensorAnalyzer.IsUsableReading(-2.0, 0.0, 50.0);
Console.WriteLine($"Expected: False, Actual: {actual}");

List<double> readings = new List<double> { 20.0, double.NaN, 20.5, double.PositiveInfinity, -5.0, 21.0 };
List<double> cleaned = SensorAnalyzer.CleanReadings(readings, 0.0, 50.0);
Console.WriteLine($"Expected: 20, 20.5, 21, Actual: {string.Join(", ", cleaned)}");

List<double> values = new List<double> { 0.1 + 0.2 };
actual = SensorAnalyzer.ContainsApproximately(values, 0.3, 1e-12);
Console.WriteLine($"Expected: True, Actual: {actual}");

List<double> numbers = new List<double> { 1.0, 2.0, 3.0, 4.0 };
List<double> averages = SensorAnalyzer.MovingAverage(numbers, 2);
Console.WriteLine($"Expected: 1.5, 2.5, 3.5, Actual: {string.Join(", ", averages)}");

List<double> shortList = new List<double> { 2.0, 4.0, 6.0 };
List<double> emptyAverage = SensorAnalyzer.MovingAverage(shortList, 4);
Console.WriteLine($"Expected: , Actual: {string.Join(", ", emptyAverage)}");


//end Testing code

//Do not change the program skeleton
public static class SensorAnalyzer
{
    public static bool IsUsableReading(
        double reading, double minimum, double maximum)
    {
        if (!double.IsFinite(minimum) || !double.IsFinite(maximum) || minimum > maximum)
        {
            throw new ArgumentException();
        }

        if (!double.IsFinite(reading))
        {
            return false;
        }

        return reading >= minimum && reading <= maximum;
    }

    public static List<double> CleanReadings(
        IReadOnlyList<double> readings, double minimum, double maximum)
    {
        if (readings == null || !double.IsFinite(minimum) || !double.IsFinite(maximum) || minimum > maximum)
        {
            throw new ArgumentException();
        }

        List<double> cleaned = new List<double>();

        for (int i = 0; i < readings.Count; i++)
        {
            if (IsUsableReading(readings[i], minimum, maximum))
            {
                cleaned.Add(readings[i]);
            }
        }

        return cleaned;
    }

    public static bool ContainsApproximately(
        IReadOnlyList<double> readings, double target, double tolerance)
    {
        if (readings == null || !double.IsFinite(target) || !double.IsFinite(tolerance) || tolerance < 0)
        {
            throw new ArgumentException();
        }

        for (int i = 0; i < readings.Count; i++)
        {
            if (double.IsFinite(readings[i]) && Math.Abs(readings[i] - target) <= tolerance)
            {
                return true;
            }
        }

        return false;
    }

    public static List<double> MovingAverage(
        IReadOnlyList<double> readings, int windowSize)
    {
        if (readings == null || windowSize <= 0)
        {
            throw new ArgumentException();
        }

        for (int i = 0; i < readings.Count; i++)
        {
            if (!double.IsFinite(readings[i]))
            {
                throw new ArgumentException();
            }
        }

        List<double> averages = new List<double>();

        if (windowSize > readings.Count)
        {
            return averages;
        }

        for (int i = 0; i <= readings.Count - windowSize; i++)
        {
            double sum = 0;

            for (int j = 0; j < windowSize; j++)
            {
                sum += readings[i + j];
            }

            averages.Add(sum / windowSize);
        }

        return averages;
    }
}