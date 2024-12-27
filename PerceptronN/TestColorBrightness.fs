namespace PerceptronN

open System
open Perceptron.Helpers

module TestColorBrightness =
    let private bias = 1.
    let private learningRate = 1.

    let private random = Random ()

    // dark vs light colors
    let private data = [
        { Inputs=white; Output=1.}
        { Inputs=lightGrey; Output=1.}
        { Inputs=darkGrey; Output=0.}
        { Inputs=black; Output=0.} ]

    let trainHeaviside
        (counter:int)
        (layer:LayerWeights)
        : LayerWeights=
        
        layer
        |> Perceptron.train heaviside bias learningRate data counter

    let trainSigmoid
        (counter:int)
        (layer:LayerWeights)
        : LayerWeights=
        
        layer
        |> Perceptron.train sigmoid bias learningRate data counter
        
    let layerRandom =
        {   Neurons = [0..2] |> List.map (fun _ -> random.NextSingle() |> float)
            Bias = random.NextSingle() |> float }

    let layerZero =
        {   Neurons = [0.; 0.; 0.]
            Bias = 0. }

    let layerOne =
        {   Neurons = [0.; 0.; 0.]
            Bias = 1. }

    let rec private runPerceptron (activation: float -> float) (layer: LayerWeights) =
        printf "Enter color (three int inputs separated by space): "
        let inputs =
            Console
                .ReadLine()
                .Split(' ')
            |> Array.map (fun intValue -> float intValue / 255.)
        let result =
            layer
            |> Perceptron.work
                activation
                inputs
                bias
        printfn "The result is: %f" result
        runPerceptron activation layer

    let runHeaviside (layer: LayerWeights) =
        runPerceptron heaviside layer

    let runSigmoid (layer: LayerWeights) =
        runPerceptron sigmoid layer

    let test  (activation: float -> float) (layer: LayerWeights) =
        printfn "Light:"
        [   white
            lightGrey
            lightDayBlue
            lightGreen
            lightOrange ]
        |> List.iter (fun color ->
            Perceptron.work
                activation
                color
                bias
                layer
            |> printfn "input %A -> %f" color)
        printfn "Dark:"
        [   black
            darkGrey
            darkBlue
            darkGreen
            darkBrown ]
        |> List.iter (fun color ->
            Perceptron.work
                activation
                color
                bias
                layer
            |> printfn "input %A -> %f" color)

    let run () =
        printfn "=> Heaviside"
        // printfn "=> Layer Random"
        // layerRandom
        // |> trainHeaviside 5
        // |> test Perceptron.heaviside

        // printfn "=> Layer One"
        // layerOne
        // |> trainHeaviside 5
        // |> test Perceptron.heaviside

        // printfn "=> Layer Zero"
        // layerZero
        // |> trainHeaviside 5
        // |> test Perceptron.heaviside

        printfn "=> Sigmoid"
        // printfn "=> Layer Random"
        // layerRandom
        // |> trainSigmoid 30
        // |> test Perceptron.sigmoid

        // printfn "=> Layer One"
        // layerOne
        // |> trainSigmoid 30
        // |> test Perceptron.sigmoid

        printfn "=> Layer Zero"
        layerZero
        |> trainSigmoid 30
        |> test sigmoid
