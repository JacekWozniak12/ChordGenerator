using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChordGenerator
{
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2010/march/ui-frontiers-midi-music-in-wpf-applications

    public class NAudioCommunication
    {
        public static NAudioCommunication Instance { get; private set; }
        public WaveOutEvent AudioOut { get; private set; }

        public NAudioCommunication()
        {
            Instance = this;
            AudioOut = new WaveOutEvent();
        }

        /// <summary>
        /// Uses RuntimeManager to find note;
        /// </summary>
        /// <param name="name"></param>
        public void PlaySound(double frequency, string name)
        {
            PlaySound(
                frequency, 
                name, 
                0.1f, 
                1f);
        } 


        public void PlaySound(double frequency, string name, float gain, float time)
        {
            PlaySound(
                new MusicalNote(name, frequency, -1),
                gain, 
                time, 
                SignalGeneratorType.Sin
                );
        }


        public void PlaySound(MusicalNote note, float gain, float time, SignalGeneratorType signalType)
        {
            PlaySound(
                new MusicalNote[] {note},
                gain, 
                time, 
                signalType
                );
        }

        public void PlaySound(Chord chord, float gain, float time, SignalGeneratorType signalType)
        {
            PlaySound(
                chord.musicalNotes, 
                gain, 
                time, 
                signalType
                );
        }

        public async void PlaySound(MusicalNote[] musicalNotes, float gain, float time, SignalGeneratorType signalType)
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
            AudioOut.Stop();
            AudioOut.Init(mix);
            AudioOut.Play();
            while (AudioOut.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay((int) time*1000);
            }
        }

        public void Dispose()
        {
            AudioOut.Dispose();
            Instance = null;
        }
    }
}