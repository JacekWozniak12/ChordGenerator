# Chord Generator

Application. that let's you test note combination by using **custom** syntax. It lets an user create chords from a text formatted by custom syntax standards, which is written below. Created chords will be displayed as image and be available to play with basic NAudio synthesizer. 

## Features

- Custom syntax for Chords
- Chords are saved within app directory
- Generated guitar chord diagram
- Frequency based on [equal-tempered scales](https://pages.mtu.edu/~suits/notefreqs.html)

## How To Use

User can write his text into the prompt. User will have to write his chord in very specific manner described below. Syntax is case sensitive but let's user make mistakes. After that, user should click *generate*, which will generate the chord which be displayed on screen and being able to play by button.

User can change the settings of app for his liking, but he need to save them before they'll be applied to played chord. 

### Syntax

#### Chord notes

##### Symbols

* `+` Add semitone amount
* `-` Subtract semitone amount
* `^` Add another note to chord

##### Examples

- Single note examples:
  - `A4` Note A4
  - `A4 + 5` Note D4
  - `A4 - 1` Note Ab4 / G#4
  
- Multi note examples:
  - `A4 ^ E4` Powerchord A4
  - `A4 ^ D4 + 2` Powerchord A4
  - `A4 ^ C4 ^ E4` A minor chord


## Used Software / Libraries

* Program written in C# and Windows Presentation Forms. 
* Icon created in Inkscape and GIMP
* Audio rendering provided by https://github.com/naudio/NAudio
* Guidance from Microsoft on Serialization

## Legal

All rights of quoted sources in part *Used Software / Libraries* are in hold of their respective owners.
