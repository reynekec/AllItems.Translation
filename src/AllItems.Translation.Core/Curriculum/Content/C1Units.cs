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
        }
    ];
}
