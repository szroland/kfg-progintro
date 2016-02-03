# Info szakkör

## 2016.02.03 

### Változók, típusok, tömbök

### Új metódusok a ```ScriptedController``` osztályban
```csharp

//Megkeresi és visszaadja az adott tag-gel rendelkező objektumokat
GameObject[] Keres(string tag);

//Megkeresi és visszaadja az adott tag-gel rendelkező objektumok közül az elsőt
GameObject KeresEgy(string tag);

//Felszedi az alattunk talált objektumot, ha changeTag igaz, átállítja a tag-jét
Felszed(bool changeTag = false);    

//Elengedi a korábban felszedett objektumot, megadott kezdeti sebességgel (előre)
Elenged(float kezdetiSebesseg = 0);

//A megadott cél objektum irányába fordul
Fordul(GameObject cel);

//Menjen a megadot cel objektum helyéhez képest megadott pozícióra (deltaX, deltaY, deltaZ eltéréssel)
Menj(GameObject cel, float deltaX, float deltaY, float deltaZ);

//tetszőleges parancs végrehajtása a fő Unity szálon (delegate {})
//majd fixedUpdateFeltetel hívása addig, amíg true-val nem tér vissza (opcionális)
Vegrehajt(Execute parancs, IsDone fixedUpdateFeltetel = null);
```

### Új prefab-ek:

- Gyár: Megadott típusú prefab-eket példányosít, ha üres a lerakó rész (pl. elvittük a korábban gyártott elemet). Több prefab is megadható, akkor ezeket sorban egymásután, vagy véletlenszerűen gyártja (Veletlen Sorrend beállítástól függően). Tag-et is tud állítani a legyártott objektumokra, a BeallitandoTag beállításban lehet ezt megadni.

- Cél: Ledobási célterület, a beleeső objektumokat számolja és eltünteti

### Szállító: szállítsunk a Gyárból objektumokat a Célba.

### Ne vigyük az objektumot egészen a célig, dobjuk be a célba (haladó: számoljuk ki a jó dobáshoz szükséges kezdeti sebességet a távolság függényében)

### Építsünk falat az egyik repülővel. Másikkal romboljuk le. Romboljuk le úgy, hogy nekidobunk valamit.

### Több célterület: a gyár termeljen több féle elemet. Mindegyiket másik, a neki megfelelő célterületre vigyük.

### Haladó: csináljunk bombát, ami adott becsapódási sebesség fölött felrobban és erőhatást fejt ki a környezetében.
Környék objektumai: [Physics.OverlapSphere](http://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html)
Robbanás erőhatásai: [Rigidbody.AddExplosionForce](http://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html)

### Haladó: a robbanás helyén legyen vizuális robbanás effect (ParticleSystem a standard assetsben)

## C# bevezetés

### Pálya

A pálya gyakorlatilag egy üres síkból áll, e fölött fogunk különböző objektumokkal repkedni.
A grafikai elemekkel nem sokat fogunk bajlódni, hogy a programozásra tudjunk koncentrálni.

Alapból a pályán egy darab fánk formájú repülőgép található, egy nagyon egyszerű programmal: felszáll, lebeg, leszáll. A feladat az lesz, hogy érdekesebb dolgokra vegyük rá.

A Unity specifikus dolgokat elrejtettem a ```ScriptedController``` osztályba, az általunk írt vezérlő programoknak ebből kell majd származni, és egy egyszerűsített API-t kapnak ez által.

### Az alap vezérlő: ```Egyszeru```

Nyissuk meg a fájlt és nézzük meg a kódot. Objektum elnevezés, adattagok, metódusok, öröklés. Függvény hívás, paraméterek. Egyszerű típusok: string, bool, int, float, double.

Öröklött metódusok a vezérléshez:

```csharp
//Adott idig várakozik
Varakozik(float masodperc);

//Motor ki/be kapcsolása
Motor(bool be);

//Felirat beállítása adott szövegre
Felirat(string szoveg);

//Relatív elmozdulás a jelenlegi pozícióból
Elmozdul(float deltaX, float deltaY, float deltaZ);
//Relatív elfordulás a jelenlegi pozícióban
Fordul(float szog);
//Relatív elmozdulás adott távolságra abba az irányba, amerre nézünk (Transform.forward)
Elore(float tavolsag);

//Mozgás a megadott abszolút koordinátákra
Menj(float x, float y, float z);

//Megadott prefab-et példányosítja a jelenlegi pozíció alatt
Kidob(GameObject prefab);
```

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



