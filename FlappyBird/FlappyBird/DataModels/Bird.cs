using System;

namespace FlappyBird.DataModels;

public class Bird(double x, double y) {
    private double _gravity = 15;

    public void Flap() {
        if (_gravity > 5)
            _gravity = _gravity * -1 - 10;
    }

    public double X { get; } = x;

    public double Y { get; private set; } = y;

    public bool IsFalling => _gravity > 0;

    public void Update() {
        Y += _gravity;

        if (_gravity < 15) {
            _gravity += 2.5;
        }
    }
}