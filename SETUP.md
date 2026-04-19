# Setting Up This Repository

Step-by-step for getting *Lucid* onto GitHub. Delete this file before final submission if you want, or keep it as notes.

---

## Step 1 — Create the Unity project

1. Open **Unity Hub**
2. Click **New Project**
3. Select the **2D (Built-In Render Pipeline)** template
   - *(Or **Universal 2D** if you want the fancier lighting from Phase 3 of our plan)*
4. **Project name:** `Lucid` (or `lucid-runner`, `lucid-game`, etc.)
5. **Location:** somewhere easy to find, e.g. `Documents/UnityProjects/`
6. Unity Editor version: **2022.3 LTS** (any 2022.3.x version is fine)
7. Click **Create project**
8. Wait for Unity to finish (a minute or two)
9. **Close Unity** once it opens — avoids file-system conflicts when we add files from outside

## Step 2 — Drop the repo files into your project root

Your new Unity project folder will look like this:

```
Lucid/
├── Assets/
├── Packages/
└── ProjectSettings/
```

Copy these files from this package into that **root folder** (the one containing `Assets/`, not inside `Assets/`):

- `README.md`
- `ASSETS.md`
- `DESIGN.md`
- `.gitignore`
- `LICENSE`

After copying, the root should look like:

```
Lucid/
├── Assets/
├── Packages/
├── ProjectSettings/
├── .gitignore          ← new
├── ASSETS.md           ← new
├── DESIGN.md           ← new
├── LICENSE             ← new
└── README.md           ← new
```

**If you can't see `.gitignore`:** files starting with `.` are hidden by default.
- **Mac:** In Finder, press **Cmd + Shift + .** to toggle hidden files on
- **Windows:** In File Explorer, **View → Show → Hidden items**

## Step 3 — Create the GitHub repo

1. Go to [github.com/new](https://github.com/new)
2. **Repository name:** `lucid` (or whatever you named your project)
3. **Description:** "A one-button auto-runner set in the drifting logic of a dream. Final project for CGDD 109."
4. **Public** (so your professor can view without being added)
5. ❌ **Do NOT** check "Add a README file"
6. ❌ **Do NOT** add a `.gitignore` from the dropdown
7. ❌ **Do NOT** add a license from the dropdown
8. Click **Create repository**

Leave this page open — you'll come back to it.

## Step 4 — Connect via GitHub Desktop

1. Open **GitHub Desktop** (install from [desktop.github.com](https://desktop.github.com/) if you haven't)
2. **File → Add Local Repository**
3. Point it at your `Lucid` project folder
4. If it says "this isn't a Git repository," click the **create a repository** link
5. Leave the defaults and click **Create Repository**
6. GitHub Desktop will now show you a list of changes — this should be your project files, **not** hundreds of `Library/` files. If you see `Library/` listed, your `.gitignore` isn't being picked up correctly (see Troubleshooting below).
7. Click **Publish repository** at the top
8. Uncheck "Keep this code private" (unless you want it private)
9. Click **Publish Repository**

Your code is now on GitHub.

---

## Commit Cadence — Meets the "2–3 Commits Over 2–3 Days" Note

### Commit 1 (Today) — Initial setup
**Message:** `Initial Unity project setup and project documentation`

Everything you already have. Makes the first push.

### Commit 2 (Day 2) — Organize assets + import initial art/audio
**Message:** `Add initial art assets and organize Assets folder structure`

- Open Unity and create the subfolders from the README's structure:
  - `Assets/Art/`, `Assets/Audio/`, `Assets/Data/`, `Assets/Prefabs/Chunks/`, `Assets/Prefabs/Obstacles/`, `Assets/Prefabs/Collectibles/`, `Assets/Scenes/`, `Assets/Scripts/Player/`, `Assets/Scripts/Spawning/`, `Assets/Scripts/Obstacles/`, `Assets/Scripts/Collectibles/`, `Assets/Scripts/Managers/`, `Assets/Scripts/FX/`
- Import any starting art, fonts, or audio (Kenney packs, itch.io pastel assets, freesound SFX)
- **Update `ASSETS.md`** with every source and license
- Commit and push

### Commit 3 (Day 2 or 3) — Tune the design doc + README
**Message:** `Refine game concept and project roadmap`

- Any changes or tightening to the README roadmap paragraph
- Add open questions or design notes to `DESIGN.md` as you think of them
- Commit and push

---

## Submitting to Your Professor

Your repo URL is: `https://github.com/YOUR-USERNAME/lucid`

Submit that link.

---

## Pre-Submission Sanity Check

- [ ] `.gitignore` is at the repo root (same level as `Assets/`)
- [ ] Repo size is reasonable — typically well under 100 MB. If it's 500+ MB, `Library/` got committed accidentally
- [ ] README loads on the GitHub page and looks correct
- [ ] `ASSETS.md` reflects whatever you've actually imported (not just the placeholder template)
- [ ] At least 2 commits visible in the History tab
- [ ] Repo is **Public** (or professor was added as collaborator if Private)

---

## Troubleshooting

**GitHub Desktop wants to commit `Library/` or `Temp/` folders:**
Your `.gitignore` isn't being picked up. Check that:
1. The file is exactly named `.gitignore` (no `.txt` extension, check with "Show hidden files")
2. It's in the project root, not inside `Assets/`
3. You haven't already committed those folders before adding the `.gitignore` — if so, you'll need to untrack them: in GitHub Desktop, go to **Repository → Open in Terminal** (or Command Prompt on Windows) and run:
   ```
   git rm -r --cached Library Temp Logs
   git commit -m "Remove auto-generated folders from tracking"
   ```

**Unity shows errors after reopening:**
Close and reopen Unity. If errors persist, delete the `Library/` folder — Unity will regenerate it.

**I accidentally committed a huge file and can't push:**
GitHub rejects files over 100 MB. If that happens, either:
- Untrack it (`git rm --cached <file>`, then commit)
- Look into Git LFS (see Max's lecture slide 11)
