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

## Used Libraries
* [pardeike/Harmony](https://github.com/pardeike/Harmony) - [Harmony Docs](https://harmony.pardeike.net/)
* [BIGDummyHead/DummyReflection](https://github.com/BIGDummyHead/DummyReflection)
