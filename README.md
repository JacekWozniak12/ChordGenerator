# ChordGenerator
Let me create some funk for you.

## Legal
All rights of quoted sources in part *Used Materials* are in hold of their respective owners. 

## Used Libraries
https://github.com/naudio/NAudio

## Description
Chord Generator is simple program written in C# and Windows Presentation Forms. It lets an user create chords from a text formatted by custom syntax standards, which is written below. Created chords will be displayed as img and be avalaible to play with Midi synthesizer. 

## Features
* Custom syntax for Chords
* Last 5 chords are saved within app history
* Generated guitar chord diagram

## How To Use
User can write his text into the prompt. After that, user should click *generate*, which will generate the chord image and sound to play by button. User will have to write his chord in very specific manner. Settings can be changed via prompt, or by menu Window.

### Syntax

#### Settings
"{Name of setting}`[value of setting`]"

#### Chord notes
* Single note examples:
  * *A4* -> Note A4
  * *A4 + 5* -> Note D4
  * *A4 - 1* -> Note G4b / G4#
* Multi note examples:
 * *A4 ^ E4* -> Powerchord A4
 * *A4 ^ D4 + 2* -> Powerchord A4
 * *A4 ^ C4 ^ E4* -> A minor chord

## Implementation

### Database - notes
* Sound note database should be initialized from **C0** to **C9**
* Init once, give base freq to each one note, based on freqs from this [page]( https://pages.mtu.edu/~suits/notefreqs.html )
* Octave 
  * Starts from ***C*** note
  * Contains 12 notes
    * Third octave consists of notes 
      * {***C3, C#3, D3, D#3, E3, F3, F#3, G3, G#3, A3, A#3, B3***} 
      * Note naming depends on system and context, you can for example meet with notes like ***Db3***, which in context of our program is the same thing as ***C#3***

* Each time when single note frequency is changed, change all to correspond the change.

### Sound engine
Using NAudio;

### GUI
Using WPF XAML

#### Guitar chord diagram
* Standard EADGBE tuning with 24 frets
* Possible chords to play or also those impossible aswell?
  * Possible
    * One note per string
    * Notes can't be stretched more than 5 frets and empty
    * Empty strings are counted before anything else
    * More important notes first
  * Impossible:
    * All notes within a string
    * Notes can't be stretched more than 5 frets and empty
    * Importance does not matter
* History: 
  * From fresh to old.
  * 5 last chords
  * Contains only chord scheme, without additional settings

#### Window

Standard Windows Metro theme

* Modern flat UI
* Rather simple
* *Roboto* or *Montserrat* font suggested.

## Used materials
### Learn X in Y Minutes
* [C# features]( https://learnxinyminutes.com/docs/csharp/ ), most notable:
  * *params string[]*, the way that we can pass objects or arrays

     ```c#
     int MethodSignatures(
              int maxCount, // First variable, expects an int
              int count = 0, // will default the value to 0 if not passed in
              int another = 3,
              params string[] otherParams 
     			// captures all other parameters passed to method
          )
          {
              return -1;
          }
     
     ```
  * *where*, the way of narrowing down types of objects
  
    ```c#
    public static void IterateAndPrint<T>(T toPrint) where T: IEnumerable<int>
            {
                // We can iterate, since T is a IEnumerable
                foreach (var item in toPrint)
                    // Item is an int
                    Console.WriteLine(item.ToString());
            }
    ```

### Microsoft Documentation
* [WPF tutorial]( https://docs.microsoft.com/en-us/visualstudio/designers/introduction-to-wpf?view=vs-2019 )

### Youtube
* [C++ Sound Implementation tutorial]( https://www.youtube.com/watch?v=tgamhuQnOkM )
* [WPF tutorial]( https://www.youtube.com/watch?v=gSfMNjWNoX0 )

### Other
* [Frequencies of musical notes]( https://pages.mtu.edu/~suits/notefreqs.html )
* [Chord symbols and how they are made]( http://www.guitarchordspedia.com/chords/chord-symbols/ )
* [Midi Format]( https://www.csie.ntu.edu.tw/~r92092/ref/midi/ )

## Milestones
### 0.1
- Basic sound engine
- Sound fade-out
- Oscillators
### 0.2
* String to sound implementation
* Sound engine additions
### 0.3
* Playing chord one sound after another
* Playing chord as a whole in one step
### 0.4
* Guitar chord diagram
### 0.5
* Guitar chord diagram reworks
### 0.6
* Flat to sharp integrations
  * Simple method to convert flat to sharp if needed.
* Midi instruments integration
### 0.7
* Loading from the MIDI
  * Choosing one tick within midi track
### 0.8
* Saving chord to MIDI file
### 0.9
* Fixing stuff
### 1.0
* Fixes and refractoring 
