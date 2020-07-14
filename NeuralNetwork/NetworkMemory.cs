namespace NeuralNetwork
{
    public struct NetworkMemory
    {
        public int[] Sizes { get; set; }
        public float[][,] Biases { get; set; }
        public float[][,] Weights { get; set; }
    }
}
