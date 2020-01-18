using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;


namespace ChordGenerator
{
    public class NAudioCommunication
    {
        double semitone = Math.Pow(2, 1.0 / 12);



        public static void PlaySound(float frequency)
        {
            PlaySound(0.1f, frequency, 1);
        }

        public static void PlaySound(float gain, float frequency, float time)
        {
            PlaySound(gain, frequency, time, SignalGeneratorType.Sin);
        }

        public static void PlaySound(float gain, float frequency, float time, SignalGeneratorType signalType)
        {
            var Signal = new SignalGenerator()
            {
                Gain =      gain,
                Frequency = frequency,
                Type =      signalType               
            }
            .Take(TimeSpan.FromSeconds(1));
            using (var wo = new WaveOutEvent())
            {
                wo.Init(Signal);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
                wo.Dispose();
            }
        }

        public static void PlaySound(Chord chord)
        {
            //
        }


    }
}
