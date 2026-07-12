namespace AllItems.Translation.Core.Curriculum.Content;

/// <summary>
/// B2 units building on B1: Konjunktiv I (reported speech), passive with modal verbs and the
/// state/process passive distinction, past Konjunktiv II, extended participial attributes,
/// nominalization, advanced connectors, relative clauses with was/wo/dessen/deren, formal
/// correspondence, arguing a position, and media vocabulary.
/// </summary>
public static class B2Units
{
    public static IReadOnlyList<CurriculumUnit> All { get; } =
    [
        new CurriculumUnit
        {
            Id = "b2-u1-konjunktiv-i",
            Level = CefrLevel.B2,
            SortOrder = 1,
            Title = "Konjunktiv I: indirekte Rede",
            Description = "Reported speech - the formal, journalistic way to relay what someone said.",
            Exercises =
            [
                new ConjugationDrillExercise
                {
                    Id = "b2-u1-konjunktiv-i-e1",
                    Instruction = "Fill in every Konjunktiv I form of 'sein'.",
                    Explanation = "Konjunktiv I of 'sein' is distinctively irregular - worth learning as its own set.",
                    BaseWord = "sein (Konjunktiv I)",
                    Slots =
                    [
                        new ConjugationSlot("ich", "sei"),
                        new ConjugationSlot("du", "seist"),
                        new ConjugationSlot("er/sie/es", "sei"),
                        new ConjugationSlot("wir", "seien"),
                        new ConjugationSlot("ihr", "seiet"),
                        new ConjugationSlot("sie/Sie", "seien")
                    ]
                },
                new ClozeExercise
                {
                    Id = "b2-u1-konjunktiv-i-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "Reported speech uses Konjunktiv I to signal \"this is what he said, not necessarily fact\": er sei müde.",
                    TextBefore = "Er sagt, er ",
                    TextAfter = " müde.",
                    CorrectAnswer = "sei"
                },
                new ClozeExercise
                {
                    Id = "b2-u1-konjunktiv-i-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "Konjunktiv I of 'haben' for 'sie': sie habe.",
                    TextBefore = "Sie sagt, sie ",
                    TextAfter = " keine Zeit.",
                    CorrectAnswer = "habe"
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u1-konjunktiv-i-e4",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Konjunktiv I marks a statement as someone else's claim, distancing the speaker from vouching for it.",
                    Question = "What does Konjunktiv I signal in reported speech?",
                    Options = ["The statement is someone else's claim, not necessarily fact", "The statement is definitely true", "The statement is a question", "The statement is in the past"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u1-konjunktiv-i-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "Konjunktiv I of 'können' for 'er': er könne.",
                    TextBefore = "Er sagt, dass er kommen ",
                    TextAfter = ".",
                    CorrectAnswer = "könne"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u2-passiv-erweitert",
            Level = CefrLevel.B2,
            SortOrder = 2,
            Title = "Passiv mit Modalverben, Zustandspassiv",
            Description = "Passive with modal verbs, and the difference between a state and a process.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b2-u2-passiv-erweitert-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "Modal + passive infinitive: müssen + Partizip II + werden, with werden at the very end.",
                    TextBefore = "Das ",
                    TextAfter = " gemacht werden.",
                    CorrectAnswer = "muss"
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u2-passiv-erweitert-e2",
                    Instruction = "Choose the correct sentence.",
                    Explanation = "\"Der Brief muss geschrieben werden\" places the modal verb second and 'geschrieben werden' at the end.",
                    Question = "\"The letter must be written.\"",
                    Options = ["Der Brief muss geschrieben werden.", "Der Brief wird geschrieben müssen.", "Der Brief muss werden geschrieben.", "Der Brief geschrieben muss werden."],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u2-passiv-erweitert-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "\"Die Tür wird geöffnet\" (Vorgangspassiv) describes the action of opening happening right now.",
                    Question = "\"Die Tür wird geöffnet\" describes...",
                    Options = ["An ongoing process (being opened)", "A finished state (already open)", "A future event only", "A question"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u2-passiv-erweitert-e4",
                    Instruction = "Choose the correct answer.",
                    Explanation = "\"Die Tür ist geöffnet\" (Zustandspassiv, with 'sein') describes the resulting state, not the action itself.",
                    Question = "\"Die Tür ist geöffnet\" describes...",
                    Options = ["A resulting state (it's open)", "An ongoing action of opening", "A question", "The past tense of opening"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u3-konjunktiv-ii-vergangenheit",
            Level = CefrLevel.B2,
            SortOrder = 3,
            Title = "Konjunktiv II der Vergangenheit",
            Description = "Talking about things that didn't happen: \"would have\", \"could have\".",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b2-u3-konjunktiv-ii-vergangenheit-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "Past Konjunktiv II = hätte/wäre + Partizip II. 'haben' takes 'gehabt' as its own participle here.",
                    TextBefore = "Wenn ich Zeit gehabt ",
                    TextAfter = ", wäre ich gekommen.",
                    CorrectAnswer = "hätte"
                },
                new ClozeExercise
                {
                    Id = "b2-u3-konjunktiv-ii-vergangenheit-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'kommen' takes 'sein' as its auxiliary, so the past Konjunktiv II uses 'wäre'.",
                    TextBefore = "Wenn ich Zeit gehabt hätte, ",
                    TextAfter = " ich gekommen.",
                    CorrectAnswer = "wäre"
                },
                new ClozeExercise
                {
                    Id = "b2-u3-konjunktiv-ii-vergangenheit-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich hätte das nicht gemacht\" = \"I wouldn't have done that\".",
                    TextBefore = "Ich ",
                    TextAfter = " das nicht gemacht.",
                    CorrectAnswer = "hätte"
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u3-konjunktiv-ii-vergangenheit-e4",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Past Konjunktiv II is built from hätte/wäre (whichever the verb normally pairs with) plus the Partizip II.",
                    Question = "How is past Konjunktiv II formed?",
                    Options = ["hätte/wäre + Partizip II", "würde + Partizip II", "hatte/war + Infinitiv", "habe/sei + Partizip II"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u4-partizipialattribute",
            Level = CefrLevel.B2,
            SortOrder = 4,
            Title = "Erweiterte Partizipialattribute",
            Description = "Participles used as compact adjectives before a noun - common in written German.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u4-partizipialattribute-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"fahrende\" (from fahren) is a present participle acting as an adjective: the fast-moving train.",
                    Question = "\"der schnell fahrende Zug\" means?",
                    Options = ["the fast-moving train", "the train that will drive fast", "the train's fast driver", "the train station"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u4-partizipialattribute-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "\"geschriebene\" (from schreiben) is a past participle acting as an adjective: the book written by me.",
                    Question = "\"das von mir geschriebene Buch\" means?",
                    Options = ["the book written by me", "the book I am writing now", "the book I will write", "my favorite book"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u4-partizipialattribute-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "This construction packs a whole relative clause into a single adjective phrase before the noun - very common in formal writing.",
                    Question = "An extended participial attribute is mainly a compact way to...",
                    Options = ["Replace a relative clause with an adjective phrase before the noun", "Form the passive voice", "Express reported speech", "Form the future tense"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u5-nominalisierung",
            Level = CefrLevel.B2,
            SortOrder = 5,
            Title = "Nominalisierung",
            Description = "Turning verbs into nouns - a hallmark of formal and written German.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u5-nominalisierung-e1",
                    Instruction = "Choose the correct noun.",
                    Explanation = "ankommen -> die Ankunft (arrival).",
                    Question = "Noun form of 'ankommen' (to arrive)?",
                    Options = ["die Ankunft", "das Ankommen", "der Ankommer", "die Ankommung"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u5-nominalisierung-e2",
                    Instruction = "Choose the correct noun.",
                    Explanation = "untersuchen -> die Untersuchung (examination/investigation).",
                    Question = "Noun form of 'untersuchen' (to examine)?",
                    Options = ["die Untersuchung", "das Untersuchen", "der Untersucher", "die Untersuchheit"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u5-nominalisierung-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "Almost any infinitive can become a neuter noun: lernen -> das Lernen.",
                    TextBefore = "",
                    TextAfter = " macht Spaß.",
                    CorrectAnswer = "Lernen"
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u5-nominalisierung-e4",
                    Instruction = "Choose the correct noun.",
                    Explanation = "entscheiden -> die Entscheidung (decision).",
                    Question = "Noun form of 'entscheiden' (to decide)?",
                    Options = ["die Entscheidung", "das Entscheiden", "der Entscheider", "die Entscheidheit"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u6-konnektoren",
            Level = CefrLevel.B2,
            SortOrder = 6,
            Title = "Konnektoren: je...desto, sowohl...als auch, weder...noch",
            Description = "More sophisticated ways to link ideas.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b2-u6-konnektoren-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"je...desto\" (the more... the more) - the 'je' clause sends its verb to the end, the 'desto' clause inverts verb-subject.",
                    TextBefore = "Je mehr ich lerne, ",
                    TextAfter = " besser verstehe ich.",
                    CorrectAnswer = "desto"
                },
                new ClozeExercise
                {
                    Id = "b2-u6-konnektoren-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"sowohl...als auch\" = \"both...and\", and it takes a plural verb.",
                    TextBefore = "Sowohl er ",
                    TextAfter = " auch sie kommen.",
                    CorrectAnswer = "als"
                },
                new ClozeExercise
                {
                    Id = "b2-u6-konnektoren-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"weder...noch\" = \"neither...nor\".",
                    TextBefore = "Weder er ",
                    TextAfter = " sie kommt.",
                    CorrectAnswer = "noch"
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u6-konnektoren-e4",
                    Instruction = "Choose the correct translation.",
                    Explanation = "\"Je mehr, desto besser\" is a classic fixed phrase meaning \"the more, the better\".",
                    Question = "\"Je mehr, desto besser\" means?",
                    Options = ["The more, the better", "More or less", "Neither more nor better", "As much as better"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u7-relativsaetze-erweitert",
            Level = CefrLevel.B2,
            SortOrder = 7,
            Title = "Relativsätze mit was, wo, dessen/deren",
            Description = "Relative clauses beyond der/die/das - for whole ideas, places, and possession.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b2-u7-relativsaetze-erweitert-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "'was' refers to an indefinite antecedent like 'das' or an entire preceding clause.",
                    TextBefore = "Das, ",
                    TextAfter = " du sagst, ist wichtig.",
                    CorrectAnswer = "was"
                },
                new ClozeExercise
                {
                    Id = "b2-u7-relativsaetze-erweitert-e2",
                    Instruction = "Fill in the blank.",
                    Explanation = "'wo' can replace 'in der' when referring to a place.",
                    TextBefore = "Die Stadt, ",
                    TextAfter = " ich geboren bin, ist klein.",
                    CorrectAnswer = "wo"
                },
                new ClozeExercise
                {
                    Id = "b2-u7-relativsaetze-erweitert-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "'dessen' is the relative possessive pronoun for a masculine antecedent (\"whose\", genitive): der Mann, dessen Auto...",
                    TextBefore = "Der Mann, ",
                    TextAfter = " Auto kaputt ist, wartet hier.",
                    CorrectAnswer = "dessen"
                },
                new ClozeExercise
                {
                    Id = "b2-u7-relativsaetze-erweitert-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "'deren' is the relative possessive pronoun for a feminine antecedent: die Frau, deren Tasche...",
                    TextBefore = "Die Frau, ",
                    TextAfter = " Tasche verloren ging, ist traurig.",
                    CorrectAnswer = "deren"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u8-formelle-korrespondenz",
            Level = CefrLevel.B2,
            SortOrder = 8,
            Title = "Formelle Korrespondenz",
            Description = "Standard phrases for formal letters and emails.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u8-formelle-korrespondenz-e1",
                    Instruction = "Choose the correct formal greeting.",
                    Explanation = "\"Sehr geehrte Damen und Herren\" is the standard formal greeting when you don't know the recipient's name.",
                    Question = "Formal greeting when you don't know the recipient's name?",
                    Options = ["Sehr geehrte Damen und Herren,", "Hallo zusammen,", "Liebe Freunde,", "Hi Leute,"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u8-formelle-korrespondenz-e2",
                    Instruction = "Choose the correct formal closing.",
                    Explanation = "\"Mit freundlichen Grüßen\" is the standard formal sign-off, equivalent to \"Sincerely\".",
                    Question = "Standard formal closing for a letter/email?",
                    Options = ["Mit freundlichen Grüßen", "Bis bald", "Tschüss", "Alles Liebe"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u8-formelle-korrespondenz-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich schreibe Ihnen bezüglich...\" = \"I am writing to you regarding...\" - a standard formal opener.",
                    TextBefore = "Ich schreibe ",
                    TextAfter = " bezüglich meiner Bestellung.",
                    CorrectAnswer = "Ihnen"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u9-position-vertreten",
            Level = CefrLevel.B2,
            SortOrder = 9,
            Title = "Eine Position vertreten",
            Description = "Phrases for arguing a position and weighing both sides.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "b2-u9-position-vertreten-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Einerseits..., andererseits...\" = \"on one hand..., on the other hand...\".",
                    TextBefore = "",
                    TextAfter = " ist es teuer, andererseits spart man Zeit.",
                    CorrectAnswer = "Einerseits"
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u9-position-vertreten-e2",
                    Instruction = "Choose the correct translation.",
                    Explanation = "\"Man könnte argumentieren, dass...\" is a common hedge for introducing a claim.",
                    Question = "\"Man könnte argumentieren, dass...\" means?",
                    Options = ["One could argue that...", "One must argue that...", "Nobody argues that...", "I argued that..."],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u9-position-vertreten-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Es lässt sich nicht leugnen, dass...\" = \"It cannot be denied that...\".",
                    TextBefore = "Es lässt sich nicht ",
                    TextAfter = ", dass das Problem ernst ist.",
                    CorrectAnswer = "leugnen"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u10-medien",
            Level = CefrLevel.B2,
            SortOrder = 10,
            Title = "Medien und Nachrichten",
            Description = "Vocabulary for discussing news and media.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u10-medien-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Schlagzeile' means headline.",
                    Question = "\"headline\" in German?",
                    Options = ["Schlagzeile", "Berichterstattung", "Quelle", "Ausgabe"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u10-medien-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Berichterstattung' means reporting/coverage.",
                    Question = "\"news coverage/reporting\" in German?",
                    Options = ["Berichterstattung", "Schlagzeile", "Meinung", "Quelle"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u10-medien-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'Quelle' means source.",
                    Question = "\"source\" (of information) in German?",
                    Options = ["Quelle", "Ausgabe", "Beitrag", "Redaktion"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u10-medien-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'glaubwürdig' means credible/believable.",
                    Question = "\"credible/believable\" in German?",
                    Options = ["glaubwürdig", "berühmt", "wichtig", "aktuell"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u11-politik-gesellschaft",
            Level = CefrLevel.B2,
            SortOrder = 11,
            Title = "Politik und Gesellschaft",
            Description = "Vocabulary: politics and society.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u11-politik-gesellschaft-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Regierung' means government.",
                    Question = "\"government\" in German?",
                    Options = ["die Regierung", "die Wahl", "das Gesetz", "die Gesellschaft"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u11-politik-gesellschaft-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Wahl' means election.",
                    Question = "\"election\" in German?",
                    Options = ["die Wahl", "die Regierung", "der Bürger", "die Demokratie"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u11-politik-gesellschaft-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Bürger' means citizen.",
                    Question = "\"citizen\" in German?",
                    Options = ["der Bürger", "der Politiker", "der Wähler", "der Beamte"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u11-politik-gesellschaft-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Gerechtigkeit' means justice.",
                    Question = "\"justice\" in German?",
                    Options = ["die Gerechtigkeit", "die Ungleichheit", "das Gesetz", "die Freiheit"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u11-politik-gesellschaft-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Ungleichheit' means inequality.",
                    Question = "\"inequality\" in German?",
                    Options = ["die Ungleichheit", "die Gerechtigkeit", "die Demokratie", "die Wahl"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u11-politik-gesellschaft-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Die Regierung hat ein neues Gesetz beschlossen\" = \"The government passed a new law\".",
                    TextBefore = "Die ",
                    TextAfter = " hat ein neues Gesetz beschlossen.",
                    CorrectAnswer = "Regierung"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u12-wirtschaft",
            Level = CefrLevel.B2,
            SortOrder = 12,
            Title = "Wirtschaft",
            Description = "Vocabulary: the economy.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u12-wirtschaft-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Unternehmen' means company/enterprise.",
                    Question = "\"company/enterprise\" in German?",
                    Options = ["das Unternehmen", "der Gewinn", "die Steuer", "der Markt"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u12-wirtschaft-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Gewinn' means profit.",
                    Question = "\"profit\" in German?",
                    Options = ["der Gewinn", "der Verlust", "die Steuer", "das Angebot"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u12-wirtschaft-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Inflation' means inflation.",
                    Question = "\"inflation\" in German?",
                    Options = ["die Inflation", "die Steuer", "die Nachfrage", "der Markt"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u12-wirtschaft-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Nachfrage' means demand.",
                    Question = "\"demand\" (economic) in German?",
                    Options = ["die Nachfrage", "das Angebot", "der Gewinn", "die Steuer"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u12-wirtschaft-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Angebot' means supply.",
                    Question = "\"supply\" (economic) in German?",
                    Options = ["das Angebot", "die Nachfrage", "der Gewinn", "die Inflation"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u12-wirtschaft-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Angebot und Nachfrage bestimmen den Preis\" = \"Supply and demand determine the price\".",
                    TextBefore = "",
                    TextAfter = " und Nachfrage bestimmen den Preis.",
                    CorrectAnswer = "Angebot"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u13-wissenschaft-forschung",
            Level = CefrLevel.B2,
            SortOrder = 13,
            Title = "Wissenschaft und Forschung",
            Description = "Vocabulary: science and research.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u13-wissenschaft-forschung-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Forschung' means research.",
                    Question = "\"research\" in German?",
                    Options = ["die Forschung", "das Experiment", "die Hypothese", "das Ergebnis"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u13-wissenschaft-forschung-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Hypothese' means hypothesis.",
                    Question = "\"hypothesis\" in German?",
                    Options = ["die Hypothese", "die Theorie", "das Ergebnis", "der Beweis"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u13-wissenschaft-forschung-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'beweisen' means to prove.",
                    Question = "\"to prove\" in German?",
                    Options = ["beweisen", "vermuten", "messen", "analysieren"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u13-wissenschaft-forschung-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Erkenntnis' means insight/finding.",
                    Question = "\"insight/finding\" in German?",
                    Options = ["die Erkenntnis", "das Experiment", "die Hypothese", "die Forschung"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u13-wissenschaft-forschung-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'analysieren' means to analyze.",
                    Question = "\"to analyze\" in German?",
                    Options = ["analysieren", "beweisen", "messen", "vermuten"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u13-wissenschaft-forschung-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Die Forschung zeigt neue Ergebnisse\" = \"The research shows new results\".",
                    TextBefore = "Die ",
                    TextAfter = " zeigt neue Ergebnisse.",
                    CorrectAnswer = "Forschung"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u14-kunst-kultur",
            Level = CefrLevel.B2,
            SortOrder = 14,
            Title = "Kunst und Kultur",
            Description = "Vocabulary: art and culture.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u14-kunst-kultur-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Gemälde' means painting.",
                    Question = "\"painting\" in German?",
                    Options = ["das Gemälde", "die Skulptur", "die Ausstellung", "das Meisterwerk"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u14-kunst-kultur-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Ausstellung' means exhibition.",
                    Question = "\"exhibition\" in German?",
                    Options = ["die Ausstellung", "das Gemälde", "der Künstler", "die Skulptur"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u14-kunst-kultur-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Skulptur' means sculpture.",
                    Question = "\"sculpture\" in German?",
                    Options = ["die Skulptur", "das Gemälde", "das Meisterwerk", "die Ausstellung"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u14-kunst-kultur-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Erbe' means heritage/legacy.",
                    Question = "\"heritage/legacy\" in German?",
                    Options = ["das Erbe", "die Tradition", "das Meisterwerk", "die Kultur"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u14-kunst-kultur-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'zeitgenössisch' means contemporary.",
                    Question = "\"contemporary\" in German?",
                    Options = ["zeitgenössisch", "traditionell", "historisch", "altmodisch"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u14-kunst-kultur-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Der Künstler stellt seine Werke aus\" = \"The artist is exhibiting his works\".",
                    TextBefore = "Der ",
                    TextAfter = " stellt seine Werke aus.",
                    CorrectAnswer = "Künstler"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "b2-u15-gesundheit-medizin",
            Level = CefrLevel.B2,
            SortOrder = 15,
            Title = "Gesundheit und Medizin",
            Description = "Vocabulary: advanced health and medicine.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "b2-u15-gesundheit-medizin-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Diagnose' means diagnosis.",
                    Question = "\"diagnosis\" in German?",
                    Options = ["die Diagnose", "die Behandlung", "die Therapie", "die Genesung"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u15-gesundheit-medizin-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Behandlung' means treatment.",
                    Question = "\"treatment\" in German?",
                    Options = ["die Behandlung", "die Diagnose", "die Impfung", "das Medikament"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u15-gesundheit-medizin-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Medikament' means medication.",
                    Question = "\"medication\" in German?",
                    Options = ["das Medikament", "die Impfung", "die Diagnose", "die Therapie"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u15-gesundheit-medizin-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'chronisch' means chronic.",
                    Question = "\"chronic\" in German?",
                    Options = ["chronisch", "akut", "ansteckend", "harmlos"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "b2-u15-gesundheit-medizin-e5",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Genesung' means recovery.",
                    Question = "\"recovery\" (from illness) in German?",
                    Options = ["die Genesung", "die Diagnose", "die Impfung", "die Behandlung"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "b2-u15-gesundheit-medizin-e6",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Ich wünsche dir gute Genesung\" = \"I wish you a good recovery\".",
                    TextBefore = "Ich wünsche dir gute ",
                    TextAfter = ".",
                    CorrectAnswer = "Genesung"
                }
            ]
        }
    ];
}
