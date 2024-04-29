let points = [],
    pointCount = 10000;

let weight1_1 = 0,
    weight1_2 = 0,
    weight2_1 = 0,
    weight2_2 = 0;

let weight1_1_slider,
    weight1_2_slider,
    weight2_1_slider,
    weight2_2_slider;

function setup() {
    let canvas = createCanvas(windowWidth, windowHeight - 40);
    addPoints();

    canvas.parent("container");

    weight1_1_slider = createSlider(-100, 100);
    weight1_2_slider = createSlider(-100, 100);
    weight2_1_slider = createSlider(-100, 100);
    weight2_2_slider = createSlider(-100, 100);

    frameRate(60);
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

    weight1_1 = weight1_1_slider.value() / 100;
    weight1_2 = weight1_2_slider.value() / 100;
    weight2_1 = weight2_1_slider.value() / 100;
    weight2_2 = weight2_2_slider.value() / 100;

    for (let i = 0; i < points.length; i++) {
        updatePoint(points[i]);
    }
}

function updatePoint(point) {
    let result = decide(point);

    if (result === 1) {
        fill("green");
    } else if (result === 0) {
        fill("red");
    }

    ellipse(point.x, windowHeight - point.y, 10);
}

function decide(point) {
    let result1 = point.x * weight1_1 + point.y * weight1_2;
    let result2 = point.x * weight2_1 + point.y * weight2_2;

    return result1 > result2 ? 1 : 0;
}