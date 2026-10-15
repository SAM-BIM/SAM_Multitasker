# Multitasker Roslyn Diagnostic collision — 2026-10-15

## Status and scope

PR [SAM_Multitasker#12](https://github.com/SAM-BIM/SAM_Multitasker/pull/12), focused correction into `sow/2026-Q4`, based on Multitasker `aa126fa6eeb34cbffb72cbf5a5129aa17be1ec6b`.
Validated content SHA: `cfe9d116c1d9837e23ada308d535029eaf6de773`.
Affected-project build, all four NUnit tests, and full BuildAlls_v5 pass locally. Initial PR-head GitHub Build and SPDX pass; final record-only head checks are reported in the PR description. No merge authorized.
SAM#204 is unchanged and can proceed independently of this correction: the failure reproduces at its Q4 base, before its changes.

## Proven cause and dependency boundary

Within namespace `SAM.Core.Multitasker`, adding `SAM.Core.Diagnostic` changes resolution of the unqualified `Diagnostic` identifier from the imported Roslyn class to the enclosing SAM.Core class. `script.Compile()` still returns Roslyn diagnostics, producing CS0029. The same resolution affects the stored diagnostics and public result constructor/property, so both production files require explicit aliases.

The first breaking SAM commit is `967a8639eb6f7f614ff4168d62ab3dfd979efbe4` (shared Diagnostics Phase 1, SAM#201, 2026-10-15 11:31 +02). Its immediate parent `a71435dd88d28c9b0da0bca43c21fbca5e1038b9` builds successfully with the identical Multitasker source. Multitasker was unchanged at `aa126fa6eeb34cbffb72cbf5a5129aa17be1ec6b`; the failing source originated in `4e7e01e61770b19f1647f387934c788e80b21c8c` (2024).

Isolated `git archive` source builds, without switching the SAM checkout:

| SAM SHA | Multitasker source | SAM.Core | Consumer | API/behavior probe |
|---|---|---|---|---|
| `0649e15ee9c1727af5834d9206017da9860917c5` | `aa126fa` | pass | pass | 4 assertions pass |
| `a71435dd88d28c9b0da0bca43c21fbca5e1038b9` | `aa126fa` | pass | pass | 4 assertions pass |
| `967a8639eb6f7f614ff4168d62ab3dfd979efbe4` | `aa126fa` | pass | CS0029 | not run |
| `6b12e2a65817556693ba408674211610f4a20dda` (Q4 before #204) | `aa126fa` | pass | CS0029 | not run |
| `967a8639eb6f7f614ff4168d62ab3dfd979efbe4` | `aa126fa` + alias fix | reused passing build | pass | 4 assertions pass |

Only the SAM.Core reference varied in this boundary experiment; other SAM assembly references were held at the same local build. Each archive used the actual SAM.Core and consumer project/source, with scratch-only absolute HintPaths and `RunPostBuildEvent=Never`. The ordinary affected-project build independently reproduced the original CS0029 before the fix.

## Last known good full configuration

The latest preserved successful full run is the morning 2026-10-15 `BuildAlls_v5.bat nopause`, completed about 09:19:44 +02 in `00:09:32.0360117`: Restore/Clean/Rebuild across the 26 active repositories, Revit 2025/2026/2027, final native OCCT checks passed.
Machine-local evidence: parent `.buildall-v5-validation/json-fix-2026-10-15/BuildAlls-full-build.log`, `BuildAlls-working-trees.log` and `BuildAlls-JSON-report.md`.

That log did not record SHAs. The exact SHA configuration in [initial-and-morning-configurations.json](evidence/diagnostic-collision-2026-10-15/initial-and-morning-configurations.json) is **reconstructed from local HEAD reflogs**, not a contemporaneous SHA manifest: equal heads at 09:10:12 and 09:19:44 +02, no entries during 09:10–09:20 in any of the 26 repos, and the contemporaneous working-tree log reports all clean. Relevant heads: SAM `0649e15ee9c1727af5834d9206017da9860917c5`, Multitasker `aa126fa6eeb34cbffb72cbf5a5129aa17be1ec6b`.

The same JSON records the clean investigation-start heads/branches. Multitasker's recorded feature branch was created from the clean Q4 head before edits; initial checkout was `sow/2026-Q4`. [full-build-configurations.json](evidence/diagnostic-collision-2026-10-15/full-build-configurations.json) records the current full-run heads, with Multitasker at the tested content SHA and SAM#204 at `4a8afbebb186ace81eb5d49aed32deb502120781`.

## Changes and safeguards

- `SAM_Multitasker/SAM.Core.Multitasker/Classes/Multitasker.cs`: distinct `RoslynDiagnostic` alias for compilation results.
- `SAM_Multitasker/SAM.Core.Multitasker/Classes/MultitaskerResults.cs`: same alias for storage, constructor and public property. Historical CLR types remain `IEnumerable<Microsoft.CodeAnalysis.Diagnostic>` / `List<Microsoft.CodeAnalysis.Diagnostic>`.
- Both touched production files receive required SPDX headers.
- Existing `design/grasshopper-icons/tests/GhIconTest/GhIconTest.csproj`: reference the core project; new `MultitaskerDiagnosticsTests.cs` covers public types, object-preserving diagnostic transfer and invalid-script compilation errors.
- This record and four compact JSON evidence files preserve the boundary and repository configurations.
- No algorithm, component, shared SAM.Core type, workflow or PROJECT_PROGRESS change. No SAM#204 edits.

## Reproduction and local validation

Prerequisites: sibling SAM build first, .NET SDK, installed Rhino 8 for the existing NUnit host fixture. Builds emit existing System.Memory reference and nullable warnings; no new suppression added.

From the Multitasker repository:

```powershell
dotnet build SAM_Multitasker/SAM.Core.Multitasker/SAM.Core.Multitasker.csproj -c Debug --no-restore -p:RunPostBuildEvent=Never
$env:SAM_ICON_MANIFESTS = (Resolve-Path design/grasshopper-icons/manifest.json).Path
$env:SAM_ICON_ASSEMBLY_DIR = (Resolve-Path build).Path
dotnet test design/grasshopper-icons/tests/GhIconTest/GhIconTest.csproj -c Debug -p:RunPostBuildEvent=Never --logger 'trx;LogFileName=Multitasker-regression.trx'
& ..\BuildAlls_v5.bat nopause
```

- Affected project: pass, 0 errors (2 warnings).
- Existing NUnit project: **4 passed, 0 failed, 0 skipped** (three new regression tests plus existing icon test).
- Isolated boundary: exact outcomes above, raw summary in [boundary-results.json](evidence/diagnostic-collision-2026-10-15/boundary-results.json).
- Similar-collision audit: only other active Roslyn source is in SAM_Tas GenOpt test namespaces. It uses inferred diagnostic values and Roslyn DiagnosticSeverity without the affected SAM.Core namespace lookup; no equivalent collision found. No Tas changes.
- Full BuildAlls_v5: **PASS**, exit 0, `00:07:41.6286121`, 2026-10-15 21:07:51–21:15:36 +02. All 26 active repositories Restore/Clean/Rebuild, Revit 2025/2026/2027 and final OCCT ABI/dependency/hash checks passed. CS0029 is absent; no new build errors. Existing warnings remain (package compatibility/duplicate references, assembly-version/architecture conflicts and XML documentation). This is build validation, not general installed-host engineering acceptance.
- GitHub Build at record-only head `7f95bfb7d92006ae37324bf472717cc97929ba1a`: **PASS**, [run 38791612549](https://github.com/SAM-BIM/SAM_Multitasker/actions/runs/38791612549); SPDX also passed. Logs confirm `Cloning SAM-BIM/SAM @ sow/2026-Q4` and actual SAM.Core.Multitasker compilation with no CS0029. Final docs-only commit checks are tracked in the PR description.
- Post-build status: all 26 source trees clean; only Multitasker head advanced from tested content by its record-only commit. SAM#204 remains clean at `4a8afbebb186ace81eb5d49aed32deb502120781`. [validation-results.json](evidence/diagnostic-collision-2026-10-15/validation-results.json) records full-run timing, test totals and SHA256 hashes of the retained machine-local logs/TRX.

Machine-local scratch evidence is under `C:\Users\michal\.codex\visualizations\2026\10\15\01a140c3-07d7-7db2-83db-a3f98397ac92\multitasker-regression`: `investigate.py` archive harness, core/consumer/probe logs, affected-project log, NUnit TRX, and full-run log. These raw logs/harness are not committed; copy/provision them if needed on another machine. The committed tests, JSON matrix and exact SHAs/commands above preserve the critical outcomes. Original pre-fix log: adjacent `pr204-review/Multitasker-before-fix.log`.

## Remaining work

Verify final record-only head GitHub checks, then request owner review and merge only with approval. The tested source remains `cfe9d116c1d9837e23ada308d535029eaf6de773`; subsequent commits change records only. After merge, add the known-merge-SHA docs-only Q4 closeout under repository policy. Keep SAM#204 independent.
