using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio;
using NAudio.Midi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;


namespace ChordGenerator
{
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2010/march/ui-frontiers-midi-music-in-wpf-applications

    public class NAudioCommunication
    {
        double semitone = Math.Pow(2, 1.0 / 12);

        public static void PlaySound(float frequency)
        {

            MidiOut midiOut = new MidiOut(0);
            midiOut.Send(MidiMessage.StartNote(60, 127, 0).RawData);
            Thread.Sleep(1000);
            midiOut.Send(MidiMessage.StopNote(60, 0, 0).RawData);
            Thread.Sleep(1000);
            midiOut.Close();
            midiOut.Dispose();
            // PlaySound(0.1f, frequency, 1);
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
            .Take(TimeSpan.FromSeconds(time));
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

        public static void PlaySounds(MusicalNote[] musicalNotes, float gain, float time, SignalGeneratorType signalType)
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
                    Thread.Sleep(500);
                }
                wo.Dispose();
            }          
        }

    }
}
