using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using FlappyBird.DataModels;
using FlappyBird.Views;

namespace FlappyBird.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private readonly Bird _bird = new Bird(100, 500);

    public Bird Bird => _bird;

    public void UpdateGame() {
        //Update Bird
        _bird.Update();

        //Update Pipes
    }
    
}