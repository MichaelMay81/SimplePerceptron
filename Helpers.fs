namespace Perceptron

open System

module Helpers =
    let heaviside (input:float) : float =
        if input > 0 then 1. else 0.
    let sigmoid (input:float) : float =
        1. / (1. + Math.Exp (-input))

    let color (r:int) (g:int) (b:int) : float list =
        [   float r / 255.
            float g / 255.
            float b / 255. ]

    let white = color 255 255 255
    let lightGrey = color 192 192 192
    let darkGrey = color 65 65 65
    let black = color 0 0 0
    let darkBlue = color 0 0 128
    let lightDayBlue = color 173 223 255
    let darkGreen = color 0 100 0
    let lightGreen = color 144 238 144
    let darkBrown = color 101 67 33
    let lightOrange = color 254 216 177