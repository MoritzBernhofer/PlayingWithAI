using System;

namespace FlappyBird.DataModels;

public class Pipe(double x, double StartPos, PipeType type, Random rd) {
    public PipeType Type { get; private set; } = type;

    public double X { get; private set; } = x;

    public double DistanceBetween { get; } = rd.Next(25, 75);
    private const double Speed = 8;

    public void Update() {
        X -= Speed;

        if (X <= 00)
            X = 2100;
    }

    public void SetToStartPos() {
        X = StartPos;
    }
}