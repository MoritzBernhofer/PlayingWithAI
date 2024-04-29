function Layer(inNodes, outNodes) {
    this.inNodes = inNodes;
    this.inNodes_weights = [];

    for (let i = 0; i < outNodes; i++) {
        let weights = [];
        for (let j = 0; j < inNodes; j++) {
            weights.push(Math.random() * 2 - 1);
        }
        this.inNodes_weights.push(weights);
    }

    this.outNodes = outNodes;
    this.outNodes_bias = Array(outNodes).fill(0);

    this.calculate = (input) => {
        const weightedInputs = Array(this.outNodes).fill(0);
        for (let i = 0; i < this.outNodes; i++) {
            let weightedInput = this.outNodes_bias[i];
            for (let j = 0; j < this.inNodes; j++) {
                weightedInput += input[j] * this.inNodes_weights[i][j];
            }

            weightedInputs[i] = activationFunction_sigmoid(weightedInput);
        }

        return weightedInputs;
    }
}

function activationFunction_simple(value) {
    return value > 0 ? 1 : 0;
}

function activationFunction_sigmoid(value) {
    return 1 / (1 + Math.exp(-value));
}

function activationFunction_Hyperbolic_tangent(value) {
    let e2w = Math.exp(2 * value);
    return (e2w + 1) / (e2w - 1);
}

function activationFunction_Hyperbolic_SiLU(value) {
    return value / (1 + Math.exp(-value));
}
function activationFunction_ReLU(value) {
    return Math.max(0, value);
}