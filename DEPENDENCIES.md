Dependencies and installation notes for shinyverse (ULTRON demo)

Python (primary runtime)
- Recommended: Python 3.10+ (project has been exercised with 3.10-3.13 in compiled bytecode files)

Primary Python packages (required for full functionality):
- fastapi
- uvicorn
- websockets
- aiohttp
- psutil

Optional / provider integrations (only required for full AI/voice/vision features):
- openai (if using OpenAI provider endpoints)
- anthropic (if using Anthropic provider integrations)
- ollama (if using Ollama)
- tts or speech packages (platform dependent) for `voice_manager`
- pillow, opencv-python (for vision manager image handling)

Node (frontend dev)
- Node.js 16+ (for Create React App in `ultron-agent`)
- npm or pnpm

Installation examples

Install Python packages from `requirements.txt`:

```powershell
python -m pip install -r requirements.txt
```

Install React dependencies and start dev server:

```powershell
cd ultron-agent
npm install
npm start
```

Notes
- `launch_ultron_web.py` performs a light check for required Python packages. If you add new packages used at runtime, update `DEPENDENCIES.md` and `requirements.txt`.
- The project tolerates missing optional packages and will fall back to demo-mode responses (see `ultron_web_server.py`).
