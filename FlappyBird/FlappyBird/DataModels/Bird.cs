using System;
using System.Collections.Generic;
using System.Linq;
using FlappyBird.ViewModels;

namespace FlappyBird.DataModels;

public class Bird(double x, double y) {
    private double _gravity = -15;
    private readonly Nn _neuralNetwork = new Nn([4, 6, 2]);
    public double X { get; } = x;

    public double Y { get; private set; } = y;

    public bool IsFalling => _gravity < 0;
    public bool IsAlive { get; set; } = true;
    public int Score { get; set; }

    private void Flap() {
        _gravity = 25;
    }

    public void Think(List<Pipe> pipes) {
        var inputValues = GetInputValues(pipes);
        var output = _neuralNetwork.Classify(inputValues);

        if (output[0] > output[1]) {
            Flap();
        }
    }

    private double[] GetInputValues(IReadOnlyCollection<Pipe> pipes) {
        var closestPipeBottom = pipes.Where(pipe => pipe.Type == PipeType.BottomPipe).MinBy(pipe => pipe.X);
        var closestPipeTop = pipes.Where(pipe => pipe.Type == PipeType.TopPipe).MinBy(pipe => pipe.X);

        if (closestPipeBottom == null ||
            closestPipeTop == null)
            return [];

        var gameHeight = 1000;
        var gameWidth = 1778;

        var distanceToNextPipe = (X - closestPipeBottom.X) / gameWidth;
        var pipeHeightBottom = (-300 - closestPipeBottom.DistanceBetween + 655) / gameHeight;
        var pipeHeightTop = (500 + closestPipeTop.DistanceBetween + pipeHeightBottom) / gameHeight;
        var heightBird = Y / gameHeight;

        return [distanceToNextPipe, pipeHeightBottom, pipeHeightTop, heightBird];
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