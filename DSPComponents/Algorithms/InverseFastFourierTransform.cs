using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseFastFourierTransform : Algorithm
    {

        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public void IFFTrecursive(ref List<Complex> samples, int cnt)
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
            IFFTrecursive(ref even_samples, cnt / 2);
            IFFTrecursive(ref odd_samples, cnt / 2);
            for (int k = 0; k < cnt / 2; ++k)
            {
                double real = (double)Math.Cos((2.0f * Math.PI * ((double)k)) / (double)cnt);
                double imaginary = (double)Math.Sin((2.0f * Math.PI * ((double)k)) / (double)cnt);
                Complex t = new Complex(real, imaginary) * odd_samples[k];
                samples[k] = even_samples[k] + t;
                samples[k + (cnt / 2)] = even_samples[k] - t;
            }
        }

        public override void Run()
        {
            OutputTimeDomainSignal = InputFreqDomainSignal;
            OutputTimeDomainSignal.Samples = new List<float>();
            int N = InputFreqDomainSignal.FrequenciesAmplitudes.Count;
            List<Complex> freq_domain = new List<Complex>();
            for (int i = 0; i < N; ++i)
            {
                double amplitude = InputFreqDomainSignal.FrequenciesAmplitudes[i];
                double phase_shift = InputFreqDomainSignal.FrequenciesPhaseShifts[i];


                double real_ = (amplitude * Math.Cos(phase_shift));
                double imaginary_ = (amplitude * Math.Sin(phase_shift));
                freq_domain.Add(new Complex(real_, imaginary_));
            }
            IFFTrecursive(ref freq_domain, N);
            for (int i = 0; i < N; ++i)
            {
                OutputTimeDomainSignal.Samples.Add((float)Math.Round(freq_domain[i].Real / (double)N, 3));
            }
        }
    }
}