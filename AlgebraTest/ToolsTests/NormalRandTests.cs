using Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgebraTests.ToolsTests
{
    public class NormalRandTests
    {
        [Fact]
        public void NormalRandNominalBehavior()
        {
            List<float> list = new List<float>();

            for (int i = 0; i < 1000; i++)
            {
                list.Add(ThreadSafeRandom.NormalRand());
            }

            List<List<float>> ranged = new List<List<float>>();
            for (float n = -3; n < 3; n = n + 0.1f)
            {
                var r = new List<float>();

                foreach (var l in list)
                {
                    if (n < l && l < n + 0.2f)
                    {
                        r.Add(l);
                    }
                }

                ranged.Add(r);
            }


            foreach (var r in ranged)
            {
                if (r.Any()) Console.Write($"\t{r[^1]} : {r[0]} ; {r.Count}\t");
                for (var n = 0; n < r.Count; n++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }
        }
    }
}
