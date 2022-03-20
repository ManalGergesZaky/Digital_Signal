using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> O = new List<float>();
            float N = InputSignal.Samples.Count;
            float sum = 0;
            float output=0;
            for (int u = 0; u < N; u++) //number of main samples (u)
            {
                sum = 0;
                for (int x = 0; x < N; x++) { //make sum 
                     sum+=(float)(InputSignal.Samples[x] *(float) (Math.Cos(((float)(Math.PI) * (2 * x + 1) * u)/(2*N))));
                   }
                if (u == 0) {
                    output = (float)Math.Sqrt(1 / N) * sum;
                }
                else
                {
                    output = (float)Math.Sqrt(2 / N) * sum;
                }
               
                O.Add(output);
            }
            OutputSignal = new Signal(O, false);
        }
    }
}
