using DataLoaders;
using NeuralNetwork;
using System;
using System.Collections.Generic;
using System.IO;

namespace DigitReaderML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digit reader neural network");

            List<Data> testingDatas;
            List<Data> trainingDatas;

            Network network = new Network(new int[] { 784, 30, 10 });

            using (IDataLoader loader = new ImageLoader())
            {
                Console.WriteLine("Loading data");

                string testingFolders = "..\\..\\..\\mnist_png\\testing\\";
                string trainingFolders = "..\\..\\..\\mnist_png\\training\\";

                List<Data> loadDatas(string folder)
                {
                    List<Data> datas = new List<Data>();
                    for (int i = 0; i < 10; i++)
                    {
                        string path = folder + i + "\\";
                        Console.WriteLine("loading folder : " + path);
                        foreach (string file in Directory.GetFiles(path))
                        {
                            Data test = new Data { Id = file, Expected = Expected.Answers[i], Inputs = loader.Load(file) };
                            datas.Add(test);
                        }
                    }

                    return datas;
                }

                testingDatas = loadDatas(testingFolders);
                trainingDatas = loadDatas(trainingFolders);
            }

            network.StochasticGradientDescent(trainingDatas, 30, 10, 3.0f, testingDatas);
        }
    }
}
