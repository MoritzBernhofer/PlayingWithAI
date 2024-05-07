using System;
using System.Collections.Generic;
using System.Linq;
using FlappyBird.DataModels;

namespace FlappyBird.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private readonly List<Pipe> _pipes = [];

    private readonly List<Bird> _birds = [];

    public IEnumerable<Bird> Birds => _birds;

    public IEnumerable<Pipe> Pipes => _pipes;
    public MainWindowViewModel() {
        var rd = new Random();
        
        int[] xPositions = [1000, 1300, 1700, 2000, 2300];

        foreach (var xPos in xPositions)
        {
            _pipes.Add(new Pipe(xPos, PipeType.BottomPipe, rd));
            _pipes.Add(new Pipe(xPos, PipeType.TopPipe, rd));
        }
        
        const double startX = 100;
        const double startY = 500;

        for (var i = 0; i < 100; i++) {
            _birds.Add(new Bird(startX, startY + rd.Next(10, 20)));
        }
    }
    
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
            pipe.ResetPipe();
        }

        foreach (var bird in _birds) {
            bird.ResetBird();
        }
    }

    public void LetBirdsThink() {
        foreach (var bird in _birds) {
            bird.Think(_pipes);
        }
    }
}