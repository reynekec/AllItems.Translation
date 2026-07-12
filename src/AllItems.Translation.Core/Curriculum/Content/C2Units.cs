namespace AllItems.Translation.Core.Curriculum.Content;

/// <summary>
/// C2 units building on C1: rhetorical devices, near-synonym nuance, bureaucratic/legal German,
/// proverbs, archaic/literary forms, advanced modal particle nuance, genre-specific style,
/// wordplay and ambiguity, advanced rhetorical structures, and interpreting poetry/prose. The
/// final tier of the framework's A1-C2 progression.
/// </summary>
public static class C2Units
{
    public static IReadOnlyList<CurriculumUnit> All { get; } =
    [
        new CurriculumUnit
        {
            Id = "c2-u1-rhetorische-mittel",
            Level = CefrLevel.C2,
            SortOrder = 1,
            Title = "Rhetorische Mittel",
            Description = "Irony, sarcasm, hyperbole, and understatement - the tools of persuasive and literary language.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u1-rhetorische-mittel-e1",
                    Instruction = "Choose the correct distinction.",
                    Explanation = "Ironie can be gentle or playful; Sarkasmus is a sharper, mocking form of irony meant to wound or ridicule.",
                    Question = "How does \"Sarkasmus\" differ from \"Ironie\"?",
                    Options = ["Sarkasmus is a sharper, more biting/mocking form of irony", "They are unrelated concepts", "Sarkasmus is always gentle and kind", "Ironie only appears in writing"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u1-rhetorische-mittel-e2",
                    Instruction = "Choose the correct term.",
                    Explanation = "\"Ich habe dir das schon tausendmal gesagt\" is a hyperbole (Übertreibung) - obviously not literally a thousand times.",
                    Question = "\"Ich habe dir das schon tausendmal gesagt.\" is an example of...",
                    Options = ["Übertreibung/Hyperbel (hyperbole)", "Litotes (understatement)", "Ironie", "eine wörtliche Aussage (a literal statement)"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u1-rhetorische-mittel-e3",
                    Instruction = "Choose the correct term.",
                    Explanation = "\"nicht schlecht\" (not bad) understates a positive quality via double negation - that's Litotes.",
                    Question = "\"nicht schlecht\" (meaning \"quite good\") is an example of...",
                    Options = ["Litotes (understatement)", "Hyperbel (exaggeration)", "Sarkasmus", "eine rhetorische Frage"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u1-rhetorische-mittel-e4",
                    Instruction = "Choose the correct definition.",
                    Explanation = "A rhetorische Frage is asked for persuasive effect, not because the speaker wants an answer.",
                    Question = "A \"rhetorische Frage\" is a question that...",
                    Options = ["is asked for effect, not expecting a real answer", "always has a yes/no answer", "is used only in formal writing", "must be answered immediately"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u2-synonym-nuancen",
            Level = CefrLevel.C2,
            SortOrder = 2,
            Title = "Feinheiten der Synonyme",
            Description = "Near-synonyms that differ in register or connotation rather than core meaning.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u2-synonym-nuancen-e1",
                    Instruction = "Choose the correct nuance.",
                    Explanation = "All four mean \"to look\", but 'gucken' is distinctly colloquial/casual.",
                    Question = "Which of these \"to look\" verbs is the most colloquial/casual?",
                    Options = ["gucken", "blicken", "schauen", "erblicken"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u2-synonym-nuancen-e2",
                    Instruction = "Choose the correct nuance.",
                    Explanation = "'starren' implies an intense, fixed gaze - stronger than the neutral 'schauen'.",
                    Question = "Which word implies an intense, fixed gaze (\"to stare\")?",
                    Options = ["starren", "schauen", "gucken", "blicken"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u2-synonym-nuancen-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "At C2, picking the right near-synonym for the situation - not just any correct word - is what marks native-like fluency.",
                    Question = "Why does synonym choice matter at a near-native level?",
                    Options = ["The wrong synonym can sound unnatural even if technically correct", "Synonyms are fully interchangeable in German", "Only one synonym per concept actually exists", "Register doesn't affect word choice"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u3-amtsdeutsch",
            Level = CefrLevel.C2,
            SortOrder = 3,
            Title = "Amtsdeutsch und Fachsprache",
            Description = "The dense, formal register of legal and bureaucratic German.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u3-amtsdeutsch-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'unbeschadet' = \"notwithstanding/without prejudice to\" - common in legal texts.",
                    Question = "\"unbeschadet\" (in a legal text) means?",
                    Options = ["notwithstanding / without prejudice to", "damaged", "unfortunately", "immediately"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u3-amtsdeutsch-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'vorbehaltlich' = \"subject to/pending\" - reserving a condition.",
                    Question = "\"vorbehaltlich\" means?",
                    Options = ["subject to / pending", "regardless of", "in spite of", "as soon as possible"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u3-amtsdeutsch-e3",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"im Sinne des Gesetzes\" = \"within the meaning of the law\" - a standard legal phrase.",
                    Question = "\"im Sinne des Gesetzes\" means?",
                    Options = ["within the meaning of the law", "against the law", "outside the law", "before the law existed"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u4-sprichwoerter",
            Level = CefrLevel.C2,
            SortOrder = 4,
            Title = "Sprichwörter",
            Description = "Proverbs - deeply cultural idioms whose meaning goes well beyond the literal words.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u4-sprichwoerter-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"the apple doesn't fall far from the trunk\" - children resemble their parents.",
                    Question = "\"Der Apfel fällt nicht weit vom Stamm.\" means?",
                    Options = ["Children resemble their parents", "Fruit should be eaten fresh", "Trees grow slowly", "Family visits should be frequent"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u4-sprichwoerter-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"whoever says A must also say B\" - if you start something, you must follow through.",
                    Question = "\"Wer A sagt, muss auch B sagen.\" means?",
                    Options = ["If you start something, you must follow through", "Always speak in complete sentences", "Learn the alphabet first", "Say things in the correct order"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u4-sprichwoerter-e3",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"Other countries, other customs\" - equivalent to \"when in Rome...\".",
                    Question = "\"Andere Länder, andere Sitten.\" means?",
                    Options = ["Customs vary between cultures - adapt accordingly", "Travel broadens the mind", "Every country has laws", "People are the same everywhere"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u4-sprichwoerter-e4",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"what loves each other, teases each other\" - teasing can be a sign of affection.",
                    Question = "\"Was sich liebt, das neckt sich.\" means?",
                    Options = ["Teasing can be a sign of affection", "Love always ends in conflict", "Avoid teasing people you love", "Only strangers tease each other"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u5-archaische-formen",
            Level = CefrLevel.C2,
            SortOrder = 5,
            Title = "Archaische und literarische Formen",
            Description = "Older constructions still found in literature, poetry, and set phrases.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u5-archaische-formen-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"des Nachts\" is an archaic genitive-of-time construction meaning \"at night\", still seen in literary writing.",
                    Question = "\"des Nachts\" means?",
                    Options = ["at night", "of the night sky", "nightly (adjective)", "never"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u5-archaische-formen-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"vonnöten sein\" is an older way of saying \"nötig sein\" (to be necessary).",
                    Question = "\"vonnöten sein\" means?",
                    Options = ["to be necessary", "to be forbidden", "to be far away", "to be finished"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u5-archaische-formen-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Archaic forms rarely appear in everyday spoken German, but recognizing them is essential for reading literature.",
                    Question = "Where would you most likely encounter constructions like \"des Nachts\" today?",
                    Options = ["Literature and poetry", "Casual text messages", "Spoken small talk", "Children's picture books"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u6-partikel-feinheiten",
            Level = CefrLevel.C2,
            SortOrder = 6,
            Title = "Modalpartikeln: letzte Feinheiten",
            Description = "The subtlest layer of particle usage - assumption, concession, and double meanings.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u6-partikel-feinheiten-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"mag sein, dass...\" concedes a point while still holding some reservation - \"it may be that...\".",
                    Question = "\"Mag sein, dass er recht hat.\" means?",
                    Options = ["It may be that he's right (conceding, with some doubt)", "He is definitely right", "He may leave now", "He likes being right"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u6-partikel-feinheiten-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'wohl' here signals a probability/assumption, not literal well-being.",
                    Question = "\"Er wird wohl kommen.\" - what does 'wohl' express here?",
                    Options = ["Probability/assumption (\"he'll probably come\")", "That he feels well", "A strong command", "Certainty"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u6-partikel-feinheiten-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "\"eigentlich\" can be a literal \"actually/originally\" or a conversational softener depending entirely on context and tone.",
                    Question = "Why is \"eigentlich\" tricky even at an advanced level?",
                    Options = ["Its meaning shifts between literal and conversational-softening depending on context", "It has only one fixed meaning", "It is never used in speech", "It only appears in questions"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u7-textsorten-stil",
            Level = CefrLevel.C2,
            SortOrder = 7,
            Title = "Textsortenspezifische Stilmittel",
            Description = "How style shifts across journalism, literature, and scientific writing.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u7-textsorten-stil-e1",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Journalistic style favors short, punchy sentences and a clear lead - very different from dense academic nominal style.",
                    Question = "Which style favors short, direct sentences with a clear \"lead\" up front?",
                    Options = ["Journalistischer Stil (journalistic style)", "Wissenschaftlicher Stil (scientific style)", "Amtsdeutsch (bureaucratic style)", "Lyrik (poetry)"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u7-textsorten-stil-e2",
                    Instruction = "Choose the correct term.",
                    Explanation = "Subtext (der Subtext) is meaning implied beneath the literal words - central to interpreting literary and persuasive texts.",
                    Question = "The implied meaning beneath the literal words of a text is called?",
                    Options = ["der Subtext", "die Grammatik", "der Wortschatz", "die Rechtschreibung"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u7-textsorten-stil-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Recognizing genre conventions helps a reader know what to expect and read between the lines appropriately.",
                    Question = "Why does recognizing a text's genre (Textsorte) matter for interpretation?",
                    Options = ["Each genre has its own conventions for how meaning is signaled", "All genres use identical style rules", "Genre only affects vocabulary, never structure", "Genre is irrelevant to meaning"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u8-wortspiele",
            Level = CefrLevel.C2,
            SortOrder = 8,
            Title = "Wortspiele und Doppeldeutigkeit",
            Description = "Puns and double meanings that rely on sound, spelling, or ambiguity.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u8-wortspiele-e1",
                    Instruction = "Choose the correct term.",
                    Explanation = "\"Mehrdeutigkeit\" describes a word or phrase that genuinely has more than one valid meaning.",
                    Question = "A word or phrase with more than one valid meaning is called...",
                    Options = ["mehrdeutig", "eindeutig", "unwichtig", "veraltet"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u8-wortspiele-e2",
                    Instruction = "Choose the correct answer.",
                    Explanation = "\"Wenn Fliegen hinter Fliegen fliegen, fliegen Fliegen Fliegen nach\" plays on 'Fliegen' meaning both \"flies\" (insects) and \"to fly\" - a classic German wordplay example.",
                    Question = "\"Wenn Fliegen hinter Fliegen fliegen, fliegen Fliegen Fliegen nach\" is a pun built on which double meaning?",
                    Options = ["\"Fliegen\" as both the insect \"flies\" and the verb \"to fly\"", "A pun on two different colors", "A play on regional dialect", "A pun on formal vs. informal \"you\""],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u8-wortspiele-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Catching wordplay in real time - jokes, headlines, advertising - is one of the last things learners develop, since it relies on instantly recognizing multiple meanings at once.",
                    Question = "Why is recognizing wordplay considered a near-native (C2) skill?",
                    Options = ["It requires instantly holding multiple meanings of a word in mind at once", "It only requires knowing more vocabulary", "It is taught explicitly starting at A1", "It never appears in native speech"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u9-rhetorische-strukturen",
            Level = CefrLevel.C2,
            SortOrder = 9,
            Title = "Chiasmus, Anapher, Parallelismus",
            Description = "Advanced structural rhetorical devices used in speeches and literary writing.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u9-rhetorische-strukturen-e1",
                    Instruction = "Choose the correct term.",
                    Explanation = "\"Nicht der ist frei, der tut, was er will, sondern der will, was er tut\" crosses \"tut...will\" with \"will...tut\" - a classic chiasmus (ABBA pattern).",
                    Question = "\"Nicht der ist frei, der tut, was er will, sondern der will, was er tut.\" is an example of...",
                    Options = ["Chiasmus (a crossed ABBA structure)", "Anapher (repetition at the start of clauses)", "Litotes", "eine rhetorische Frage"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u9-rhetorische-strukturen-e2",
                    Instruction = "Choose the correct term.",
                    Explanation = "Repeating \"Wir werden\" at the start of each clause is anaphora - a device common in persuasive speeches.",
                    Question = "\"Wir werden kämpfen, wir werden nicht aufgeben, wir werden siegen.\" is an example of...",
                    Options = ["Anapher (repetition at the start of clauses)", "Chiasmus", "Litotes", "Ironie"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u9-rhetorische-strukturen-e3",
                    Instruction = "Choose the correct definition.",
                    Explanation = "Parallelismus repeats a grammatical structure across clauses to create rhythm and emphasis.",
                    Question = "\"Parallelismus\" refers to...",
                    Options = ["Repeating a grammatical structure across clauses for rhythm/emphasis", "Two people speaking at once", "A grammar mistake", "A type of question"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u10-lyrik-prosa",
            Level = CefrLevel.C2,
            SortOrder = 10,
            Title = "Interpretation von Lyrik und Prosa",
            Description = "Vocabulary and concepts for close-reading poetry and literary prose.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u10-lyrik-prosa-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Strophe' means stanza.",
                    Question = "\"stanza\" (of a poem) in German?",
                    Options = ["die Strophe", "der Reim", "die Zeile", "der Vers"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u10-lyrik-prosa-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Reim' means rhyme.",
                    Question = "\"rhyme\" in German?",
                    Options = ["der Reim", "die Strophe", "das Enjambement", "der Rhythmus"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u10-lyrik-prosa-e3",
                    Instruction = "Choose the correct definition.",
                    Explanation = "Enjambement is when a sentence or thought continues past a line break without pause - a common poetic device.",
                    Question = "\"das Enjambement\" refers to...",
                    Options = ["A sentence continuing past a line break without pause", "The final line of a poem", "A poem with no rhyme", "The title of a poem"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u10-lyrik-prosa-e4",
                    Instruction = "Choose the correct answer.",
                    Explanation = "This is the natural capstone question of the whole framework: reading literature closely draws on everything from A1 grammar through C2 nuance at once.",
                    Question = "Close literary interpretation typically draws on...",
                    Options = ["The full range of grammar, vocabulary, and cultural nuance built up across all levels", "Only vocabulary memorization", "Only grammar rules", "Nothing learned at earlier levels"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u11-literarischer-wortschatz",
            Level = CefrLevel.C2,
            SortOrder = 11,
            Title = "Literarischer Wortschatz",
            Description = "Vocabulary for naming literary forms and devices precisely.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u11-literarischer-wortschatz-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Novelle' means novella - a work shorter than a novel but longer than a short story.",
                    Question = "\"novella\" in German?",
                    Options = ["die Novelle", "der Roman", "das Epos", "die Elegie"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Novelle", "novella")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u11-literarischer-wortschatz-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Satire' means satire.",
                    Question = "\"satire\" in German?",
                    Options = ["die Satire", "die Parodie", "die Allegorie", "die Elegie"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Satire", "satire")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u11-literarischer-wortschatz-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Allegorie' means allegory - a story with a symbolic second meaning.",
                    Question = "\"allegory\" in German?",
                    Options = ["die Allegorie", "die Satire", "die Parodie", "das Epos"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Allegorie", "allegory")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u11-literarischer-wortschatz-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Protagonist' means protagonist/main character.",
                    Question = "\"protagonist/main character\" in German?",
                    Options = ["der Protagonist", "der Erzähler", "der Antagonist", "der Autor"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Protagonist", "protagonist")
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u12-philosophische-begriffe",
            Level = CefrLevel.C2,
            SortOrder = 12,
            Title = "Philosophische Begriffe",
            Description = "Terminology for discussing philosophy.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u12-philosophische-begriffe-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Ethik' means ethics.",
                    Question = "\"ethics\" in German?",
                    Options = ["die Ethik", "die Metaphysik", "die Existenz", "die Vernunft"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Ethik", "ethics")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u12-philosophische-begriffe-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Metaphysik' means metaphysics.",
                    Question = "\"metaphysics\" in German?",
                    Options = ["die Metaphysik", "die Ethik", "die Erkenntnistheorie", "der Determinismus"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Metaphysik", "metaphysics")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u12-philosophische-begriffe-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Vernunft' means reason (the faculty of rational thought).",
                    Question = "\"reason\" (rational thought) in German?",
                    Options = ["die Vernunft", "die Existenz", "die Weltanschauung", "die Ethik"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Vernunft", "reason")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u12-philosophische-begriffe-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Weltanschauung' means worldview.",
                    Question = "\"worldview\" in German?",
                    Options = ["die Weltanschauung", "die Vernunft", "die Metaphysik", "der Determinismus"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Weltanschauung", "worldview")
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u13-historische-kulturelle-begriffe",
            Level = CefrLevel.C2,
            SortOrder = 13,
            Title = "Historische und kulturelle Begriffe",
            Description = "Vocabulary for discussing history and cultural change.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u13-historische-kulturelle-begriffe-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Zeitalter' means era/age.",
                    Question = "\"era/age\" in German?",
                    Options = ["das Zeitalter", "die Epoche", "der Umbruch", "der Brauch"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Zeitalter", "era")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u13-historische-kulturelle-begriffe-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Umbruch' means upheaval/radical change.",
                    Question = "\"upheaval/radical change\" in German?",
                    Options = ["der Umbruch", "das Zeitalter", "der Brauch", "der Zeitgeist"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Umbruch", "upheaval")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u13-historische-kulturelle-begriffe-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Brauch' means custom.",
                    Question = "\"custom\" (cultural practice) in German?",
                    Options = ["der Brauch", "die Epoche", "der Umbruch", "das Zeitalter"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Brauch", "custom")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u13-historische-kulturelle-begriffe-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Zeitgeist' means \"spirit of the age\" - the defining mood/ideas of a period.",
                    Question = "\"zeitgeist / spirit of the age\" in German?",
                    Options = ["der Zeitgeist", "die Epoche", "der Brauch", "das Zeitalter"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Zeitgeist", "zeitgeist")
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u14-wissenschaftliche-terminologie",
            Level = CefrLevel.C2,
            SortOrder = 14,
            Title = "Wissenschaftliche Terminologie",
            Description = "Advanced scientific terminology.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u14-wissenschaftliche-terminologie-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Quantenmechanik' means quantum mechanics.",
                    Question = "\"quantum mechanics\" in German?",
                    Options = ["die Quantenmechanik", "die Genetik", "die Evolution", "die Synthese"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Quantenmechanik", "quantum mechanics")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u14-wissenschaftliche-terminologie-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Molekül' means molecule.",
                    Question = "\"molecule\" in German?",
                    Options = ["das Molekül", "der Organismus", "das Phänomen", "die Synthese"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Molekül", "molecule")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u14-wissenschaftliche-terminologie-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Organismus' means organism.",
                    Question = "\"organism\" in German?",
                    Options = ["der Organismus", "das Molekül", "die Genetik", "das Phänomen"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Organismus", "organism")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u14-wissenschaftliche-terminologie-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Wechselwirkung' means interaction (between forces, particles, systems, etc.).",
                    Question = "\"interaction\" (between systems/forces) in German?",
                    Options = ["die Wechselwirkung", "die Synthese", "das Phänomen", "die Genetik"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Wechselwirkung", "interaction")
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c2-u15-gehobene-woerter",
            Level = CefrLevel.C2,
            SortOrder = 15,
            Title = "Seltene und gehobene Wörter",
            Description = "Rare, elevated vocabulary found in literary and formal writing.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c2-u15-gehobene-woerter-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'die Muße' is an elevated word for leisure - unhurried, contemplative free time.",
                    Question = "\"die Muße\" means?",
                    Options = ["leisure (unhurried, contemplative free time)", "exhaustion", "boredom", "haste"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Muße", "leisure")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u15-gehobene-woerter-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'die Zwietracht' means discord/strife between people.",
                    Question = "\"die Zwietracht\" means?",
                    Options = ["discord/strife", "harmony", "friendship", "silence"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Zwietracht", "discord")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u15-gehobene-woerter-e3",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'erhaben' means sublime/exalted - inspiring awe through greatness.",
                    Question = "\"erhaben\" means?",
                    Options = ["sublime/exalted", "ordinary", "hidden", "broken"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("erhaben", "sublime")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u15-gehobene-woerter-e4",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'unergründlich' means unfathomable - impossible to fully understand or measure.",
                    Question = "\"unergründlich\" means?",
                    Options = ["unfathomable", "shallow", "obvious", "temporary"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("unergründlich", "unfathomable")
                },
                new MultipleChoiceExercise
                {
                    Id = "c2-u15-gehobene-woerter-e5",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'die Anmut' means grace/gracefulness.",
                    Question = "\"die Anmut\" means?",
                    Options = ["grace/gracefulness", "clumsiness", "strength", "speed"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Anmut", "grace")
                }
            ]
        }
    ];
}
