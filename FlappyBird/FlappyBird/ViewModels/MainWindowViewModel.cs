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
        
        _pipes.Add(new Pipe(1000, rd.Next(200, 350)));
        _pipes.Add(new Pipe(1300, rd.Next(200, 350)));
        _pipes.Add(new Pipe(1700, rd.Next(200, 350)));
        _pipes.Add(new Pipe(2000, rd.Next(200, 350)));
        _pipes.Add(new Pipe(2300, rd.Next(200, 350)));
    }

    private readonly List<Pipe> _pipes = [];

    public Bird Bird { get; } = new Bird(100, 500);

    public IEnumerable<Pipe> Pipes => _pipes;
    public void UpdateGame() {
        //Update Bird
        Bird.Update();

        //Update Pipes
        foreach (var pipe in _pipes) {
            pipe.Update();
        }
    }
    
}