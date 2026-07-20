# Training Area Explanatory Popup Plan

## Problem Statement
Users can open a CEFR level such as A1 and see a list of teaching areas, but the current UI only shows a short title and one-line description. That is not enough to explain why each area matters, what skill it is building, or how it connects to real German usage. The result is that users enter a training area without understanding its purpose.

The app needs a clear, low-friction way to explain each teaching area in plain English before or during selection, without disrupting the existing training flow. The explanation must be discoverable from the level screen, visually integrated into each teaching-area card, and substantial enough to teach the user what the area is for and what kinds of patterns they should expect to learn.

## Proposed Approach
Add a dedicated info action to each teaching-area card on the training units screen. The row itself should keep its current behavior and still open the training area directly. A separate info icon button should open a well-designed popup or dialog that explains the selected teaching area in clear English.

To stay aligned with the current architecture, the explanatory content should live alongside the existing hand-authored curriculum metadata in the core curriculum layer rather than being derived from exercise text at runtime. Each CurriculumUnit should gain structured teaching-area guidance fields, for example:
- Why this matters
- What the learner should notice
- A small set of plain-language examples
- Optional practical situations where this grammar or vocabulary appears

The training UI should bind the new icon and popup content through the existing UnitTileViewModel/TrainingViewModel path. The popup should be visually richer than a plain message box: clear heading, short intro, purpose statement, example block(s), and a concise “what you’ll practice here” section. Content will be authored in English only for now, across all A1-C2 teaching areas.

## Phases

### Phase 1: Confirm and model the teaching-area explanation data
- [ ] Review the existing curriculum content model in the core layer and identify the smallest extension to CurriculumUnit that can support rich explanatory content without changing exercise behavior.
- [ ] Define a structured teaching-area explanation model for unit metadata, such as a record/class with fields for overview, purpose, learner outcome, and example items.
- [ ] Decide whether examples should be a simple list of strings or a richer shape with German example plus English explanation.
- [ ] Ensure the structure is easy to hand-author in the existing static unit files and does not force future persistence or database changes.
- [ ] Keep the model English-only for now, but shape it so later localization is possible without a redesign.

### Phase 2: Author curated explanation content for all A1-C2 teaching areas
- [ ] Inventory every existing CurriculumUnit across A1, A2, B1, B2, C1, and C2.
- [ ] For each unit, write a plain-English explanation that answers: what this area teaches, why it matters, and what the learner should pay attention to.
- [ ] Add at least two helpful examples per teaching area where practical, using short German examples paired with clear English interpretation.
- [ ] Keep tone consistent across all levels, with beginner-friendly explanations for A1/A2 and progressively more precise explanations for higher CEFR levels.
- [ ] Avoid overly academic grammar terminology unless it is immediately explained in plain English.
- [ ] Cross-check each explanation against the actual exercises in the unit so the popup accurately previews what the learner will encounter.
- [ ] Fill the new explanation metadata directly in the static content files under the curriculum content folder.

### Phase 3: Extend the training view-model path
- [ ] Update UnitTileViewModel so it exposes the new teaching-area explanation metadata needed by the UI.
- [ ] Add a command path in TrainingViewModel to open the explanation popup for a selected unit without starting the unit.
- [ ] Keep unit selection behavior unchanged so clicking the card still opens training immediately.
- [ ] Ensure the popup command can be triggered independently from the icon button inside the unit card.
- [ ] If needed, add a lightweight popup-specific view model to shape the displayed sections cleanly instead of binding raw content directly.

### Phase 4: Add the info icon and popup UI in the training screen
- [ ] Update the unit-card template in the training window so each teaching area shows an info icon beside the title, without making the card feel cramped.
- [ ] Prevent the icon click from accidentally triggering the card’s existing open-unit action.
- [ ] Introduce a training-specific popup/dialog UI component that matches the app’s current WPF UI style better than a basic message box.
- [ ] Lay out the popup with a strong visual hierarchy: teaching area title, short summary, “Why this matters”, “What you will practice”, and examples.
- [ ] Make the popup readable on the current dark theme, including spacing, text wrapping, section headings, and comfortable maximum width.
- [ ] Ensure the popup is usable for both short A1 explanations and denser C1/C2 content without overflow problems.
- [ ] Add scrolling behavior inside the popup only if necessary, preferring a layout that keeps the first screen highly legible.

### Phase 5: Refine interaction and content presentation
- [ ] Add tooltip/help text to the info icon so users understand it opens an explanation.
- [ ] Verify keyboard accessibility and focus behavior for the icon and popup close action.
- [ ] Ensure repeated opens/closes do not leave stale content when switching between units.
- [ ] Review title-row layout on narrower window widths so long titles plus the info icon remain readable.
- [ ] Tune wording and formatting so examples are visually distinct from explanatory paragraphs.

### Phase 6: Validate behavior and guard against regressions
- [ ] Build the app and fix any compile or binding issues introduced by the new metadata and commands.
- [ ] Manually validate the training flow for at least one unit in each of A1, A2, B1, B2, C1, and C2.
- [ ] Verify that clicking the card still opens the unit and clicking the icon only opens the explanation popup.
- [ ] Validate that the popup content corresponds to the selected teaching area and updates correctly between cards.
- [ ] Confirm that long-form text wraps properly and remains readable at the app’s minimum supported training window size.
- [ ] Check that the feature behaves correctly when future units are added with missing or partial explanation content.

## Acceptance Criteria
- Each teaching area card in the training units screen shows an info icon next to the area title.
- Clicking the teaching area card still opens the exercises exactly as before.
- Clicking the info icon opens a dedicated popup/dialog for that teaching area only.
- The popup explains the teaching area in clear English, including why the learner is doing it and what skill it develops.
- The popup includes multiple examples or example-style explanations that help the user understand the concept before starting.
- Explanation content exists for all current teaching areas from A1 through C2.
- The popup layout is readable, visually structured, and consistent with the app’s existing dark WPF style.
- No existing training navigation or exercise behavior regresses.
- The solution builds successfully after the change.

## Open Questions / Assumptions
- Assumption: The feature will cover all currently authored teaching areas from A1 through C2 in one implementation pass.
- Assumption: Explanatory content will be curated and authored in-repo rather than generated dynamically from an external service at runtime.
- Assumption: Content will be English-only for this release.
- Assumption: The unit card remains the primary action for entering training, while the new info icon is a secondary action for context.
- Open question for implementation: whether Wpf.Ui already offers a dialog control worth reusing, or whether the cleanest option is a lightweight custom modal window/user control for this training-only feature.
- Open question for implementation: whether explanation examples should be represented as plain strings or as structured German/English pairs for better layout consistency.
