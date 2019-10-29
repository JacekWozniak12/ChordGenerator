# ChordGenerator
Let me create some funk for you.

## Legal
All rights of quoted sources in part *Used Materials* are in hold of their respective owners. 

## Description

## Features
* Custom syntax for Chords
* Last 5 chords are saved within app history
* Generated guitar chord diagram

## How To Use

### Syntax
It's based on European music theory.

#### Settings
* **[*SoundValue*]{*Frequency*}** means frequency of predefined sound. 
  * The standard European frequency for note *A4* is *440 Hz* written like **[*A4*]{*440*}** 
  * Frequency can be change on all notes, but each change will change all frequencies
* ...

#### Chord notes
* **(X)** means base [it can be letter from *A* to *F*]
  * **(X)** can be for example note ***E3***
* **(X + value)** is syntax for notes, that are *value* higher from *X* 
  * For example (**X + 3**) means note ***G3***, which is 3 halfstep higher than **E3**.
* **(X - value)** is syntax for notes, that are *value* lower from *X* 
  * For example (**X + 3**) means note ***C#3***, which is 3 halfstep lower than **E3**.

## Implementation

### Database - notes
* Sound note database should be initialized from **C0** to **C9**
* Init once, give base freq to each one note, based on freqs from this [page]( https://pages.mtu.edu/~suits/notefreqs.html )
* Octave 
  * Starts from ***C*** note
  * Contains 12 notes
    * Third octave consists of notes 
      * {***C3, C#3, D3, D#3, E3, F3, F#3, G3, G#3, A3, A#3, B3***} 
      * Note naming depends on system and context, you can for example meet with notes like ***Db3***, which is the same thing as ***C#3***
      * Let's pretend for now that we can write only with sharps.

* Each time when single note frequency is changed, change all to correspond the change.
* Write database as array of the objects based on class

  ```c#
  public class MusicalNote
  {
      private string Name;
      private float Frequency;
      
      public MusicalNote(string name, float freq)
      {
          if( IsValidName(name) )
          	Name = name;
          if( IsValidFrequency(freq) )
          	Frequency = freq;
          if (Name == Null || Frequency == Null)
              Throw New System.ArgumentException("Object lacks name or frequency")
      }
      
      public void ChangeMusicalNote(float freq, bool test)
      {
          if (test) 
              if (!IsValidFrequency(freq))
              {
                 Throw New System.ArgumentException("Frequency out of range")
                 return;
              };
         
           Frequency = freq;
      }
      
      public void ChangeMusicalNote(float freq)
      {
          ChangeMusicalNote(freq, false)
      }
      
      private bool IsValidName()
      {
          // length test
          	// C#3 is 3 symbols
          	// C2 is 2 symbols
          // passed length test
          	// check first char for range <A, G>
          	// check last char for range <0, 9>
          	// if length = 3 check mid symbol for #
          // if passed then true      	
      }
      
      private bool IsValidFrequency()
      {
          // is number?
          // is within range?
          // if passed then true 
      }
  }
  ```
  
* There should be initialized two databases, one which hold change and another, that is fallback, if first one got issues within.

* Database should be changed in order:

  	1. First element (check if is possible to make new frequency)
   	2. Last element (check if is possible to make new frequency)
   	3. Second first to last but one

### Syntax reader
* Order
  * Settings
    * Frequency
    * ...
  * Notes
* Error handling
  * General issues
    * Syntax misspellings
    * Not closed brackets
      * Content within brackets should be only made out of 1 string.
      * Check for type of brackets.
    * Notes out of range 
  * Settings - notes
    * Frequency out of range 

### Sound engine
wip

### GUI
wip

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
