// A simple perceptron implementation with two neurons
// Idea from: https://towardsdatascience.com/first-neural-network-for-beginners-explained-with-code-4cfd37e06eaf
namespace Perceptron2

type LayerWeights = {
    Neuron1 : float
    Neuron2 : float
    Bias : float }

type TestData = {
    Input1 : float
    Input2 : float
    Output : float }

module Perceptron =
    let work (activation:float->float) (input1:float) (input2:float) (bias:float) (layer:LayerWeights) : float =
        input1 * layer.Neuron1 +
        input2 * layer.Neuron2 +
        bias * layer.Bias
        |> activation

    let trainLayer (activation:float->float) (bias:float) (learningRate:float) (layer:LayerWeights) (data:TestData) : float*LayerWeights =
        let outputP = layer |> work activation data.Input1 data.Input2 bias
        let error = data.Output - outputP
        
        let layer =
            {   Neuron1 = layer.Neuron1 + (error * data.Input1 * learningRate)
                Neuron2 = layer.Neuron2 + (error * data.Input2 * learningRate)
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