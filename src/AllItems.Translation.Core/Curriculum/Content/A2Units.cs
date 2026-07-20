using AllItems.Translation.Core.Domain;

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
        },
        new CurriculumUnit
        {
            Id = "a2-u11-berufe",
            Level = CefrLevel.A2,
            SortOrder = 11,
            Title = "Berufe",
            Description = "Vocabulary: professions.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u11-berufe-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Lehrer' means teacher (male); 'die Lehrerin' for female.",
                    Question = "\"teacher\" in German?",
                    Options = ["der Lehrer", "der Ingenieur", "der Anwalt", "der Bauer"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Lehrer", "teacher", Article: "der", ExampleSentence: "Der Lehrer erklärt viel.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u11-berufe-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Ingenieur' means engineer.",
                    Question = "\"engineer\" in German?",
                    Options = ["der Ingenieur", "der Verkäufer", "der Lehrer", "der Bauer"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Ingenieur", "engineer", Article: "der", ExampleSentence: "Der Ingenieur baut Brücken.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u11-berufe-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Verkäufer' means salesperson.",
                    Question = "\"salesperson\" in German?",
                    Options = ["der Verkäufer", "der Anwalt", "der Ingenieur", "der Koch"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Verkäufer", "salesperson", Article: "der", ExampleSentence: "Der Verkäufer berät Kunden.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u11-berufe-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Anwalt' means lawyer.",
                    Question = "\"lawyer\" in German?",
                    Options = ["der Anwalt", "der Bauer", "der Lehrer", "der Verkäufer"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Anwalt", "lawyer", Article: "der", ExampleSentence: "Der Anwalt liest Verträge.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u11-berufe-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Bauer' means farmer.",
                    Question = "\"farmer\" in German?",
                    Options = ["der Bauer", "der Ingenieur", "der Anwalt", "der Koch"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Bauer", "farmer", Article: "der", ExampleSentence: "Der Bauer pflanzt Kartoffeln.", Highlights: [])
                },
                new ClozeExercise
                {
                    Id = "a2-u11-berufe-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Er ist Lehrer von Beruf\" = \"He is a teacher by profession\".",
                    TextBefore = "Er ist ",
                    TextAfter = " von Beruf.",
                    CorrectAnswer = "Lehrer"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u12-wetter",
            Level = CefrLevel.A2,
            SortOrder = 12,
            Title = "Wetter",
            Description = "Vocabulary: weather.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u12-wetter-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Sonne' means sun.",
                    Question = "\"sun\" in German?",
                    Options = ["die Sonne", "der Regen", "der Schnee", "der Wind"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Sonne", "sun", Article: "die", ExampleSentence: "Die Sonne scheint hell.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u12-wetter-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Regen' means rain.",
                    Question = "\"rain\" in German?",
                    Options = ["der Regen", "der Schnee", "die Sonne", "die Wolke"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Regen", "rain", Article: "der", ExampleSentence: "Der Regen fällt stark.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u12-wetter-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Schnee' means snow.",
                    Question = "\"snow\" in German?",
                    Options = ["der Schnee", "der Regen", "der Wind", "die Wolke"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Schnee", "snow", Article: "der", ExampleSentence: "Der Schnee fällt leise.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u12-wetter-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Wind' means wind.",
                    Question = "\"wind\" in German?",
                    Options = ["der Wind", "die Wolke", "der Regen", "die Sonne"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Wind", "wind", Article: "der", ExampleSentence: "Der Wind weht stark.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u12-wetter-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Wolke' means cloud.",
                    Question = "\"cloud\" in German?",
                    Options = ["die Wolke", "der Wind", "der Schnee", "die Sonne"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Wolke", "cloud", Article: "die", ExampleSentence: "Die Wolke ist grau.", Highlights: [])
                },
                new ClozeExercise
                {
                    Id = "a2-u12-wetter-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Die Sonne scheint\" = \"The sun is shining\".",
                    TextBefore = "Die ",
                    TextAfter = " scheint.",
                    CorrectAnswer = "Sonne"
                },
                new ClozeExercise
                {
                    Id = "a2-u12-wetter-e7",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Es schneit\" uses 'Schnee' as its root word; here 'Schnee' fills a descriptive slot: viel Schnee.",
                    TextBefore = "Im Winter gibt es viel ",
                    TextAfter = ".",
                    CorrectAnswer = "Schnee"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u13-freizeit",
            Level = CefrLevel.A2,
            SortOrder = 13,
            Title = "Freizeit",
            Description = "Vocabulary: hobbies and leisure activities.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u13-freizeit-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'schwimmen' means to swim.",
                    Question = "\"to swim\" in German?",
                    Options = ["schwimmen", "wandern", "malen", "tanzen"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("schwimmen", "to swim", Article: null, ExampleSentence: "Wir schwimmen gern zusammen.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u13-freizeit-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'wandern' means to hike.",
                    Question = "\"to hike\" in German?",
                    Options = ["wandern", "schwimmen", "tanzen", "fotografieren"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("wandern", "to hike", Article: null, ExampleSentence: "Wir wandern gern zusammen.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u13-freizeit-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'malen' means to paint.",
                    Question = "\"to paint\" in German?",
                    Options = ["malen", "tanzen", "wandern", "schwimmen"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("malen", "to paint", Article: null, ExampleSentence: "Sie malt ein Bild.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u13-freizeit-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'tanzen' means to dance.",
                    Question = "\"to dance\" in German?",
                    Options = ["tanzen", "malen", "fotografieren", "wandern"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("tanzen", "to dance", Article: null, ExampleSentence: "Sie tanzen jeden Abend.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u13-freizeit-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Hobby' means hobby.",
                    Question = "\"hobby\" in German?",
                    Options = ["das Hobby", "die Freizeit", "der Sport", "das Spiel"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Hobby", "hobby", Article: "das", ExampleSentence: "Mein Hobby ist Lesen.", Highlights: [])
                },
                new ClozeExercise
                {
                    Id = "a2-u13-freizeit-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich gehe gern schwimmen\" = \"I like to go swimming\".",
                    TextBefore = "Ich gehe gern ",
                    TextAfter = ".",
                    CorrectAnswer = "schwimmen"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u14-verkehrsmittel",
            Level = CefrLevel.A2,
            SortOrder = 14,
            Title = "Verkehrsmittel",
            Description = "Vocabulary: modes of transportation.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u14-verkehrsmittel-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Zug' means train.",
                    Question = "\"train\" in German?",
                    Options = ["der Zug", "der Bus", "das Fahrrad", "das Schiff"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Zug", "train", Article: "der", ExampleSentence: "Der Zug kommt pünktlich.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u14-verkehrsmittel-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Fahrrad' means bicycle.",
                    Question = "\"bicycle\" in German?",
                    Options = ["das Fahrrad", "der Zug", "das Flugzeug", "der Bus"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Fahrrad", "bicycle", Article: "das", ExampleSentence: "Das Fahrrad ist neu.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u14-verkehrsmittel-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Flugzeug' means airplane.",
                    Question = "\"airplane\" in German?",
                    Options = ["das Flugzeug", "das Schiff", "der Zug", "das Fahrrad"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Flugzeug", "airplane", Article: "das", ExampleSentence: "Das Flugzeug fliegt hoch.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u14-verkehrsmittel-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Schiff' means ship.",
                    Question = "\"ship\" in German?",
                    Options = ["das Schiff", "das Flugzeug", "der Bus", "die U-Bahn"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Schiff", "ship", Article: "das", ExampleSentence: "Das Schiff fährt langsam.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u14-verkehrsmittel-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die U-Bahn' means subway/underground.",
                    Question = "\"subway/underground\" in German?",
                    Options = ["die U-Bahn", "der Zug", "das Schiff", "das Fahrrad"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("U-Bahn", "subway", Article: "die", ExampleSentence: "Die U-Bahn ist voll.", Highlights: [])
                },
                new ClozeExercise
                {
                    Id = "a2-u14-verkehrsmittel-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich fahre mit dem Zug\" = \"I travel by train\".",
                    TextBefore = "Ich fahre mit dem ",
                    TextAfter = ".",
                    CorrectAnswer = "Zug"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u15-einkaufen",
            Level = CefrLevel.A2,
            SortOrder = 15,
            Title = "Einkaufen",
            Description = "Vocabulary: shopping.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "a2-u15-einkaufen-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Preis' means price.",
                    Question = "\"price\" in German?",
                    Options = ["der Preis", "die Größe", "das Geld", "die Kasse"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Preis", "price", Article: "der", ExampleSentence: "Der Preis ist hoch.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u15-einkaufen-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'billig' means cheap.",
                    Question = "\"cheap\" in German?",
                    Options = ["billig", "teuer", "groß", "klein"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("billig", "cheap", Article: null, ExampleSentence: "Das Auto ist billig.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u15-einkaufen-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'teuer' means expensive.",
                    Question = "\"expensive\" in German?",
                    Options = ["teuer", "billig", "günstig", "kostenlos"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("teuer", "expensive", Article: null, ExampleSentence: "Das Auto ist teuer.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u15-einkaufen-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Größe' means size.",
                    Question = "\"size\" in German?",
                    Options = ["die Größe", "der Preis", "die Kasse", "das Geld"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Größe", "size", Article: "die", ExampleSentence: "Die Größe passt gut.", Highlights: [])
                },
                new MultipleChoiceExercise
                {
                    Id = "a2-u15-einkaufen-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'bezahlen' means to pay.",
                    Question = "\"to pay\" in German?",
                    Options = ["bezahlen", "kaufen", "verkaufen", "kosten"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("bezahlen", "to pay", Article: null, ExampleSentence: "Ich bezahle die Rechnung.", Highlights: [])
                },
                new ClozeExercise
                {
                    Id = "a2-u15-einkaufen-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich bezahle an der Kasse\" = \"I pay at the checkout\".",
                    TextBefore = "Ich bezahle an der ",
                    TextAfter = ".",
                    CorrectAnswer = "Kasse",
                    Teaches = new VocabularyTeaching("Kasse", "checkout", Article: "die", ExampleSentence: "Die Kasse ist leer.", Highlights: [])
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "a2-u16-retrain-kurzsaetze",
            Level = CefrLevel.A2,
            SortOrder = 16,
            Title = "Retrain: kurze Sätze",
            Description = "Very short A2 practice sentences for retraining.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "Perfekt with haben.",
                    TextBefore = "Ich habe ihn ",
                    TextAfter = ".",
                    CorrectAnswer = "gesehen"
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "Perfekt with sein.",
                    TextBefore = "Wir sind nach Hause ",
                    TextAfter = ".",
                    CorrectAnswer = "gegangen"
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "Simple Präteritum form.",
                    TextBefore = "Ich ",
                    TextAfter = " müde.",
                    CorrectAnswer = "war"
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "Dative article with Mann.",
                    TextBefore = "Ich helfe ",
                    TextAfter = " Mann.",
                    CorrectAnswer = "dem"
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "Separable verb in main clause.",
                    TextBefore = "Ich stehe früh ",
                    TextAfter = ".",
                    CorrectAnswer = "auf"
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "Comparative form.",
                    TextBefore = "Heute ist es ",
                    TextAfter = " als gestern.",
                    CorrectAnswer = "kälter",
                    AcceptedAnswers = ["kaelter"]
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e7",
                    Instruction = "Fill in the blank.",
                    Explanation = "Subordinate clause with weil.",
                    TextBefore = "Ich bleibe zu Hause, weil ich ",
                    TextAfter = ".",
                    CorrectAnswer = "krank bin"
                },
                new ClozeExercise
                {
                    Id = "a2-u16-retrain-kurzsaetze-e8",
                    Instruction = "Fill in the blank.",
                    Explanation = "Reflexive pronoun with ich.",
                    TextBefore = "Ich freue ",
                    TextAfter = ".",
                    CorrectAnswer = "mich"
                }
            ]
        }
    ];
}
