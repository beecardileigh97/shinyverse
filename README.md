# shinyverse

shinyverse is a small, local AI playground and dashboard suite (the "ULTRON" demo). It bundles a lightweight Python backend (FastAPI + uvicorn) that can serve a single-file web dashboard and JS assets, a React-based agent UI under `ultron-agent/`, and a purely static dashboard under `public-dashboard/`.

This repository is intended for local experimentation, quick demos, and iterative development of AI-assisted UI features (voice, vision, automation hooks). The backend runs in a DEMO MODE when optional components are missing.

## Quick summary

- Backend: `ultron_web_server.py` (FastAPI) — default port 8009
- Launcher: `launch_ultron_web.py` (checks deps, starts server and opens browser)
- Static dashboard: `public-dashboard/index.html` (single-file deployable)
- React frontend: `ultron-agent/` (Create React App)

## Quickstart (Windows PowerShell)

1. Install Python dependencies (see `DEPENDENCIES.md` and `requirements.txt`):

```powershell
python -m pip install -r requirements.txt
```

2. Start the backend using the launcher (recommended):

```powershell
python launch_ultron_web.py
```

This script will check dependencies, ensure `logs/` and `screenshots/` exist, launch the FastAPI server, and open the dashboard in your browser.

3. Or run the server directly with reload (for development):

```powershell
python ultron_web_server.py
# or using uvicorn directly
uvicorn ultron_web_server:app --reload --port 8009
```

4. Serve the static demo dashboard locally (no backend required):

```powershell
cd public-dashboard
python -m http.server 8000
# visit http://localhost:8000
```

5. Frontend React app (if working on UI):

```powershell
cd ultron-agent
npm install
npm start
# visit http://localhost:3000
```

## Important runtime facts (from code)

- The backend uses port 8009 by default. Dashboard URL: http://localhost:8009
- WebSocket endpoint: ws://localhost:8009/ws
- Key endpoints: `/`, `/status`, `/health`, `/chat`, `/chat/openai`, `/chat/anthropic`, `/chat/ollama`, `/voice/*`, `/vision/*` (including `/vision/screenshot`).
- Logs are written in `logs/` using a pid-stamped filename. Screenshots are saved to `screenshots/`.

## Where to make changes

- API & server behaviour: `ultron_web_server.py`
- Launcher and simple checks: `launch_ultron_web.py`
- Static dashboard UI: `public-dashboard/index.html`, `ultron-web-dashboard.html`, `ultron-dashboard.js`
- React app UI: `ultron-agent/src/*`
- Agent core logic: `agent_core.py` and `ai_config.py` (may be optional / stubbed for demo mode)

When you add features that write files, follow existing patterns: use `os.makedirs(..., exist_ok=True)` and write to `logs/` or `screenshots/` as appropriate.

## Tests

This repository includes pytest-style tests (files named `test_*.py`). Run tests from repo root with:

```powershell
pytest -q
```

If tests reference optional components (voice/vision/automation), they may run in DEMO MODE when those modules aren't importable.

## Dependencies

See `DEPENDENCIES.md` and `requirements.txt` for the canonical lists. The launcher also lists the core Python packages it checks for: `fastapi`, `uvicorn`, `websockets`, `aiohttp`, `psutil`.

## Documentation & changelog policy

- Always add a short, human-readable entry to `CHANGELOG.md` when you make repository changes. Use the format shown in `CHANGELOG.md`'s top section.
- Document non-trivial code changes inline with a short comment and update `NOTES.md` if you introduce new design or operational decisions.
- Keep `.github/copilot-instructions.md` in sync when making architecture or workflow changes.

## Contributing / modifying files

Guidelines to follow when modifying this repo:

1. Update the relevant README/notes for any behavior you change.
2. Add or update tests for behavioural changes (happy-path + at least one edge case).
3. Run linting / quick smoke test locally: start the server and query `/status`.
4. Update `CHANGELOG.md` with a brief line describing the change (who, what, why, date).

Example changelog entry (append to top of `CHANGELOG.md`):

```markdown
## [Unreleased] - 2025-10-08
- chore: update repository docs and add dependency and notes files (author: repo-bot)
```

## Notes and support

- If optional components fail to import, the server will run in DEMO MODE with stubbed responses — useful for local dev without external APIs.
- See `ultron_web_server.py` for specific fallback behaviours and message formats.

---

If you want, I can also run the test suite now, create GitHub issue/PR templates, or add a GitHub Actions workflow to run tests and linting on push. Tell me which you'd like next.
