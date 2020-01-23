using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChordGenerator
{
    /// <summary>
    /// may change name later
    /// </summary>
    public class RuntimeManager
    {
        private NAudioCommunication nAudioCommunication;
        public static RuntimeManager Instance { get; private set; }
        public Settings runtimeSettings;
        public List<Chord> ChordsPlayed = new List<Chord>();

        /// <summary>
        /// Handles startup
        /// </summary>
        public RuntimeManager()
        {
            Instance = this;
            runtimeSettings = new Settings();
            nAudioCommunication = new NAudioCommunication();
            HandleReadingSettings();
            HandleReadingChords();
        }

        /// <summary>
        /// Handles playing sound in view
        /// </summary>
        /// <param name="chord"></param>
        public void AddSound(Chord chord)
        {
            try
            {
                ChordsPlayed.Add(chord);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public async void PlaySound()
        {
            bool AllAtOnce = false;

            try
            {
                var chord = ChordsPlayed[ChordsPlayed.Count - 1];
                if (AllAtOnce)
                {
                    nAudioCommunication.
                            PlaySound(
                            chord,
                            runtimeSettings.Volume,
                            runtimeSettings.Duration,
                            SignalGeneratorType.Sin //todo przenieść to
                        );
                }
                else
                {
                    foreach (var note in chord.MusicalNotes)
                    {
                        nAudioCommunication.
                            PlaySound(
                            note,
                            runtimeSettings.Volume,
                            runtimeSettings.Duration
                            / chord.MusicalNotes.Length,
                            SignalGeneratorType.Sin //todo przenieść to
                        );

                        await Task.Delay((int)runtimeSettings.Duration
                            / chord.MusicalNotes.Length * 1000);
                    }
                }

            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public void ChangeNoteArray(string note, float frequency)
        {
            nAudioCommunication.AudioOut.Stop();
            runtimeSettings.GenerateMusicalNoteArray(note, frequency);
        }

        public void Close()
        {
            HandleSavingSettings();
            HandleSavingChords();
            nAudioCommunication.Dispose();
        }

        private void HandleSavingChords()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Chord>));
                TextWriter writer = new StreamWriter("Chords.xml");
                serializer.Serialize(writer, ChordsPlayed);
                writer.Close();
            }
            catch (IOException e){ }
        }

        private void HandleSavingSettings()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("Settings.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, runtimeSettings);
                stream.Close();
            }
            catch (IOException e) { }
        }

        private void HandleReadingSettings()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("Settings.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Settings runtimeSettings = (Settings) formatter.Deserialize(stream);
                stream.Close();
            }
            catch (IOException e)  { }
        }

        private void HandleReadingChords()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Chord>));
                FileStream fileStream = new FileStream("Chords.xml", FileMode.Open);
                ChordsPlayed = (List<Chord>) serializer.Deserialize(fileStream);
                serializer.UnknownNode += new
                XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new
                XmlAttributeEventHandler(serializer_UnknownAttribute);

                foreach (var i in ChordsPlayed)
                {
                    int e = 0;
                    foreach (var n in i.MusicalNotes)
                    {
                        if (!MusicalNote.IsValidFrequency(n.Frequency) || !MusicalNote.IsValidName(n.Name))
                        {
                            e++;
                        }
                    }
                    if (e > 0)
                    {
                        ChordsPlayed.Remove(i);
                        e = 0;
                    }
                }
            }
            catch (IOException e)  { }
        }

        private void serializer_UnknownNode
        (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private void serializer_UnknownAttribute
        (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}