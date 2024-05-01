namespace FlappyBird.DataModels;

public class Pipe(double x, double y) {
    private double _x = x;
    private double _y = y;
    private const double Speed = 2;

    public void Update() {
        _x -= Speed;
    }
    
}