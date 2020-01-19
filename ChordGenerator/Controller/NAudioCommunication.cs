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
        /// <summary>
        /// Uses RuntimeManager to find note;
        /// </summary>
        /// <param name="name"></param>
        public static void PlaySound(string name)
        {
            float frequency = RuntimeManager.instance.musicalNotes.Find(x => x.name == name).frequency;
            PlaySound(frequency);
        }

        public static void PlaySound(float frequency)
        {
            PlaySound(0.1f, frequency, 1);
        }

        public static void PlaySound(float frequency, float gain, float time)
        {
            PlaySound(gain, frequency, time, SignalGeneratorType.Sin);
        }

        public static void PlaySound(float frequency, float gain, float time, SignalGeneratorType signalType)
        {
            PlaySound(
                new MusicalNote[] { new MusicalNote(" ", frequency) },
                gain, time, signalType);
        }

        public static void PlaySound(MusicalNote[] musicalNotes, float gain, float time, SignalGeneratorType signalType)
        {
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            var mix = new MixingSampleProvider(waveFormat);

            foreach (var I in musicalNotes)
            {
                var Signal = new SignalGenerator
                {
                    Gain = gain,
                    Frequency = I.frequency,
                    Type = signalType
                }
                .Take(TimeSpan.FromSeconds(time));

                mix.AddMixerInput(Signal);
            }
            using (var wo = new WaveOutEvent())
            {
                wo.Init(mix);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(15);
                }
                wo.Dispose();
            }
        }
    }
}