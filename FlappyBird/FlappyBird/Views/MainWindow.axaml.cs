using System;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using FlappyBird.DataModels;
using FlappyBird.ViewModels;

namespace FlappyBird.Views;

public partial class MainWindow : Window {
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly Image? _flappyBird;

    public MainWindow() {
        InitializeComponent();

        _flappyBird = MyCanvas.Children[1] as Image;

        _mainWindowViewModel = new MainWindowViewModel();

        var topLevel = TopLevel.GetTopLevel(this)!;
        topLevel.KeyDown += HandleKey;

        var gameThread = new Thread(GameLoop) {
            IsBackground = true,
            Priority = ThreadPriority.AboveNormal
        };

        gameThread.Start();
    }


    private void HandleKey(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Space) {
            _mainWindowViewModel.Bird.Flap();
        }
    }

    private void GameLoop() {
        while (true) {
            _mainWindowViewModel.UpdateGame();

            Draw(_mainWindowViewModel.Bird);

            Thread.Sleep(50);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private void Draw(Bird bird) {
        Dispatcher.UIThread.Invoke(() => {
            if (_flappyBird == null) return;
            
            var rotateTransform = bird.IsFalling ? new RotateTransform(30) : new RotateTransform(0);

            _flappyBird.RenderTransform = rotateTransform;
 
            Canvas.SetLeft(_flappyBird, bird.X);
            Canvas.SetTop(_flappyBird, bird.Y);
            
            
        });
    }
}