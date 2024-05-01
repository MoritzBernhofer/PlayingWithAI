using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using FlappyBird.DataModels;
using FlappyBird.ViewModels;

namespace FlappyBird.Views;

public partial class MainWindow : Window {
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly Image? _flappyBird;
    private readonly List<Tuple<Image, Image, Pipe>> _pipes = new();
    private bool _gameOver = false;

    public MainWindow() {
        InitializeComponent();

        _flappyBird = MyCanvas.Children[1] as Image;

        _mainWindowViewModel = new MainWindowViewModel();

        var pipes = _mainWindowViewModel.Pipes;

        foreach (var pipe in pipes) {
            var bottom = new Image {
                Width = 360,
                Height = 655,
                Source = new Bitmap("../../../Assets/pipe-bottom.png"),
            };

            var top = new Image() {
                Source = new Bitmap("../../../Assets/pipe-top.png"),
                Width = 360,
                Height = 655
            };

            _pipes.Add(new Tuple<Image, Image, Pipe>(bottom, top, pipe));

            MyCanvas.Children.Add(top);
            MyCanvas.Children.Add(bottom);
        }

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
        while (!_gameOver) {
            //update game
            _mainWindowViewModel.UpdateGame();

            //draw bird and pipes
            Draw();

            //check if Bird hit any pipes or hit the floor
            CheckIfBirdCrashed();

            Thread.Sleep(50);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private void Draw() {
        Dispatcher.UIThread.Invoke(() => {
            if (_flappyBird == null) return;

            var bird = _mainWindowViewModel.Bird;

            var rotateTransform = bird.IsFalling ? new RotateTransform(30) : new RotateTransform(0);

            foreach (var (bottom, top, pipe) in _pipes) {
                Canvas.SetLeft(top, pipe.X);
                Canvas.SetTop(top, -pipe.Distance);

                Canvas.SetLeft(bottom, pipe.X);
                Canvas.SetTop(bottom, 400 + pipe.Distance);
            }

            _flappyBird.RenderTransform = rotateTransform;

            Canvas.SetLeft(_flappyBird, bird.X);
            Canvas.SetTop(_flappyBird, bird.Y);
        });
    }

    private void CheckIfBirdCrashed() {
        Dispatcher.UIThread.Invoke(() => {
            foreach (var (bottom, top, _) in _pipes) {
                if (!CheckImagesIntersect(bottom, _flappyBird!) && !CheckImagesIntersect(top, _flappyBird!))
                    continue;
                GameOver();
            }

            if (_mainWindowViewModel.Bird.Y >= 1100) {
                GameOver();
            }
            
        });
    }

    private void GameOver() {
        _gameOver = true;
        Background.Source = new Bitmap("../../../Assets/game_over.jpeg");
    }

    private bool CheckImagesIntersect(Layoutable img1, Layoutable img2) {
        var bounds1 = GetBounds(img1);
        var bounds2 = GetBounds(img2);

        // DrawDebugBounds(bounds1);
        // DrawDebugBounds(bounds2);

        return bounds1.Intersects(bounds2);
    }

    private void DrawDebugBounds(Rect bounds) {
        var debugRect = new Rectangle {
            Stroke = Brushes.Red,
            StrokeThickness = 2,
            Width = bounds.Width,
            Height = bounds.Height,
            IsHitTestVisible = false
        };

        Canvas.SetLeft(debugRect, bounds.X);
        Canvas.SetTop(debugRect, bounds.Y);
        MyCanvas.Children.Add(debugRect);
    }

    private Rect GetBounds(Layoutable img) {
        var x = Canvas.GetLeft(img);
        var y = Canvas.GetTop(img);

        var width = img.Width;
        var height = img.Height;

        if (double.IsNaN(x)) x = 0;
        if (double.IsNaN(y)) y = 0;

        return new Rect(x, y, width, height);
    }
}