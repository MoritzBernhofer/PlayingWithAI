using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using FlappyBird.DataModels;
using FlappyBird.ViewModels;

namespace FlappyBird.Views;

public partial class MainWindow : Window {
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    private readonly List<Tuple<Image, Bird>> _birds = [];
    private int _birdsAlive;
    
    private readonly List<Tuple<Image, Pipe>> _pipes = [];
    
    

    public MainWindow() {
        InitializeComponent();

        _mainWindowViewModel = new MainWindowViewModel();

        var birdImage = new Bitmap(AssetLoader.Open(new Uri("avares://FlappyBird/Assets/FlappyBird.png",
            UriKind.RelativeOrAbsolute)));

        var birds = _mainWindowViewModel.Birds;

        foreach (var bird in birds) {
            var image = new Image {
                Width = 42,
                Height = 42,
                Source = birdImage
            };

            _birds.Add(new Tuple<Image, Bird>(image, bird));
            MyCanvas.Children.Add(image);
        }

        _birdsAlive = _birds.Count;

        var pipes = _mainWindowViewModel.Pipes;

        var bottomPipe = new Bitmap(AssetLoader.Open(new Uri("avares://FlappyBird/Assets/pipe-bottom.png",
            UriKind.RelativeOrAbsolute)));
        var topPipe = new Bitmap(AssetLoader.Open(new Uri("avares://FlappyBird/Assets/pipe-top.png",
            UriKind.RelativeOrAbsolute)));

        foreach (var pipe in pipes) {
            var image = new Image {
                Width = 360,
                Height = 655,
                Source = pipe.Type == PipeType.BottomPipe ? bottomPipe : topPipe
            };

            _pipes.Add(new Tuple<Image, Pipe>(image, pipe));

            MyCanvas.Children.Add(image);
        }

        // var topLevel = TopLevel.GetTopLevel(this)!;
        // topLevel.KeyDown += HandleKey;

        var gameThread = new Thread(GameLoop) {
            IsBackground = true,
            Priority = ThreadPriority.AboveNormal
        };

        gameThread.Start();
    }


    // private void HandleKey(object? sender, KeyEventArgs e) {
    //     if (e.Key != Key.Space) return;
    //
    //     foreach (var (image, bird) in _flappyBirds) {
    //         bird.Flap();
    //     }
    // }

    private void GameLoop() {
        while (true) {
            if (_birdsAlive <= 0) {
                _birdsAlive = _birds.Count;

                var newBirds = Ga.CreateNewGeneration(_birds.Select(value => value.Item2).ToList());

                for (var i = 0; i < _birds.Count; i++) {
                    _birds[i] = new Tuple<Image, Bird>(_birds[i].Item1, newBirds[i]);
                }
                
                _mainWindowViewModel.ResetGame();
            }
            
            //update game
            _mainWindowViewModel.UpdateGame();

            //get birds to think
            foreach (var (_, bird) in _birds) {
                bird.Think();
            }

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
            foreach (var (image, bird) in _birds) {
                if (!bird.IsAlive) continue;

                var rotateTransform = bird.IsFalling ? new RotateTransform(30) : new RotateTransform(0);

                image.RenderTransform = rotateTransform;

                Canvas.SetLeft(image, bird.X);
                Canvas.SetBottom(image, bird.Y);
            }

            foreach (var (image, pipe) in _pipes) {
                switch (pipe.Type) {
                    case PipeType.BottomPipe:
                        Canvas.SetLeft(image, pipe.X);
                        Canvas.SetBottom(image, -300 - pipe.DistanceBetween);
                        break;
                    case PipeType.TopPipe:
                        Canvas.SetLeft(image, pipe.X);
                        Canvas.SetBottom(image, 500 + pipe.DistanceBetween);
                        break;
                }
            }
        });
    }

    private void CheckIfBirdCrashed() {
        Dispatcher.UIThread.Invoke(() => {
            foreach (var (imageBird, bird) in _birds) {
                if (!bird.IsAlive) continue;
                
                foreach (var (imagePipe, _) in _pipes) {
                    if (!CheckImagesIntersect(imageBird, imagePipe) && bird.Y is >= -50 and <= 1050) continue;
                    
                    bird.IsAlive = false;
                    _birdsAlive--;
                }

                bird.Score++;
            }
        });
    }

    // private void GameOver() {
    //     _gameOver = true;
    //     var gameOverImg = new Bitmap(AssetLoader.Open(new Uri("avares://FlappyBird/Assets/game_over.jpeg",
    //         UriKind.RelativeOrAbsolute)));
    //
    //     Background.Source = gameOverImg;
    // }

    private bool CheckImagesIntersect(Layoutable img1, Layoutable img2) {
        var bounds1 = GetBounds(img1);
        var bounds2 = GetBounds(img2);

        // DrawDebugBounds(bounds1);
        // DrawDebugBounds(bounds2);

        return bounds1.Intersects(bounds2);
    }

    // private void DrawDebugBounds(Rect bounds) {
    //     var debugRect = new Rectangle {
    //         Stroke = Brushes.Red,
    //         StrokeThickness = 2,
    //         Width = bounds.Width,
    //         Height = bounds.Height,
    //         IsHitTestVisible = false
    //     };
    //
    //     Canvas.SetLeft(debugRect, bounds.X);
    //     Canvas.SetBottom(debugRect, bounds.Y);
    //     MyCanvas.Children.Add(debugRect);
    // }

    private Rect GetBounds(Layoutable img) {
        var x = Canvas.GetLeft(img);
        var y = Canvas.GetBottom(img);

        var width = img.Width;
        var height = img.Height;

        if (double.IsNaN(x)) x = 0;
        if (double.IsNaN(y)) y = 0;

        return new Rect(x, y, width, height);
    }
}