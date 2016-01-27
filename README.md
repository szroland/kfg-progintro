# Info szakkör

## C# bevezetés

### Pálya

A pálya gyakorlatilag egy üres síkból áll, e fölött fogunk különböző objektumokkal repkedni.
A grafikai elemekkel nem sokat fogunk bajlódni, hogy a programozásra tudjunk koncentrálni.

Alapból a pályán egy darab fánk formájú repülőgép található, egy nagyon egyszerű programmal: felszáll, lebeg, leszáll. A feladat az lesz, hogy érdekesebb dolgokra vegyük rá.

A Unity specifikus dolgokat elrejtettem a ```ScriptedController``` osztályba, az általunk írt vezérlő programoknak ebből kell majd származni, és egy egyszerűsített API-t kapnak ez által.

### Az alap vezérlő: ```Egyszeru```

Nyissuk meg a fájlt és nézzük meg a kódot. Objektum elnevezés, adattagok, metódusok, öröklés. Függvény hívás, paraméterek. Egyszerű típusok: string, bool, int, float, double.

Öröklött metódusok a vezérléshez:

- Varakozik(int masodperc)
- Motor(bool be)
- Felirat(string szoveg)

- Elmozdul(float x, float y, float z)
- Fordul(float szog)
- Elore(float tavolsag)
- Menj(float x, float y, float z)

- Kidob(GameObject prefab)

### Referencia más objektumokra

Állítsuk be, hogy a repülő kidobjon egy gömböt, mielőtt leszáll.
Publikus tagváltozó, Unity editorból típus alapján beállítás.

### Példányok, prefab-ek Unity-ben

Tegyünk fel még egy ```Repulo``` prefab-et, állítsuk át zöldre / nagyobbra, állítsuk át a vezérlő szkriptjét, dobáljon más fajta objektumot (pl. zöld kockát)

### Egyszerű vezérlés: for ciklus

Dobáljunk ki egymás után 3-4 gömböt. Mi lenne, ha sokat kellene kidobni ugyanígy: for ciklus

### Elágazás: if / else

Legyen még egy fajta kidobható objektum. Felváltva dobáljuk egyiket majd a másikat.
Műveletek, bool kifejezések.

### Lokális változók

Először 1, majd 2, majd 3 stb. gömböt dobjunk ki ugyanazon a helyen!
Beágyazott for ciklus.

### Haladó (Memória kezelés)
Heap, stack. Class, struct (referencia vs. value type).
Garbage collector.

### Haladó (Threading)

Nézzük meg a ```ScriptedController```-t. Thread-ek, thread-ek közötti kommunikáció. Implementáljunk új API-kat magasabb szintű vezérléshez.

### Haladó (Dinamikus rendszer szabályozása)

A repülést egy egyszerű PD szabályozó vezérli a ```target``` bemenettel. PD szabályozó, arányos és differenciáló tag szerepe, paraméterválasztás jelentősége (beállási idő, túllengés).





