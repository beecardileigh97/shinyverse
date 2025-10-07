# TODO

Short-term tasks:

- [ ] Add automated GitHub Actions that run `pytest -q` on push and PRs.
- [ ] Add linting config (flake8/ruff) and CI lint step.
- [ ] Add example tests that simulate DEMO MODE for `/chat` endpoints.
- [ ] Add a minimal CONTRIBUTING.md with PR and branch naming guidelines.

Long-term ideas:

- Wire up provider credentials (OpenAI/Anthropic/Ollama) via environment variables and document in DEPENDENCIES.md.
- Add an end-to-end test that boots the server (in a subprocess) and checks `/status` and `/health`.
