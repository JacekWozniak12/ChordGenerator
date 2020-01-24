using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ChordGenerator.Controller
{
    public class IOHandler
    {
        public const string FILE_SETTINGS = "settings.bin";
        public const string FILE_CHORD = "chords.xml";

        /// <summary>
        /// Tries to save XML file with played chords
        /// </summary>
        public List<Chord> HandleSavingChords(string fileName = FILE_CHORD, object obj = null)
        {
            try
            {
                var a = (List<Chord>)obj;
                if (a.Count == 0)
                {
                    File.Delete(FILE_CHORD);
                    return null;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<Chord>));
                TextWriter writer = new StreamWriter(fileName);
                serializer.Serialize(writer, obj);
                writer.Close();               
            }
            catch (IOException e)
            {
            }

            return (List<Chord>)obj;
        }

        /// <summary>
        /// Tries to save binary files with app settings
        /// </summary>
        public Settings HandleSavingSettings(string fileName = FILE_SETTINGS, object obj = null)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, obj);
                stream.Close();
            }
            catch (Exception e) { }

            return (Settings)obj;
        }

        /// <summary>
        /// Tries to read settings binaries
        /// </summary>
        public Settings HandleReadingSettings(string fileName = FILE_SETTINGS, object obj = null)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                obj = (Settings)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception e) { }

            return (Settings) obj;
        }

        /// <summary>
        /// Tries to read chord files
        /// </summary>
        public List<Chord> HandleReadingChords(string fileName = FILE_CHORD, object obj = null)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Chord>));
                FileStream fileStream = new FileStream(fileName, FileMode.Open);
                obj = (List<Chord>)serializer.Deserialize(fileStream);
                serializer.UnknownNode += new
                XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new
                XmlAttributeEventHandler(serializer_UnknownAttribute);
            }
            catch (Exception e)
            {
                Chords_CheckXMLFileForUnvalidChanges((List<Chord>)obj);
            }

            return (List<Chord>)obj;
        }

        public void Chords_CheckXMLFileForUnvalidChanges(List<Chord> Chords)
        {
            foreach (Chord i in Chords)
            {
                int y = 0;
                foreach (var n in i.MusicalNotes)
                {
                    if (!MusicalNote.IsValidName(n.Name))
                    {
                        y++;
                    }
                }
                if (y > 0)
                {
                    Chords.Remove(i);
                }
            }
        }

        public void serializer_UnknownNode
        (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        public void serializer_UnknownAttribute
        (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}