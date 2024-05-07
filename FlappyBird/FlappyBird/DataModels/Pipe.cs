using System;

namespace FlappyBird.DataModels;

public class Pipe(double x, PipeType type, Random rd) {
    private const double Speed = 8;
    private readonly double _startPos = x;
    public PipeType Type { get; } = type;

    public double X { get; private set; } = x;
    public double DistanceBetween { get; } = rd.Next(25, 75);

    public void Update() {
        X -= Speed;

        if (X <= 00)
            X = 2100;
    }

    public void ResetPipe() {
        X = _startPos;
    }
}