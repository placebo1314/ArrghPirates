# Arrgh Pirates – Részletes kódaudit és feature terv (HU)

## 1) Mi ez a projekt jelenleg?
Ez egy **Unity 2021.3** alapú korai prototípus, ami két fő irányt kever:
- egy **torpedó/battleship jellegű lövöldözős pályát** (fixen előre lerakott célpontokkal),
- és egy **inventory/dokk/legénység menedzsment** rendszert.

Fő jelenetek:
- `MenuScene` (navigáció),
- `SinglePlayBoard` (lövöldözés),
- `InventoryScene` (jelenleg félkész, több helyen instabil).

## 2) Mi használható jól már most?

### Jól újrahasznosítható alapok
1. **Játéktábla állapotkezelés alapja** (`TileScript`):
   - Rács méret konstansok (`Width`, `Height`),
   - lövés állapot (hit/miss/unknown),
   - hajópozíciók és maradék hajók számlálása.
2. **Célzás és lövés loop alap** (`SelectTarget`):
   - kurzor mozgatás a rácson,
   - lövés korlátozás, találat/mellé logika,
   - HUD frissítések.
3. **Egyszerű menü és scene váltás** (`MenuScript`).
4. **Asset állományok** (hajó/pirate prefabok, textúrák) mint tartalom-mag.

Ezekből gyorsan lehet MVP-t építeni.

## 3) Mi problémás / mi egyértelműen rossz?

### Kritikus (stabilitást/fordítást törő) problémák
1. **`Inventory.SetupDocks` változó scope hiba**: a `newShip` használva van úgy, hogy egy ágban nincs deklarálva. Ez fordítási hiba.
2. **Null kezelési hiány**: ha nincs mentés (`stats.txt`), az inventory inicializáció könnyen `NullReferenceException`.
3. **UI eseménykezelő lifecycle hiány**: gomb listener-ek feliratkoznak, de nincs leiratkozás scene újratöltésnél (dupla callback veszély).
4. **Drag&Drop védtelen pointer kezelés**: üres felületre dobásnál NRE kockázat.

### Architektúra / technikai adósság
1. **Domain logika MonoBehaviour-ekbe szórva**: nehezen tesztelhető, nehezen bővíthető.
2. **Erős scene-object függés (`GameObject.Find`)**: törékeny és lassan skálázódik.
3. **Perzisztencia vegyes minőségű**:
   - `TypeNameHandling.All` biztonsági kockázat (ha fájl manipulálható),
   - ASCII használat UTF-8 helyett,
   - hardcodeolt mentési mezők (`DockSize=2`, `Lvl=1`).
4. **Nincs tiszta modulhatár a “board combat” és “fleet/inventory” között**.

## 4) Mit érdemes megtartani, újraírni, törölni?

## Megtartandó
- `TileScript` mögötti adatszerkezet szemlélet,
- `SelectTarget` játékérzet-kezdeménye (HUD + lövés feedback),
- prefab és vizuális asset készlet,
- névgenerálás ötlete (de implementáció cserélendő).

## Újraírandó (magas prioritás)
1. **Inventory modul** (`Inventory`, `DragAndDrop`, `ShipScript` egy része)
   - Külön domain szolgáltatásra bontás:
     - `FleetService`, `CrewAssignmentService`, `SaveGameService`.
2. **Perzisztencia** (`DataSaver`)
   - UTF-8, verziózott mentési modell,
   - biztonságos serializer beállítás,
   - default save bootstrap.
3. **Board setup**
   - fix `BoardLayouts.BasicFleet` helyett dinamikus generálás + seedelhető random.
4. **Input/UI kötés**
   - közvetlen gombfüggés helyett command jellegű metódusok.

## Törlendő / kivezetendő
- félkész/stub metódusok (`SetupOnBoardPirates` stb.) amik csak zajt adnak,
- duplikált vagy nem használt konstans/osztályok,
- ritkán használt, sérülékeny `GameObject.Find` hívások.

## 5) Van-e felesleges elem?
Igen, valószínűleg van:
- sok legacy prefab variáns (2x/Original/teszt verziók),
- félbehagyott inventory-flow script mezők,
- egymást átfedő “dockless/dock” prefab logikák.

Javaslat: készíts „**used-by-scene**” auditot és ami semmilyen scene/prefab láncon nem referált, azt archíváld majd töröld.

## 6) Célarchitektúra a kalózos torpedó játékhoz

## Rövid vízió
Egy körökre osztott, torpedó-szerű tengeri taktikai játék:
- **dinamikus játéktér** (sziget, sekély víz, köd, vihar),
- **fejleszthető hajók** (fegyverzet, páncél, manőverezhetőség),
- **kalóz legénység** (szerepek + bónuszok),
- PvE kampány fókusz.

## Modulok
1. **Combat Core**
   - grid + fog of war + találati modell,
   - turn resolver.
2. **Fleet & Crew**
   - hajó statok + slotok,
   - kalóz role rendszer (kapitány, lövész, kormányos, matróz).
3. **Meta Progression**
   - jutalmak, zsákmány, fejlesztések,
   - kikötői fejlesztések.
4. **Content System**
   - ScriptableObject alapú item/ship/crew definíciók.

## 7) Konkrét feature roadmap (ajánlott)

## Fázis 1 – Stabil MVP (2–3 hét)
- Inventory fordítási hibák és null-kezelés javítás.
- Dinamikus board generátor (ship placement rules).
- Egyszerű AI: random + “hunt” mód találat után.
- Egy kör = játékos lövés + AI lövés.
- Save/load v2 alapmodell.

**Kimenet:** teljes meccs játszható elejétől végéig.

## Fázis 2 – Mélyebb taktika (2–4 hét)
- Hajó upgrade rendszer (ágyú, páncél, motor/vitorla).
- Legénységi bónuszok és sérülés/javítás.
- Pályamódosítók: köd, szél, akadály.
- Kiegyensúlyozás + alap tutorial.

**Kimenet:** “kalózos torpedó” identitás érezhető.

## Fázis 3 – Tartalom és polish (folyamatos)
- Több hajóosztály és ellenfél frakció.
- Küldetés típusok (konvoj vadászat, kikötőrombolás).
- UI/UX tisztítás, animáció polish, hangok.
- Telemetria + balancing iteráció.

## 8) Javasolt technikai next-step backlog (prioritás szerint)
1. **Blocker fix pack**: Inventory compile + null guard + drag/drop guard.
2. **Domain extraction**: board/inventory logika kivétele tiszta C# service-ekbe.
3. **Test alapok**:
   - EditMode unit teszt: board rules, shot resolver,
   - PlayMode smoke: scene boot + alap flow.
4. **Data migration**: biztonságos, verziózott save formátum.
5. **Content refactor**: ScriptableObject definíciók a hardcode helyett.

## 9) Rövid döntési javaslat: mit dobnánk ki most azonnal?
- Minden olyan inventory kódrészt, ami nincs aktív scene flow-ban és csak félkész placeholder.
- Minden olyan asset variánst, amire nincs referenciád scene/prefab dependency graphban.
- Minden olyan utilt, ami csak egyszer használatos és közben növeli a couplingot.

---

## Zárás
A projekt **nem menthetetlen**, sőt: jó alapja van egy hangulatos kalózos taktikai játéknak. A kulcs, hogy a mostani prototípus jellegű script-halmazt gyorsan át kell fordítani **moduláris, tesztelhető** játékmagra, és az inventoryt stabilizálni kell, mielőtt új feature-eket raknátok rá.
