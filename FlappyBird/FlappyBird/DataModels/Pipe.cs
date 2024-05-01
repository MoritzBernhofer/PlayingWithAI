using System;

namespace FlappyBird.DataModels;

public class Pipe(double x, double distance) {
    private const double Speed = 8;
    public double X { get; private set; } = x;

    public readonly double Distance = distance;

    public void Update() {
        X -= Speed;
        
        if (X <= 00)
            X = 2100;
    }
}