# Lucid

A one-button auto-runner set in the soft, drifting logic of a dream.

![Status](https://img.shields.io/badge/status-in_development-yellow) ![Unity](https://img.shields.io/badge/Unity-2022.3_LTS-black) ![License](https://img.shields.io/badge/license-MIT-blue)

---

## About

The world in *Lucid* breathes between two colors — pink and teal — and so do you. Every jump flips your color. Obstacles only hurt you when their color doesn't match yours, so a single press of the space bar has to answer two questions at once: *where* do I want to be, and *what color* do I need to be when I land?

It's a small game about the almost-control of a lucid dream. You can feel the rhythm of the world. You can't quite slow it down.

## Roadmap (Next Two Weeks)

Development is focused on three pillars: **the color-flip core loop**, **game feel**, and **a cohesive dream aesthetic**.

The core loop is built around chunk-based procedural generation. Pre-designed level segments stream in ahead of the player and despawn behind, pulling from progressively harder pools as survival time grows. Each chunk contains a mix of three obstacle types — ground spikes, low-flying hazards that force a duck, and wide gaps that demand longer-held jumps — each tagged with a color that must match the player's current state to be safely passable. Obstacle data is backed by ScriptableObjects so new hazard variants can be tuned and added from the Inspector.

On game feel, every interaction is getting layered feedback: a satisfying squash on landing, a particle burst that carries the player's current color, pitch-laddered pickup sounds, subtle hitstop and screen shake on death, and camera smoothing that sells the forward momentum. A looping soundtrack will tie the experience together, with a stretch goal of syncing obstacle spawn timing to the beat.

Visually, *Lucid* is moving toward a soft pastel palette with Unity's 2D lit renderer — gentle bloom on collectibles, a subtle vignette, and a slow color-shifting background gradient that drifts the whole time you play. The goal is something that feels less like a twitch game and more like a dream you can't quite wake up from.

If time permits, stretch goals include a rhythm-sync mode where obstacles pulse on the music's beat, a gentle "memory fragment" collectible that unlocks short illustrated vignettes between runs, and a ghost replay of your best run drifting behind you on subsequent attempts.

## Controls

| Action | Input |
|--------|-------|
| Jump / Flip color | **Space**, **Left Click**, or **Up Arrow** |
| Hold for longer jump | Same, hold the input |
| Restart after death | **R** |

That's the whole control scheme. One button. Everything else is reading the world.

## How It Works

- Your character auto-runs to the right at a steady pace.
- The world alternates between **pink** and **teal** states.
- **Pressing jump** lifts you into the air *and* flips your current color.
- **Pink obstacles** hurt you when you're teal. **Teal obstacles** hurt you when you're pink.
- **Matching color** lets you pass through obstacles harmlessly.
- Collect **pulse-orbs** for score; chain them without dying for a multiplier.
- One hit ends the run.

## Built With

- [Unity 2022.3 LTS](https://unity.com/) (2D)
- C#
- Unity Post-Processing

## Project Structure

```
Assets/
├── Art/              # Sprites, tiles, UI, background layers
├── Audio/            # Music, SFX
├── Data/             # ScriptableObject instances (obstacles, chunks)
├── Prefabs/
│   ├── Chunks/       # Level chunk prefabs
│   ├── Obstacles/    # Spike, flyer, gap-marker
│   └── Collectibles/ # Pulse-orb
├── Scenes/
├── Scripts/
│   ├── Player/       # PlayerController, ColorState
│   ├── Spawning/     # ChunkManager, ChunkData
│   ├── Obstacles/    # ObstacleData (SO) + behaviors
│   ├── Collectibles/
│   ├── Managers/     # GameManager, AudioManager, ScoreManager
│   └── FX/           # CameraShake, HitStop, ColorFlipFX
└── Settings/         # URP, post-processing profiles
```

## Status

Active development as the final project for CGDD 109.

## Credits

Full asset attribution lives in [`ASSETS.md`](./ASSETS.md).

## License

Source code: MIT License (see [`LICENSE`](./LICENSE)). Third-party assets retain their original licenses — see `ASSETS.md`.
