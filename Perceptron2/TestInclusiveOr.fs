namespace Perceptron2

open System
open Perceptron.Helpers

module TestInclusiveOr =
    let private bias = 1.
    let private learningRate = 1.

    let private random = Random ()

    // inclusive or
    let private data = [
        { Input1=1.; Input2=1.; Output=1.}
        { Input1=1.; Input2=0.; Output=1.}
        { Input1=0.; Input2=1.; Output=1.}
        { Input1=0.; Input2=0.; Output=0.} ]

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
        {   Neuron1 = random.NextSingle() |> float
            Neuron2 = random.NextSingle() |> float
            Bias = random.NextSingle() |> float }

    let layerZero =
        {   Neuron1 = 0.
            Neuron2 = 0.
            Bias = 0. }

    let layerOne =
        {   Neuron1 = 1.
            Neuron2 = 1.
            Bias = 1. }

    let rec private runPerceptron (activation: float -> float) (layer: LayerWeights) =
        printf "Enter two inputs separated by space: "
        let inputs =
            Console
                .ReadLine()
                .Split(' ')
            |> Array.map float
        let result =
            layer
            |> Perceptron.work
                activation
                inputs.[0]
                inputs.[1]
                bias
        printfn "The result is: %f" result
        runPerceptron activation layer

    let runHeaviside (layer: LayerWeights) =
        runPerceptron heaviside layer

    let runSigmoid (layer: LayerWeights) =
        runPerceptron sigmoid layer

    let run () =
        printfn "=> Heaviside"
        printfn "=> Layer Random"
        layerRandom
        |> trainHeaviside 3
        |> ignore

        printfn "=> Layer One"
        layerOne
        |> trainHeaviside 3
        |> ignore

        printfn "=> Layer Zero"
        layerZero
        |> trainHeaviside 3
        |> ignore
        // |> runSigmoid

        // printfn "=> Sigmoid"
        // printfn "=> Layer Random"
        // layerRandom
        // |> trainSigmoid 30
        // |> ignore

        // printfn "=> Layer One"
        // layerOne
        // |> trainSigmoid 30
        // |> ignore

        // printfn "=> Layer Zero"
        // layerZero
        // |> trainSigmoid 30
        // // |> ignore
        // // |> runSigmoid
        // |> runHeaviside
