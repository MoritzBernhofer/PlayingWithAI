using System.Collections.Generic;

namespace FlappyBird.DataModels;

public class Nn {
    private readonly List<Layer> _layers = new List<Layer>();
    public Nn(params int[] layers) {
        for (var i = 0; i < layers.Length - 1; i++) {
            _layers.Add(new Layer(layers[i], layers[i + 1]));
        }
    }

    public double[] Classify(double[] inputs) {
        var output = inputs;

        foreach (var layer in _layers) {
            output = layer.Predict(output);
        }

        return output;
    }

    public void Mutate(double learnRate) {
        foreach (var layer in _layers) {
            layer.Mutate(learnRate);
        }
    }
    
}