# Tic‑Tac‑Toe Remix (Unity 6 LTS)

This folder contains the scaffolding for a modern **Unity 6 LTS** 2D tic‑tac‑toe remix designed for a casual mobile game.  The goal is to provide a minimal viable product (MVP) that you can open in Unity, tweak and extend for your business plan.  It leverages **Unity 6**'s latest improvements—better 2D workflows, support for world‑space UI via UI Toolkit, built‑in accessibility APIs, and improved diagnostics to monitor performance on both Android and iOS devices【939613020340693†L112-L160】—while still remaining lightweight and easy to customize.

## Contents

```
unity_tictactoe/
 ├── Assets/
 │   ├── Scripts/      # C# components for gameplay and UI
 │   │   ├── GameManager.cs
 │   │   └── CellHighlighter.cs
 │   ├── Sprites/      # Flat icons used for X and O
 │   │   ├── cross.png
 │   │   └── circle.png
 │   └── Scenes/       # You can create scenes in here (none provided)
 └── README.md         # Setup instructions and overview (this file)
```

### Sprites

Two simple, transparent PNGs are included under **Assets/Sprites**:

* **cross.png** – a dark‑blue “X” mark with smooth corners.
* **circle.png** – an orange ring for the “O” mark.

The images are optimized for mobile resolution and have transparent backgrounds so they can be tinted or scaled in‑game.  When you import them into Unity, set their **Texture Type** to **Sprite (2D/UI)** so they work correctly with the `Image` component.

### Scripts

* **GameManager.cs** – central coordinator responsible for:
  * Maintaining the board state (3×3 array) and tracking the current player.
  * Responding to button clicks and preventing illegal moves.
  * Checking for winners or draws and updating the status text accordingly.
  * Allowing the match to be restarted without reloading the scene.

  The script exposes `cellButtons`, `xSprite`, `oSprite`, `statusText` and `restartButton` in the Inspector.  Assign your `Button` components in row‑major order (top‑left to bottom‑right), link the sprite assets, and drag a `Text` component (or a TMP Text) to display status messages.  The restart button is automatically hidden during play and shown when the match ends.

* **CellHighlighter.cs** – optional hover effect that scales up a cell slightly when the pointer is over it, giving tactile feedback.  Attach this component to each tic‑tac‑toe button if you want the effect.  You can adjust the `hoverScale` in the Inspector.

## Setting up the scene

1. **Create a new Unity 6 LTS project** using the **2D** template.  Unity 6.0 LTS offers enhanced 2D tooling, including a Sprite Editor preview toggle and support for 2D enhancers【939613020340693†L112-L135】.  These improvements make it easier to iterate on your sprites and tilemaps.
2. In the **Project** window, copy the **Assets** folder from this repository into your project’s `Assets` directory.  Unity will automatically import the scripts and sprites.
3. **Add a Canvas** to your scene via **GameObject → UI → Canvas**.  Set its **Render Mode** to **Screen Space – Overlay** for a traditional 2D UI or **World Space** if you want to explore the new World‑Space UI workflow introduced in Unity 6.2【939613020340693†L112-L135】 (this allows UI elements to exist in 3D/2D space without a separate overlay camera).
4. **Create nine Buttons** (UI → Button) as children of the Canvas and arrange them in a 3×3 grid.  A **Grid Layout Group** component on a parent GameObject can help align them uniformly.  Set the **Image** component of each button to have no sprite initially (Sprite = None).
5. **Create a Text or TMP Text** element to display messages (e.g., “Player X’s turn”, “Player O wins!”).  Place it above the grid.
6. **Create a Restart Button** and place it below the grid.  Leave its GameObject disabled in the Inspector.
7. **Add the GameManager component:**
   - Drag **GameManager.cs** onto an empty GameObject (e.g., “GameManager”) in the hierarchy.
   - In the Inspector, assign the nine buttons to the `Cell Buttons` array in row‑major order (0–8).  Drag **cross.png** to `X Sprite` and **circle.png** to `O Sprite`.  Assign your message Text to `Status Text` and the restart button to `Restart Button`.
8. (Optional) Attach **CellHighlighter.cs** to each button to enable the hover animation.
9. **Play the scene.**  The GameManager will initialize the board, handle turns, detect winners or draws, and show a restart button when the match ends.

## Extending the MVP

This scaffold is intentionally minimal.  Here are a few suggestions to “remix” the classic tic‑tac‑toe experience into something more engaging for casual mobile players:

* **Alternate board sizes** – Modify the `boardState` array to handle 4×4 or 5×5 grids and adjust the win condition accordingly.
* **Timed turns or power‑ups** – Introduce a countdown timer for each move, or allow players to earn power‑ups that let them place two marks in one turn or block a cell.
* **Dynamic skins/themes** – Use Unity 6’s improved 2D sprite tools to swap in different sprite sheets or apply color themes (e.g., neon, pastel).  The new Sprite Editor preview toggle【939613020340693†L112-L135】 makes editing and previewing your art much easier.
* **World‑space animations** – Taking advantage of Unity 6.2’s world‑space UI support【939613020340693†L112-L135】, you could animate the board floating in a 3D environment or attach particle effects to winning lines.
* **Accessibility and localization** – Unity 6 adds mobile screen reader APIs and an accessibility hierarchy viewer【181520473720683†L204-L226】.  Consider exposing game state through these APIs and localizing status messages to reach a wider audience.

For more information about Unity 6 LTS improvements, refer to the official “What’s New in Unity 6.2” documentation【939613020340693†L112-L160】 and the general 2D system updates【181520473720683†L136-L197】.

## Build targets

Unity 6 LTS supports Android (ARM64/ARMv7), iOS and other platforms out‑of‑the‑box.  When you’re ready to ship your MVP:

1. Open **File → Build Settings…** and switch the **Platform** to **Android** or **iOS**.  Unity will prompt you to install the necessary build support modules if they aren’t already installed.
2. Configure your **Build Profiles** to enable or disable diagnostic data collection【939613020340693†L154-L162】 based on your privacy requirements.
3. Use **Player Settings** to set your product name, company name, bundle identifier, icons and splash screens.  Unity 6’s improved diagnostics will help you monitor crashes and ANR issues on live devices【939613020340693†L154-L167】.

## Conclusion

This scaffold provides a clean starting point for building a polished tic‑tac‑toe remix in Unity 6 LTS.  By taking advantage of Unity 6’s improved 2D workflows, UI Toolkit enhancements and mobile accessibility features, you can iterate quickly and deliver a modern, accessible casual game on both Android and iOS platforms.  Feel free to extend the codebase, add visual flair, integrate monetization (rewarded ads, in‑app purchases) and analytics as your business plan requires.
