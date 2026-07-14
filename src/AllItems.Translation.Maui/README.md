# AllItems.Translation.Maui (iPhone flashcards)

A native .NET MAUI app that runs the flashcards on iPhone/iPad, reusing the desktop's flashcard engine
(`AllItems.Translation.Core` + `AllItems.Translation.Infrastructure.Persistence`). It is **study-only** and
**offline**: it opens a local SQLite database and runs the same SM-2 `StudySessionService` as the desktop.

## How syncing works (one-way: desktop → phone)

1. On the **desktop** (Windows), open **Flashcards → Export to phone** (add a GitHub token in **Settings** first).
   This checkpoints the local database and pushes `sync/allitems.db` + `sync/manifest.json` into the public
   GitHub repo.
2. On the **phone**, open the **Import** tab and tap **Import latest flashcards**. It downloads the database
   over the public raw URL (no login) and replaces the on-device copy.

Grades you make on the phone are saved locally but are **overwritten on the next import** — the desktop is the
source of truth.

> The exported database is committed to a **public** repo, so your vocabulary and review progress are publicly
> visible. That was an intentional trade for zero-hosting sync.

## Building (requires a Mac)

Apple requires a Mac + Xcode to compile and sign an iOS app. This project is intentionally **not** in
`AllItems.Translation.slnx` so a Windows solution build of the desktop app doesn't try (and fail) to build iOS.

```bash
# one-time
dotnet workload install maui

# build / run
dotnet build src/AllItems.Translation.Maui/AllItems.Translation.Maui.csproj -f net10.0-maccatalyst   # quick logic check on the Mac
dotnet build src/AllItems.Translation.Maui/AllItems.Translation.Maui.csproj -f net10.0-ios            # iOS simulator/device
```

To deploy to a physical iPhone you need an Apple Developer account and a provisioning profile / automatic
signing (configure the bundle id `de.reynekes.allitems.flashcards` in your Apple developer account or via Xcode).

## Notes / follow-ups

- The app icon and splash under `Resources/` are placeholders — replace with real artwork before shipping.
- The study card shows example sentences as plain text; per-word grammatical highlighting (as on the desktop)
  can be added later.
- On a real device, confirm the SQLite native provider (`SQLitePCLRaw.bundle_e_sqlite3`) loads under iOS AOT —
  the one thing that can't be verified off-device.
