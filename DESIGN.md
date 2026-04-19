# Lucid — Design Document

> Living design doc. Captures the concept, sprint rubric mapping, and planned systems. Will evolve as development progresses.

---

## One-Line Pitch

A one-button auto-runner where every jump flips the world's color, and survival means landing in the right shade at the right moment.

## Comps

- **Geometry Dash** for the auto-runner + one-button loop
- **Ikaruga** for the color-polarity mechanic (but much more forgiving)
- **Gris** for the soft pastel, drifting dream aesthetic
- **Thumper** (distant comp) for the feeling of riding a rhythm you barely control

## Core Message & Feel

**Message:** The almost-control of a lucid dream. The world moves on its own. You can nudge it. You can't stop it.

**Metaphor:** A dreamer drifting through two overlapping realities, only fully present in one at a time. Every jump is a blink, a choice of which version of the world to inhabit when you land.

**Intended feel:** Soft but tense. Musical rather than twitchy. The jump input feels *heavy* — it's not just a hop, it's a shift of state. Quick deaths and instant retries keep the pressure of "just one more run."

## The Core Loop

1. Player auto-runs to the right
2. Obstacles stream in from the right, each tagged pink or teal
3. Player presses jump → character jumps **and** flips color
4. Player must land (or pass through) in the correct color to survive
5. Pulse-orbs scattered along the path add score + combo
6. Difficulty ramps: speed increases, chunks get denser and more color-tricky
7. One hit → game over → instant retry

---

## Sprint Rubric Mapping

Every requirement from the Endless Runner sprint, mapped to a planned system:

| Requirement | Implementation |
|---|---|
| Procedural/automatic spawning | `ChunkManager` streams pre-designed chunk prefabs ahead of player, despawns them behind |
| Difficulty increases over time | `difficulty` float scales player speed, chunk selection pool, and obstacle density |
| 3+ types of obstacles | **Spikes** (ground, must jump over), **Flyers** (mid-air, must duck/time), **Gaps** (floor holes, must hold jump longer) — plus each carries a pink/teal color tag |
| 1+ collectible | **Pulse-orbs** (+1 score, combo multiplier on chains), **Memory fragments** (rare, unlock vignettes — stretch) |
| Character animations (idle + moving) | Run cycle, jump arc, landing squash, color-flip flash, death dissolve |
| Audio + one juicy effect | Pitch-laddered pickup SFX, jump whoosh, hit thud, color-flip chime, screen shake + hitstop + particle burst on death |
| **Optional:** Power-ups | Double-jump, slow-mo, shield (one free hit) |

---

## Systems Plan

### Player Controller

Simple auto-move + jump. Three states: `Running`, `Jumping`, `Dead`.

```csharp
public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float jumpForce = 10f;
    public float holdJumpMultiplier = 1.4f;
    public float maxHoldTime = 0.25f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpHoldTimer;

    void Update()
    {
        // Auto-move forward
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);

        // Jump on press
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            ColorState.Instance.Flip();
        }

        // Hold for higher jump
        if (Input.GetButton("Jump") && jumpHoldTimer < maxHoldTime)
        {
            rb.linearVelocity += Vector2.up * holdJumpMultiplier * Time.deltaTime;
            jumpHoldTimer += Time.deltaTime;
        }
    }
}
```

### Color State System

A singleton that tracks the player's current color. Every jump calls `Flip()`, which triggers a visual event that all color-reactive objects subscribe to.

```csharp
public enum GameColor { Pink, Teal }

public class ColorState : MonoBehaviour
{
    public static ColorState Instance;
    public GameColor Current { get; private set; } = GameColor.Pink;
    public event System.Action<GameColor> OnColorChanged;

    public void Flip()
    {
        Current = (Current == GameColor.Pink) ? GameColor.Teal : GameColor.Pink;
        OnColorChanged?.Invoke(Current);
    }
}
```

Obstacles listen to `OnColorChanged` and update their hitbox behavior: if their color matches the player's current color, the hitbox becomes a trigger that only plays a pass-through effect. If mismatched, the hitbox kills.

### Chunk Spawning (Arrays & Lists — from Lecture)

Pre-designed level chunks, each roughly 15 units wide, built by hand in the Unity Editor as prefabs. Each chunk has an `entryPoint` and `exitPoint` child GameObject for seamless stitching.

```csharp
// Fixed-size arrays for chunk pools by difficulty tier
public GameObject[] easyChunks;
public GameObject[] mediumChunks;
public GameObject[] hardChunks;

// Flexible list for currently-active chunks (grows and shrinks)
private List<GameObject> activeChunks = new List<GameObject>();
```

`ChunkManager` picks a chunk from the appropriate difficulty array based on current `difficulty` float, instantiates it so its `entryPoint` aligns with the previous chunk's `exitPoint`, and despawns chunks that are more than 2 chunk-widths behind the player.

### Obstacles (ScriptableObjects — from Lecture)

```csharp
[CreateAssetMenu(menuName = "Lucid/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public GameObject prefab;
    public GameColor color;
    public ObstacleType type;        // Spike, Flyer, Gap
    public bool canMove;
    public float moveSpeed;
    [Range(0f, 1f)] public float spawnWeight;
}
```

New obstacle variants = new prefab + new `ObstacleData` asset. Zero code changes.

### Difficulty Curve

Single `GameManager.difficulty` float, driven by an `AnimationCurve` in the Inspector for easy visual tuning.

- **0–20s:** Easy chunks only. Sparse obstacles. Forgiving color patterns.
- **20–45s:** Medium chunks enter the pool. Spawn rate ramps up slightly.
- **45–90s:** Hard chunks enter. Player speed starts increasing.
- **90s+:** Hard chunks only. Speed continues ramping. Asymptotic cap.

### Score & Combo

- Each pulse-orb = 1 point base.
- Consecutive pulse-orbs without missing = combo multiplier (1x → 2x → 3x → cap at 5x).
- Missing a pulse-orb or getting hit resets combo.
- `PlayerPrefs` stores lifetime high score.
- Also track: longest run (seconds), most orbs collected in a run.

---

## Juice Checklist (Steve Swink — Polish)

**On jump / color flip**
- [ ] Small whoosh SFX
- [ ] Soft chime layered in (color-flip cue)
- [ ] Screen tint shifts subtly toward the new color for 0.2s
- [ ] Particle trail behind player carries the new color

**On landing**
- [ ] Squash-and-stretch on player sprite
- [ ] Dust puff particle at feet
- [ ] Low "thump" SFX
- [ ] Tiny camera dip (not shake — a gentle sink)

**On pulse-orb pickup**
- [ ] Scale-punch on player
- [ ] Radial particle burst in orb's color
- [ ] Pitch-laddered "ping" SFX (rises with combo count)
- [ ] Score counter eases up to new value

**On death**
- [ ] Hitstop (0.1s freeze)
- [ ] Screen shake (short, sharp)
- [ ] Player dissolves into particles in their current color
- [ ] Music ducks briefly, then a soft piano sting
- [ ] Vignette closes in
- [ ] Restart prompt fades in within 0.8s (instant retry feel)

**Ambient**
- [ ] Looping ambient music (dreamy, low-key)
- [ ] Slow background color gradient that drifts regardless of player actions
- [ ] Parallax cloud layers at 2–3 depths
- [ ] Tiny floating particles drifting upward across the whole screen

---

## Visual Direction

- **Palette:** Tight pastel pink + teal duo as the "playable" colors, with cream and deep navy as frame colors. Pick from [Lospec](https://lospec.com/palette-list) — something like "Pastel Qualitative" or hand-pick.
- **Background:** 3-layer parallax with a slowly color-shifting gradient sky. Cloud silhouettes drift across at different speeds.
- **Foreground:** Obstacles are geometric, flat-colored shapes with soft outer glow matching their color tag.
- **Player:** Small, ambiguous creature — a wisp, a paper figure, a glowing bean. Should read as "dreamer" without being too specific.
- **Post-processing:** Soft bloom on pulse-orbs and color-matching obstacles, heavy but subtle vignette, very mild chromatic aberration at the edges to sell the "slightly-off reality" feel.

---

## Scope Guardrails

From the Final Project Kickoff lecture: *cutting hurts, so cut early.* Priority order below. Anything below the MVP line only gets built if everything above is finished and polished.

**MVP (must have):**
1. Auto-running player with one-button jump (variable height via hold)
2. Color state system with flip-on-jump
3. Three obstacle types with color tags
4. Chunk-based procedural spawning
5. Difficulty ramp tied to time/score
6. Pulse-orb collectibles with combo system
7. Score + high score persistence
8. Player animations (run, jump, land, death)
9. At least 4 SFX + 1 ambient music loop
10. Screen shake + particle burst on death (the one required juicy effect)

--- MVP line ---

**Stretch (nice to have):**
11. 2D lit renderer + post-processing (bloom, vignette)
12. Background parallax + drifting gradient
13. Hitstop on death
14. Combo-pitched pickup SFX
15. Game over screen with run stats

**Dream (only if comfortably ahead):**
16. Rhythm-sync mode (obstacles pulse to music beats)
17. Memory fragment collectibles with illustrated vignettes
18. Ghost replay of best run
19. Shield / slow-mo / double-jump power-ups
20. Main menu with settings

---

## Open Questions

- Should color-flip happen on jump *press* or jump *release*? (Leaning press — it's more responsive. Playtest to confirm.)
- Should dying mid-combo cost the combo, or preserve it through one death? (Leaning: lose it, for honesty.)
- Is the color metaphor clear without a tutorial? (Probably needs one very short intro chunk with floating text.)
- Should there be an option to colorblind-swap pink/teal for other distinct pairs? (Yes, eventually — accessibility matters for this mechanic specifically.)
