# Copilot Instructions for ULTRON Atlas GUI & Web Dashboard

## Big Picture Architecture
- **Multi-component system**: Combines Python backends (`ultron_web_server.py`, `launch_ultron_web.py`, etc.), React-based SPA (`ultron-agent/src/`), and pure HTML/JS dashboards (`public-dashboard/`, `ultron-dashboard.js`).
- **Frontend Dashboards**: `public-dashboard/` is a self-contained static dashboard (no backend required). `ultron-agent/` is a React app for richer UI and integration.
- **Backend Services**: Python scripts provide system metrics, agent logic, and serve web UIs. Communication is typically via HTTP endpoints or local server.
- **Integration**: Frontends may connect to Python backends for real data, but can run in demo mode with randomized metrics.

## Developer Workflows
## Copilot instructions — ULTRON / shinyverse (concise)

These notes give an AI coding agent the exact, discoverable facts to be productive in this repo.

- Big picture: this repo contains a small Python "ULTRON" backend (FastAPI + uvicorn) that serves a single-file dashboard and JS assets, a separate React app (under `ultron-agent/`) for a richer UI, and a purely static `public-dashboard/` that can be deployed alone.

- Key files to inspect first:
  - `ultron_web_server.py` — FastAPI app, websocket manager, endpoints (e.g. `/`, `/status`, `/health`, `/chat`, `/voice/*`, `/vision/*`) and dashboard files served from repo root.
  - `launch_ultron_web.py` — lightweight launcher that checks deps and spawns `ultron_web_server.py` (lists required pip packages: fastapi, uvicorn, websockets, aiohttp, psutil).
  - `real-system-monitor.py` — alternative/related FastAPI service (also uses uvicorn).
  - `public-dashboard/index.html` and `ultron-web-dashboard.html` / `ultron-dashboard.js` — static dashboards (demo vs. integrated mode).
  - `ultron-agent/` — Create React App project (dev: `npm start`, build: `npm run build`).

- Runtime / integration facts (don't guess these):
  - Backend runs on port 8009 by default (see uvicorn.run in `ultron_web_server.py`). WebSocket endpoint is ws://localhost:8009/ws and dashboard URL is http://localhost:8009.
  - The server tolerates missing optional components (AI/voice/vision/automation). See the guarded imports / try-except blocks in `ultron_web_server.py` — if imports fail the service runs in DEMO MODE with stubbed responses.
  - Important endpoints to reference in code changes or tests: `/chat`, `/chat/openai`, `/chat/anthropic`, `/chat/ollama`, `/status`, `/health`, `/voice/*`, `/vision/*` and `/vision/screenshot` (saves screenshots to `screenshots/`).
  - Logs are written under `logs/` with pid-stamped filenames; the launcher and server create `logs/` and `screenshots/` if missing.

- Developer workflows (concrete commands):
  - Start backend locally (fastest): `python launch_ultron_web.py` — launcher checks deps and opens the browser.
  - Run server directly (dev reload): `python ultron_web_server.py` (or uvicorn: `uvicorn ultron_web_server:app --reload --port 8009`).
  - Serve static demo dashboard: `cd public-dashboard ; python -m http.server 8000`.
  - Frontend dev (React): `cd ultron-agent && npm start`. Build: `npm run build`.
  - Tests: repo contains pytest-style tests (files like `test_*.py`) — run `pytest -q` from repo root.

- Project-specific patterns and conventions to follow when editing code:
  - Feature gating via soft imports: code must handle optional modules (agent_core, voice_manager, vision, ultron_automation) failing to import — prefer non-fatal fallbacks and DEMO MODE responses.
  - Endpoints broadcast to WebSocket clients via a ConnectionManager; prefer using manager.broadcast({...}) for live UI updates.
  - Keep UI assets in repo root (`ultron-web-dashboard.html`, `ultron-dashboard.js`) or `public-dashboard/` for static-only deployments.
  - Where file writes occur (logs/screenshots), use the existing directories and filename conventions (see `ultron_web_server.py` screenshot path and log naming).

- Integration points and external dependencies:
  - External AI providers are modeled in endpoints (`openai`, `anthropic`, `ollama`) — the code expects UltronAgent.process_message to accept provider names.
  - Optional local services: voice TTS/listening and vision screenshot APIs — if present, endpoints will call into `voice_manager` and `vision_manager`.
  - Deployment-friendly static dashboard: `public-dashboard/` can be deployed to Netlify/Vercel/GitHub Pages without backend.

- Testing and safety checks an AI should add when changing behavior:
  - Unit / integration tests touching endpoints should assert DEMO MODE behaviour when optional components are missing.
  - When adding new endpoints that trigger file writes, ensure `os.makedirs(..., exist_ok=True)` or use the same `logs`/`screenshots` dirs.

If you'd like, I can: (1) trim or expand any section, (2) add code snippets (examples of calling `/chat` or WS messages), or (3) run tests and validate a quick change. Which would you prefer?
  ```sh
