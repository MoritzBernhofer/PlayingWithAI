using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using FlappyBird.DataModels;
using FlappyBird.Views;

namespace FlappyBird.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public MainWindowViewModel() {
        var rd = new Random(DateTime.Today.Millisecond);

        _pipes.Add(new Pipe(1000, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(1000, PipeType.TopPipe, rd));

        _pipes.Add(new Pipe(1300, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(1300, PipeType.TopPipe, rd));
        
        _pipes.Add(new Pipe(1700, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(1700, PipeType.TopPipe, rd));

        _pipes.Add(new Pipe(2000, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(2000, PipeType.TopPipe, rd));
        
        _pipes.Add(new Pipe(2300, PipeType.BottomPipe, rd));
        _pipes.Add(new Pipe(2300, PipeType.TopPipe, rd));

        //, 655, 360

        const double startX = 100;
        const double startY = 500;

        for (var i = 0; i < 100; i++) {
            _birds.Add(new Bird(startX, startY));
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

    public Tuple<double, double, double, double, double> GetValuesForBird(Bird bird) {
        var heightBird = bird.Y;
        // var distanceNext = 
        return null;
    }
}