function Nn(layers) {
    this.layers = [];

    for (let i = 0; i < layers.length - 1; i++) {
        this.layers.push(new Layer(layers[i], layers[i + 1]));
    }


    this.calculate = (input) => {
        let output = input;

        for (let i = 0; i < this.layers.length; i++) {
            output = this.layers[i].calculate(output);
        }

        return output;
    }

    this.classify = (point) => {
        const output = this.calculate(point);

        return (output[0] > output[1]) ? 1 : 0;
    }

    this.update = () => {
        let weightIndex = 0;
        let biasIndex = 0;

        for (let i = 0; i < this.layers.length; i++) {
            for (let j = 0; j < this.layers[i].inNodes_weights.length; j++) {
                for (let k = 0; k < this.layers[i].inNodes_weights[j].length; k++) {
                    this.layers[i].inNodes_weights[j][k] = weightSliders[weightIndex].value() / 4;
                    weightIndex++;
                }
            }

            for (let j = 0; j < this.layers[i].outNodes_bias.length; j++) {
                this.layers[i].outNodes_bias[j] = biasSliders[biasIndex].value() / 4;
                biasIndex++;
            }
        }
    }
}

