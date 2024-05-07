using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace FlappyBird.DataModels;

public static class Ga {
    private static readonly Random Rd = new Random();
    
    public static void PopulateNewGeneration(List<Tuple<Image, Bird>> birds) {
        var bestBirds = birds.Select(bird => bird.Item2).OrderBy(bird => bird.Score).Reverse().Take(5).ToArray();

        Console.WriteLine($"Best Bird: {bestBirds[0].Score}");
        
        for (var i = 0; i < birds.Count; i++) {
            var parent = bestBirds[Rd.Next(0, bestBirds.Length - 1)];
            parent.Mutate();
        }
    }
}