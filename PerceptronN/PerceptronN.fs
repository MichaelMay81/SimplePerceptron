// A simple perceptron implementation with N neurons
namespace PerceptronN

type LayerWeights = {
    Neurons : float list
    Bias : float }

type TestData = {
    Inputs : float list
    Output : float }

module Perceptron =
    let work (activation:float->float) (inputs:float seq) (bias:float) (layer:LayerWeights) : float =
        (Seq.zip inputs layer.Neurons
        |> Seq.map (fun (input, neuron) -> input * neuron)
        |> Seq.sum)
        + bias * layer.Bias
        |> activation
        
    let trainLayer (activation:float->float) (bias:float) (learningRate:float) (layer:LayerWeights) (data:TestData) : float*LayerWeights =
        let outputP = layer |> work activation data.Inputs bias
        let error = data.Output - outputP
        let neurons =
            List.zip data.Inputs layer.Neurons
            |> List.map (fun (input, neuron) ->
                neuron + (error * input * learningRate))

        let layer =
            {   Neurons = neurons
                Bias = layer.Bias + (error * bias * learningRate) }
        error, layer

    let train (activation:float->float) (bias:float) (learningRate:float) (data:TestData list) (counter:int) (layer:LayerWeights) : LayerWeights = 
        let rec recFunc (counter:int) (layer:LayerWeights) : LayerWeights =
            match counter with
            | 0 -> layer
            | _ ->
                let errorSum, layer =
                    data
                    |> Seq.fold (fun (errorSum, layer) testData ->
                        let error, testData =
                            testData |> trainLayer activation bias learningRate layer
                        (errorSum + (error |> abs), testData))
                        (0., layer)
                printfn $"{counter} Error: {errorSum}"
                recFunc (counter-1) layer
        recFunc counter layer