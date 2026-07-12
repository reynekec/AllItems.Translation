namespace AllItems.Translation.Core.Curriculum.Content;

/// <summary>
/// B1 units building on A2: Konjunktiv II, Passiv, Relativsätze, Genitiv, Wechselpräpositionen,
/// adjective declension, indirect questions, Futur I, subordinating conjunctions, and expressing
/// opinions.
/// </summary>
public static class B1Units
{
    public static IReadOnlyList<CurriculumUnit> All { get; } =
    [
        new CurriculumUnit
        {
            Id = "b1-u1-konjunktiv-ii",
            Level = CefrLevel.B1,
            SortOrder = 1,
            Title = "Konjunktiv II: würde, hätte, wäre",
            Description = "Polite requests and hypothetical situations.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "b1-u1-konjunktiv-ii-e1",
                    Instruction = "Fill in every form of 'würde' (the Konjunktiv II of werden, used with an infinitive).",
                    Explanation = "'würde + Infinitiv' is the most common way to express a hypothetical action.",
                    BaseWord = "würde",
                    Slots =
                    [
                        new ConjugationSlot("ich", "würde"),
                        new ConjugationSlot("du", "würdest"),
                        new ConjugationSlot("er/sie/es", "würde"),
                        new ConjugationSlot("wir", "würden"),
                        new ConjugationSlot("ihr", "würdet"),
                        new ConjugationSlot("sie/Sie", "würden")
                    ]
                },
                new ClozeExercise
                {
                    Id = "b1-u1-konjunktiv-ii-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich hätte gerne...\" is the standard polite way to order or request something.",
                    TextBefore = "Ich ",
                    TextAfter = " gerne einen Kaffee.",
                    CorrectAnswer = "hätte"
                },
                new ClozeExercise
                {
                    Id = "b1-u1-konjunktiv-ii-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "The 'wenn' clause uses 'hätte'; the main clause uses 'würde + Infinitiv'.",
                    TextBefore = "Wenn ich Zeit hätte, ",
                    TextAfter = " ich kommen.",
                    CorrectAnswer = "würde"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u1-konjunktiv-ii-e4",
                    Instruction = "Choose the correct answer.",
                    Explanation = "'wäre' is the Konjunktiv II form of 'sein' (to be).",
                    Question = "'wäre' is the Konjunktiv II form of which verb?",
                    Options = ["sein", "haben", "werden", "würden"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b1-u1-konjunktiv-ii-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Das wäre schön\" = \"That would be nice\" - a common polite phrase.",
                    TextBefore = "Das ",
                    TextAfter = " schön.",
                    CorrectAnswer = "wäre"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u2-passiv",
            Level = CefrLevel.B1,
            SortOrder = 2,
            Title = "Passiv (Präsens)",
            Description = "The passive voice - when the focus is on the action, not who does it.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "b1-u2-passiv-e1",
                    Instruction = "Fill in every present-tense form of 'werden' (used to build the passive).",
                    Explanation = "'werden' is the auxiliary verb for the passive voice, just like 'haben'/'sein' for the Perfekt.",
                    BaseWord = "werden",
                    Slots =
                    [
                        new ConjugationSlot("ich", "werde"),
                        new ConjugationSlot("du", "wirst"),
                        new ConjugationSlot("er/sie/es", "wird"),
                        new ConjugationSlot("wir", "werden"),
                        new ConjugationSlot("ihr", "werdet"),
                        new ConjugationSlot("sie/Sie", "werden")
                    ]
                },
                new ClozeExercise
                {
                    Id = "b1-u2-passiv-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "Passive = werden (conjugated) + Partizip II: das Haus wird gebaut.",
                    TextBefore = "Das Haus ",
                    TextAfter = " gebaut.",
                    CorrectAnswer = "wird"
                },
                new ClozeExercise
                {
                    Id = "b1-u2-passiv-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Das Auto wird repariert\" - the car is being repaired.",
                    TextBefore = "Das Auto ",
                    TextAfter = " repariert.",
                    CorrectAnswer = "wird"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u2-passiv-e4",
                    Instruction = "Choose the correct answer.",
                    Explanation = "The passive is formed with werden + Partizip II, not haben/sein.",
                    Question = "The passive voice uses which auxiliary verb + Partizip II?",
                    Options = ["werden", "sein", "haben", "würde"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u2-passiv-e5",
                    Instruction = "Choose the correct passive sentence.",
                    Explanation = "\"Die Tür wird geschlossen\" correctly puts werden in second position and the participle at the end.",
                    Question = "\"The door is being closed.\"",
                    Options = ["Die Tür wird geschlossen.", "Die Tür geschlossen wird.", "Die Tür ist geschlossen wird.", "Die Tür schließt wird."],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u3-relativsaetze",
            Level = CefrLevel.B1,
            SortOrder = 3,
            Title = "Relativsätze",
            Description = "Relative clauses - der/die/das used to describe a noun in more detail.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b1-u3-relativsaetze-e1",
                    Instruction = "Choose the correct relative pronoun.",
                    Explanation = "The relative pronoun matches 'der Mann' (masculine) and is the subject here, so it's nominative: der.",
                    Question = "Der Mann, ___ dort steht, ist mein Vater.",
                    Options = ["der", "den", "dem", "die"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u3-relativsaetze-e2",
                    Instruction = "Choose the correct relative pronoun.",
                    Explanation = "'die Frau' is feminine; as the object of 'kenne' it's accusative, which looks the same as nominative for feminine: die.",
                    Question = "Die Frau, ___ ich kenne, wohnt hier.",
                    Options = ["die", "der", "den", "das"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u3-relativsaetze-e3",
                    Instruction = "Choose the correct relative pronoun.",
                    Explanation = "'das Buch' is neuter, and neuter accusative matches nominative: das.",
                    Question = "Das Buch, ___ ich lese, ist interessant.",
                    Options = ["das", "der", "die", "den"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b1-u3-relativsaetze-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'der Film' is masculine, and as the accusative object of 'gesehen habe', it becomes 'den'.",
                    TextBefore = "Der Film, ",
                    TextAfter = " ich gesehen habe, war gut.",
                    CorrectAnswer = "den"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u4-genitiv",
            Level = CefrLevel.B1,
            SortOrder = 4,
            Title = "Genitiv",
            Description = "The genitive case - showing possession, similar to English 's.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b1-u4-genitiv-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "Masculine/neuter nouns add -s or -es in the genitive, and the article becomes 'des'.",
                    TextBefore = "Das ist das Auto ",
                    TextAfter = " Mannes.",
                    CorrectAnswer = "des"
                },
                new ClozeExercise
                {
                    Id = "b1-u4-genitiv-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "Feminine nouns don't change, but 'die' becomes 'der' in the genitive.",
                    TextBefore = "Das ist die Tasche ",
                    TextAfter = " Frau.",
                    CorrectAnswer = "der"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u4-genitiv-e3",
                    Instruction = "Choose the correct genitive form.",
                    Explanation = "'das Kind' is neuter, so genitive is 'des Kindes'.",
                    Question = "Genitive of 'das Kind'?",
                    Options = ["des Kindes", "der Kind", "dem Kind", "das Kindes"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u4-genitiv-e4",
                    Instruction = "Choose the correct genitive form.",
                    Explanation = "'der Vater' is masculine, so genitive is 'des Vaters'.",
                    Question = "Genitive of 'der Vater'?",
                    Options = ["des Vaters", "der Vater", "dem Vater", "des Vater"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u5-wechselpraepositionen",
            Level = CefrLevel.B1,
            SortOrder = 5,
            Title = "Wechselpräpositionen",
            Description = "Two-way prepositions: accusative for direction (wohin?), dative for location (wo?).",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b1-u5-wechselpraepositionen-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"legen\" describes motion onto something, so it takes the accusative: den Tisch.",
                    TextBefore = "Ich lege das Buch auf ",
                    TextAfter = " Tisch.",
                    CorrectAnswer = "den"
                },
                new ClozeExercise
                {
                    Id = "b1-u5-wechselpraepositionen-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"liegen\" describes a fixed location, so it takes the dative: dem Tisch.",
                    TextBefore = "Das Buch liegt auf ",
                    TextAfter = " Tisch.",
                    CorrectAnswer = "dem"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u5-wechselpraepositionen-e3",
                    Instruction = "Choose the correct case.",
                    Explanation = "'wo' asks about a fixed location, which takes the dative case.",
                    Question = "Which case answers the question \"wo?\" (location)?",
                    Options = ["Dativ", "Akkusativ"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u5-wechselpraepositionen-e4",
                    Instruction = "Choose the correct case.",
                    Explanation = "'wohin' asks about a direction, which takes the accusative case.",
                    Question = "Which case answers the question \"wohin?\" (direction)?",
                    Options = ["Akkusativ", "Dativ"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b1-u5-wechselpraepositionen-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"stellen\" (to put/place, motion) takes the accusative: den Schrank.",
                    TextBefore = "Ich stelle die Lampe neben ",
                    TextAfter = " Schrank.",
                    CorrectAnswer = "den"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u6-adjektivdeklination",
            Level = CefrLevel.B1,
            SortOrder = 6,
            Title = "Adjektivdeklination",
            Description = "Adjective endings change depending on the article and case.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b1-u6-adjektivdeklination-e1",
                    Instruction = "Choose the correct sentence.",
                    Explanation = "After 'der' (which already shows the gender), the adjective just adds -e: der große Mann.",
                    Question = "Which is correct?",
                    Options = ["Der große Mann kommt.", "Der großer Mann kommt.", "Der großes Mann kommt.", "Der großen Mann kommt."],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u6-adjektivdeklination-e2",
                    Instruction = "Choose the correct sentence.",
                    Explanation = "After 'ein' (which doesn't show gender by itself), the adjective takes -er for masculine nominative: ein großer Mann.",
                    Question = "Which is correct?",
                    Options = ["Ein großer Mann kommt.", "Ein große Mann kommt.", "Ein großes Mann kommt.", "Ein großen Mann kommt."],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b1-u6-adjektivdeklination-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Auto' is neuter, so after 'ein' the adjective takes -es: ein neues Auto.",
                    TextBefore = "Ich habe ein ",
                    TextAfter = " Auto.",
                    CorrectAnswer = "neues"
                },
                new ClozeExercise
                {
                    Id = "b1-u6-adjektivdeklination-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Idee' is feminine, so after 'eine' the adjective takes -e: eine gute Idee.",
                    TextBefore = "Das ist eine ",
                    TextAfter = " Idee.",
                    CorrectAnswer = "gute"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u7-indirekte-fragen",
            Level = CefrLevel.B1,
            SortOrder = 7,
            Title = "Indirekte Fragen",
            Description = "Indirect questions with 'ob' or a question word - the verb moves to the end.",
            Exercises =
            [
                new SentenceOrderExercise
                {
                    Id = "b1-u7-indirekte-fragen-e1",
                    Instruction = "Put the words in the correct order (after the comma).",
                    Explanation = "'ob' works like 'weil'/'dass' - it pushes the conjugated verb to the end.",
                    ScrambledWords = ["kommt", "ob", "er"],
                    CorrectOrder = ["ob", "er", "kommt"]
                },
                new SentenceOrderExercise
                {
                    Id = "b1-u7-indirekte-fragen-e2",
                    Instruction = "Put the words in the correct order (after the comma).",
                    Explanation = "The W-word 'wo' also sends the verb 'ist' to the end of the indirect question.",
                    ScrambledWords = ["ist", "wo", "der", "Bahnhof", "?"],
                    CorrectOrder = ["wo", "der", "Bahnhof", "ist", "?"]
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u7-indirekte-fragen-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Indirect questions behave like other subordinate clauses: the verb goes to the end.",
                    Question = "In an indirect question with 'ob' or a question word, where does the verb go?",
                    Options = ["At the end", "Second position", "First position"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b1-u7-indirekte-fragen-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ob' introduces a yes/no indirect question - like English \"whether/if\".",
                    TextBefore = "Ich weiß nicht, ",
                    TextAfter = " er Zeit hat.",
                    CorrectAnswer = "ob"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u8-futur",
            Level = CefrLevel.B1,
            SortOrder = 8,
            Title = "Futur I",
            Description = "Talking about the future with werden + Infinitiv.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "b1-u8-futur-e1",
                    Instruction = "Fill in every Futur I form of 'arbeiten'.",
                    Explanation = "Futur I = werden (conjugated) + the infinitive at the end.",
                    BaseWord = "arbeiten (Futur I)",
                    Slots =
                    [
                        new ConjugationSlot("ich", "werde arbeiten"),
                        new ConjugationSlot("du", "wirst arbeiten"),
                        new ConjugationSlot("er/sie/es", "wird arbeiten"),
                        new ConjugationSlot("wir", "werden arbeiten"),
                        new ConjugationSlot("ihr", "werdet arbeiten"),
                        new ConjugationSlot("sie/Sie", "werden arbeiten")
                    ]
                },
                new ClozeExercise
                {
                    Id = "b1-u8-futur-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ich werde' + infinitive at the end: ich werde ... arbeiten.",
                    TextBefore = "Ich ",
                    TextAfter = " morgen arbeiten.",
                    CorrectAnswer = "werde"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u8-futur-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Futur I always pairs werden with an infinitive, never a participle.",
                    Question = "How is Futur I formed?",
                    Options = ["werden + Infinitiv", "haben + Partizip II", "sein + Partizip II", "würde + Infinitiv"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u9-konjunktionen",
            Level = CefrLevel.B1,
            SortOrder = 9,
            Title = "Konjunktionen: obwohl, damit, trotzdem",
            Description = "More nuanced ways to connect ideas: although, so that, nevertheless.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b1-u9-konjunktionen-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "'obwohl' (although) is a subordinating conjunction - it sends the verb to the end.",
                    TextBefore = "",
                    TextAfter = " es regnet, gehe ich spazieren.",
                    CorrectAnswer = "Obwohl"
                },
                new ClozeExercise
                {
                    Id = "b1-u9-konjunktionen-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'damit' (so that) expresses purpose and also sends the verb to the end.",
                    TextBefore = "Ich lerne Deutsch, ",
                    TextAfter = " ich einen guten Job bekomme.",
                    CorrectAnswer = "damit"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u9-konjunktionen-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "'trotzdem' is not a subordinating conjunction - it just occupies position 1, and the verb stays in second position.",
                    Question = "\"trotzdem\" (nevertheless) - what's its word order?",
                    Options = ["Position 1, verb stays second", "Subordinating - verb moves to the end"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b1-u9-konjunktionen-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Trotzdem gehe ich spazieren\" - trotzdem in position 1, verb 'gehe' right after it.",
                    TextBefore = "Es regnet. ",
                    TextAfter = " gehe ich spazieren.",
                    CorrectAnswer = "Trotzdem"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b1-u10-meinungen",
            Level = CefrLevel.B1,
            SortOrder = 10,
            Title = "Meinungen äußern",
            Description = "Phrases for expressing and discussing opinions.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b1-u10-meinungen-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich bin der Meinung, dass...\" = \"I am of the opinion that...\".",
                    TextBefore = "Ich bin der ",
                    TextAfter = ", dass das richtig ist.",
                    CorrectAnswer = "Meinung"
                },
                new ClozeExercise
                {
                    Id = "b1-u10-meinungen-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Meiner Meinung nach...\" = \"In my opinion...\".",
                    TextBefore = "Meiner ",
                    TextAfter = " nach ist das eine gute Idee.",
                    CorrectAnswer = "Meinung"
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u10-meinungen-e3",
                    Instruction = "Choose the correct translation.",
                    Explanation = "\"Ich finde, dass...\" is a common, slightly less formal way to say \"I think that...\".",
                    Question = "\"Ich finde, dass...\" means?",
                    Options = ["I think that...", "I find that place...", "I found...", "I feel..."],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b1-u10-meinungen-e4",
                    Instruction = "Choose the correct German phrase.",
                    Explanation = "\"Ich stimme zu\" = \"I agree\".",
                    Question = "\"I agree\" in German?",
                    Options = ["Ich stimme zu.", "Ich stimme gegen.", "Ich meine nicht.", "Ich bin dagegen."],
                    CorrectOptionIndex = 0
                }
            ]
        }
    ];
}
