# FastLoader

FastLoader is the official API for Fast and Low, this is completly open source to help modders for Unity and all around. 

## Injected Code

Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), "FastLoader.dll"))?.GetType("FastandLow.Bootstrap.Boot")?.GetMethod("StartMods", (BindingFlags)(-1))?.Invoke(null, new object[0]); 

This is the injected code into the game, this code was manually put in by the devloper, but you can inject your own code using my other library : [BIGDummyHead/DummyLib](https://github.com/BIGDummyHead/Dummy-Lib)


## Used Libraries
*[pardeike/Harmony](https://github.com/pardeike/Harmony) - [Harmony Docs](https://harmony.pardeike.net/)
*[BIGDummyHead/DummyReflection](https://github.com/BIGDummyHead/DummyReflection)
