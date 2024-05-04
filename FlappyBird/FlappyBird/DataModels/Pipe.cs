using System;

namespace FlappyBird.DataModels;

public class Pipe {
    public Pipe(double x, PipeType type, Random rd) {
        X = x;
        Type = type;

        if (type == PipeType.TopPipe) {
            DistanceBetween = rd.Next(50, 150);
        }
    }

    public PipeType Type { get; private set; }

    public double X { get; private set; }

    public double? DistanceBetween { get; }
    private const double Speed = 8;

    public void Update() {
        X -= Speed;

        if (X <= 00)
            X = 2100;
    }
}