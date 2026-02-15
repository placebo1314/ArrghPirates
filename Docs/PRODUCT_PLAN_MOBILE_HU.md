# Arrgh Pirates – Product & Delivery terv (mobil / Google Play)

## Cél
Készítsünk egy stabil, mobilra optimalizált, kalózos torpedó-játékot, amit publikálni lehet a Google Play-re.

## Mit tartunk meg a jelenlegi prototípusból
- Board lövés alaplogika (találat/mellé, HUD visszajelzés).
- Menü és scene navigáció alapok.
- Hajó/pirate vizuális asset készlet.

## Mit írunk újra / stabilizálunk
1. Inventory inicializáció és save fallback (null-biztos működés).
2. Drag&Drop hibatűrés (üres drop target ne dobjon kivételt).
3. UI listener lifecycle (scene reload után ne duplázódjanak callbackek).
4. Mentés/olvasás kódolás (UTF-8) és adatkezelési robusztusság.

## Feature lista (MVP + javasolt új)

### A. Core gameplay (MVP)
- Dinamikus játéktér-generálás (nem fix layout).
- Hajóelhelyezés szabályok (ütközés tiltás, pályahatár ellenőrzés).
- Kör alapú játék: játékos + AI lövés.
- Győzelem/vereség állapot és jutalom.

### B. Hajó és kalóz rendszer
- Hajó osztályok: könnyű, közepes, nehéz.
- Fejleszthető statok: sebzés, páncél, látótáv, pontosság.
- Kalóz role bónuszok: kapitány/lövész/kormányos/matróz.
- Legénység hozzárendelés és hatás a harcra.

### C. Meta progresszió
- Port menü: javítás, fejlesztés, új hajó vásárlás.
- Napi jutalom és küldetésrendszer.
- Egyszerű economy (arany, nyersanyag).

### D. Mobil UX
- Touch-first vezérlés (nagy targetek, swipe támogatás).
- Rövid játékkörök (2–5 perc).
- Energiakímélő beállítások (FPS limit, quality profile).
- Kis kijelzőn olvasható HUD és kontrasztos UI.

### E. Google Play release readiness
- Android build pipeline (AAB), release keystore.
- Application ID, versioning, signing, min/target SDK validálás.
- Privacy policy + Data Safety form.
- Crash reporting és alap analitika.
- Store listing (ikon, screenshot, feature graphic, rövid/hosszú leírás).

## Feladatokra bontott végrehajtás

## Sprint 1 – Stabilizáció (1–2 hét)
- [x] Inventory null-safe fallback.
- [x] SetupDocks compile fix.
- [x] Drag&Drop null-safe drop kezelés.
- [x] SelectTarget listener lifecycle stabilizálás.
- [x] DataSaver UTF-8 + üres adat guard.
- [ ] PlayMode smoke test (board scene boot).

## Sprint 2 – Játszható MVP (2–3 hét)
- [ ] Dinamikus board generator.
- [ ] Hajóelhelyezés UI + validáció.
- [ ] AI lövés logika (random + hunt mode).
- [ ] Match summary + reward.

## Sprint 3 – Meta + mobil polish (2–4 hét)
- [ ] Upgrade UI és progresszió.
- [ ] Teljesítmény profilozás low-end Androidon.
- [ ] Tutorial és onboarding.
- [ ] Accessibility (színvak opció, nagyobb font mód).

## Sprint 4 – Play kiadás (1–2 hét)
- [ ] Beta (internal testing track).
- [ ] ANR/Crash triage.
- [ ] Store assetek véglegesítése.
- [ ] Production rollout (staged 5% → 25% → 100%).

## Kiadási minimális minőségi kapu (DoD)
- Nincs blocker crash az első 10 percben.
- Match flow végigjátszható új install után is (save nélkül is).
- 60 FPS cél közepes eszközön, 30 FPS minimum low-end eszközön.
- ANR < 0.47%, crash-free users > 99% (Play vitals cél).

## Következő javasolt bővítések
- Több státuszú hajósérülés rendszer (engine/fire/flooding).
- Pályaesemények: vihar, köd, áramlat.
- Szezonális eventek és limitált jutalmak.
- PvP aszinkron “ghost fleet” mód.
