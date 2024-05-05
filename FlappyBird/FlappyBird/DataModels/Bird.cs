using System;
using FlappyBird.ViewModels;

namespace FlappyBird.DataModels;

public class Bird(double x, double y, MainWindowViewModel model)  {
    private double _gravity = -15;
    private readonly Nn _neuralNetwork = new Nn([4, 6, 2]);
    public double X { get; } = x;

    public double Y { get;  private set; } = y;

    public bool IsFalling => _gravity < 0;
    public bool IsAlive { get;  set; } = true;
    public int Score { get; set; }

    private void Flap() {
        if (_gravity < 0)
            _gravity = 25;
    }

    public void Think() {
        var inputValues = model.GetValuesForBird(this);
        var output = _neuralNetwork.Classify(inputValues);

        if (output[0] > output[1]) {
            Flap();
        }
    }

    public void Update() {
        Y += _gravity;

        if (_gravity > -15) {
            _gravity -= 2.5;
        }
    }

    public void Mutate() {
        _neuralNetwork.Mutate(0.01);
    }
    

    public void ResetBird() {
        Y = y;
        IsAlive = true;
        Score = 0;
    }
}