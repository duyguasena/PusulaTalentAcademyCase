using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class LongestVowelSubsequenceAsJson
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || !words.Any())
        {
            return "[]";
        }

        var results = new List<object>();
        char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

        foreach (var word in words)
        {
            string maxVowelSequence = "";
            string currentVowelSequence = "";

            if (string.IsNullOrEmpty(word))
            {
                results.Add(new { word = "", sequence = "", length = 0 });
                continue;
            }

            foreach (char c in word.ToLower())
            {
                if (vowels.Contains(c))
                {
                    currentVowelSequence += c;
                }
                else
                {
                    if (currentVowelSequence.Length > maxVowelSequence.Length)
                    {
                        maxVowelSequence = currentVowelSequence;
                    }
                    currentVowelSequence = "";
                }
            }

            
            if (currentVowelSequence.Length > maxVowelSequence.Length)
            {
                maxVowelSequence = currentVowelSequence;
            }

            results.Add(new
            {
                word = word,
                sequence = maxVowelSequence,
                length = maxVowelSequence.Length
            });
        }

        return JsonSerializer.Serialize(results);
    }
}