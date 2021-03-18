# FastLoader

FastLoader is the official API for Fast and Low, this is completly open source to help modders for Unity and all around. 

## Wiki

There is a [wiki](https://github.com/BIGDummyHead/FastLoader/wiki) for this project!

## Injected Code

```csharp
Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), "FastLoader.dll"))?.GetType("FastandLow.Bootstrap.Boot")?.GetMethod("StartMods", (BindingFlags)(-1))?.Invoke(null, new object[0]); 
```

This is the injected code into the game, this code was manually put in by the devloper, but you can inject your own code using my other library : [BIGDummyHead/DummyLib](https://github.com/BIGDummyHead/Dummy-Lib)

## Copy Right Info
There will be no copyright info, as I see there is no reason to do so. You would have no use for using this on the game that it is meant for so feel free to take code from it and use it wherever you like. I hope this helps some people get started in making their own loader!

## Safety / Legality
I and the developer of Fast and Low are not responsible for any Mod(s) that may contain Dynamic Link Libraries that execute malicous code. You download Mod(s) at your own risk. Also, beware of copy versions of the project that do not have the source code uploaded, I am not responsible for these versions as they are not made by me and not distributed by me. On another note please be careful of what you download, since this project is open-source you can review the code inside the solution and compile it yourself if you do not feel safe, other versions may not share this same ability.

## How to spot Mods with Malware
1. Mods with malware may be in the form of Executables don't launch them
2. Mods can be decompilied with [dnSpy](https://github.com/dnSpy/dnSpy) - you can search through and report these mods
3. Look for inconsistensies 
   * The mod does not contain a .mod file 
   * The mod does not contain a dll file
   * The mod contains other files than mentioned in the description 
4. Make sure that the mod was uploaded by a trusted user in the community - While anyone can upload trusted members won't upload malicous mods...

This list was not to scare you and we have had 0 reports of Malicous mod reports...

## Used Libraries
* [pardeike/Harmony](https://github.com/pardeike/Harmony) - [Harmony Docs](https://harmony.pardeike.net/)
* [BIGDummyHead/DummyReflection](https://github.com/BIGDummyHead/DummyReflection)
