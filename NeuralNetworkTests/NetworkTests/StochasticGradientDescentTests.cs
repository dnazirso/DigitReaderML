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
            List<Data> testingDatas = new List<Data>();
            List<Data> trainingDatas = new List<Data>();

            Network network = new Network(new int[] { 784, 16, 16, 10 });

            using (IDataLoader loader = new ImageLoader())
            {
                string testingFolders = ".\\dataset\\";

                List<Data> loadDatas(string folder)
                {
                    List<Data> datas = new List<Data>();
                    for (int i = 0; i < 10; i++)
                    {
                        string path = folder + i + "\\";
                        foreach (string file in Directory.GetFiles(path))
                        {
                            Data test = new Data { Id = file, Expected = Expected.Answers[i], Inputs = loader.Load(file) };
                            datas.Add(test);
                        }
                    }

                    return datas;
                }

                testingDatas = loadDatas(testingFolders);
                trainingDatas = loadDatas(testingFolders);
            }

            // Act
            network.StochasticGradientDescent(trainingDatas, 30, 10, 3.0f, testingDatas);

            // Assert
            Assert.NotEqual(testingDatas, trainingDatas);
        }
    }
}
