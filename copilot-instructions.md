# Copilot Instructions for Unity Tic Tac Toe Remix

## Project Overview
This is a Unity 6 LTS-based Tic Tac Toe game project. The codebase follows modern Unity development practices and C# coding standards.

## Technology Stack
- **Unity Version**: Unity 6 LTS (6000.x)
- **Language**: C# 9.0+
- **Scripting Backend**: IL2CPP (for production builds)
- **.NET Version**: .NET Standard 2.1

## Code Style and Conventions

### C# Coding Standards
- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use **PascalCase** for public members, classes, methods, and properties
- Use **camelCase** for private fields with underscore prefix: `_privateField`
- Use **PascalCase** for constants: `MaxPlayers`
- Prefer explicit access modifiers (always declare `public`, `private`, `protected`)
- Use `readonly` for fields that shouldn't change after initialization
- Prefer `async`/`await` for asynchronous operations

### Unity-Specific Conventions
- **MonoBehaviour Scripts**: One class per file, filename matches class name
- **SerializeField**: Use `[SerializeField]` for private fields that need Inspector exposure
- **Namespace**: Use `TicTacToe` as the root namespace, with subfolders as sub-namespaces (e.g., `TicTacToe.UI`, `TicTacToe.GameLogic`)
- **Component References**: Cache component references in `Awake()` or `Start()`
- **Coroutines**: Prefer async/await over coroutines for new code when possible
- **ScriptableObjects**: Use for game configuration and data

### File Naming
- **Scripts**: PascalCase (e.g., `GameManager.cs`, `TicTacToeBoard.cs`)
- **Scenes**: PascalCase (e.g., `MainMenu.scene`, `GamePlay.scene`)
- **Prefabs**: PascalCase (e.g., `GameTile.prefab`, `PlayerMarker.prefab`)
- **Materials**: PascalCase with suffix (e.g., `GridMaterial.mat`)
- **Sprites/Textures**: lowercase-with-dashes (e.g., `x-marker.png`, `o-marker.png`)

## Project Structure

```
Assets/
├── Scenes/              # Unity scenes
├── Scripts/             # C# scripts
│   ├── Core/           # Core game logic
│   ├── UI/             # UI-related scripts
│   ├── Managers/       # Singleton managers
│   └── Utilities/      # Helper/utility scripts
├── Prefabs/            # Reusable game objects
├── Materials/          # Materials and shaders
├── Textures/           # Sprites and textures
├── Audio/              # Sound effects and music
├── Animations/         # Animation clips and controllers
└── Resources/          # Runtime-loaded assets
```

## Architecture Patterns

### Singleton Pattern (for Managers)
```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
```

### Events and Delegates
- Use `UnityEvent` for Inspector-assignable events
- Use C# events with `EventHandler` pattern for code-driven events
- Consider using ScriptableObject-based events for decoupled architecture

### Dependency Injection
- Pass dependencies through constructor or public properties
- Avoid heavy use of `FindObjectOfType` or `GetComponent` in Update loops

## Unity 6 LTS Best Practices

### Performance
- Use **object pooling** for frequently instantiated objects
- Minimize `Update()` calls; use events or coroutines where appropriate
- Cache component references instead of repeated `GetComponent()` calls
- Use `CompareTag()` instead of string comparison: `gameObject.CompareTag("Player")`
- Prefer structs over classes for small data containers when appropriate

### New Input System
- Use Unity's **Input System** package (com.unity.inputsystem) over legacy Input
- Define Input Actions in Input Action Assets
- Use `InputAction` callbacks for event-driven input handling

### UI Toolkit vs. uGUI
- For new UI, prefer **UI Toolkit** for editor UI and runtime UI where appropriate
- Use **TextMeshPro** for all text rendering (never legacy Text)
- Implement responsive UI using anchors and Canvas Scaler

### Universal Render Pipeline (URP)
- If using URP, keep materials URP-compatible
- Use URP's Shader Graph for custom shaders
- Optimize rendering with batching and atlasing

### Addressables
- Consider using **Addressables** for asset management in larger projects
- Keep scenes and frequently-used assets in build; use Addressables for optional content

## Testing Guidelines
- Write unit tests using **Unity Test Framework** (UTF)
- Place tests in `Assets/Tests/` directory
- Use `[Test]` for edit mode tests, `[UnityTest]` for play mode tests
- Mock dependencies for isolated unit testing
- Aim for testable, modular code

## Version Control (Git)

### .gitignore
Ensure the following are ignored:
- `/[Ll]ibrary/`
- `/[Tt]emp/`
- `/[Oo]bj/`
- `/[Bb]uild/`
- `/[Bb]uilds/`
- `*.csproj`
- `*.sln`
- `*.userprefs`

### Commit Messages
Follow conventional commit format:
```
type(scope): brief description

Detailed explanation if needed
```
Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

### Branch Strategy
- `main`: Production-ready code
- `develop`: Integration branch for features
- `feature/*`: New features
- `bugfix/*`: Bug fixes
- `hotfix/*`: Critical production fixes

## Documentation
- Add XML documentation comments for public APIs
- Include README.md with setup instructions
- Document complex algorithms or game logic inline
- Keep a CHANGELOG.md for version tracking

## Security and Best Practices
- Never commit API keys, credentials, or sensitive data
- Use `.env` files or Unity's Cloud Services for secrets
- Validate all player input
- Handle edge cases and null references gracefully

## AI Assistant Guidelines
When generating code for this project:
1. Always include appropriate namespaces
2. Add XML documentation for public methods
3. Follow the established project structure
4. Use Unity 6 LTS APIs and features
5. Implement proper error handling with try-catch or null checks
6. Consider performance implications
7. Write testable, modular code
8. Add TODO comments for incomplete implementations

## Common Patterns for Tic Tac Toe

### Game State Management
```csharp
public enum GameState
{
    Menu,
    Playing,
    GameOver,
    Paused
}
```

### Board Representation
- Use 2D array or 1D array for board state: `int[3,3]` or `int[9]`
- Represent empty: `0`, Player X: `1`, Player O: `2`

### Win Condition Checking
- Check rows, columns, and diagonals efficiently
- Consider using bitboards for advanced optimization

## Additional Resources
- [Unity 6 Documentation](https://docs.unity3d.com/6000.0/Documentation/Manual/)
- [Unity Best Practices](https://unity.com/how-to/programming-unity)
- [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
