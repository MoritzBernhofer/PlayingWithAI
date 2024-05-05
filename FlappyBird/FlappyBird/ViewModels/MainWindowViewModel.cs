using System;
using System.Collections.Generic;
using System.Linq;
using FlappyBird.DataModels;

namespace FlappyBird.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public MainWindowViewModel() {
        var rd = new Random(DateTime.Today.Millisecond);

        _pipes.Add(new Pipe(1000, 1000, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(1000, 1000, PipeType.TopPipe, rd));

        _pipes.Add(new Pipe(1300, 1300, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(1300, 1300, PipeType.TopPipe, rd));

        _pipes.Add(new Pipe(1700, 1700, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(1700, 1700, PipeType.TopPipe, rd));

        _pipes.Add(new Pipe(2000, 2000, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(2000, 2000, PipeType.TopPipe, rd));

        _pipes.Add(new Pipe(2300, 2300, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(2300, 2300, PipeType.TopPipe, rd));

        //, 655, 360

        const double startX = 100;
        const double startY = 500;

        for (var i = 0; i < 100; i++) {
            _birds.Add(new Bird(startX, startY + rd.Next(10, 20), this));
        }
    }

    private readonly List<Pipe> _pipes = [];

    private readonly List<Bird> _birds = [];

    public IEnumerable<Bird> Birds => _birds;

    public IEnumerable<Pipe> Pipes => _pipes;

    public void UpdateGame() {
        //Update Birds
        foreach (var bird in Birds) {
            if (bird.IsAlive)
                bird.Update();
        }

        //Update Pipes
        foreach (var pipe in _pipes) {
            pipe.Update();
        }
    }

    public void ResetGame() {
        foreach (var pipe in _pipes) {
            pipe.SetToStartPos();
        }

        foreach (var bird in _birds) {
            bird.ResetBird();
        }
    }
    

    public double[] GetValuesForBird(Bird bird) {
        var closestPipeBottom = _pipes.Where(pipe => pipe.Type == PipeType.BottomPipe).MinBy(pipe => pipe.X);
        var closestPipeTop = _pipes.Where(pipe => pipe.Type == PipeType.TopPipe).MinBy(pipe => pipe.X);

        if (closestPipeBottom == null ||
            closestPipeTop?.DistanceBetween == null)
            return [];

        var heightBird = bird.Y;
        var distanceNextBottom = Math.Sqrt(Math.Pow(closestPipeBottom.X - bird.X, 2) +
                                           Math.Pow(-300 - bird.Y, 2));
        var distanceNextTop = Math.Sqrt(Math.Pow(closestPipeTop.X - bird.X, 2) +
                                        Math.Pow(500 + closestPipeTop.DistanceBetween - bird.Y, 2));

        var pipeHeightBottom = -300 - closestPipeBottom.DistanceBetween;


        return [heightBird, distanceNextBottom, distanceNextTop, pipeHeightBottom];
    }
}