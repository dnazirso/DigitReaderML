using Algebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public struct Data
    {
        public string Id { get; set; }
        public Matrix Inputs { get; set; }
        public Matrix Expected { get; set; }
    }
}
