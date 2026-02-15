# Arrgh Pirates – Product Plan (újratervezett) v2.0

**Platform:** Android / Google Play  
**Termékcél:** stabil, mobilra optimalizált, publikálható kalózos torpedó/taktikai hajóharc játék  
**Designcél (MVP):** 2–5 perces, köralapú tactical match loop (`deploy → körök → győzelem/vereség → jutalom`)  

---

## 1) Megtartott prototípus elemek (integráció, nem újratervezés)

- **KEEP-001 – Board lövés alaplogika:** találat/mellé + HUD feedback alapelvek maradnak.
- **KEEP-002 – Menü + scene navigáció alapok:** flow megtartható, de lifecycle fix kötelező.
- **KEEP-003 – Vizuális asset készlet:** jelenlegi hajó/pirate/icon/sprite sheet marad, atlasz-optimalizáció megengedett.

---

## 2) Scope és prioritás

### P0 (MVP kötelező, release blocker)
- **Stabilizáció:** STAB-001..005
- **Match loop:** GAME-001..005
- **Combat minimum:** COMBAT-001..002
- **Felderítés:** VISION-001
- **Hajók:** SHIP-001..002
- **Legénység:** CREW-001..002
- **AI:** AI-001..002
- **Meta:** META-001..002
- **Mobil UX:** UX-001..004
- **Release readiness:** REL-001..005

### P1 (MVP után közvetlenül)
- COMBAT-003..005, SHIP-003..004, CREW-003..005, META-003..004, UX-005

### P2 (későbbi live expansion)
- Permadeath, komplex sérülés (fire/flooding/engine), pályaesemények, aszinkron PvP ghost fleet, teljes boarding combat.

---

## 3) Részletes feature codex (implementációs bontás)

## 3.1 Stabilizáció és alap rendszerek (P0)

### STAB-001 – Inventory init + save fallback (null-biztos)
**Cél:** clean install/üres/korrupt save mellett is végigjátszható flow.  
**Megoldás:** save loader guard + default inventory/ship/crew seed.  
**AC:**
- Clean install után tutorial nélkül is elindítható match.
- Hibás save esetén fallback + log, nincs kivétel.

### STAB-002 – Drag&Drop hibatűrés
**Cél:** félre-drag ne okozzon crash-t.  
**Megoldás:** üres target `no-op`, invalid target esetén visszapattanás + rövid hibaüzenet.  
**AC:** 100 hibás drag sorozat kivétel és state duplikáció nélkül.

### STAB-003 – UI listener lifecycle
**Cél:** scene reload után ne duplázódjanak callbackek.  
**Megoldás:** egységes subscribe/unsubscribe `OnEnable/OnDisable/OnDestroy` szabály szerint.  
**AC:** 10× board scene nyit-zár után input event darabszám nem nő.

### STAB-004 – Mentés/olvasás robusztus adatkezeléssel
**Cél:** UTF-8 + verziózott schema (`SaveVersion`) + fallback defaultok.  
**AC:** régi save migrációval vagy fallbackgel betölthető.

### STAB-005 – PlayMode smoke test
**Cél:** minimális automata quality gate.  
**AC:** board scene boot + 1 lövés + vissza menübe hiba nélkül.

## 3.2 Core gameplay – Match loop (P0)

### GAME-001 – Dinamikus játéktér-generálás
- Seedelt, reprodukálható grid generátor (S/M/L profil).
- Opcionális akadály támogatás (MVP-ben lehet `none`).
- **AC:** mobilon <200ms generálás.

### GAME-002 – Kezdő telepítés két térfélen
- Saját félre helyezés, ütközés és pályahatár tiltás.
- Egyszerű orientációs szabály (MVP: tengelyhez kötött).
- **AC:** invalid lerakás nem menthető, UI egyértelmű hibajelzést ad.

### GAME-003 – Körsorrend (initiative)
- Formula: `Initiative = baseSpeed × állapotSzorzó × modul/crew bónusz`.
- Sérülés lassít, fejlesztés gyorsít.
- **AC:** determinisztikus sorrend + HUD listanézet.

### GAME-004 – Körfázisok
- **A:** Move (MVP-ben opcionális),
- **B:** Fire,
- **C:** End-of-round tickek.
- **AC:** kilépés/alt-tab esetén state nem sérül.

### GAME-005 – Match lezárás + summary + jutalom
- Győzelem: ellenfél flotta harcképtelen.
- Summary: találati arány, süllyesztések, kapott sebzés.
- Jutalom: arany + alap nyersanyag.
- **AC:** match mindig lezárható, summary null-safe.

## 3.3 Harcrendszer (P0/P1)

### COMBAT-001 (P0) – ShotsPerTurn és rész-sérülés hatás
- `ShotsPerTurn = aktívÁrbócokSzáma` (MVP), crew/weapon módosítóval.
- Rész-sérülés csökkentheti lövésszámot/pontosságot.
- **AC:** sérüléshatás következő körben azonnal érvényes.

### COMBAT-002 (P0) – Fegyver range + célzás validáció
- Hatótávon kívüli lövés tiltása + UI `out of range` feedback.
- **AC:** invalid lövés nem küldhető be.

### COMBAT-003 (P1) – Perzisztens veszélyzóna
- Időzített hazard koordináták (égő/szilánkmező).
- **AC:** hazard timer kör-alapú, nincs végtelen állapot.

### COMBAT-004 (P1) – AoE fegyverek
- Több cellát érintő mintázatok + előnézet (ghost).

### COMBAT-005 (P1) – Grapple stub
- Közeli hajók esetén gomb látható.
- MVP után bővítés valós boardingra.

## 3.4 Felderítés (P0)

### VISION-001 – Látómező/Fog of war
- Radius + crew/upgrade bónusz.
- Ismeretlen cellák elsötétítve, ellenségpozíció nem leakelhető.
- **AC:** nincs UI/tooltip exploit.

## 3.5 Hajók és állapot (P0/P1)

### SHIP-001 (P0) – Hajóosztályok
- Legalább 3 sablon: light/medium/heavy (`HP`, `baseSpeed`, `armor`, `mastCount`, `crewSlots`).

### SHIP-002 (P0) – Sérülésmodell
- `Hull HP` + `Mast HP` hatás speed/shots statokra.

### SHIP-003 (P1) – Port javítás/tisztítás
- Költségalapú helyreállítás + UI visszajelzés.

### SHIP-004 (P1) – Modul slotok
- Cannon/Sail slotok, drag&drop kompatibilitás STAB-002 szerint.

## 3.6 Legénység (P0/P1)

### CREW-001 (P0) – Role bónuszok
- Captain/Gunner/Helmsman/Sailor szerepek mérhető stat-hatással.

### CREW-002 (P0) – Crew hozzárendelés hajóhoz
- Crew slot alapú assignment, üres slot megengedett (hátránnyal).

### CREW-003..005 (P1+) – Gear, XP/skill, sérülésmodell
- MVP után fokozatos bővítés.

## 3.7 AI (P0)

### AI-001 – Lövés logika (random + hunt)
- Találat után szomszéd prioritás.
- **AC:** 100 szimuláció softlock nélkül.

### AI-002 – Deploy logika
- Saját térfélen mindig valid kezdő lerakás.

## 3.8 Meta progresszió (P0/P1)

### META-001 (P0) – Port menü alap
- Ship management, Crew, Start battle, Rewards inbox.

### META-002 (P0) – Economy minimum
- Arany + nyersanyag, negatív egyenleg tiltás.

### META-003..004 (P1)
- Hajóvásárlás/upgrade, daily/quest.

## 3.9 Mobil UX (P0/P1)

### UX-001..004 (P0)
- Touch-first, nagy targetek, rövid körök, olvasható HUD, teljesítményprofilok.

### UX-005 (P1)
- Accessibility bővítés (színvak mód, nagyobb font).

## 3.10 Google Play release readiness (P0)

- **REL-001:** AAB pipeline + signing + versioning.
- **REL-002:** Privacy policy + Data Safety.
- **REL-003:** Crash reporting + analitika.
- **REL-004:** Store listing assetek.
- **REL-005:** Beta track → staged rollout.

---

## 4) Delivery terv (8–10 hét, release-ready MVP)

### Sprint 0 (0.5 hét) – Tervezési baseline
- Feature flag + seed stratégia + telemetry eseménylista.
- DoD/AC checklista véglegesítése.

### Sprint 1 (1.5 hét) – Stabilizációs fal
- STAB-001..005 lezárás.
- Save migration + smoke test csővezeték.

### Sprint 2 (2 hét) – Match core
- GAME-001..004 + COMBAT-001..002 + SHIP-001..002.
- Determinisztikus initiative + HUD sorrend.

### Sprint 3 (1.5 hét) – AI + Vision + Meta minimum
- AI-001..002 + VISION-001 + META-001..002 + CREW-001..002.

### Sprint 4 (1.5 hét) – Mobil UX + teljesítmény
- UX-001..004 véglegesítés, low/med/high profilok.
- 2–5 perces match-idő validáció analitikából.

### Sprint 5 (1–2 hét) – Play kiadási readiness
- REL-001..005, internal/beta, staged rollout előkészítés.

---

## 5) Minőségi kapu (DoD)

## DoD-CORE
- Nincs blocker crash az első 10 percben (clean install, save nélkül).
- Match flow teljesen végigjátszható.
- Scene reload után input event nem duplázódik.
- 60 FPS cél közepes eszközön, 30 FPS minimum low-enden.
- Play Vitals cél: **ANR < 0.47%**, **crash-free users > 99%**.

## DoD-QA (automatizálás + manuális)
- PlayMode smoke teszt kötelező minden builden.
- Save/load regresszióteszt: üres, régi verzió, korrupt save minták.
- AI szimuláció (100+ meccs) softlock és végtelen kör ellen.

---

## 6) Kockázatok és mitigáció

- **Kockázat:** scene lifecycle regressziók.  
  **Mitigáció:** centralizált listener binding + reload stress test.

- **Kockázat:** mobil teljesítmény esés nagyobb boardon.  
  **Mitigáció:** grid profilok, pooling, quality profile defaultok.

- **Kockázat:** scope creep (P1/P2 elemek befolynak MVP-be).  
  **Mitigáció:** P0 freeze sprint 3 után, P1 backlog lock.

---

## 7) MVP utáni bővítési irány

1. COMBAT-003/004 hazard + AoE fegyverek.
2. SHIP-003/004 port karbantartás + modulrendszer.
3. CREW progression (XP/skill) és mélyebb meta.
4. Accessibility és élő üzemeltetési event-rendszer.

---

## 8) Kötelező végrehajtási szabály: minden lépés után tesztelhető build

Ez a dokumentum innentől **stage-gate** módszerrel értelmezendő:

1. **Egy feature-t egyszerre** szállítunk (pl. STAB-001), nem több félkész elemet párhuzamosan.
2. Minden feature végén kötelező:
   - automata tesztek futtatása,
   - célzott manuális teszt lefuttatása,
   - rövid tesztjegyzőkönyv rögzítése.
3. **Csak sikeres tesztek után** jelölhető készre az adott feature/sprint.
4. Csak ezután indulhat a következő feature.

### 8.1 Feature státuszok (kötelező jelölés)

- `[ ] TODO` – még nincs implementáció
- `[-] IN PROGRESS` – aktív fejlesztés
- `[T] TESTING` – implementálva, teszt alatt
- `[x] DONE` – automata + manuális teszt sikeres, bizonyíték rögzítve
- `[!] BLOCKED` – külső akadály vagy regresszió

### 8.2 Kötelező feature záró checklist (minden itemre)

- [ ] AC pontok ellenőrizve (feature saját Acceptance Criteria)
- [ ] Automata tesztek zöldek (PlayMode/smoke + releváns regresszió)
- [ ] Manuális teszt script lefutott
- [ ] Nincs új blocker/critical bug
- [ ] Feature státusz `DONE`-ra állítva
- [ ] Következő feature `IN PROGRESS`-re nyitva

---

## 9) Manuális teszt forgatókönyvek (indítástól feature validálásig)

## 9.1 Standard tesztelési előkészítés (minden feature előtt)

1. Tiszta állapot indítása:
   - alkalmazás reinstall vagy app data törlés,
   - opcionálisan korrupt/legacy save tesztfájl előkészítése.
2. Build indítása teszt módban (dev/profiling build).
3. Ellenőrzés, hogy a főmenü betölt és nincs azonnali exception.

## 9.2 Smoke útvonal (minden feature után kötelező)

1. App indítás.
2. Main Menu → Start/Board.
3. Board scene betölt.
4. 1 érvényes lövés leadása.
5. Visszalépés Main Menu-be.
6. App újraindítás és save/load ellenőrzés.

**Elvárt eredmény:** nincs crash, nincs input duplikáció, state konzisztens.

## 9.3 Feature-csoportonkénti manuális teszt script (MVP/P0)

### STAB (STAB-001..005)
- Clean install + save nélküli indítás → játék játszható.
- Korrupt save injektálás → fallback megtörténik, flow nem törik.
- 100 invalid drag/drop művelet → no exception, UI visszaáll.
- Board scene 10× nyit-zár → callbackek nem szaporodnak.

### GAME (GAME-001..005)
- Több seeddel pályagenerálás (S/M/L), reprodukció ellenőrzés.
- Kezdő lerakás validáció (ütközés, határ, orientáció).
- 1 teljes match lefuttatás summary képernyőig.
- Jutalom jóváírás economy-ban.

### COMBAT P0 (COMBAT-001..002)
- Árbóc sérülés után következő körben lövésszám csökkenés.
- Hatótávon kívüli célra lövés tiltva + UI jelzés.

### VISION (VISION-001)
- Fog of war: ismeretlen cellák eltakarva.
- Ellenség pozíció csak látótávban látható.

### SHIP/CREW P0 (SHIP-001..002, CREW-001..002)
- 3 hajóosztály statjai különböznek és hatnak a körökre.
- Crew role assignment mentődik és stat-hatása mérhető.

### AI (AI-001..002)
- AI deploy mindig szabályos.
- AI random+hunt viselkedés megfigyelhető, nincs softlock.

### META/UX/REL P0
- Port menü navigáció minden főpontra működik.
- Economy nem mehet negatívba.
- Touch targetek mobilon elég nagyok; HUD olvasható.
- AAB/signing/privacy/analytics/store checklist ellenőrizve.

## 9.4 Sprint szintű manuális exit tesztek

- **Sprint 1 exit:** STAB teljes regresszió + smoke útvonal 3 egymás utáni futásban.
- **Sprint 2 exit:** teljes match loop teszt (deploy→win/lose→summary) 5 futással.
- **Sprint 3 exit:** AI + vision + crew/meta integrációs teszt 5 futással.
- **Sprint 4 exit:** low-end teljesítmény és HUD/UX olvashatóság validáció.
- **Sprint 5 exit:** release checklist dry-run + beta rollout próba.

---

## 10) Tesztjegyzőkönyv sablon (kötelező minden lezárt feature után)

```
Feature ID: STAB-00X / GAME-00X / ...
Build verzió: vX.Y.Z (commit: <hash>)
Teszt dátum: YYYY-MM-DD
Tesztelő: <név>

Automata tesztek:
- [PASS/FAIL] PlayMode smoke
- [PASS/FAIL] Releváns regressziók

Manuális lépések:
1) ... [PASS/FAIL]
2) ... [PASS/FAIL]
3) ... [PASS/FAIL]

Eredmény:
- [ ] DONE (következő feature indítható)
- [ ] BLOCKED (hibajegy: <ID>)

Megjegyzés:
- Kockázat / ismert limitáció / következő lépés
```

### 10.1 Döntési szabály a következő feature indításához

- **Ha bármely kötelező teszt FAIL**, a feature nem zárható le (`DONE` tiltott).
- Hiba javítása után kötelező teljes újratesztelés (automata + manuális).
- Csak teljesen zöld eredmény után javasolható a módosítás és nyitható a következő feature.
