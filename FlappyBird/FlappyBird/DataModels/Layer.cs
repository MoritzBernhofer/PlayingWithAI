using System;
using System.Linq;

namespace FlappyBird.DataModels;

public class Layer {
    private double[,] _inputsWeights;
    private double[] _bias;
    private Random _rd = new Random();


    public Layer(int inputLayers, int outputLayers) {
        _inputsWeights = new double[outputLayers, inputLayers];
        _bias = new double[outputLayers];
        _outputLayers = outputLayers;
        _inputLayers = inputLayers;


        for (var i = 0; i < _inputsWeights.GetLength(0); i++) {
            for (var j = 0; j < _inputsWeights.GetLength(1); j++) {
                _inputsWeights[i, j] = _rd.NextDouble();
            }
        }

        for (var i = 0; i < _bias.Length; i++) {
            _bias[i] = _rd.NextDouble();
        }
    }

    private readonly int _outputLayers;
    private readonly int _inputLayers;

    public double[] Predict(double[] inputs) {
        var weightedOutputs = new double[_outputLayers];

        for (var i = 0; i < weightedOutputs.Length; i++) {
            var output = _bias[i] + inputs.Select((value, j) =>
                value * _inputsWeights[i, j]).Sum();

            weightedOutputs[i] = Sigmoid(output);
        }

        return weightedOutputs;
    }

    private static double Sigmoid(double value) {
        return 1 / (1 + Math.Exp(-value));
    }

    public void Mutate(double learnRate) {
        for (var i = 0; i < _inputsWeights.GetLength(0); i++) {
            for (var j = 0; j < _inputsWeights.GetLength(1); j++) {
                if (_rd.NextDouble() < learnRate)
                    _inputsWeights[i, j] += SampleGaussian(_rd, 0, 0.1);
            }
        }

        for (var i = 0; i < _bias.Length; i++) {
            if (_rd.NextDouble() < learnRate)
                _bias[i] += SampleGaussian(_rd, 0, 0.1);
        }
    }
    private static double SampleGaussian(Random random, double mean, double stddev) {
        var x1 = 1 - random.NextDouble();
        var x2 = 1 - random.NextDouble();

        var y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);

        return y1 * stddev + mean;
    }
}