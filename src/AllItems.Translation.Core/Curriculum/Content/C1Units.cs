namespace AllItems.Translation.Core.Curriculum.Content;

/// <summary>
/// C1 units building on B2: modal particles, function verb constructions, nominal vs. verbal
/// style, idiomatic expressions, Konjunktiv I in academic writing, higher-level text connectors,
/// word formation, register levels, argumentation phrases, and literary/cultural analysis
/// vocabulary.
/// </summary>
public static class C1Units
{
    public static IReadOnlyList<CurriculumUnit> All { get; } =
    [
        new CurriculumUnit
        {
            Id = "c1-u1-modalpartikeln",
            Level = CefrLevel.C1,
            SortOrder = 1,
            Title = "Modalpartikeln: doch, ja, eben, halt, mal",
            Description = "Small flavoring words that shift tone and emphasis without changing the literal meaning.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u1-modalpartikeln-e1",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "'doch' here adds emphasis, implying the listener should already know or agree.",
                    Question = "\"Das ist doch klar!\" - what does 'doch' add?",
                    Options = ["Emphasis - implying this should be obvious to the listener", "A question", "Uncertainty", "Politeness"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u1-modalpartikeln-e2",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "'ja' here signals shared or already-known information, like \"you know\" in English.",
                    Question = "\"Er ist ja krank.\" - what does 'ja' signal?",
                    Options = ["This is already known/shared information", "A strong yes/agreement", "Doubt", "A command"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u1-modalpartikeln-e3",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "'eben'/'halt' both express resignation - \"that's just the way it is\".",
                    Question = "\"Das ist eben so.\" - closest English equivalent?",
                    Options = ["\"That's just how it is.\"", "\"That's definitely wrong.\"", "\"Is that so?\"", "\"That was even better.\"" ],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u1-modalpartikeln-e4",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "'mal' softens a request or command, making it sound casual rather than blunt.",
                    Question = "\"Komm mal her.\" - what does 'mal' do here?",
                    Options = ["Softens the request, makes it sound casual", "Makes it more formal", "Turns it into a question", "Negates the request"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u2-funktionsverbgefuege",
            Level = CefrLevel.C1,
            SortOrder = 2,
            Title = "Funktionsverbgefüge",
            Description = "Noun + light verb combinations that replace a simple verb - common in formal register.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u2-funktionsverbgefuege-e1",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "\"zur Anwendung kommen\" is a formal way of saying \"angewendet werden\" (to be applied).",
                    Question = "\"zur Anwendung kommen\" means?",
                    Options = ["to be applied/used", "to arrive somewhere", "to come to an end", "to be forgotten"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u2-funktionsverbgefuege-e2",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "\"in Frage stellen\" is a formal way of saying \"bezweifeln\" (to doubt/question).",
                    Question = "\"in Frage stellen\" means?",
                    Options = ["to question/doubt", "to ask a question", "to answer", "to place in a room"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u2-funktionsverbgefuege-e3",
                    Instruction = "Choose the closest meaning.",
                    Explanation = "\"zur Verfügung stehen\" is a formal way of saying \"verfügbar sein\" (to be available).",
                    Question = "\"zur Verfügung stehen\" means?",
                    Options = ["to be available", "to stand in line", "to be in charge", "to be finished"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "c1-u2-funktionsverbgefuege-e4",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Anlass geben zu\" = \"to give cause/occasion to\".",
                    TextBefore = "Der Vorfall gab Anlass ",
                    TextAfter = " einer Untersuchung.",
                    CorrectAnswer = "zu"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u3-nominalstil",
            Level = CefrLevel.C1,
            SortOrder = 3,
            Title = "Nominalstil vs. Verbalstil",
            Description = "Formal and academic German prefers packing meaning into nouns rather than verbs.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u3-nominalstil-e1",
                    Instruction = "Choose the more formal, nominal version.",
                    Explanation = "\"die Durchführung der Untersuchung\" packs the action into a noun phrase - typical of formal/scientific writing.",
                    Question = "Which is the nominal-style version of \"dass die Untersuchung durchgeführt wird\"?",
                    Options = ["die Durchführung der Untersuchung", "die Untersuchung wird gemacht", "man untersucht das", "die Untersuchung geschieht"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u3-nominalstil-e2",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Nominal style is denser and more common in reports, academic papers, and official documents.",
                    Question = "Nominal style (Nominalstil) is most typical of...",
                    Options = ["formal/academic writing", "casual conversation", "text messages", "children's stories"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u3-nominalstil-e3",
                    Instruction = "Choose the correct nominalization.",
                    Explanation = "\"die Entwicklung\" (development) is the nominal form of \"sich entwickeln\" (to develop).",
                    Question = "Nominal-style equivalent of \"dass sich die Wirtschaft entwickelt\"?",
                    Options = ["die Entwicklung der Wirtschaft", "die Wirtschaft entwickelt", "man entwickelt die Wirtschaft", "die Wirtschaft ist entwickelt"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u4-redewendungen",
            Level = CefrLevel.C1,
            SortOrder = 4,
            Title = "Idiomatische Redewendungen",
            Description = "Common idioms whose meaning isn't obvious from the individual words.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u4-redewendungen-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to press one's thumbs\" - the German equivalent of crossing your fingers.",
                    Question = "\"die Daumen drücken\" means?",
                    Options = ["to keep your fingers crossed", "to be under pressure", "to give a thumbs up", "to press a button"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u4-redewendungen-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to fall into the water\" - used for plans that get cancelled.",
                    Question = "\"ins Wasser fallen\" means?",
                    Options = ["to fall through / be cancelled", "to go swimming", "to fail a test", "to drown"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u4-redewendungen-e3",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to be on the wooden path\" - used for being mistaken about something.",
                    Question = "\"auf dem Holzweg sein\" means?",
                    Options = ["to be mistaken / on the wrong track", "to be lost in a forest", "to be very successful", "to work with wood"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u4-redewendungen-e4",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to hit the nail on the head\" - identical idiom to English.",
                    Question = "\"den Nagel auf den Kopf treffen\" means?",
                    Options = ["to describe something exactly right", "to hurt someone", "to build furniture", "to make a mistake"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u5-konjunktiv-i-wissenschaft",
            Level = CefrLevel.C1,
            SortOrder = 5,
            Title = "Konjunktiv I in wissenschaftlichen Texten",
            Description = "Using Konjunktiv I to report findings neutrally, without endorsing them as fact.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "c1-u5-konjunktiv-i-wissenschaft-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "Konjunktiv I of 'sein': das Ergebnis sei signifikant - reporting the study's claim without personally vouching for it.",
                    TextBefore = "Die Studie zeigt, das Ergebnis ",
                    TextAfter = " signifikant.",
                    CorrectAnswer = "sei"
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u5-konjunktiv-i-wissenschaft-e2",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Konjunktiv I lets a writer report a claim while explicitly not taking a position on whether it's true.",
                    Question = "Why do academic texts use Konjunktiv I when citing a source?",
                    Options = ["To report a claim neutrally, without endorsing it as fact", "To make the text sound more polite", "To ask a rhetorical question", "To express a wish"],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "c1-u5-konjunktiv-i-wissenschaft-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "Konjunktiv I of 'haben': die Forscher hätten neue Daten gefunden.",
                    TextBefore = "Der Autor behauptet, die Forscher ",
                    TextAfter = " neue Daten gefunden.",
                    CorrectAnswer = "hätten"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u6-textkohaerenz",
            Level = CefrLevel.C1,
            SortOrder = 6,
            Title = "Textkohärenz: höhere Konnektoren",
            Description = "Sophisticated connectors that hold together longer, more formal texts.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u6-textkohaerenz-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'des Weiteren' = \"furthermore\", used to add another point in formal writing.",
                    Question = "\"des Weiteren\" means?",
                    Options = ["furthermore", "however", "nevertheless", "for example"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u6-textkohaerenz-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'nichtsdestotrotz' = \"nevertheless/nonetheless\".",
                    Question = "\"nichtsdestotrotz\" means?",
                    Options = ["nevertheless", "therefore", "for instance", "meanwhile"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u6-textkohaerenz-e3",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'infolgedessen' = \"as a result/consequently\".",
                    Question = "\"infolgedessen\" means?",
                    Options = ["as a result / consequently", "on the other hand", "in addition", "for that reason it is doubted"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u6-textkohaerenz-e4",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "'diesbezüglich' = \"in this regard / concerning this\".",
                    Question = "\"diesbezüglich\" means?",
                    Options = ["in this regard / concerning this", "nevertheless", "immediately", "unfortunately"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u7-wortbildung",
            Level = CefrLevel.C1,
            SortOrder = 7,
            Title = "Wortbildung: Komposita und Ableitungen",
            Description = "How German builds complex words from compounds, prefixes, and suffixes.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u7-wortbildung-e1",
                    Instruction = "Choose the correct components.",
                    Explanation = "'Umweltverschmutzung' (environmental pollution) is built from 'Umwelt' (environment) + 'Verschmutzung' (pollution).",
                    Question = "\"Umweltverschmutzung\" is built from which two words?",
                    Options = ["Umwelt + Verschmutzung", "Umwelt + Schmutz", "Welt + Verschmutzung", "Um + Weltverschmutzung"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u7-wortbildung-e2",
                    Instruction = "Choose the correct noun.",
                    Explanation = "The suffix -heit turns an adjective into a noun: frei -> die Freiheit (freedom).",
                    Question = "Noun form of 'frei' (free) using -heit?",
                    Options = ["die Freiheit", "die Freiung", "der Freiling", "die Freischaft"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u7-wortbildung-e3",
                    Instruction = "Choose the correct noun.",
                    Explanation = "The suffix -schaft often forms abstract nouns: Freund -> die Freundschaft (friendship).",
                    Question = "Noun form of 'Freund' (friend) using -schaft?",
                    Options = ["die Freundschaft", "die Freundheit", "die Freundung", "der Freundling"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u7-wortbildung-e4",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "The prefix miss- often gives a negative/failed sense: misslingen = to fail (lit. \"not-succeed\").",
                    Question = "\"misslingen\" means?",
                    Options = ["to fail / not succeed", "to succeed greatly", "to misunderstand", "to mistrust"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u8-stilebenen",
            Level = CefrLevel.C1,
            SortOrder = 8,
            Title = "Stilebenen: formell, umgangssprachlich, fachsprachlich",
            Description = "Recognizing which register - formal, colloquial, or technical - fits a given context.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u8-stilebenen-e1",
                    Instruction = "Choose the correct register.",
                    Explanation = "'Kumpel' is a casual, colloquial word for a close friend/buddy.",
                    Question = "\"Kumpel\" belongs to which register?",
                    Options = ["umgangssprachlich (colloquial)", "formell (formal)", "fachsprachlich (technical)", "literarisch (literary)"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u8-stilebenen-e2",
                    Instruction = "Choose the correct register.",
                    Explanation = "\"werter Herr\" (esteemed sir) is an old-fashioned, very formal way of addressing someone.",
                    Question = "\"werter Herr\" belongs to which register?",
                    Options = ["sehr formell (very formal)", "umgangssprachlich (colloquial)", "neutral", "fachsprachlich (technical)"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u8-stilebenen-e3",
                    Instruction = "Choose the correct answer.",
                    Explanation = "Choosing the wrong register - e.g. being too casual in a job application - can come across as inappropriate even if the grammar is correct.",
                    Question = "Why does register matter even when grammar is correct?",
                    Options = ["The wrong register can sound inappropriate for the situation", "It changes the verb conjugation", "It changes the case system", "It has no real effect"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u9-argumentation",
            Level = CefrLevel.C1,
            SortOrder = 9,
            Title = "Argumentation und Debatte",
            Description = "Phrases for structuring a formal argument or debate.",
            Exercises =
            [
                new ClozeExercise
                {
                    Id = "c1-u9-argumentation-e1",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Dagegen spricht, dass...\" = \"Against this speaks the fact that...\" - introducing a counterargument.",
                    TextBefore = "",
                    TextAfter = " spricht, dass die Kosten steigen würden.",
                    CorrectAnswer = "Dagegen"
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u9-argumentation-e2",
                    Instruction = "Choose the correct translation.",
                    Explanation = "\"Zu bedenken ist außerdem, dass...\" = \"One must also consider that...\".",
                    Question = "\"Zu bedenken ist außerdem, dass...\" means?",
                    Options = ["One must also consider that...", "It is forbidden that...", "It is doubted that...", "One forgets that..."],
                    CorrectOptionIndex = 0
                },
                new ClozeExercise
                {
                    Id = "c1-u9-argumentation-e3",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Dies führt zu dem Schluss, dass...\" = \"This leads to the conclusion that...\".",
                    TextBefore = "Dies führt zu dem ",
                    TextAfter = ", dass eine Reform nötig ist.",
                    CorrectAnswer = "Schluss"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u10-literarische-analyse",
            Level = CefrLevel.C1,
            SortOrder = 10,
            Title = "Literarische und kulturelle Analyse",
            Description = "Vocabulary for discussing literature, film, and culture analytically.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u10-literarische-analyse-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Metapher' means metaphor.",
                    Question = "\"metaphor\" in German?",
                    Options = ["die Metapher", "die Ironie", "der Erzähler", "die Perspektive"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u10-literarische-analyse-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Erzähler' means narrator.",
                    Question = "\"narrator\" in German?",
                    Options = ["der Erzähler", "der Autor", "der Leser", "der Held"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u10-literarische-analyse-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Ironie' means irony.",
                    Question = "\"irony\" in German?",
                    Options = ["die Ironie", "die Metapher", "die Symbolik", "die Perspektive"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u10-literarische-analyse-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Perspektive' means perspective/point of view.",
                    Question = "\"perspective/point of view\" in German?",
                    Options = ["die Perspektive", "die Symbolik", "die Ironie", "der Erzähler"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u11-weitere-redewendungen",
            Level = CefrLevel.C1,
            SortOrder = 11,
            Title = "Weitere Redewendungen",
            Description = "More idioms with meanings well beyond their literal words.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u11-weitere-redewendungen-e1",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to buy the cat in the bag\" - to buy something without inspecting it first.",
                    Question = "\"die Katze im Sack kaufen\" means?",
                    Options = ["to buy something sight unseen / a pig in a poke", "to adopt a pet", "to go shopping for groceries", "to negotiate a good price"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u11-weitere-redewendungen-e2",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to step into the little fat pot\" - to make an embarrassing social mistake.",
                    Question = "\"ins Fettnäpfchen treten\" means?",
                    Options = ["to make an embarrassing social gaffe", "to slip and fall", "to cook a meal badly", "to gain weight"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u11-weitere-redewendungen-e3",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to have pig\" - an old idiom for being lucky.",
                    Question = "\"Schwein haben\" means?",
                    Options = ["to be lucky", "to eat too much", "to be dirty", "to own a farm"],
                    CorrectOptionIndex = 0
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u11-weitere-redewendungen-e4",
                    Instruction = "Choose the correct meaning.",
                    Explanation = "Literally \"to leave the church in the village\" - don't blow things out of proportion.",
                    Question = "\"die Kirche im Dorf lassen\" means?",
                    Options = ["to keep things in proportion / not overreact", "to go to church regularly", "to stay in one's hometown", "to respect religious traditions"],
                    CorrectOptionIndex = 0
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u12-fachsprache-recht",
            Level = CefrLevel.C1,
            SortOrder = 12,
            Title = "Fachsprache: Recht",
            Description = "Legal terminology.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u12-fachsprache-recht-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Klage' means lawsuit.",
                    Question = "\"lawsuit\" in German?",
                    Options = ["die Klage", "das Urteil", "die Berufung", "der Vertrag"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Klage", "lawsuit")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u12-fachsprache-recht-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Urteil' means verdict/judgment.",
                    Question = "\"verdict/judgment\" in German?",
                    Options = ["das Urteil", "die Klage", "das Gericht", "die Haftung"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Urteil", "verdict")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u12-fachsprache-recht-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Berufung' means (legal) appeal.",
                    Question = "\"(legal) appeal\" in German?",
                    Options = ["die Berufung", "die Klage", "das Urteil", "der Vertrag"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Berufung", "appeal")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u12-fachsprache-recht-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Haftung' means liability.",
                    Question = "\"liability\" in German?",
                    Options = ["die Haftung", "das Gericht", "die Klage", "die Berufung"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Haftung", "liability")
                },
                new ClozeExercise
                {
                    Id = "c1-u12-fachsprache-recht-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Er reicht eine Klage ein\" = \"He is filing a lawsuit\".",
                    TextBefore = "Er reicht eine ",
                    TextAfter = " ein.",
                    CorrectAnswer = "Klage"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u13-fachsprache-wirtschaft",
            Level = CefrLevel.C1,
            SortOrder = 13,
            Title = "Fachsprache: Wirtschaft",
            Description = "Advanced business and finance terminology.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u13-fachsprache-wirtschaft-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Fusion' means (corporate) merger.",
                    Question = "\"merger\" (of companies) in German?",
                    Options = ["die Fusion", "die Insolvenz", "die Rendite", "die Investition"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Fusion", "merger")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u13-fachsprache-wirtschaft-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Insolvenz' means insolvency/bankruptcy.",
                    Question = "\"insolvency/bankruptcy\" in German?",
                    Options = ["die Insolvenz", "die Fusion", "der Aktienmarkt", "das Kapital"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Insolvenz", "insolvency")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u13-fachsprache-wirtschaft-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Rendite' means (financial) return/yield.",
                    Question = "\"return/yield\" (on an investment) in German?",
                    Options = ["die Rendite", "das Kapital", "die Investition", "der Wettbewerb"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Rendite", "return/yield")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u13-fachsprache-wirtschaft-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'der Wettbewerb' means competition.",
                    Question = "\"competition\" in German?",
                    Options = ["der Wettbewerb", "die Rendite", "die Fusion", "die Effizienz"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Wettbewerb", "competition")
                },
                new ClozeExercise
                {
                    Id = "c1-u13-fachsprache-wirtschaft-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Das Unternehmen meldete Insolvenz an\" = \"The company filed for bankruptcy\".",
                    TextBefore = "Das Unternehmen meldete ",
                    TextAfter = " an.",
                    CorrectAnswer = "Insolvenz"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u14-abstrakte-konzepte",
            Level = CefrLevel.C1,
            SortOrder = 14,
            Title = "Abstrakte Konzepte",
            Description = "Abstract concepts used in philosophical and analytical discussion.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u14-abstrakte-konzepte-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Verantwortung' means responsibility.",
                    Question = "\"responsibility\" in German?",
                    Options = ["die Verantwortung", "die Freiheit", "die Identität", "die Moral"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Verantwortung", "responsibility")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u14-abstrakte-konzepte-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Bewusstsein' means consciousness/awareness.",
                    Question = "\"consciousness/awareness\" in German?",
                    Options = ["das Bewusstsein", "die Wahrnehmung", "die Autonomie", "das Dilemma"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Bewusstsein", "consciousness")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u14-abstrakte-konzepte-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Wahrnehmung' means perception.",
                    Question = "\"perception\" in German?",
                    Options = ["die Wahrnehmung", "das Bewusstsein", "die Identität", "die Verantwortung"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Wahrnehmung", "perception")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u14-abstrakte-konzepte-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'das Dilemma' means dilemma.",
                    Question = "\"dilemma\" in German?",
                    Options = ["das Dilemma", "die Autonomie", "die Moral", "die Freiheit"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Dilemma", "dilemma")
                },
                new ClozeExercise
                {
                    Id = "c1-u14-abstrakte-konzepte-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Er trägt die Verantwortung für die Entscheidung\" = \"He bears the responsibility for the decision\".",
                    TextBefore = "Er trägt die ",
                    TextAfter = " für die Entscheidung.",
                    CorrectAnswer = "Verantwortung"
                }
            ]
        },
        new CurriculumUnit
        {
            Id = "c1-u15-emotionale-nuancen",
            Level = CefrLevel.C1,
            SortOrder = 15,
            Title = "Feine emotionale Nuancen",
            Description = "Subtle emotional vocabulary beyond basic feelings.",
            Exercises =
            [
                new MultipleChoiceExercise
                {
                    Id = "c1-u15-emotionale-nuancen-e1",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Wehmut' means wistfulness/melancholy - a bittersweet longing.",
                    Question = "\"wistfulness/melancholy\" in German?",
                    Options = ["die Wehmut", "die Gelassenheit", "die Ehrfurcht", "die Demut"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Wehmut", "wistfulness")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u15-emotionale-nuancen-e2",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Zerrissenheit' means a state of inner conflict, being torn between two things.",
                    Question = "\"inner conflict / being torn\" in German?",
                    Options = ["die Zerrissenheit", "die Gelassenheit", "die Verbitterung", "die Wehmut"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Zerrissenheit", "inner conflict")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u15-emotionale-nuancen-e3",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Gelassenheit' means composure/calmness.",
                    Question = "\"composure/calmness\" in German?",
                    Options = ["die Gelassenheit", "die Zerrissenheit", "die Verbitterung", "die Ehrfurcht"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Gelassenheit", "composure")
                },
                new MultipleChoiceExercise
                {
                    Id = "c1-u15-emotionale-nuancen-e4",
                    Instruction = "Choose the correct German word.",
                    Explanation = "'die Ehrfurcht' means awe/reverence.",
                    Question = "\"awe/reverence\" in German?",
                    Options = ["die Ehrfurcht", "die Demut", "die Wehmut", "die Gelassenheit"],
                    CorrectOptionIndex = 0,
                    Teaches = new VocabularyTeaching("Ehrfurcht", "awe")
                },
                new ClozeExercise
                {
                    Id = "c1-u15-emotionale-nuancen-e5",
                    Instruction = "Fill in the blank.",
                    Explanation = "\"Sie blickte mit Wehmut zurück\" = \"She looked back with wistfulness\".",
                    TextBefore = "Sie blickte mit ",
                    TextAfter = " zurück.",
                    CorrectAnswer = "Wehmut"
                }
            ]
        }
    ];
}
