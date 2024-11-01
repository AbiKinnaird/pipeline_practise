using System;
using System.Collections.Generic;

public class BowlingGame
{
    // Main method to calculate the total score
    public int CalculateScore(string input)
    {
        var rolls = ParseInput(input);
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++) // 10 frames in total
        {
            if (IsStrike(rolls, ref rollIndex, ref score)) // Strike
            {
                continue; // Move to the next frame since a strike is one roll
            }
            else if (IsSpare(rolls, ref rollIndex, ref score)) // Spare
            {
                rollIndex += 2; // Move to the next frame (spare is two rolls)
            }
            else // Open frame
            {
                score += rolls[rollIndex] + rolls[rollIndex + 1]; // Direct addition
                rollIndex += 2; // Move to the next frame
            }
        }

        return score;
    }

    // Check if its a strike and add all aditional
    private bool IsStrike(List<int> rolls, ref int rollIndex, ref int score)
    {
        if (rolls[rollIndex] == 10)
        {
            score += 10 + rolls[rollIndex + 1] + rolls[rollIndex + 2]; 
            rollIndex++; 
            return true;
        }
        return false;
    }

    //Check if its a Spare and add additional
    private bool IsSpare(List<int> rolls, ref int rollIndex, ref int score)
    {
        if (rolls[rollIndex] + rolls[rollIndex + 1] == 10)
        {
            score += 10 + rolls[rollIndex + 1]; // Direct addition
            return true;
        }
        return false;
    }

    //Parse the input string
    private List<int> ParseInput(string input)
    {
        var rolls = new List<int>();

        for (int i  = 0; i < input.Length; i++){

        char c = input[i];

        switch(c)
        {
            case 'X':
                rolls.Add(10);
                break;
            case '/':
                rolls.Add(10 - rolls[rolls.Count - 1]); //To calculate the number represented by /
                break;
            case '-':
                rolls.Add(0);
                break;
            default:
                if (char.IsDigit(c)) // Needed to add this check to get it to run
                {
                    rolls.Add(int.Parse(c.ToString()));
                }
                break;
        }

    }
        return rolls;
    }

   
    public static void Main(string[] args)
    {
        BowlingGame game = new BowlingGame();
        string input = "X 7/ 9- X -8 8/ -6 X X X81"; // Sample input
        int score = game.CalculateScore(input);
        Console.WriteLine($"Total: {score}");
    }
}