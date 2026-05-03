# Lucid

A one-button auto-runner set in the soft, drifting logic of a dream.

## About

The world in Lucid breathes between two colors, pink and teal, and so do you. Every jump flips your color. Obstacles only hurt you when their color doesn't match yours, so a single press of the space bar has to answer two questions at once: where do I want to be, and what color do I need to be when I land?

It's a small game about the almost-control of a lucid dream. You can feel the rhythm of the world. You can't quite slow it down.

## Roadmap (Next Two Weeks)

Development is focused on three pillars: the color-flip core loop, game feel, and a cohesive dream aesthetic.

The core loop runs on chunk-based procedural generation. Pre-designed level segments stream in ahead of the player and despawn behind, pulling from progressively harder pools as survival time grows. Each chunk contains a mix of three obstacle types: ground spikes, low-flying hazards that force a duck, and wide gaps that demand longer-held jumps. Each is tagged with a color that must match the player's current state to be safely passable. Obstacle data is backed by ScriptableObjects so new hazard variants can be tuned and added from the Inspector.

For game feel, every interaction gets layered feedback: a satisfying squash on landing, a particle burst that carries the player's current color, pitch-laddered pickup sounds, subtle hitstop and screen shake on death, and camera smoothing that sells the forward momentum. A looping soundtrack ties the experience together, with a stretch goal of syncing obstacle spawn timing to the beat.

Visually, Lucid is heading toward a soft pastel palette with Unity's 2D lit renderer: gentle bloom on collectibles, a subtle vignette, and a slow color-shifting background gradient that drifts the whole time you play. The goal is something that feels less like a twitch game and more like a dream you can't quite wake up from.

If time permits, stretch goals include a rhythm-sync mode where obstacles pulse on the music's beat, a gentle "memory fragment" collectible that unlocks short illustrated vignettes between runs, and a ghost replay of your best run drifting behind you on subsequent attempts.

## Controls

| Action | Input |
|---|---|
| Jump / Flip color | Space, Left Click, or Up Arrow |
| Hold for longer jump | Same, hold the input |
| Restart after death | R |

One button. Everything else is reading the world.

## How It Works

- Your character auto-runs to the right at a steady pace
- The world alternates between pink and teal states
- Pressing jump lifts you into the air and flips your current color
- Pink obstacles hurt you when you're teal. Teal obstacles hurt you when you're pink
- Matching color lets you pass through obstacles harmlessly
- Collect pulse-orbs for score; chain them without dying for a multiplier
- One hit ends the run

## Built With

- Unity 2022.3 LTS (2D)
- C#
- Unity Post-Processing

## Project Structure

Current state of the repository:

Assets/
├── Pixel Art Character Template/
│   ├── Idle/
│   ├── Run/
│   ├── Jump/
│   ├── Crouch-Idle/
│   ├── Crouch-Walk/
│   ├── Air-Spin/
│   ├── Land/
│   ├── Roll/
│   ├── Walk/
│   ├── Wall-Land/
│   ├── Wall-Slide/
│   ├── Ledge-Climb/
│   └── Ledge-Grab/
├── Scenes/
│   └── SampleScene.unity
└── PlayerController.cs
Target structure once asset organization and core systems are in place:
Assets/
├── Art/
├── Audio/
├── Data/
├── Prefabs/
│   ├── Chunks/
│   ├── Obstacles/
│   └── Collectibles/
├── Scenes/
├── Scripts/
│   ├── Player/
│   ├── Spawning/
│   ├── Obstacles/
│   ├── Collectibles/
│   ├── Managers/
│   └── FX/
└── Settings/
## Status

Active development as the final project for CGDD 109.

## Credits

**Player character art**
Pixel Art Character Template by Deymoon. Source: https://deymoon.itch.io/character-template

All other assets, scripts, and scene setup are original work for this project. Additional asset attribution will be added here as new assets are sourced.

## License

Source code: MIT License (see LICENSE). Third-party assets retain their original licenses.
