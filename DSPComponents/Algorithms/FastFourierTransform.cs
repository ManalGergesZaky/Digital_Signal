using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class FastFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public int InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }



        public void FFTrecursive(ref List<Complex> samples, int cnt)
        {
            if (cnt == 1)
                return;

            List<Complex> even_samples = new List<Complex>();
            List<Complex> odd_samples = new List<Complex>();

            for (int i = 0; i < cnt; ++i)
            {
                if (i % 2 == 0)
                    even_samples.Add(samples[i]);
                else
                    odd_samples.Add(samples[i]);
            }
            FFTrecursive(ref even_samples, cnt / 2);
            FFTrecursive(ref odd_samples, cnt / 2);
            for (int k = 0; k < cnt / 2; ++k)
            {
                double real = (double)Math.Cos((-2.0f * Math.PI * ((double)k)) / (double)cnt);
                double imaginary = (double)Math.Sin((-2.0f * Math.PI * ((double)k)) / (double)cnt);
                Complex t = new Complex(real, imaginary) * odd_samples[k];
                samples[k] = even_samples[k] + t;
                samples[k + (cnt / 2)] = even_samples[k] - t;
            }
        }
        public override void Run()
        {

            OutputFreqDomainSignal = new Signal(false, new List<float>(), new List<float>(), new List<float>());

            List<Complex> freq_domain = new List<Complex>();
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; ++i)
                freq_domain.Add(new Complex(InputTimeDomainSignal.Samples[i], 0));
            FFTrecursive(ref freq_domain, InputTimeDomainSignal.Samples.Count);

            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; ++i)
            {
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add((float)Math.Sqrt(freq_domain[i].Real * freq_domain[i].Real + freq_domain[i].Imaginary * freq_domain[i].Imaginary));
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)Math.Round(Math.Atan2(freq_domain[i].Imaginary, freq_domain[i].Real), 10));
            }
            double freq_base = ((double)(2.0f * Math.PI)) / (((double)InputTimeDomainSignal.Samples.Count) * (1.0f / ((double)InputSamplingFrequency)));
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; ++i)
            {
                OutputFreqDomainSignal.Frequencies.Add((float)(freq_base * (double)(i + 1)));
            }

        }
    }
}
