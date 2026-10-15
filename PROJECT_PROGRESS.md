# Project Progress - SAM_Multitasker (2026-Q4)

## Roslyn Diagnostic collision repair closeout - 2026-10-15

- [SAM_Multitasker#12](https://github.com/SAM-BIM/SAM_Multitasker/pull/12) merged into `sow/2026-Q4` as `e58b00ba999a993fe86ebeadd7a30b40100dc9e8`, pinned to approved final head `e245510aaa934e1f4bd67f7434170aeede271997`. Owner authorized merging once green; final Build and SPDX passed before merge. Validated content `cfe9d116c1d9837e23ada308d535029eaf6de773`; later commits contain evidence only. Merged production/test trees match that content exactly.
- Root cause proven with archive-isolated builds: unchanged Multitasker `aa126fa6eeb34cbffb72cbf5a5129aa17be1ec6b` passes with SAM `a71435dd88d28c9b0da0bca43c21fbca5e1038b9`, fails at its immediate child `967a8639eb6f7f614ff4168d62ab3dfd979efbe4` (SAM#201), which adds `SAM.Core.Diagnostic`. Enclosing-namespace lookup overrides the imported Roslyn type inside `SAM.Core.Multitasker`. Q4 `6b12e2a65817556693ba408674211610f4a20dda` before SAM#204 also fails; #204 remains independent and unchanged.
- Smallest correction: distinct `RoslynDiagnostic` aliases in `Classes/Multitasker.cs` and `Classes/MultitaskerResults.cs`; preserve historical public Roslyn constructor/property types and algorithms. Add three regression tests and a core ProjectReference in the existing GhIconTest project; required SPDX headers, validation record and four evidence JSON files. No component, shared SAM.Core, workflow or unrelated behavior changes.
- Last preserved successful full BuildAll: 2026-10-15 morning, SAM `0649e15ee9c1727af5834d9206017da9860917c5` + Multitasker `aa126fa6eeb34cbffb72cbf5a5129aa17be1ec6b`. Its full 26-repository SHA configuration is reconstructed from HEAD reflogs and corroborated by the contemporaneous clean-tree log; the original full-build log did not record SHAs.
- Local validation: affected project build PASS (0 errors); existing NUnit project 4 passed / 0 failed / 0 skipped; isolated good/first-bad/fixed dependency and public-API/behavior probes match expectations. Full `BuildAlls_v5.bat nopause` PASS, exit 0, `00:07:41.6286121`, all 26 repositories Restore/Clean/Rebuild, Revit 2025/2026/2027 and final OCCT ABI/dependency/hash checks. CS0029 is gone; no new build errors; existing warnings remain. Audit found no equivalent remaining Roslyn/SAM diagnostic collision across active repositories.
- Final-head GitHub validation: Build [38792606241](https://github.com/SAM-BIM/SAM_Multitasker/actions/runs/38792606241) and SPDX [38792606472](https://github.com/SAM-BIM/SAM_Multitasker/actions/runs/38792606472) SUCCESS. Inspected build logs confirm dependency `SAM @ sow/2026-Q4`, actual Multitasker compilation and no compiler/MSBuild errors. Local full BuildAll additionally used SAM#204 `4a8afbebb186ace81eb5d49aed32deb502120781`.
- Reproduction, changed files, exact SHA manifests, raw-log hashes and machine-local prerequisites/evidence locations: `documentation/Diagnostic-Collision-Validation-2026-10-15.md` and `documentation/evidence/diagnostic-collision-2026-10-15/`. Reproduce by building sibling SAM first, then `dotnet build SAM_Multitasker/SAM.Core.Multitasker/SAM.Core.Multitasker.csproj -c Debug --no-restore -p:RunPostBuildEvent=Never`, the documented `dotnet test` command (Rhino 8 required), then `..\BuildAlls_v5.bat nopause`. Raw archive harness/logs/TRX are machine-local and must be copied or provisioned on another computer.
- Cleanup complete: verified expected remote task head, full containment in remote Q4, no open PR using it and no remaining worktree use; deleted `codex/fix-roslyn-diagnostic-collision-q4` remotely with a lease and locally without force. Checkout returned to Q4. No unrelated branch or worktree removed.
- Status: complete; no known blocker for this repair. Full-build success does not claim general installed-host engineering acceptance. Exact next step: continue SAM#204 through its independent review/approval workflow; use merged Multitasker Q4 for subsequent builds. No SAM#204 merge is authorized by this closeout.

## Q4 agent instructions rollout closeout - 2026-10-12

- SAM-BIM/SAM_Multitasker#11 merged as `77a3fe3c648f44c635d3acafd4452c86d8b2c86d` (merge commit pinned to approved head `2f8218843e7212ef42cb5db15dd6969541b9fc0c`; base `sow/2026-Q4` `eb8a68a2e443841be0272bd390f676c322c95b7e`; validated content `861788078bc3e52b55404bcdebe53301db8ed863`). Owner-authorized 2026-10-12 after an independent review (`AGENT_ROLLOUT_REVIEW_2026-10-12.md`, local to the owner's workspace, not committed).
- Docs only: AGENTS.md, CLAUDE.md, plus the rollout record `documentation/Agent-Continuity-Rollout-Q4.md`. No product code, build, workflow, dependency pin or tool changed; this closeout is the only `PROJECT_PROGRESS.md` change.
- Validation: local documentation checks and commands are in `documentation/Agent-Continuity-Rollout-Q4.md` (`git diff --check`, file scope, UTF-8/LF/no BOM, import). Hosted checks at the approved head: build success (run 38257990601); spdx success (run 38257990695). Dependency selection: dependency clones in the build log: SAM @ sow/2026-Q4.
- Cleanup: remote docs/agent-instructions-rollout-q4 deleted with an expected-SHA lease on 2f8218843e7212ef42cb5db15dd6969541b9fc0c after confirming it is contained in sow/2026-Q4 and no open PR uses it as head or base; the local branch and its worktree are preserved (worktree in use). Other branches, worktrees and the frozen quarters were not touched.
- Remaining gaps: no agent-client behaviour or token benchmark has been run.
- Exact next step: Continue from the shared `AGENTS.md`. Open item carried from the pilots: verify on each installed agent client, in a fresh Q4 session, that it follows the instructions without loading the full progress history, and record the result.

## Icon migration closeout — 2026-10-11

- [SAM_Multitasker#10](https://github.com/SAM-BIM/SAM_Multitasker/pull/10) merged as `34bfc242a2c6dc72e2dafbd3823a2e11a83b60b9`; owner authorized merging after green/safe validation. Feature branch preserved.
- Completed frozen SAM#179 glyph/palette/badge migration; component GUIDs, APIs, namespaces, metadata and behavior preserved. Icon resources, manifests/generation kit, token assignments and PR validation record changed; no unrelated production refactor.
- Fresh local `Release` build PASS; source guard 9 token swaps / 0 non-icon C# changes (standard SPDX headers and additive resource properties excepted); 9 resource references / 0 errors. Final-head GitHub checks passed before merge. Available regression test results are recorded in `documentation/Icon-Migration-Validation-2026-10-11.md`; hosted Rhino smoke is not claimed as a fresh pass.
- Revit-family additional builds: 2025/2027 passed where applicable. OCCT native build and native integration cases were not required for resource-only changes; skips remain documented.
- Remaining: no known blocker for this merged icon PR. SAM_Building and separate local test/environment failures elsewhere remain tracked in the organisation audit; unrelated PR9 is not merged by this task. All network operations target SAM-BIM; historical external integration/attribution text is unchanged.
- Exact next step: owner visual review of merged icons in the installed host, then continue other work from current Q4. No installer/submodule repin is included in this closeout.


## Branch

`sow/2026-Q4` - bootstrapped 2026-10-06 from `master` `45f6854d`. Frozen Q3 record: `sow/2026-Q3` @ `ca04b80e` (not modified).

## Last updated

2026-10-06 (Q4 icon-redesign migration).

## Current status

Q4 branch cut from `master` `45f6854d`, which is the exact commit pinned in SAM_Deploy's frozen Q3 baseline (`v20261006.1`). Bootstrap added only internal docs (this file, `AGENTS.md`). No product source changed. No Q4 product work has started.

## Q4 priorities

Not yet set by the owner. Record them here at the first Q4 planning pass. Known carry-over work is listed below.

## Known carry-over work

- **SAM Grasshopper icon redesign - PR #8** (`feature/sam-gh-icon-redesign` @ `670b41b1`, open, base `sow/2026-Q3`, not merged). Analysed 2026-10-06: the branch carries only its own 4 icon-only commits (`03c2df7`, `36b483a`, `44e2f56`, `670b41b`) on top of Q3 commit `55337969`. Those commits are not reachable from `sow/2026-Q4` (Q4 is built on the promoted `master` line), so a plain retarget would list 9 commits. Replaying exactly those commits onto `sow/2026-Q4` @ `93a893d5` is conflict-free (verified commit-by-commit with `git merge-tree`; identical to the net-diff merge). Planned action: rebase-onto Q4 as a new branch + PR, then close this one; owner-approved controlled task, not yet executed. **Update:** migrated; replacement Q4 PR SAM_Multitasker#10 (see the icon-redesign migration section); this old PR stays open for now.

## Repository-specific next steps

- Await Q4 planning. Open PRs for Q4 work against `sow/2026-Q4`.
- Follow the continuity convention in `AGENTS.md` for every PR and closeout.

## Decisions / assumptions

- Q4 base is `master` `45f6854d`; the internal files were recovered from `sow/2026-Q3` into this branch only, never onto `master`.
- Q4 history intentionally does not contain the Q3 branch history (the maintained `master` is the promoted Q3 line, which is not a descendant of `sow/2026-Q3`); the frozen `sow/2026-Q3` branch is the permanent record.
- Historical Q2/Q3 content below is kept as evidence; its branch names, SHAs and next steps describe Q3 and are not current instructions.

## Validation

- Bootstrap verified 2026-10-06: `sow/2026-Q4` was created at exactly `45f6854d` and the push was a normal (non-forced) branch creation.

## Issues / blockers

- None at bootstrap.

## Next step

- Owner to set Q4 priorities; then start the first Q4 task from this branch.

## Q4 operational cleanup (2026-10-06)

- Reviewed every active Q2/Q3 reference in this repository on `sow/2026-Q4` (workflow branch filters, dependency-branch resolution, `.gitmodules`/validation, docs). Historical Q2/Q3 mentions (feature documentation records, the frozen Q3 section below) are intentionally unchanged.
- Changed (`93a893d`): removed the dead `$candidates += 'sow/2026-Q2'` fallback from the dependency-branch resolution in `.github/workflows/build.yml`. No dependency repository has a `sow/2026-Q2` branch, so the entry never matched and resolution already fell through to the default branch; behaviour is unchanged (PR head ref, current sow ref, then the dependency's default branch) and no per-quarter edit is needed.
- Checked, no action: the `github.repository_owner == 'SAM-BIM'` build guard (intentional; its comment names HoareLea only to explain why the guard exists), CODEOWNERS (SAM-BIM owners), and workflow secrets (no HoareLea-named secret). The local `upstream` (HoareLea) remote is preserved.
- Carry-over: **SAM Grasshopper icon redesign - PR #8** (`feature/sam-gh-icon-redesign` @ `670b41b1`, open, base `sow/2026-Q3`, not merged). Analysed 2026-10-06: the branch carries only its own 4 icon-only commits (`03c2df7`, `36b483a`, `44e2f56`, `670b41b`) on top of Q3 commit `55337969`. Those commits are not reachable from `sow/2026-Q4` (Q4 is built on the promoted `master` line), so a plain retarget would list 9 commits. Replaying exactly those commits onto `sow/2026-Q4` @ `93a893d5` is conflict-free (verified commit-by-commit with `git merge-tree`; identical to the net-diff merge). Planned action: rebase-onto Q4 as a new branch + PR, then close this one; owner-approved controlled task, not yet executed.
- Full cross-repository record, migration table and owner decisions: `SAM_Deploy:sow/2026-Q4` `PROJECT_PROGRESS.md`.

## Q4 icon-redesign migration (2026-10-06)

- Old PR: SAM-BIM/SAM_Multitasker#8 (`feature/sam-gh-icon-redesign` @ `670b41b1`, base `sow/2026-Q3`) - **preserved, open, untouched**.
- New branch `feature/sam-gh-icon-redesign-q4` cut from `sow/2026-Q4` @ `0a4b781c`; new PR **SAM-BIM/SAM_Multitasker#10** (base `sow/2026-Q4`), feature head `761b0a43`. **Not merged.**
- Replayed (old -> new, `cherry-pick -x`; commit set taken from the GitHub PR metadata): `03c2df7`->`61b4cde`, `36b483a`->`191db40`, `44e2f56`->`fc8988a`, `670b41b`->`487acf3`; replay-only tip `487acf39`; plus one new docs commit `761b0a4` pointing the PR record at the new PR. No Q3 history imported.
- Verified at the replay-only tip, before the record commit: result tree identical to the net-diff merge of the old feature onto Q4 (`502458b492`); same aggregate and per-commit `git patch-id`, file set (63 files), numstat and blobs as the old PR; no workflow/`.gitmodules`/`AGENTS.md`/`PROJECT_PROGRESS.md`/solution changes. The final PR head is not tree-identical to the old feature by design (extra documentation-only commit).
- Validation: `check_source.py origin/sow/2026-Q4` OK, `check_assemblies.py` OK, local build 0 errors, relevant tests green (see the PR body); PR CI `build` success, `spdx` success; mergeable: mergeable.
- Next: owner decides whether/when to close the old PR; merge remains the maintainer's call.

---

# Historical record - 2026-Q3 (frozen)

Source: last revision of the file on `sow/2026-Q3`, commit `676bd7e` (the file was removed from the Q3 tip by `ca04b80`; `sow/2026-Q3` tip is `ca04b80e`). Preserved verbatim except that heading levels are shifted down one. Everything below describes Q3 and is not a current instruction.

## Project Progress

### Branch
`sow/2026-Q3`

### Last updated
2026-09-22 - app.config cleanup merged

### Current status
Part of the repo-family .NET Framework `app.config` cleanup: base [SAM#126](https://github.com/SAM-BIM/SAM/pull/126) plus 17 sibling PRs, all merged into `sow/2026-Q3` on 2026-09-22 (SAM first), with their branches deleted.

### Completed
- [SAM_Multitasker#6](https://github.com/SAM-BIM/SAM_Multitasker/pull/6) merged as `50adfdfd`: removed dead .NET Framework `app.config` files.

### Decisions / assumptions
- Every project here targets `netstandard2.0` or `net8.0(-windows)` and is an `OutputType Library`. Library `.dll.config` files are never read at runtime (only the host `Rhino.exe`/`Revit.exe` config is), so the net472-era binding redirects, `<supportedRuntime>` and `loadFromRemoteSources` were inert. They only emitted stale `.dll.config` files into `build/` and `%APPDATA%\SAM`.
- No `ConfigurationManager`/`AppSettings` use in the repo; deleted files held binding/runtime config only.

### Files changed
- `Grasshopper/SAM.Analytical.Grasshopper.Multitasker/app.config` (deleted)
- `Grasshopper/SAM.Core.Grasshopper.Multitasker/app.config` (deleted)

### Validation
- Before merge, full `BuildAlls_v4.bat` (Debug Restore;Clean;Rebuild of every repo, starting from an emptied `%APPDATA%\SAM`): exit 0, 0 errors. The redeployed `%APPDATA%\SAM` has no `SAM.*.dll.config`.
- CI on the PR: build + spdx pass.

### Issues / blockers
- None known for this cleanup.

### Next step
- None for this cleanup. Continue with the next planned task on `sow/2026-Q3`.
