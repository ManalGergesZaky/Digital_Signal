using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
           List<float> Out = new List<float>();
            float mean = 0;
            float sum = 0;
            float N = InputSignal.Samples.Count;
            for (int i = 0; i < N; i++)
            {
                sum += InputSignal.Samples[i];
            }
            mean = sum / N;
            float output = 0;
            for (int i = 0; i < N; i++)
            {
                output = InputSignal.Samples[i] - mean;
                Out.Add(output);
            }
            
            OutputSignal = new Signal (Out,false);
        }
    }
}