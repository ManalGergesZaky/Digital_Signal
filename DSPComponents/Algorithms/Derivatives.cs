using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            List<float> Der1 = new List<float>();
            List<float> Der2 = new List<float>();


            int N = InputSignal.Samples.Count;
            Der1.Add(InputSignal.Samples[0]);
            Der2.Add(InputSignal.Samples[0 + 1] - (2 * InputSignal.Samples[0]));

            for (int i = 1; i < N - 1; i++)
            {

                Der1.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);


                Der2.Add(InputSignal.Samples[i + 1] - (2 * InputSignal.Samples[i]) + InputSignal.Samples[i - 1]);
            }

            FirstDerivative = new Signal(Der1, false);
            SecondDerivative = new Signal(Der2, false);

        }
    }
}