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
    this.outNodes_bias = Array(outNodes).fill(Math.random() * 2 - 1);

    this.calculate = (input) => {
        const weightedInputs = Array(this.outNodes).fill(0);
        for (let i = 0; i < this.outNodes; i++) {
            let weightedInput = this.outNodes_bias[i];
            for (let j = 0; j < this.inNodes; j++) {
                weightedInput += input[j] * this.inNodes_weights[i][j];
            }

            weightedInputs[i] = weightedInput;
        }

        return weightedInputs;
    }
}