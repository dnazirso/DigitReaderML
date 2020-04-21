using DataLoaders;
using NeuralNetwork;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class StochasticGradientDescentTests
    {
        [Fact]
        public void StochasticGradientDescentNominalBehavior()
        {
            // Arrange
            List<Network> trainingDatas = new List<Network>();
            List<Network> testingDatas = new List<Network>();

            Network network = new Network(new List<int> { 784, 16, 16, 10 });

            using (IDataLoader loader = new ImageLoader())
            {
                List<int> hiddenLayersShape = new List<int> { 16, 16 };

                string trainingFolders = ".\\dataset\\";

                List<float[,]> expecteds = new List<float[,]> {
                    new float[,] { {1},{0},{0},{0},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{1},{0},{0},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{1},{0},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{1},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{1},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{1},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{1},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{0},{1},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{0},{0},{1},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{0},{0},{0},{1} },
                };

                for (int i = 0; i < 10; i++)
                {
                    foreach (string file in Directory.GetFiles(trainingFolders + i + "\\"))
                    {
                        float[,] inputs = loader.Load(file);
                        Network test = new Network(inputs, expecteds[i], hiddenLayersShape);
                        testingDatas.Add(test);
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    foreach (string file in Directory.GetFiles(trainingFolders + i + "\\"))
                    {
                        float[,] inputs = loader.Load(file);
                        Network train = new Network(inputs, expecteds[i], hiddenLayersShape);
                        trainingDatas.Add(train);
                    }
                }
            }

            // Act
            network.StochasticGradientDescent(trainingDatas, 30, 10, 3.0f, testingDatas);

            // Assert
            Assert.NotEqual(testingDatas, trainingDatas);
        }
    }
}
