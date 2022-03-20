using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }




        public override void Run()
        {
            if (L > 0 && M > 0) //UP smapling && down sampling
            {

                List<float> output1 = new List<float>();

                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output1.Add(InputSignal.Samples[i]);

                    for (int k = 0; k < L - 1; k++)
                    {
                        output1.Add(0);
                    }
                }

                Signal mySignal = new Signal(output1, false);

                FIR fir = new FIR();
                fir.InputTimeDomainSignal = mySignal;
                fir.InputFilterType = FILTER_TYPES.LOW;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputCutOffFrequency = 1500;
                fir.InputTransitionBand = 500;

                fir.Run();

                List<float> output2 = new List<float>();

                //Console.WriteLine("signal size : " + fir.OutputYn.Samples.Count);

                for (int i = 0; i < fir.OutputYn.Samples.Count; i += M)
                {
                    //Console.WriteLine(i);
                    output2.Add(fir.OutputYn.Samples[i]);
                }
                OutputSignal = new Signal(output2, false);

                Console.WriteLine("size : " + output2.Count);

            }
            else if (L == 0 && M > 0) //down sample
            {

                FIR fir = new FIR();
                fir.InputTimeDomainSignal = InputSignal;
                fir.InputFilterType = FILTER_TYPES.LOW;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputCutOffFrequency = 1500;
                fir.InputTransitionBand = 500;

                fir.Run();

                List<float> output = new List<float>();

                /*According to the number of m, 
                 * take a value from the sample and
                 * sign values with the number of m-1 from the samples
                 */

                for (int i = 0; i < fir.OutputYn.Samples.Count; i += M)
                {
                    output.Add(fir.OutputYn.Samples[i]);
                }
                OutputSignal = new Signal(output, false);


            }
            else if (L > 0 && M == 0) //UP sampling
            {

                List<float> output = new List<float>();

                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output.Add(InputSignal.Samples[i]);
                    // Put 0 by the number of (L-1) between each value in samples
                    for (int k = 0; k < L - 1; k++)
                    {
                        output.Add(0);
                    }
                }

                Signal mySignal = new Signal(output, false);

                FIR fir = new FIR();
                fir.InputTimeDomainSignal = mySignal;
                fir.InputFilterType = FILTER_TYPES.LOW;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputCutOffFrequency = 1500;
                fir.InputTransitionBand = 500;

                fir.Run();

                OutputSignal = fir.OutputYn;
                Console.WriteLine("actual size : " + OutputSignal.Samples.Count);

            }
            else if (L == 0 && M == 0) //Error
            {
                throw new Exception("can't have both values as 0");
            }
        }
    }

}