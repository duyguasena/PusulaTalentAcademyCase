using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class MaxIncreasingSubArrayAsJson
{
    public static string FindMaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        if (numbers == null || !numbers.Any())
        {
            return "[]";
        }

        List<int> maxSubarray = new List<int>();
        List<int> currentSubarray = new List<int>();

        for (int i = 0; i < numbers.Count; i++)
        {
            if (i == 0 || numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
            }
            else
            {
                if (currentSubarray.Sum() > maxSubarray.Sum())
                {
                    maxSubarray = new List<int>(currentSubarray);
                }
                currentSubarray = new List<int> { numbers[i] };
            }
        }

        
        if (currentSubarray.Sum() > maxSubarray.Sum())
        {
            maxSubarray = new List<int>(currentSubarray);
        }
        
       
        if (!maxSubarray.Any() && numbers.Any())
        {
            return JsonSerializer.Serialize(numbers.OrderByDescending(n => n).Take(1));
        }

        return JsonSerializer.Serialize(maxSubarray);
    }
}