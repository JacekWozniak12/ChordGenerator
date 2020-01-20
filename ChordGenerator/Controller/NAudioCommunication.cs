using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Linq;
using System.Threading;

namespace ChordGenerator
{
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2010/march/ui-frontiers-midi-music-in-wpf-applications

    public class NAudioCommunication
    {
        public static NAudioCommunication Instance { get; private set; }
        private WaveOutEvent wo;
        
        public NAudioCommunication()
        {
            Instance = this;
            wo = new WaveOutEvent();
        }

        /// <summary>
        /// Uses RuntimeManager to find note;
        /// </summary>
        /// <param name="name"></param>
        public void PlaySound(float frequency)
        {
            PlaySound(0.1f, frequency, 1);
        }

        public void PlaySound(float frequency, float gain, float time)
        {
            PlaySound(gain, frequency, time, SignalGeneratorType.Sin);
        }

        public void PlaySound(float frequency, float gain, float time, SignalGeneratorType signalType)
        {
            PlaySound(
                new MusicalNote[] { new MusicalNote(" ", frequency, 0) },
                gain, time, signalType);
        }

        public void PlaySound(MusicalNote[] musicalNotes, float gain, float time, SignalGeneratorType signalType)
        {
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            var mix = new MixingSampleProvider(waveFormat);

            foreach (var I in musicalNotes)
            {
                var Signal = new SignalGenerator
                {
                    Gain = gain,
                    Frequency = I.Frequency,
                    Type = signalType
                }
                .Take(TimeSpan.FromSeconds(time));

                mix.AddMixerInput(Signal);
            }
            wo.Init(mix);
            wo.Play();
            while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(15);
                }
        }

        public void Dispose()
        {
            wo.Dispose();
            Instance = null;
        }
    }
}