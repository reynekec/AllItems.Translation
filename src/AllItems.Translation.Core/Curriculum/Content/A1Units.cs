namespace AllItems.Translation.Core.Curriculum.Content;

/// <summary>
/// A real first batch of A1 units covering the core beginner grammar/vocabulary points: sein/haben,
/// present tense, articles, negation, word order, modal verbs, accusative case, numbers/time,
/// everyday vocabulary, and plurals. A2-C2 stay structurally ready but empty until authored.
/// </summary>
public static class A1Units
{
    public static IReadOnlyList<CurriculumUnit> All { get; } =
    [
        new CurriculumUnit
        {
            Id = "a1-u1-sein-haben",
            Level = CefrLevel.A1,
            SortOrder = 1,
            Title = "Sein und haben",
            Description = "The two most common verbs in German: to be and to have.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "a1-u1-sein-haben-e1",
                    Instruction = "Fill in every form of 'sein' (to be).",
                    Explanation = "'sein' is irregular in every form - these six are worth memorizing by heart.",
                    BaseWord = "sein",
                    Slots =
                    [
                        new ConjugationSlot("ich", "bin"),
                        new ConjugationSlot("du", "bist"),
                        new ConjugationSlot("er/sie/es", "ist"),
                        new ConjugationSlot("wir", "sind"),
                        new ConjugationSlot("ihr", "seid"),
                        new ConjugationSlot("sie/Sie", "sind")
                    ]
                },
                new ConjugationDrillExercise
                {
                    Id = "a1-u1-sein-haben-e2",
                    Instruction = "Fill in every form of 'haben' (to have).",
                    Explanation = "Only 'du' and 'er/sie/es' shorten the stem (hast/hat) - the rest just add the normal endings.",
                    BaseWord = "haben",
                    Slots =
                    [
                        new ConjugationSlot("ich", "habe"),
                        new ConjugationSlot("du", "hast"),
                        new ConjugationSlot("er/sie/es", "hat"),
                        new ConjugationSlot("wir", "haben"),
                        new ConjugationSlot("ihr", "habt"),
                        new ConjugationSlot("sie/Sie", "haben")
                    ]
                },
                new ClozeExercise
                {
                    Id = "a1-u1-sein-haben-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ich' always takes 'bin' with sein.",
                    TextBefore = "Ich ",
                    TextAfter = " dreißig Jahre alt.",
                    CorrectAnswer = "bin"
                },
                new ClozeExercise
                {
                    Id = "a1-u1-sein-haben-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'wir' always takes 'haben' with haben.",
                    TextBefore = "Wir ",
                    TextAfter = " zwei Kinder.",
                    CorrectAnswer = "haben"
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u1-sein-haben-e5",
                    Instruction = "Choose the correct form.",
                    Explanation = "'du' + sein = 'du bist'.",
                    Question = "How do you say \"you are\" (du, informal)?",
                    Options = ["du bist", "du bin", "du ist", "du sind"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u1-sein-haben-e6",
                    Instruction = "Choose the correct form.",
                    Explanation = "The formal 'Sie' takes 'sind', same form as 'wir' and 'sie' (plural).",
                    Question = "___ Sie Frau Müller?",
                    Options = ["Sind", "Bist", "Ist", "Habt"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u2-praesens",
            Level = CefrLevel.A1,
            SortOrder = 2,
            Title = "Präsens: regelmäßige Verben",
            Description = "Present tense of regular verbs like wohnen and kommen.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "a1-u2-praesens-e1",
                    Instruction = "Fill in every form of 'wohnen' (to live).",
                    Explanation = "Regular verbs just add -e/-st/-t/-en/-t/-en to the stem 'wohn-'.",
                    BaseWord = "wohnen",
                    Slots =
                    [
                        new ConjugationSlot("ich", "wohne"),
                        new ConjugationSlot("du", "wohnst"),
                        new ConjugationSlot("er/sie/es", "wohnt"),
                        new ConjugationSlot("wir", "wohnen"),
                        new ConjugationSlot("ihr", "wohnt"),
                        new ConjugationSlot("sie/Sie", "wohnen")
                    ]
                },
                new ConjugationDrillExercise
                {
                    Id = "a1-u2-praesens-e2",
                    Instruction = "Fill in every form of 'kommen' (to come).",
                    Explanation = "Same regular pattern as 'wohnen': stem 'komm-' plus the usual endings.",
                    BaseWord = "kommen",
                    Slots =
                    [
                        new ConjugationSlot("ich", "komme"),
                        new ConjugationSlot("du", "kommst"),
                        new ConjugationSlot("er/sie/es", "kommt"),
                        new ConjugationSlot("wir", "kommen"),
                        new ConjugationSlot("ihr", "kommt"),
                        new ConjugationSlot("sie/Sie", "kommen")
                    ]
                },
                new ClozeExercise
                {
                    Id = "a1-u2-praesens-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'er' takes the -t ending: er kommt.",
                    TextBefore = "Er ",
                    TextAfter = " aus Deutschland.",
                    CorrectAnswer = "kommt"
                },
                new ClozeExercise
                {
                    Id = "a1-u2-praesens-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ich' takes the -e ending: ich wohne.",
                    TextBefore = "Ich ",
                    TextAfter = " in Berlin.",
                    CorrectAnswer = "wohne"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u3-artikel",
            Level = CefrLevel.A1,
            SortOrder = 3,
            Title = "Artikel: der, die, das",
            Description = "Nominative-case definite articles - the gender of German nouns.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a1-u3-artikel-e1",
                    Instruction = "Choose the correct article.",
                    Explanation = "'Mann' is masculine: der Mann.",
                    Question = "___ Mann",
                    Options = ["der", "die", "das"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u3-artikel-e2",
                    Instruction = "Choose the correct article.",
                    Explanation = "'Frau' is feminine: die Frau.",
                    Question = "___ Frau",
                    Options = ["der", "die", "das"],
                    CorrectOptionIndex = 1
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u3-artikel-e3",
                    Instruction = "Choose the correct article.",
                    Explanation = "'Kind' is neuter: das Kind.",
                    Question = "___ Kind",
                    Options = ["der", "die", "das"],
                    CorrectOptionIndex = 2
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u3-artikel-e4",
                    Instruction = "Choose the correct article.",
                    Explanation = "'Buch' is neuter: das Buch - gender often just has to be learned with the noun.",
                    Question = "___ Buch",
                    Options = ["der", "die", "das"],
                    CorrectOptionIndex = 2
                },
                new ClozeExercise
                {
                    Id = "a1-u3-artikel-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Tisch' is masculine: der Tisch.",
                    TextBefore = "",
                    TextAfter = " Tisch ist groß.",
                    CorrectAnswer = "Der"
                },
                new ClozeExercise
                {
                    Id = "a1-u3-artikel-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Lampe' is feminine: die Lampe.",
                    TextBefore = "",
                    TextAfter = " Lampe ist neu.",
                    CorrectAnswer = "Die"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u4-verneinung",
            Level = CefrLevel.A1,
            SortOrder = 4,
            Title = "Verneinung: nicht und kein",
            Description = "Negating verbs/adjectives with 'nicht' and nouns with 'kein'.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "a1-u4-verneinung-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "'nicht' negates a whole statement like 'mein Auto' here.",
                    TextBefore = "Das ist ",
                    TextAfter = " mein Auto.",
                    CorrectAnswer = "nicht"
                },
                new ClozeExercise
                {
                    Id = "a1-u4-verneinung-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'kein' negates a noun; 'Geschwister' is plural, so it takes the plural form 'keine'.",
                    TextBefore = "Ich habe ",
                    TextAfter = " Geschwister.",
                    CorrectAnswer = "keine"
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u4-verneinung-e3",
                    Instruction = "Choose the correct word.",
                    Explanation = "'müde' is an adjective, so it's negated with 'nicht', not 'kein'.",
                    Question = "Er ist ___ müde.",
                    Options = ["nicht", "kein", "keine", "keinen"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u4-verneinung-e4",
                    Instruction = "Choose the correct word.",
                    Explanation = "'Auto' is a neuter noun, so 'kein' stays unchanged: kein Auto.",
                    Question = "Wir haben ___ Auto.",
                    Options = ["kein", "keine", "keinen", "nicht"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u5-wortstellung",
            Level = CefrLevel.A1,
            SortOrder = 5,
            Title = "Fragen und Wortstellung",
            Description = "Question words and the German verb-second word order.",
            Exercises =
            [
                new SentenceOrderExercise
                {
                    Id = "a1-u5-wortstellung-e1",
                    Instruction = "Put the words in the correct order.",
                    Explanation = "Question words (W-words) come first, then the verb, then the subject.",
                    ScrambledWords = ["kommen", "Woher", "Sie", "?"],
                    CorrectOrder = ["Woher", "kommen", "Sie", "?"]
                },
                new SentenceOrderExercise
                {
                    Id = "a1-u5-wortstellung-e2",
                    Instruction = "Put the words in the correct order.",
                    Explanation = "In a plain statement, the verb is always the second element: Ich (1) arbeite (2) heute.",
                    ScrambledWords = ["heute", "Ich", "arbeite"],
                    CorrectOrder = ["Ich", "arbeite", "heute"]
                },
                new SentenceOrderExercise
                {
                    Id = "a1-u5-wortstellung-e3",
                    Instruction = "Put the words in the correct order.",
                    Explanation = "Yes/no questions put the verb first, then the subject: Wohnst (verb) du (subject)...",
                    ScrambledWords = ["in", "Wohnst", "Berlin", "du", "?"],
                    CorrectOrder = ["Wohnst", "du", "in", "Berlin", "?"]
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u5-wortstellung-e4",
                    Instruction = "Which sentence has the correct word order?",
                    Explanation = "\"Ich arbeite heute\" keeps the verb in second position - the one rule that matters most in German statements.",
                    Question = "Which is correct?",
                    Options = ["Ich heute arbeite.", "Heute ich arbeite.", "Ich arbeite heute.", "Arbeite ich heute."],
                    CorrectOptionIndex = 2
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u6-modalverben",
            Level = CefrLevel.A1,
            SortOrder = 6,
            Title = "Modalverben: können, möchten",
            Description = "The most common modal verbs for ability and polite requests.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "a1-u6-modalverben-e1",
                    Instruction = "Fill in every form of 'können' (to be able to/can).",
                    Explanation = "Modal verbs change their stem vowel in the singular (kann) but stay 'könn-' in the plural.",
                    BaseWord = "können",
                    Slots =
                    [
                        new ConjugationSlot("ich", "kann"),
                        new ConjugationSlot("du", "kannst"),
                        new ConjugationSlot("er/sie/es", "kann"),
                        new ConjugationSlot("wir", "können"),
                        new ConjugationSlot("ihr", "könnt"),
                        new ConjugationSlot("sie/Sie", "können")
                    ]
                },
                new ConjugationDrillExercise
                {
                    Id = "a1-u6-modalverben-e2",
                    Instruction = "Fill in every form of 'möchten' (would like to).",
                    Explanation = "'möchten' is the polite way to say 'want' - useful for ordering food or asking for things.",
                    BaseWord = "möchten",
                    Slots =
                    [
                        new ConjugationSlot("ich", "möchte"),
                        new ConjugationSlot("du", "möchtest"),
                        new ConjugationSlot("er/sie/es", "möchte"),
                        new ConjugationSlot("wir", "möchten"),
                        new ConjugationSlot("ihr", "möchtet"),
                        new ConjugationSlot("sie/Sie", "möchten")
                    ]
                },
                new ClozeExercise
                {
                    Id = "a1-u6-modalverben-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ich' + können = 'ich kann'.",
                    TextBefore = "Ich ",
                    TextAfter = " gut Deutsch sprechen.",
                    CorrectAnswer = "kann"
                },
                new ClozeExercise
                {
                    Id = "a1-u6-modalverben-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'du' + können = 'du kannst' - used here to politely ask for help.",
                    TextBefore = "",
                    TextAfter = " du mir helfen?",
                    CorrectAnswer = "Kannst"
                },
                new ClozeExercise
                {
                    Id = "a1-u6-modalverben-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "'wir' + möchten = 'wir möchten' - a polite way to order.",
                    TextBefore = "Wir ",
                    TextAfter = " einen Kaffee, bitte.",
                    CorrectAnswer = "möchten"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u7-akkusativ",
            Level = CefrLevel.A1,
            SortOrder = 7,
            Title = "Akkusativ",
            Description = "The accusative case - only masculine articles change.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a1-u7-akkusativ-e1",
                    Instruction = "Choose the correct accusative article.",
                    Explanation = "Masculine 'der' becomes 'den' in the accusative: den Mann.",
                    Question = "Ich sehe ___ Mann.",
                    Options = ["den", "die", "das"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u7-akkusativ-e2",
                    Instruction = "Choose the correct accusative article.",
                    Explanation = "Neuter 'das' never changes in the accusative.",
                    Question = "Ich kaufe ___ Buch.",
                    Options = ["den", "die", "das"],
                    CorrectOptionIndex = 2
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u7-akkusativ-e3",
                    Instruction = "Choose the correct accusative article.",
                    Explanation = "Indefinite masculine 'ein' becomes 'einen' in the accusative.",
                    Question = "Ich habe ___ Bruder.",
                    Options = ["einen", "eine", "ein"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a1-u7-akkusativ-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Kaffee' is masculine, so indefinite 'ein' becomes 'einen'.",
                    TextBefore = "Er trinkt ",
                    TextAfter = " Kaffee.",
                    CorrectAnswer = "einen"
                },
                new ClozeExercise
                {
                    Id = "a1-u7-akkusativ-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Auto' is neuter, so indefinite 'ein' stays the same in the accusative.",
                    TextBefore = "Wir brauchen ",
                    TextAfter = " Auto.",
                    CorrectAnswer = "ein"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u8-zahlen-zeit",
            Level = CefrLevel.A1,
            SortOrder = 8,
            Title = "Zahlen, Uhrzeit und Datum",
            Description = "Numbers, telling time, days of the week, and months.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a1-u8-zahlen-zeit-e1",
                    Instruction = "Choose the correct number.",
                    Explanation = "15 = fünfzehn (not to be confused with fünfzig = 50).",
                    Question = "How do you say 15?",
                    Options = ["fünfzehn", "fünfzig", "fünf", "sechzehn"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u8-zahlen-zeit-e2",
                    Instruction = "Choose the correct time.",
                    Explanation = "\"Es ist neun Uhr\" is how you say a time on the hour.",
                    Question = "What time is it? (9:00)",
                    Options = ["Es ist neun Uhr.", "Es ist neunzehn Uhr.", "Es ist neun Minuten.", "Es ist neun Tage."],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u8-zahlen-zeit-e3",
                    Instruction = "Choose the correct day.",
                    Explanation = "The week runs Montag, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag.",
                    Question = "Which day comes after Montag?",
                    Options = ["Dienstag", "Mittwoch", "Sonntag", "Freitag"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u8-zahlen-zeit-e4",
                    Instruction = "Choose the correct month.",
                    Explanation = "The year runs Januar, Februar, März, and so on.",
                    Question = "Which month comes after Januar?",
                    Options = ["Februar", "März", "Dezember", "April"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u9-familie-alltag",
            Level = CefrLevel.A1,
            SortOrder = 9,
            Title = "Familie und Alltag",
            Description = "Everyday vocabulary: family members and daily life.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a1-u9-familie-alltag-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Bruder' means brother.",
                    Question = "\"brother\" in German?",
                    Options = ["Bruder", "Schwester", "Vater", "Onkel"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u9-familie-alltag-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Mutter' means mother.",
                    Question = "\"mother\" in German?",
                    Options = ["Mutter", "Tante", "Oma", "Schwester"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u9-familie-alltag-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'essen' means to eat.",
                    Question = "\"to eat\" in German?",
                    Options = ["essen", "trinken", "schlafen", "arbeiten"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u9-familie-alltag-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Brot' means bread.",
                    Question = "\"bread\" in German?",
                    Options = ["Brot", "Wasser", "Milch", "Käse"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a1-u10-plural",
            Level = CefrLevel.A1,
            SortOrder = 10,
            Title = "Plural",
            Description = "Plural forms of common nouns.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a1-u10-plural-e1",
                    Instruction = "Choose the correct plural.",
                    Explanation = "'das Kind' becomes 'die Kinder' - plural nouns always take 'die'.",
                    Question = "Plural of 'das Kind'?",
                    Options = ["die Kinder", "die Kindes", "die Kinden", "das Kinder"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a1-u10-plural-e2",
                    Instruction = "Choose the correct plural.",
                    Explanation = "'der Tisch' becomes 'die Tische'.",
                    Question = "Plural of 'der Tisch'?",
                    Options = ["die Tische", "die Tischer", "die Tischen", "der Tische"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a1-u10-plural-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Buch' has an irregular plural: Bücher (note the umlaut).",
                    TextBefore = "Ich habe drei ",
                    TextAfter = ".",
                    CorrectAnswer = "Bücher"
                },
                new ClozeExercise
                {
                    Id = "a1-u10-plural-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Kind' becomes 'Kinder' in the plural.",
                    TextBefore = "Wir haben zwei ",
                    TextAfter = ".",
                    CorrectAnswer = "Kinder"
                }
            ]
        }
    ];
}
