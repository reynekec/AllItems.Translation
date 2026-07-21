# Testing Charter — AllItems.Translation

## Purpose
Find real bugs and verify the application is **user-friendly, understandable, and genuinely high quality** through **UI/end-to-end automation testing**.

## Testing Scope
- **Framework**: WPF (Windows Presentation Foundation)
- **Automation Tool**: FlaUI with UIA3
- **Entry Point**: `AllItems.Translation.App` (.NET 10.0-windows)
- **Test Project**: `AllItems.Translation.Automation` (to be created as needed)

## Severity Definitions
- **Critical**: Crash, data loss, security breach, core feature completely broken, user blocked from achieving primary goal
- **High**: Major feature broken or significantly degraded; workaround exists but difficult; UX is confusing
- **Medium**: Feature partially broken; edge case failure; misleading error message; minor UX friction
- **Low**: Polish issue; cosmetic glitch; documentation gap; nice-to-have improvement

## Testing Principles
1. **User-centric**: Test like a real user, not like a compiler. Exercise primary workflows, edge cases, and error conditions.
2. **Reproducible**: Every finding must have clear, deterministic repro steps.
3. **Evidence-backed**: Screenshots, logs, exact control references when possible.
4. **Regression-aware**: After each fix, re-validate the scenario plus nearby smoke checks.
5. **UX quality**: Probe for clarity, feedback, actionability, and surprise friction.

## Test Report Convention
- **Location**: `.copilot-tracking/testing-reports/`
- **Naming**: `YYYY-MM-DD_HH-mm_<scope-slug>.md`
- **Structure**: Findings ranked by severity; each finding includes repro steps, expected vs. actual, and UX note
- **Closure**: Moved from `InProgress/` to `Completed/` at end of run; includes run closure block with status and next action

## Run Tracking
Each test run produces a dated report with:
- Detected framework and automation setup
- Test scope and execution plan
- All findings (ranked by severity)
- Fixes applied (if any) with validation
- Run status and recommended next step
