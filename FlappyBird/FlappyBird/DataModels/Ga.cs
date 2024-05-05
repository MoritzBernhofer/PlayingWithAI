using System;
using System.Collections.Generic;
using System.Linq;

namespace FlappyBird.DataModels;

public class Ga {
    private static Random rd = new Random(DateTime.Today.Millisecond);
    
    public static List<Bird> CreateNewGeneration(List<Bird> birds) {
        var bestBirds = birds.OrderBy(bird => bird.Score).Take(5).ToArray();
        var newBirds = new List<Bird>();

        for (var i = 0; i < birds.Count(); i++) {
            var parent = bestBirds[rd.Next(0, bestBirds.Length - 1)];
            parent.Mutate();
            
            newBirds.Add(parent);
        }

        return newBirds;
    }
}