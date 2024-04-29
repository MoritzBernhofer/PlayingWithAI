let points = [],
    pointCount = 10000;

let weightSliders = [];
let biasSliders = [];

let neuralNetwork;

let found = false;

function setup() {
    let canvas = createCanvas(windowWidth, windowHeight - 40);
    canvas.parent("container");
    addPoints();

    neuralNetwork = new Nn([2, 3, 2]);

    // Create sliders for weights
    for (let i = 0; i < 2; i++) {
        for (let j = 0; j < 3; j++) {
            weightSliders.push(createSliderWithLabel(`Weight ${i + 1}-${j + 1}`, -100, 100));
        }
    }

    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 2; j++) {
            weightSliders.push(createSliderWithLabel(`Weight ${i + 1}-${j + 1}`, -100, 100));
        }
    }

    // Create sliders for biases
    for (let i = 0; i < 3; i++) {
        biasSliders.push(createSliderWithLabel(`Bias ${i + 1}`, -100, 100));
    }

    for (let i = 0; i < 2; i++) {
        biasSliders.push(createSliderWithLabel(`Output Bias ${i + 1}`, -100, 100));
    }

    frameRate(60);
}

function createSliderWithLabel(label, min, max) {
    let div = createDiv();

    let p = createP(label);
    p.parent(div);

    let slider = createSlider(min, max, (Math.random() * 2 - 1) * 100);
    slider.parent(div);

    return slider;
}

function addPoints() {
    for (let i = 0; i < pointCount; i++) {
        points.push({
            x: Math.round(Math.random() * windowWidth),
            y: Math.round(Math.random() * windowHeight)
        });
    }
}

function draw() {
    background(220);

    neuralNetwork.update();

    for (let i = 0; i < points.length; i++) {
        updatePoint(points[i]);
    }
}

function updatePoint(point) {
    let result = neuralNetwork.classify([point.x / windowWidth, point.y / (windowHeight - 40)]);

    if (result === 1) {
        fill("green");
    } else if (result === 0) {
        fill("red");
    }

    ellipse(point.x,  point.y, 20);
}