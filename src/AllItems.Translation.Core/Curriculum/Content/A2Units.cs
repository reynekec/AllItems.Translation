namespace AllItems.Translation.Core.Curriculum.Content;

/// <summary>
/// A2 units building on A1: Perfekt (haben/sein), Präteritum of sein/haben/modals, dative case,
/// separable verbs, comparative/superlative, subordinate clauses (weil/dass/wenn), reflexive
/// verbs, and everyday vocabulary for health and directions.
/// </summary>
public static class A2Units
{
    public static IReadOnlyList<CurriculumUnit> All { get; } =
    [
        new CurriculumUnit
        {
            Id = "a2-u1-perfekt-haben",
            Level = CefrLevel.A2,
            SortOrder = 1,
            Title = "Perfekt mit haben",
            Description = "The past tense used for conversation - most verbs form it with haben.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "a2-u1-perfekt-haben-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "'sehen' has an irregular participle: gesehen.",
                    TextBefore = "Ich habe einen Film ",
                    TextAfter = ".",
                    CorrectAnswer = "gesehen"
                },
                new ClozeExercise
                {
                    Id = "a2-u1-perfekt-haben-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'essen' has an irregular participle: gegessen.",
                    TextBefore = "Wir haben Pizza ",
                    TextAfter = ".",
                    CorrectAnswer = "gegessen"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u1-perfekt-haben-e3",
                    Instruction = "Choose the correct participle.",
                    Explanation = "Regular verbs form the participle as ge- + stem + -t: spielen -> gespielt.",
                    Question = "Perfekt participle of 'spielen'?",
                    Options = ["gespielt", "gespielen", "spielt", "gespielte"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u1-perfekt-haben-e4",
                    Instruction = "Choose the correct participle.",
                    Explanation = "kaufen -> gekauft, same regular pattern.",
                    Question = "Perfekt participle of 'kaufen'?",
                    Options = ["gekauft", "gekauff", "kauft", "gekaufen"],
                    CorrectOptionIndex = 0
                },
                new ConjugationDrillExercise
                {
                    Id = "a2-u1-perfekt-haben-e5",
                    Instruction = "Fill in every form of 'machen' in the Perfekt.",
                    Explanation = "Only 'haben' conjugates - the participle 'gemacht' never changes.",
                    BaseWord = "machen (Perfekt)",
                    Slots =
                    [
                        new ConjugationSlot("ich", "habe gemacht"),
                        new ConjugationSlot("du", "hast gemacht"),
                        new ConjugationSlot("er/sie/es", "hat gemacht"),
                        new ConjugationSlot("wir", "haben gemacht"),
                        new ConjugationSlot("ihr", "habt gemacht"),
                        new ConjugationSlot("sie/Sie", "haben gemacht")
                    ]
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u2-perfekt-sein",
            Level = CefrLevel.A2,
            SortOrder = 2,
            Title = "Perfekt mit sein",
            Description = "Verbs of motion or change of state form the Perfekt with sein instead of haben.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "a2-u2-perfekt-sein-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "'gehen' is a motion verb, so it takes 'sein': ich bin gegangen.",
                    TextBefore = "Ich bin nach Hause ",
                    TextAfter = ".",
                    CorrectAnswer = "gegangen"
                },
                new ClozeExercise
                {
                    Id = "a2-u2-perfekt-sein-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'fahren' (to travel/drive) also takes 'sein'.",
                    TextBefore = "Er ist mit dem Auto ",
                    TextAfter = ".",
                    CorrectAnswer = "gefahren"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u2-perfekt-sein-e3",
                    Instruction = "Choose the correct auxiliary verb.",
                    Explanation = "'gehen' describes movement from one place to another, so it uses 'sein'.",
                    Question = "Which auxiliary verb does 'gehen' use in the Perfekt?",
                    Options = ["sein", "haben"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u2-perfekt-sein-e4",
                    Instruction = "Choose the correct form.",
                    Explanation = "kommen -> ist gekommen.",
                    Question = "Perfekt of 'kommen'?",
                    Options = ["ist gekommen", "hat gekommen", "ist gekommt", "hat gekommt"],
                    CorrectOptionIndex = 0
                },
                new ConjugationDrillExercise
                {
                    Id = "a2-u2-perfekt-sein-e5",
                    Instruction = "Fill in every form of 'gehen' in the Perfekt.",
                    Explanation = "'sein' conjugates normally here, while 'gegangen' stays fixed.",
                    BaseWord = "gehen (Perfekt)",
                    Slots =
                    [
                        new ConjugationSlot("ich", "bin gegangen"),
                        new ConjugationSlot("du", "bist gegangen"),
                        new ConjugationSlot("er/sie/es", "ist gegangen"),
                        new ConjugationSlot("wir", "sind gegangen"),
                        new ConjugationSlot("ihr", "seid gegangen"),
                        new ConjugationSlot("sie/Sie", "sind gegangen")
                    ]
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u3-praeteritum",
            Level = CefrLevel.A2,
            SortOrder = 3,
            Title = "Präteritum: sein, haben, Modalverben",
            Description = "The simple past of sein, haben, and modal verbs - used even in spoken German.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "a2-u3-praeteritum-e1",
                    Instruction = "Fill in every Präteritum form of 'sein'.",
                    Explanation = "Unlike the present tense, 'war' looks nothing like the infinitive - it just has to be memorized.",
                    BaseWord = "sein (Präteritum)",
                    Slots =
                    [
                        new ConjugationSlot("ich", "war"),
                        new ConjugationSlot("du", "warst"),
                        new ConjugationSlot("er/sie/es", "war"),
                        new ConjugationSlot("wir", "waren"),
                        new ConjugationSlot("ihr", "wart"),
                        new ConjugationSlot("sie/Sie", "waren")
                    ]
                },
                new ConjugationDrillExercise
                {
                    Id = "a2-u3-praeteritum-e2",
                    Instruction = "Fill in every Präteritum form of 'haben'.",
                    Explanation = "'hatte' follows a regular pattern once you know the stem.",
                    BaseWord = "haben (Präteritum)",
                    Slots =
                    [
                        new ConjugationSlot("ich", "hatte"),
                        new ConjugationSlot("du", "hattest"),
                        new ConjugationSlot("er/sie/es", "hatte"),
                        new ConjugationSlot("wir", "hatten"),
                        new ConjugationSlot("ihr", "hattet"),
                        new ConjugationSlot("sie/Sie", "hatten")
                    ]
                },
                new ClozeExercise
                {
                    Id = "a2-u3-praeteritum-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ich war' = 'I was'.",
                    TextBefore = "Ich ",
                    TextAfter = " letztes Jahr in Italien.",
                    CorrectAnswer = "war"
                },
                new ClozeExercise
                {
                    Id = "a2-u3-praeteritum-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'wir hatten' = 'we had'.",
                    TextBefore = "Wir ",
                    TextAfter = " keine Zeit.",
                    CorrectAnswer = "hatten"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u3-praeteritum-e5",
                    Instruction = "Choose the correct form.",
                    Explanation = "können -> konnte in the Präteritum (note the dropped umlaut).",
                    Question = "Präteritum of 'können' (ich)?",
                    Options = ["konnte", "kannte", "kann", "gekonnt"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u3-praeteritum-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "wollen -> wollte in the Präteritum.",
                    TextBefore = "Er ",
                    TextAfter = " nicht kommen.",
                    CorrectAnswer = "wollte"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u4-dativ",
            Level = CefrLevel.A2,
            SortOrder = 4,
            Title = "Dativ",
            Description = "The dative case - used for indirect objects and after certain prepositions.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u4-dativ-e1",
                    Instruction = "Choose the correct dative article.",
                    Explanation = "Masculine 'der' becomes 'dem' in the dative.",
                    Question = "Dative of 'der Mann'?",
                    Options = ["dem Mann", "den Mann", "des Mannes", "der Mann"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u4-dativ-e2",
                    Instruction = "Choose the correct dative article.",
                    Explanation = "Feminine 'die' becomes 'der' in the dative.",
                    Question = "Dative of 'die Frau'?",
                    Options = ["der Frau", "die Frau", "den Frau", "dem Frau"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u4-dativ-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'geben' takes a dative indirect object: dem Mann.",
                    TextBefore = "Ich gebe ",
                    TextAfter = " Mann das Buch.",
                    CorrectAnswer = "dem"
                },
                new ClozeExercise
                {
                    Id = "a2-u4-dativ-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'helfen' always takes the dative: der Frau.",
                    TextBefore = "Ich helfe ",
                    TextAfter = " Frau.",
                    CorrectAnswer = "der"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u4-dativ-e5",
                    Instruction = "Choose the correct pronoun.",
                    Explanation = "'mir' is the dative form of 'ich'.",
                    Question = "\"to me\" in German (dative)?",
                    Options = ["mir", "mich", "ich", "mein"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u4-dativ-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "'helfen' + dative pronoun: mir.",
                    TextBefore = "Kannst du ",
                    TextAfter = " helfen?",
                    CorrectAnswer = "mir"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u5-trennbare-verben",
            Level = CefrLevel.A2,
            SortOrder = 5,
            Title = "Trennbare Verben",
            Description = "Separable verbs - the prefix detaches and moves to the end of the clause.",
            Exercises =
            [
                new SentenceOrderExercise
                {
                    Id = "a2-u5-trennbare-verben-e1",
                    Instruction = "Put the words in the correct order.",
                    Explanation = "'aufstehen' splits into 'stehe' (2nd position) and 'auf' (end of clause).",
                    ScrambledWords = ["auf", "Ich", "stehe", "um", "sieben", "Uhr"],
                    CorrectOrder = ["Ich", "stehe", "um", "sieben", "Uhr", "auf"]
                },
                new SentenceOrderExercise
                {
                    Id = "a2-u5-trennbare-verben-e2",
                    Instruction = "Put the words in the correct order.",
                    Explanation = "'einkaufen' splits the same way: 'kaufen' becomes 'kaufe...ein'.",
                    ScrambledWords = ["ein", "Wir", "heute", "kaufen"],
                    CorrectOrder = ["Wir", "kaufen", "heute", "ein"]
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u5-trennbare-verben-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "The separable prefix always moves to the very end of a main clause.",
                    Question = "Where does the separable prefix go in a main clause?",
                    Options = ["At the end", "Right after the verb", "At the beginning", "It doesn't move"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u5-trennbare-verben-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'anrufen' -> ich rufe ... an.",
                    TextBefore = "Ich rufe dich morgen ",
                    TextAfter = ".",
                    CorrectAnswer = "an"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u5-trennbare-verben-e5",
                    Instruction = "Choose the correct sentence.",
                    Explanation = "\"Ich kaufe ein\" correctly separates the prefix 'ein' to the end.",
                    Question = "\"I shop/buy\" (einkaufen, ich)?",
                    Options = ["Ich kaufe ein.", "Ich einkaufe.", "Ich kaufe.", "Ich ein kaufe."],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u6-komparativ-superlativ",
            Level = CefrLevel.A2,
            SortOrder = 6,
            Title = "Komparativ und Superlativ",
            Description = "Comparing things: -er and am -sten.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u6-komparativ-superlativ-e1",
                    Instruction = "Choose the correct comparative.",
                    Explanation = "Regular comparatives just add -er: schnell -> schneller.",
                    Question = "Comparative of 'schnell'?",
                    Options = ["schneller", "schnellst", "mehr schnell", "schnellsten"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u6-komparativ-superlativ-e2",
                    Instruction = "Choose the correct superlative.",
                    Explanation = "'gut' is irregular: gut -> besser -> am besten.",
                    Question = "Superlative of 'gut' (am ___)?",
                    Options = ["besten", "gutesten", "guter", "besser"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u6-komparativ-superlativ-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"schneller als\" = \"faster than\".",
                    TextBefore = "Der Zug ist ",
                    TextAfter = " als das Auto.",
                    CorrectAnswer = "schneller"
                },
                new ClozeExercise
                {
                    Id = "a2-u6-komparativ-superlativ-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"am besten\" = \"the best\" (superlative with 'am').",
                    TextBefore = "Dieses Restaurant ist am ",
                    TextAfter = ".",
                    CorrectAnswer = "besten"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u6-komparativ-superlativ-e5",
                    Instruction = "Choose the correct comparative.",
                    Explanation = "'gern' is irregular: gern -> lieber -> am liebsten.",
                    Question = "Comparative of 'gern'?",
                    Options = ["lieber", "gerner", "mehr gern", "liebst"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u7-nebensaetze",
            Level = CefrLevel.A2,
            SortOrder = 7,
            Title = "Nebensätze: weil, dass, wenn",
            Description = "Subordinate clauses - the conjugated verb moves to the very end.",
            Exercises =
            [
                new SentenceOrderExercise
                {
                    Id = "a2-u7-nebensaetze-e1",
                    Instruction = "Put the words in the correct order (after the comma).",
                    Explanation = "In a 'weil' clause the verb ('bin') goes to the end, not straight after 'ich'.",
                    ScrambledWords = ["ich", "bin", "weil", "krank"],
                    CorrectOrder = ["weil", "ich", "krank", "bin"]
                },
                new SentenceOrderExercise
                {
                    Id = "a2-u7-nebensaetze-e2",
                    Instruction = "Put the words in the correct order (after the comma).",
                    Explanation = "Same rule with 'dass': the verb 'kommt' moves to the end.",
                    ScrambledWords = ["kommt", "dass", "er"],
                    CorrectOrder = ["dass", "er", "kommt"]
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u7-nebensaetze-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "weil/dass/wenn all push the conjugated verb to the end of their clause.",
                    Question = "In a weil/dass/wenn clause, where does the conjugated verb go?",
                    Options = ["At the end", "Second position", "First position", "It stays the same"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u7-nebensaetze-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'weil' introduces a reason.",
                    TextBefore = "Ich bleibe zu Hause, ",
                    TextAfter = " ich krank bin.",
                    CorrectAnswer = "weil"
                },
                new ClozeExercise
                {
                    Id = "a2-u7-nebensaetze-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "'dass' introduces a reported fact or belief.",
                    TextBefore = "Ich hoffe, ",
                    TextAfter = " du kommst.",
                    CorrectAnswer = "dass"
                },
                new ClozeExercise
                {
                    Id = "a2-u7-nebensaetze-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "'wenn' introduces a condition ('if/when').",
                    TextBefore = "",
                    TextAfter = " es regnet, bleiben wir zu Hause.",
                    CorrectAnswer = "Wenn"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u8-reflexivverben",
            Level = CefrLevel.A2,
            SortOrder = 8,
            Title = "Reflexivverben",
            Description = "Reflexive verbs - the action refers back to the subject.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "a2-u8-reflexivverben-e1",
                    Instruction = "Fill in every form of 'sich freuen' (to be happy about).",
                    Explanation = "The reflexive pronoun changes with the subject: mich, dich, sich, uns, euch, sich.",
                    BaseWord = "sich freuen",
                    Slots =
                    [
                        new ConjugationSlot("ich", "freue mich"),
                        new ConjugationSlot("du", "freust dich"),
                        new ConjugationSlot("er/sie/es", "freut sich"),
                        new ConjugationSlot("wir", "freuen uns"),
                        new ConjugationSlot("ihr", "freut euch"),
                        new ConjugationSlot("sie/Sie", "freuen sich")
                    ]
                },
                new ClozeExercise
                {
                    Id = "a2-u8-reflexivverben-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'ich freue mich' - the reflexive pronoun for 'ich' is 'mich'.",
                    TextBefore = "Ich freue ",
                    TextAfter = " auf die Ferien.",
                    CorrectAnswer = "mich"
                },
                new ClozeExercise
                {
                    Id = "a2-u8-reflexivverben-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'er wäscht sich' - the reflexive pronoun for 'er' is 'sich'.",
                    TextBefore = "Er wäscht ",
                    TextAfter = ".",
                    CorrectAnswer = "sich"
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u8-reflexivverben-e4",
                    Instruction = "Choose the correct pronoun.",
                    Explanation = "'wir' takes 'uns' as its reflexive pronoun.",
                    Question = "Reflexive pronoun for 'wir'?",
                    Options = ["uns", "euch", "sich", "mich"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u9-gesundheit",
            Level = CefrLevel.A2,
            SortOrder = 9,
            Title = "Gesundheit und Körper",
            Description = "Everyday vocabulary for talking about health and the body.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u9-gesundheit-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Kopf' means head.",
                    Question = "\"head\" in German?",
                    Options = ["Kopf", "Bauch", "Hand", "Fuß"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u9-gesundheit-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'krank' means sick/ill.",
                    Question = "\"sick/ill\" in German?",
                    Options = ["krank", "gesund", "müde", "glücklich"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u9-gesundheit-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Arzt' means doctor.",
                    Question = "\"doctor\" in German?",
                    Options = ["Arzt", "Lehrer", "Polizist", "Koch"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u9-gesundheit-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"weh tun\" = \"to hurt\": Mein Kopf tut weh.",
                    TextBefore = "Mein ",
                    TextAfter = " tut weh.",
                    CorrectAnswer = "Kopf"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u10-wegbeschreibung",
            Level = CefrLevel.A2,
            SortOrder = 10,
            Title = "Wegbeschreibung",
            Description = "Giving and understanding directions.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u10-wegbeschreibung-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'geradeaus' means straight ahead.",
                    Question = "\"straight ahead\" in German?",
                    Options = ["geradeaus", "links", "rechts", "zurück"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u10-wegbeschreibung-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'links' means left.",
                    Question = "\"left\" in German?",
                    Options = ["links", "rechts", "geradeaus", "oben"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "a2-u10-wegbeschreibung-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Gehen Sie geradeaus\" = \"Go straight ahead\".",
                    TextBefore = "Gehen Sie ",
                    TextAfter = " und dann rechts.",
                    CorrectAnswer = "geradeaus"
                },
                new ClozeExercise
                {
                    Id = "a2-u10-wegbeschreibung-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'Ampel' means traffic light - a common landmark in directions.",
                    TextBefore = "An der ",
                    TextAfter = " biegen Sie links ab.",
                    CorrectAnswer = "Ampel"
                }
            ]
        }
    ];
}
