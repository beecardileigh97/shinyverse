# Notes

Operational & design notes for contributors:

- Demo vs Real mode: `ultron_web_server.py` imports optional components with try/except. When those imports fail the server runs in DEMO MODE returning stubbed responses. This pattern is intentional to make the UI runnable without external APIs.

- Logging conventions: `ultron_web_server.py` creates `logs/` and writes a pid- and timestamp-stamped file (see top-level logging config). Keep the same directory and filename patterns when adding new log outputs.

- Screenshots: Vision screenshots, when available, are saved to `screenshots/` with `ultron_vision_<timestamp>.png` or `demo_vision_<timestamp>.png` in demo mode.

- WebSocket usage: Use the `ConnectionManager`'s `manager.broadcast({...})` method to push events to connected dashboard clients.

- Tests referencing optional components should assert demo-mode fallback behaviour if the optional modules aren't importable.

- When adding/modifying endpoints, add a line in `CHANGELOG.md` and a short note in `NOTES.md` for non-obvious behaviour changes.
