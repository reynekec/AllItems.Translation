namespace AllItems.Translation.Core.Curriculum.Content;

internal static class TeachingAreaGuideCatalog
{
    private static readonly IReadOnlyDictionary<string, TeachingAreaGuide> Guides =
        new Dictionary<string, TeachingAreaGuide>(StringComparer.Ordinal)
        {
            ["a1-u1-sein-haben"] = CreateGuide(
                overview: "This area teaches the two verbs you will use constantly from the very beginning: 'sein' for identity, state, and location, and 'haben' for possession and many everyday statements.",
                whyItMatters: "Without these verbs you cannot introduce yourself, describe people, say your age, talk about what you have, or form many basic past-tense structures later on.",
                whatToPractice: "Focus on recognizing which subject goes with which form, especially irregular forms like 'bin', 'bist', and 'ist'. The goal is fast recall, not just recognition.",
                examples:
                [
                    Example("Ich bin müde.", "I am tired.", "Use 'sein' to describe a state or condition."),
                    Example("Wir haben zwei Kinder.", "We have two children.", "Use 'haben' to talk about possession or what someone has.")
                ]),
            ["a1-u2-praesens"] = CreateGuide(
                overview: "This area introduces the normal present-tense pattern for regular verbs, which is the backbone for talking about everyday actions.",
                whyItMatters: "Once you know the regular endings, you can use hundreds of German verbs to talk about where you live, where you work, what you do, and what other people do.",
                whatToPractice: "Look for the stem and add the correct ending for each subject. The purpose is to make the verb agreement pattern feel automatic.",
                examples:
                [
                    Example("Ich wohne in Berlin.", "I live in Berlin.", "'ich' usually takes the -e ending with regular verbs."),
                    Example("Er kommt aus Deutschland.", "He comes from Germany.", "'er/sie/es' usually takes the -t ending.")
                ]),
            ["a1-u3-artikel"] = CreateGuide(
                overview: "This area helps you learn noun gender through the definite articles 'der', 'die', and 'das'.", 
                whyItMatters: "German nouns are learned together with their article because gender affects later grammar such as cases, pronouns, and adjective endings.",
                whatToPractice: "Train yourself to store the article as part of the word, not as optional extra information. The real skill is recognizing noun gender quickly.",
                examples:
                [
                    Example("der Mann", "the man", "Masculine nouns use 'der' in the nominative."),
                    Example("das Buch", "the book", "Neuter nouns use 'das'.")
                ]),
            ["a1-u4-verneinung"] = CreateGuide(
                overview: "This area teaches the two main ways German says 'not': 'nicht' for negating statements, adjectives, or specific parts of a sentence, and 'kein' for negating nouns.",
                whyItMatters: "Negation is essential for real communication because you constantly need to deny, correct, refuse, or say something is absent.",
                whatToPractice: "Notice whether you are negating a noun or something else. That single decision tells you whether you need 'kein' or 'nicht'.",
                examples:
                [
                    Example("Das ist nicht mein Auto.", "That is not my car.", "'nicht' negates the statement."),
                    Example("Ich habe keine Geschwister.", "I do not have any siblings.", "'keine' negates the noun 'Geschwister'.")
                ]),
            ["a1-u5-wortstellung"] = CreateGuide(
                overview: "This area introduces German word order, especially the idea that the verb usually sits in second position in statements and moves for questions.",
                whyItMatters: "Word order is one of the biggest differences from English. Even with the right vocabulary, a sentence can sound wrong if the verb is in the wrong place.",
                whatToPractice: "Pay attention to where the finite verb goes in statements, yes-no questions, and W-questions. This pattern controls almost every sentence you build.",
                examples:
                [
                    Example("Ich arbeite heute.", "I am working today.", "In a statement, the verb is the second element."),
                    Example("Woher kommen Sie?", "Where do you come from?", "In a W-question, the question word comes first and the verb follows.")
                ]),
            ["a1-u6-modalverben"] = CreateGuide(
                overview: "This area teaches common modal verbs used for ability and polite wants, especially 'können' and 'möchten'.",
                whyItMatters: "Modal verbs let you say what you can do, ask for help, and make requests politely, which is essential in everyday conversation.",
                whatToPractice: "Focus on the singular forms and the meaning shift they create. You are learning how tone and function change when a modal verb appears.",
                examples:
                [
                    Example("Ich kann Deutsch sprechen.", "I can speak German.", "'kann' expresses ability."),
                    Example("Wir möchten einen Kaffee.", "We would like a coffee.", "'möchten' is a polite way to ask for something.")
                ]),
            ["a1-u7-akkusativ"] = CreateGuide(
                overview: "This area introduces the accusative case, especially the key beginner rule that masculine articles change while feminine and neuter often stay the same.",
                whyItMatters: "You need the accusative for direct objects, which means it appears whenever you see, buy, need, or want something.",
                whatToPractice: "Train your eye to spot the direct object and watch the article change for masculine nouns. That is the core pattern the exercises are building.",
                examples:
                [
                    Example("Ich sehe den Mann.", "I see the man.", "'der' becomes 'den' because 'Mann' is a masculine direct object."),
                    Example("Ich kaufe das Buch.", "I buy the book.", "Neuter 'das' does not change here.")
                ]),
            ["a1-u8-zahlen-zeit"] = CreateGuide(
                overview: "This area builds the practical vocabulary for numbers, time, days, and dates used in schedules, appointments, shopping, and travel.",
                whyItMatters: "These are survival skills. You need them to understand prices, tell the time, arrange meetings, and follow daily life in German.",
                whatToPractice: "Aim for quick recognition rather than slow translation. The goal is to understand numbers and time expressions in real time.",
                examples:
                [
                    Example("Es ist neun Uhr.", "It is nine o'clock.", "A basic fixed pattern for telling the time."),
                    Example("Heute ist Dienstag.", "Today is Tuesday.", "Days and dates appear constantly in real conversation.")
                ]),
            ["a1-u9-familie-alltag"] = CreateGuide(
                overview: "This area teaches beginner vocabulary for family members and common daily-life actions.",
                whyItMatters: "These are the words you need for simple conversations about who people are, what your family is like, and what you do every day.",
                whatToPractice: "Connect the word directly to the idea instead of mentally translating every time. The aim is usable everyday vocabulary, not abstract memorization.",
                examples:
                [
                    Example("Das ist mein Bruder.", "That is my brother.", "Family vocabulary helps with introductions and personal topics."),
                    Example("Ich esse um acht Uhr.", "I eat at eight o'clock.", "Daily routine verbs are building blocks for simple conversation.")
                ]),
            ["a1-u10-plural"] = CreateGuide(
                overview: "This area teaches how German nouns form the plural, which is less predictable than in English and must often be learned word by word.",
                whyItMatters: "You need plural forms whenever you talk about more than one thing, and German uses several plural patterns rather than one simple ending.",
                whatToPractice: "Notice both the plural ending and any article change. The purpose is to get used to storing singular and plural together.",
                examples:
                [
                    Example("das Buch - die Bücher", "the book - the books", "Some plurals also add an umlaut."),
                    Example("die Frau - die Frauen", "the woman - the women", "Some plural patterns are more regular, but you still need to learn them actively.")
                ]),
            ["a1-u11-possessiv"] = CreateGuide(
                overview: "This area introduces possessive words like 'mein', 'dein', and 'sein' that show who something belongs to.",
                whyItMatters: "Possessives appear constantly in real speech when you talk about your family, your work, your things, and other people's things.",
                whatToPractice: "The main goal is to match the possessive to the owner and notice how it changes with gender and case in simple patterns.",
                examples:
                [
                    Example("Das ist mein Buch.", "That is my book.", "'mein' shows possession."),
                    Example("Ist das deine Tasche?", "Is that your bag?", "Possessives are essential for everyday questions and identification.")
                ]),
            ["a1-u12-imperativ"] = CreateGuide(
                overview: "This area teaches the imperative, the form German uses for instructions, requests, and commands.",
                whyItMatters: "You hear imperatives in classrooms, workplaces, directions, recipes, and everyday social situations.",
                whatToPractice: "Pay attention to who is being addressed and how direct or polite the form sounds. The skill here is understanding and producing short action-oriented sentences.",
                examples:
                [
                    Example("Komm bitte herein.", "Please come in.", "An informal command for one person."),
                    Example("Sprechen Sie bitte langsamer.", "Please speak more slowly.", "A formal request with 'Sie'.")
                ]),
            ["a1-u13-trennbare-verben"] = CreateGuide(
                overview: "This area introduces separable verbs at a beginner level, where a prefix splits off and moves to the end of the clause.",
                whyItMatters: "These verbs are everywhere in German, and understanding the split is necessary both for reading and for building natural sentences.",
                whatToPractice: "Watch the prefix and the verb together as one meaning unit, even when the pieces are separated in the sentence.",
                examples:
                [
                    Example("Ich stehe um sieben Uhr auf.", "I get up at seven o'clock.", "The prefix 'auf' moves to the end."),
                    Example("Wir kaufen heute ein.", "We are shopping today.", "The sentence still contains one separable verb idea.")
                ]),
            ["a1-u14-ort-richtung"] = CreateGuide(
                overview: "This area teaches core expressions for place and direction so you can describe where things are and where someone is going.",
                whyItMatters: "Location language is basic survival German for travel, directions, and everyday conversation.",
                whatToPractice: "The key skill is distinguishing static location from movement toward a destination, because German often marks that difference clearly.",
                examples:
                [
                    Example("Ich bin zu Hause.", "I am at home.", "Static location."),
                    Example("Ich gehe nach Hause.", "I am going home.", "Movement toward a destination.")
                ]),
            ["a1-u15-essen-trinken"] = CreateGuide(
                overview: "This area gives you food-and-drink vocabulary and the simple patterns needed for ordering, buying, and talking about meals.",
                whyItMatters: "This is practical German you will use in cafés, restaurants, shops, and social situations almost immediately.",
                whatToPractice: "Focus on pairing useful nouns and verbs into short real-life phrases you could actually say.",
                examples:
                [
                    Example("Ich möchte einen Kaffee.", "I would like a coffee.", "A common polite ordering phrase."),
                    Example("Wir essen heute Brot und Käse.", "We are eating bread and cheese today.", "Simple meal vocabulary in context.")
                ]),
            ["a1-u16-berufe"] = CreateGuide(
                overview: "This area teaches job titles and basic ways to say what someone does for work.",
                whyItMatters: "Talking about occupation is part of introductions, small talk, and many simple identity questions.",
                whatToPractice: "Combine profession words with 'sein' and simple personal information so you can answer common introduction questions naturally.",
                examples:
                [
                    Example("Ich bin Lehrer.", "I am a teacher.", "A basic identity statement with a job title."),
                    Example("Sie arbeitet als Ärztin.", "She works as a doctor.", "Another common way to speak about profession.")
                ]),

            ["a2-u1-perfekt-haben"] = CreateGuide(
                overview: "This area introduces the spoken past tense built with 'haben' plus a past participle, the default pattern for many verbs in everyday German.",
                whyItMatters: "If you want to talk about what you did yesterday, last week, or on holiday, this is one of the first past-tense tools you need.",
                whatToPractice: "The goal is to recognize the two-part structure quickly: auxiliary verb early in the sentence, participle later. That pattern is more important than memorizing isolated forms.",
                examples:
                [
                    Example("Ich habe einen Film gesehen.", "I watched a film.", "The auxiliary is 'habe'; the participle is 'gesehen'."),
                    Example("Wir haben Pizza gegessen.", "We ate pizza.", "Many common verbs form the spoken past this way.")
                ]),
            ["a2-u2-perfekt-sein"] = CreateGuide(
                overview: "This area teaches the Perfekt with 'sein', usually used with verbs of motion or change of state.",
                whyItMatters: "You need this to talk about going, coming, arriving, leaving, and other important real-world actions in the past.",
                whatToPractice: "Notice which verbs choose 'sein' instead of 'haben'. The big skill is learning that German groups certain past actions by type, not just by meaning.",
                examples:
                [
                    Example("Ich bin nach Hause gegangen.", "I went home.", "Movement verbs often use 'sein'."),
                    Example("Er ist mit dem Auto gefahren.", "He travelled by car.", "Travel and change-of-place verbs are typical here.")
                ]),
            ["a2-u3-praeteritum"] = CreateGuide(
                overview: "This area introduces the simple past forms most commonly kept in everyday German for verbs like 'sein', 'haben', and modal verbs.",
                whyItMatters: "Even though spoken German often prefers Perfekt, these forms still appear constantly in narration and ordinary speech.",
                whatToPractice: "Treat the high-frequency forms as useful chunks. The goal is not to master the whole literary past, but to become comfortable with the forms Germans actually keep using.",
                examples:
                [
                    Example("Ich war letztes Jahr in Italien.", "I was in Italy last year.", "'war' is a very common past form."),
                    Example("Er wollte nicht kommen.", "He did not want to come.", "Modal verbs often appear in Präteritum.")
                ]),
            ["a2-u4-dativ"] = CreateGuide(
                overview: "This area introduces the dative case, which often marks the receiver of something or follows certain verbs and prepositions.",
                whyItMatters: "You need the dative for common verbs like 'geben' and 'helfen', and for many expressions used in real conversation.",
                whatToPractice: "Learn to spot who receives the action. That role often triggers the dative and explains article changes like 'dem' and 'der'.",
                examples:
                [
                    Example("Ich gebe dem Mann das Buch.", "I give the man the book.", "The receiver 'dem Mann' takes the dative."),
                    Example("Kannst du mir helfen?", "Can you help me?", "'mir' is the dative form of 'ich'.")
                ]),
            ["a2-u5-trennbare-verben"] = CreateGuide(
                overview: "This area deepens your understanding of separable verbs so you can use them confidently in normal present-tense sentences.",
                whyItMatters: "German uses separable verbs constantly in daily life, and missing the prefix can make a sentence hard to understand.",
                whatToPractice: "Keep the full verb meaning in mind even when its pieces are split across the clause. That mental habit is the main skill being trained.",
                examples:
                [
                    Example("Ich stehe um sieben Uhr auf.", "I get up at seven o'clock.", "The prefix separates in a main clause."),
                    Example("Ich rufe dich morgen an.", "I will call you tomorrow.", "The verb idea stays whole even when the pieces move apart.")
                ]),
            ["a2-u6-komparativ-superlativ"] = CreateGuide(
                overview: "This area teaches how German compares things using forms like '-er' and 'am ...sten'.",
                whyItMatters: "Comparison is essential for describing preferences, giving opinions, and talking about differences between people, places, and options.",
                whatToPractice: "Watch for both regular patterns and common irregular forms like 'gut - besser - am besten'. The goal is to compare naturally, not mechanically.",
                examples:
                [
                    Example("Der Zug ist schneller als das Auto.", "The train is faster than the car.", "Comparative with 'als'."),
                    Example("Dieses Restaurant ist am besten.", "This restaurant is the best.", "Superlative with 'am'.")
                ]),
            ["a2-u7-nebensaetze"] = CreateGuide(
                overview: "This area introduces subordinate clauses with words like 'weil', 'dass', and 'wenn', where the conjugated verb moves to the end.",
                whyItMatters: "This is a major step toward connected, natural German because it lets you explain reasons, report thoughts, and talk about conditions.",
                whatToPractice: "The crucial habit is tracking the verb all the way to the end of the clause. That word-order shift is the real learning target.",
                examples:
                [
                    Example("Ich bleibe zu Hause, weil ich krank bin.", "I am staying home because I am ill.", "The verb 'bin' moves to the end of the subordinate clause."),
                    Example("Ich hoffe, dass du kommst.", "I hope that you are coming.", "'dass' also pushes the verb to the end.")
                ]),
            ["a2-u8-reflexivverben"] = CreateGuide(
                overview: "This area teaches reflexive verbs, where the action refers back to the subject through words like 'mich', 'dich', or 'sich'.",
                whyItMatters: "Many common German verbs are naturally reflexive, and using the wrong pronoun makes the sentence feel incomplete or incorrect.",
                whatToPractice: "Link the subject and reflexive pronoun automatically. The aim is to hear and produce these as fixed patterns.",
                examples:
                [
                    Example("Ich freue mich auf die Ferien.", "I am looking forward to the holidays.", "The reflexive pronoun must match the subject."),
                    Example("Er wäscht sich.", "He is washing himself.", "A simple action that points back to the subject.")
                ]),
            ["a2-u9-gesundheit"] = CreateGuide(
                overview: "This area builds vocabulary for the body, symptoms, and common health situations.",
                whyItMatters: "Health vocabulary is practical language you need for doctors, pharmacies, illness, and basic self-care conversations.",
                whatToPractice: "Connect the vocabulary to situations you may actually face, so the words are useful under pressure and not just remembered for a quiz.",
                examples:
                [
                    Example("Ich habe Kopfschmerzen.", "I have a headache.", "A high-frequency symptom pattern."),
                    Example("Mein Rücken tut weh.", "My back hurts.", "Useful for describing pain clearly.")
                ]),
            ["a2-u10-wegbeschreibung"] = CreateGuide(
                overview: "This area teaches the phrases and directional vocabulary used for giving and understanding routes.",
                whyItMatters: "Directions are practical survival German for travel, navigation, and helping other people in public spaces.",
                whatToPractice: "Focus on short action phrases and spatial words so you can follow instructions in sequence.",
                examples:
                [
                    Example("Gehen Sie geradeaus.", "Go straight ahead.", "A common direction command."),
                    Example("Biegen Sie links ab.", "Turn left.", "Another core routing phrase.")
                ]),
            ["a2-u11-kleidung-einkaufen"] = CreateGuide(
                overview: "This area teaches shopping and clothing language for describing items, sizes, and preferences.",
                whyItMatters: "Shopping language is highly practical and gives you repeated exposure to adjectives, colors, and polite requests.",
                whatToPractice: "Use the words in realistic request patterns, not just as isolated vocabulary items.",
                examples:
                [
                    Example("Haben Sie das in Größe M?", "Do you have that in size M?", "A useful shopping question."),
                    Example("Das Hemd ist zu teuer.", "The shirt is too expensive.", "Useful for describing and evaluating items.")
                ]),
            ["a2-u12-urlaub-reisen"] = CreateGuide(
                overview: "This area gives you vocabulary and expressions for holidays, transport, accommodation, and travel plans.",
                whyItMatters: "Travel German combines many core skills at once: past tense, booking language, locations, and practical vocabulary.",
                whatToPractice: "Train yourself to describe travel events and needs clearly, because this topic often mixes time, movement, and logistics.",
                examples:
                [
                    Example("Wir haben ein Hotelzimmer gebucht.", "We booked a hotel room.", "Travel planning in the past."),
                    Example("Der Zug fährt um acht Uhr ab.", "The train leaves at eight o'clock.", "Transport and schedule language.")
                ]),
            ["a2-u13-telefonieren"] = CreateGuide(
                overview: "This area teaches standard phrases used on the phone, where German often sounds slightly more formulaic than casual face-to-face speech.",
                whyItMatters: "Phone calls remove gestures and context, so fixed polite phrases become especially important.",
                whatToPractice: "Learn the routine expressions as chunks so you can open, continue, and close a call smoothly.",
                examples:
                [
                    Example("Hier spricht Anna Weber.", "Anna Weber speaking.", "A common phone self-introduction."),
                    Example("Kann ich bitte Herrn Müller sprechen?", "May I speak to Mr. Müller, please?", "A standard polite request on the phone.")
                ]),
            ["a2-u14-wohnungsuche"] = CreateGuide(
                overview: "This area builds vocabulary for flats, rooms, rent, and describing housing.",
                whyItMatters: "Housing is a real-life topic where you need to ask about cost, size, equipment, and location clearly.",
                whatToPractice: "Focus on combining descriptive words with practical questions, since this language is usually used for decisions and problem-solving.",
                examples:
                [
                    Example("Die Wohnung ist sehr hell.", "The flat is very bright.", "A common housing description."),
                    Example("Wie hoch ist die Miete?", "How high is the rent?", "A key practical question.")
                ]),
            ["a2-u15-feste-feiern"] = CreateGuide(
                overview: "This area teaches language for celebrations, invitations, and social occasions.",
                whyItMatters: "Social language is what turns grammar knowledge into real participation in everyday life and relationships.",
                whatToPractice: "Notice invitation phrases, response patterns, and event vocabulary you can reuse in many contexts.",
                examples:
                [
                    Example("Ich lade dich zu meiner Party ein.", "I am inviting you to my party.", "Invitation language in context."),
                    Example("Wir feiern morgen Geburtstag.", "We are celebrating a birthday tomorrow.", "A common social event statement.")
                ]),
            ["a2-u16-arbeit-schule"] = CreateGuide(
                overview: "This area expands vocabulary for work, study, and routine responsibilities.",
                whyItMatters: "These topics dominate many adult conversations and help you describe goals, schedules, and obligations.",
                whatToPractice: "Use the vocabulary to talk about routines and responsibilities with more detail than A1 allowed.",
                examples:
                [
                    Example("Ich arbeite in einer Firma.", "I work in a company.", "Basic work context language."),
                    Example("Sie lernt für die Prüfung.", "She is studying for the exam.", "Study-related routine language.")
                ]),

            ["b1-u1-konjunktiv-ii"] = CreateGuide(
                overview: "This area introduces Konjunktiv II for polite requests and hypothetical situations, a major step into more nuanced German.",
                whyItMatters: "It lets you sound less blunt, talk about unreal possibilities, and express wishes in a natural adult way.",
                whatToPractice: "Learn to hear the difference between simple fact and softened or hypothetical meaning. This is as much about tone as grammar.",
                examples:
                [
                    Example("Ich hätte gerne einen Kaffee.", "I would like a coffee.", "A standard polite request."),
                    Example("Wenn ich Zeit hätte, würde ich kommen.", "If I had time, I would come.", "A classic hypothetical pattern.")
                ]),
            ["b1-u2-passiv"] = CreateGuide(
                overview: "This area teaches the passive voice, where the focus moves from the doer to the action itself.",
                whyItMatters: "Passive structures are common in news, instructions, formal writing, and any situation where the result matters more than the actor.",
                whatToPractice: "Notice how 'werden' plus a participle lets German describe what is being done without naming who does it.",
                examples:
                [
                    Example("Das Haus wird gebaut.", "The house is being built.", "The action matters more than the builder."),
                    Example("Die Tür wird geschlossen.", "The door is being closed.", "A common passive pattern.")
                ]),
            ["b1-u3-relativsaetze"] = CreateGuide(
                overview: "This area teaches relative clauses, which let you add extra information to a noun inside the same sentence.",
                whyItMatters: "Relative clauses make your German more connected, descriptive, and natural by helping you combine ideas instead of speaking in short separate sentences.",
                whatToPractice: "Track which noun the relative pronoun refers to and what role it plays inside the clause. That is where case and form come from.",
                examples:
                [
                    Example("Der Mann, der dort steht, ist mein Vater.", "The man who is standing there is my father.", "The clause describes 'der Mann'."),
                    Example("Das Buch, das ich lese, ist interessant.", "The book that I am reading is interesting.", "The relative clause adds detail to the noun.")
                ]),
            ["b1-u4-genitiv"] = CreateGuide(
                overview: "This area introduces the genitive, a case often used to show possession or relationships between nouns.",
                whyItMatters: "Even when spoken German sometimes uses alternatives, the genitive is important for reading, formal writing, and understanding fixed expressions.",
                whatToPractice: "Focus on how articles and some noun endings change to show possession more formally.",
                examples:
                [
                    Example("das Auto des Mannes", "the man's car", "A classic genitive possession pattern."),
                    Example("die Tasche der Frau", "the woman's bag", "Feminine nouns often show the genitive mainly through the article.")
                ]),
            ["b1-u5-wechselpraepositionen"] = CreateGuide(
                overview: "This area teaches two-way prepositions, where the case changes depending on whether you mean location or movement.",
                whyItMatters: "This is a central German pattern for talking about objects in space, placement, movement, and daily actions.",
                whatToPractice: "Ask yourself 'where?' or 'to where?'. That meaning decision controls whether you need dative or accusative.",
                examples:
                [
                    Example("Das Buch liegt auf dem Tisch.", "The book is lying on the table.", "Static location uses the dative."),
                    Example("Ich lege das Buch auf den Tisch.", "I put the book onto the table.", "Movement toward a destination uses the accusative.")
                ]),
            ["b1-u6-adjektivdeklination"] = CreateGuide(
                overview: "This area teaches adjective endings, which change depending on the article, gender, number, and case.",
                whyItMatters: "Adjective endings are one of the last major systems that make intermediate German sound precise and native-like.",
                whatToPractice: "Instead of memorizing isolated tables, notice what information the article already gives and what the adjective still needs to show.",
                examples:
                [
                    Example("der große Mann", "the tall man", "After a definite article, the adjective often carries a lighter ending."),
                    Example("ein neues Auto", "a new car", "After 'ein', the adjective often carries more grammatical information.")
                ]),
            ["b1-u7-indirekte-fragen"] = CreateGuide(
                overview: "This area teaches indirect questions, which embed a question inside a larger sentence.",
                whyItMatters: "Indirect questions are essential for sounding polite, reporting uncertainty, and building more natural complex sentences.",
                whatToPractice: "Treat them like subordinate clauses: the main target is the end-position verb pattern.",
                examples:
                [
                    Example("Ich weiß nicht, ob er kommt.", "I do not know if he is coming.", "'ob' introduces an indirect yes-no question."),
                    Example("Kannst du mir sagen, wo der Bahnhof ist?", "Can you tell me where the station is?", "The question becomes embedded inside a larger sentence.")
                ]),
            ["b1-u8-futur"] = CreateGuide(
                overview: "This area teaches Futur I with 'werden' plus infinitive to talk about future actions or sometimes strong assumptions.",
                whyItMatters: "It gives you a clear way to talk about plans, predictions, and formal future statements.",
                whatToPractice: "Watch the two-part pattern and how it interacts with normal German word order. The meaning is often simpler than the form looks.",
                examples:
                [
                    Example("Ich werde morgen arbeiten.", "I will work tomorrow.", "A straightforward future statement."),
                    Example("Er wird bald kommen.", "He will come soon.", "Another common future pattern.")
                ]),
            ["b1-u9-konjunktionen"] = CreateGuide(
                overview: "This area expands your connector vocabulary with words like 'obwohl', 'damit', and 'trotzdem' that express contrast and purpose more precisely.",
                whyItMatters: "These connectors help you sound more thoughtful and organized, especially when comparing ideas or explaining goals.",
                whatToPractice: "Focus on the meaning each connector creates and whether it changes word order. Both matter equally here.",
                examples:
                [
                    Example("Obwohl es regnet, gehe ich spazieren.", "Although it is raining, I am going for a walk.", "Contrast with subordinate-clause word order."),
                    Example("Ich lerne Deutsch, damit ich einen guten Job bekomme.", "I am learning German so that I get a good job.", "Purpose with end-position verb order.")
                ]),
            ["b1-u10-meinungen"] = CreateGuide(
                overview: "This area teaches common phrases for giving opinions, agreeing, disagreeing, and supporting a point of view.",
                whyItMatters: "At B1 you start moving from factual survival German into discussion, argument, and personal perspective.",
                whatToPractice: "Learn reusable opinion frames so you can spend your mental energy on content rather than sentence-building from scratch.",
                examples:
                [
                    Example("Meiner Meinung nach ist das eine gute Idee.", "In my opinion, that is a good idea.", "A standard opinion phrase."),
                    Example("Ich stimme zu.", "I agree.", "A short but essential discussion response.")
                ]),
            ["b1-u11-arbeit-bewerbung"] = CreateGuide(
                overview: "This area builds vocabulary and phrases for jobs, applications, and professional self-presentation.",
                whyItMatters: "Professional German requires more precision and formality than everyday small talk.",
                whatToPractice: "Focus on describing qualifications, goals, and experience in a more formal register.",
                examples:
                [
                    Example("Ich habe Erfahrung im Kundenservice.", "I have experience in customer service.", "A practical application phrase."),
                    Example("Ich möchte mich bewerben.", "I would like to apply.", "A key professional action phrase.")
                ]),
            ["b1-u12-umwelt"] = CreateGuide(
                overview: "This area teaches vocabulary for environmental issues and talking about sustainability.",
                whyItMatters: "It supports news discussion, school topics, civic conversation, and modern public debate.",
                whatToPractice: "Use the terms in meaningful issue-based statements rather than as isolated topic words.",
                examples:
                [
                    Example("Wir müssen Energie sparen.", "We need to save energy.", "A common environmental statement."),
                    Example("Der Klimawandel ist ein großes Problem.", "Climate change is a major problem.", "Issue-based vocabulary in context.")
                ]),
            ["b1-u13-gesundheit-beratung"] = CreateGuide(
                overview: "This area goes beyond basic symptoms into advice, recommendations, and simple health-related discussion.",
                whyItMatters: "It helps you explain problems and understand suggestions in real care or wellbeing situations.",
                whatToPractice: "Pay attention to recommendation language and simple problem-solution structures.",
                examples:
                [
                    Example("Du solltest mehr schlafen.", "You should sleep more.", "Advice language."),
                    Example("Es wäre besser, wenn du zum Arzt gehst.", "It would be better if you went to the doctor.", "A softer recommendation pattern.")
                ]),
            ["b1-u14-kultur-freizeit"] = CreateGuide(
                overview: "This area builds vocabulary for hobbies, arts, entertainment, and cultural activities.",
                whyItMatters: "These are common topics in friendships, social life, and intermediate conversation.",
                whatToPractice: "Use the vocabulary to explain preferences and give reasons, not just list activities.",
                examples:
                [
                    Example("Ich interessiere mich für Theater.", "I am interested in theatre.", "A useful preference pattern."),
                    Example("Am Wochenende gehe ich oft ins Museum.", "I often go to the museum at the weekend.", "Leisure activity in context.")
                ]),
            ["b1-u15-reisen-erfahrungen"] = CreateGuide(
                overview: "This area helps you describe travel experiences, impressions, and comparisons in more detail than beginner travel language.",
                whyItMatters: "It supports storytelling, reflection, and richer conversation about places and events.",
                whatToPractice: "Combine past events with opinions and descriptive language so your communication becomes more narrative.",
                examples:
                [
                    Example("Die Reise war anstrengend, aber interessant.", "The trip was tiring but interesting.", "Describing an experience."),
                    Example("Am besten hat mir Wien gefallen.", "I liked Vienna best.", "Combining travel with opinion and comparison.")
                ]),
            ["b1-u16-lernen-strategien"] = CreateGuide(
                overview: "This area teaches vocabulary for learning, progress, difficulty, and study strategies.",
                whyItMatters: "It lets you talk about how you learn, what helps you, and what challenges you face in a more reflective way.",
                whatToPractice: "Use the topic language to describe process and improvement, not only facts.",
                examples:
                [
                    Example("Ich lerne am besten mit Beispielen.", "I learn best with examples.", "Talking about personal strategy."),
                    Example("Mir fällt die Aussprache noch schwer.", "I still find pronunciation difficult.", "A realistic reflection phrase.")
                ]),

            ["b2-u1-konjunktiv-i"] = CreateGuide(
                overview: "This area introduces Konjunktiv I for reported speech, a form used to relay what someone says without fully adopting it as fact.",
                whyItMatters: "It is especially important in journalism, formal reporting, and advanced reading where neutrality matters.",
                whatToPractice: "The goal is to hear the distance it creates between the speaker and the statement. This is a meaning skill as much as a grammar skill.",
                examples:
                [
                    Example("Er sagt, er sei müde.", "He says he is tired.", "The speaker reports the claim rather than endorsing it directly."),
                    Example("Sie sagt, sie habe keine Zeit.", "She says she has no time.", "Another standard reported-speech pattern.")
                ]),
            ["b2-u2-passiv-erweitert"] = CreateGuide(
                overview: "This area expands passive structures to include modal verbs and the distinction between a process and a resulting state.",
                whyItMatters: "Advanced German often needs finer distinctions such as whether something is happening now or is already in the completed state.",
                whatToPractice: "Focus on what kind of meaning the sentence carries: ongoing action, obligation, or resulting condition.",
                examples:
                [
                    Example("Der Brief muss geschrieben werden.", "The letter must be written.", "Modal verb plus passive structure."),
                    Example("Die Tür ist geöffnet.", "The door is open.", "State passive describing a result, not the action itself.")
                ]),
            ["b2-u3-konjunktiv-ii-vergangenheit"] = CreateGuide(
                overview: "This area teaches how German talks about unreal past situations, the equivalent of 'would have', 'could have', or 'should have'.",
                whyItMatters: "It is essential for regret, hindsight, counterfactual reasoning, and nuanced discussion of past alternatives.",
                whatToPractice: "Think in terms of unreal past meaning first. The form matters, but the real skill is expressing what did not actually happen.",
                examples:
                [
                    Example("Wenn ich Zeit gehabt hätte, wäre ich gekommen.", "If I had had time, I would have come.", "A classic unreal past sentence."),
                    Example("Ich hätte das nicht gemacht.", "I would not have done that.", "Counterfactual reflection on a past action.")
                ]),
            ["b2-u4-partizipialattribute"] = CreateGuide(
                overview: "This area teaches participles used as compact descriptive phrases before nouns, common in dense written German.",
                whyItMatters: "It helps you read formal and literary texts where whole relative clauses are compressed into a shorter noun phrase.",
                whatToPractice: "Notice how a long idea gets packed into an adjective-like structure before the noun. The main skill is decoding compressed information.",
                examples:
                [
                    Example("der schnell fahrende Zug", "the fast-moving train", "A present participle used as an adjective."),
                    Example("das von mir geschriebene Buch", "the book written by me", "A past participle phrase before the noun.")
                ]),
            ["b2-u5-nominalisierung"] = CreateGuide(
                overview: "This area teaches how German turns actions and processes into nouns, a style especially common in formal writing.",
                whyItMatters: "Nominalization is a major feature of academic, administrative, and professional German.",
                whatToPractice: "Learn to recognize when a noun is standing in for a whole action. This helps both reading comprehension and advanced writing.",
                examples:
                [
                    Example("die Entscheidung", "the decision", "A noun formed from a verb."),
                    Example("Das Lernen macht Spaß.", "Learning is fun.", "An infinitive used as a noun.")
                ]),
            ["b2-u6-konnektoren"] = CreateGuide(
                overview: "This area introduces more sophisticated connectors that let you compare, balance, and structure ideas precisely.",
                whyItMatters: "At B2, strong communication depends on how well you connect thoughts, not just whether each sentence is correct on its own.",
                whatToPractice: "Treat these expressions as tools for logic and structure. The skill is organizing thought clearly in German.",
                examples:
                [
                    Example("Je mehr ich lerne, desto besser verstehe ich.", "The more I learn, the better I understand.", "A proportional comparison pattern."),
                    Example("Sowohl er als auch sie kommen.", "Both he and she are coming.", "A connector for combining parallel elements.")
                ]),
            ["b2-u7-relativsaetze-erweitert"] = CreateGuide(
                overview: "This area extends relative clauses beyond basic 'der/die/das' to patterns used for whole ideas, places, and possession.",
                whyItMatters: "These forms make advanced writing and careful description much more flexible and natural.",
                whatToPractice: "Notice what the relative word refers to: a noun, a place, possession, or even a whole earlier statement.",
                examples:
                [
                    Example("Das, was du sagst, ist wichtig.", "What you are saying is important.", "'was' can refer to an indefinite or abstract idea."),
                    Example("Der Mann, dessen Auto kaputt ist, wartet hier.", "The man whose car is broken is waiting here.", "'dessen' marks possession inside the relative clause.")
                ]),
            ["b2-u8-formelle-korrespondenz"] = CreateGuide(
                overview: "This area teaches the standard formulas and register choices used in formal letters and emails.",
                whyItMatters: "Formal writing in German follows strong conventions, and using the wrong phrase can sound too casual or unprofessional.",
                whatToPractice: "Learn the opening, request, and closing formulas as dependable chunks that can be reused in real communication.",
                examples:
                [
                    Example("Sehr geehrte Damen und Herren,", "Dear Sir or Madam,", "A standard formal greeting."),
                    Example("Mit freundlichen Grüßen", "Kind regards / Sincerely", "A standard formal closing.")
                ]),
            ["b2-u9-position-vertreten"] = CreateGuide(
                overview: "This area teaches how to argue a position, weigh pros and cons, and introduce claims in a measured way.",
                whyItMatters: "B2 often requires you to discuss issues, not just describe them. That means structuring an argument clearly.",
                whatToPractice: "Use the phrases as frameworks for thought: presenting one side, the other side, and a conclusion.",
                examples:
                [
                    Example("Einerseits ist es teuer, andererseits spart man Zeit.", "On the one hand it is expensive, on the other hand it saves time.", "A balanced argument structure."),
                    Example("Man könnte argumentieren, dass ...", "One could argue that ...", "A softer way to introduce a claim.")
                ]),
            ["b2-u10-medien"] = CreateGuide(
                overview: "This area builds the vocabulary needed to discuss news, sources, reporting, and media credibility.",
                whyItMatters: "It supports mature conversation about information, reliability, and public discourse.",
                whatToPractice: "Focus on using the vocabulary to evaluate information, not just name media objects.",
                examples:
                [
                    Example("Die Schlagzeile ist irreführend.", "The headline is misleading.", "Media discussion vocabulary in context."),
                    Example("Diese Quelle ist glaubwürdig.", "This source is credible.", "Evaluating reliability.")
                ]),
            ["b2-u11-politik-gesellschaft"] = CreateGuide(
                overview: "This area teaches vocabulary for politics, institutions, and social life.",
                whyItMatters: "It helps you understand public debate, news reporting, and higher-level discussion topics.",
                whatToPractice: "Use the words to describe systems, roles, and issues rather than memorizing them as isolated labels.",
                examples:
                [
                    Example("Die Regierung plant neue Gesetze.", "The government is planning new laws.", "A typical public-affairs sentence."),
                    Example("Jeder Bürger hat gleiche Rechte.", "Every citizen has equal rights.", "Society-related vocabulary in context.")
                ]),
            ["b2-u12-wirtschaft"] = CreateGuide(
                overview: "This area builds vocabulary for work, money, business, and economic issues.",
                whyItMatters: "Economic language appears often in news, professional settings, and advanced discussions about society.",
                whatToPractice: "Link the terms to causes, consequences, and comparisons so they become usable in analysis.",
                examples:
                [
                    Example("Die Preise steigen weiter.", "Prices are continuing to rise.", "A common economic statement."),
                    Example("Das Unternehmen macht Gewinn.", "The company is making a profit.", "Business language in context.")
                ]),
            ["b2-u13-bildung-forschung"] = CreateGuide(
                overview: "This area gives you vocabulary for education, research, and structured academic discussion.",
                whyItMatters: "It supports reading articles, discussing institutions, and handling more abstract formal topics.",
                whatToPractice: "Use the words inside claim-and-support sentences, since that is how this register usually works.",
                examples:
                [
                    Example("Die Studie liefert neue Ergebnisse.", "The study provides new results.", "Research language in context."),
                    Example("Bildung spielt eine zentrale Rolle.", "Education plays a central role.", "Formal topic statement.")
                ]),
            ["b2-u14-gesellschaftliche-probleme"] = CreateGuide(
                overview: "This area teaches language for social problems, challenges, and public-policy topics.",
                whyItMatters: "Advanced communication increasingly involves social issues rather than only personal life.",
                whatToPractice: "Focus on expressing problems, causes, and possible responses in a clear structured way.",
                examples:
                [
                    Example("Arbeitslosigkeit bleibt ein großes Problem.", "Unemployment remains a major problem.", "Issue framing vocabulary."),
                    Example("Man muss langfristige Lösungen finden.", "Long-term solutions must be found.", "Problem-solving language.")
                ]),
            ["b2-u15-kultur-interpretation"] = CreateGuide(
                overview: "This area helps you discuss culture, meaning, and interpretation with more analytical language.",
                whyItMatters: "It supports essays, discussions, and any context where you must go beyond simple likes and dislikes.",
                whatToPractice: "Use the vocabulary to support interpretations and explain why something has a certain effect or meaning.",
                examples:
                [
                    Example("Der Film behandelt das Thema Freiheit.", "The film deals with the theme of freedom.", "Analytical culture language."),
                    Example("Die Symbolik wirkt sehr stark.", "The symbolism is very powerful.", "Interpretive evaluation.")
                ]),
            ["b2-u16-verhandlung-kompromiss"] = CreateGuide(
                overview: "This area teaches language for negotiation, compromise, and resolving differences.",
                whyItMatters: "It is useful in work, group decisions, and advanced interpersonal communication where agreement must be built.",
                whatToPractice: "Focus on softening, proposing, and balancing interests rather than speaking in absolute terms.",
                examples:
                [
                    Example("Wir sollten einen Kompromiss finden.", "We should find a compromise.", "A cooperative negotiation phrase."),
                    Example("Wären Sie mit diesem Vorschlag einverstanden?", "Would you agree with this proposal?", "A polite negotiating question.")
                ]),

            ["c1-u1-modalpartikeln"] = CreateGuide(
                overview: "This area teaches modal particles, the small words that often do not change literal meaning but strongly change tone, attitude, and nuance.",
                whyItMatters: "Understanding modal particles is a big step toward sounding natural and interpreting what native speakers really mean.",
                whatToPractice: "Listen for emotional shading and speaker attitude. The point is not dictionary meaning, but what the particle adds in context.",
                examples:
                [
                    Example("Das ist doch klar!", "That is obviously clear!", "'doch' adds emphasis and shared expectation."),
                    Example("Komm mal her.", "Come here for a moment.", "'mal' softens the request and makes it sound casual.")
                ]),
            ["c1-u2-funktionsverbgefuege"] = CreateGuide(
                overview: "This area teaches noun-plus-light-verb constructions common in formal and institutional German.",
                whyItMatters: "These expressions appear everywhere in official, academic, and professional language and often replace simpler verbs.",
                whatToPractice: "Learn them as fixed meaning units instead of trying to interpret each word literally every time.",
                examples:
                [
                    Example("zur Anwendung kommen", "to be applied / to come into use", "A formal expression that functions like a single verb idea."),
                    Example("in Frage stellen", "to question / call into doubt", "Another common formal chunk.")
                ]),
            ["c1-u3-nominalstil"] = CreateGuide(
                overview: "This area teaches the contrast between nominal style and verbal style, especially in academic and bureaucratic writing.",
                whyItMatters: "Nominal style makes German dense and abstract, and recognizing it is crucial for advanced reading and formal writing.",
                whatToPractice: "Notice how a whole action can be compressed into a noun phrase. That compression is the real reading challenge.",
                examples:
                [
                    Example("die Durchführung der Untersuchung", "the carrying out of the investigation", "A nominal-style phrase."),
                    Example("die Entwicklung der Wirtschaft", "the development of the economy", "Meaning packed into a noun structure.")
                ]),
            ["c1-u4-redewendungen"] = CreateGuide(
                overview: "This area introduces idioms whose meaning cannot be fully predicted from the individual words.",
                whyItMatters: "Idioms are central to natural understanding and often reveal cultural ways of expressing ideas.",
                whatToPractice: "Treat the idiom as one meaning unit and connect it to the real-life situation where a native speaker would use it.",
                examples:
                [
                    Example("die Daumen drücken", "to keep your fingers crossed", "A fixed idiomatic meaning."),
                    Example("auf dem Holzweg sein", "to be on the wrong track", "The literal picture is not the real intended meaning.")
                ]),
            ["c1-u5-konjunktiv-i-wissenschaft"] = CreateGuide(
                overview: "This area teaches Konjunktiv I as used in academic and formal source reporting.",
                whyItMatters: "It helps writers report findings neutrally without claiming them as established fact.",
                whatToPractice: "Pay attention to the stance it creates: distance, neutrality, and disciplined reporting.",
                examples:
                [
                    Example("Die Studie zeigt, das Ergebnis sei signifikant.", "The study shows that the result is reportedly significant.", "A neutral academic reporting style."),
                    Example("Der Autor behauptet, die Forscher hätten neue Daten gefunden.", "The author claims the researchers found new data.", "Reported information without full endorsement.")
                ]),
            ["c1-u6-textkohaerenz"] = CreateGuide(
                overview: "This area teaches higher-level connectors that hold together longer formal texts logically and rhetorically.",
                whyItMatters: "At C1, strong writing depends heavily on how clearly ideas are linked across sentences and paragraphs.",
                whatToPractice: "Use connectors as structural signals for readers, not as decorative vocabulary.",
                examples:
                [
                    Example("Des Weiteren ist zu beachten, dass ...", "Furthermore, it should be noted that ...", "A formal additive connector."),
                    Example("Infolgedessen änderte sich die Lage.", "As a result, the situation changed.", "A consequence-marking connector.")
                ]),
            ["c1-u7-wortbildung"] = CreateGuide(
                overview: "This area teaches how German forms complex words through compounds, prefixes, and suffixes.",
                whyItMatters: "Advanced learners read and hear long unfamiliar words all the time, and word-building knowledge helps you decode them efficiently.",
                whatToPractice: "Break complex words into meaningful parts and infer how those parts shape the whole idea.",
                examples:
                [
                    Example("Umweltverschmutzung", "environmental pollution", "A compound noun whose parts reveal its meaning."),
                    Example("Freiheit", "freedom", "A noun created from an adjective using the suffix '-heit'.")
                ]),
            ["c1-u8-stilebenen"] = CreateGuide(
                overview: "This area teaches register awareness: how formal, colloquial, technical, or literary language fits different situations.",
                whyItMatters: "At high levels, sounding correct is not enough. You also need to sound appropriate to the context.",
                whatToPractice: "Notice when a word choice changes social tone or professionalism. Register control is a major part of advanced fluency.",
                examples:
                [
                    Example("Kumpel", "buddy / pal", "A clearly colloquial choice."),
                    Example("werter Herr", "esteemed sir", "A very formal, old-fashioned register marker.")
                ]),
            ["c1-u9-argumentation"] = CreateGuide(
                overview: "This area teaches phrases for building, qualifying, and countering arguments in a structured debate style.",
                whyItMatters: "Advanced writing and speaking often depend on how well you frame a claim, evidence, objection, and conclusion.",
                whatToPractice: "Use the phrases as scaffolding for reasoning so your German argument stays clear under complexity.",
                examples:
                [
                    Example("Dagegen spricht, dass die Kosten steigen würden.", "What speaks against it is that costs would rise.", "A counterargument frame."),
                    Example("Dies führt zu dem Schluss, dass ...", "This leads to the conclusion that ...", "A conclusion frame.")
                ]),
            ["c1-u10-literarische-analyse"] = CreateGuide(
                overview: "This area teaches the vocabulary needed to discuss literature, film, and culture analytically rather than just personally.",
                whyItMatters: "It allows you to interpret works, discuss themes, and explain artistic choices with precision.",
                whatToPractice: "Connect the terms to actual interpretive moves such as naming a perspective, symbol, or narrative device.",
                examples:
                [
                    Example("Die Perspektive verändert die Wirkung der Szene.", "The perspective changes the effect of the scene.", "Analytical discussion language."),
                    Example("Die Metapher verstärkt das Thema.", "The metaphor intensifies the theme.", "A basic literary-analysis move.")
                ]),
            ["c1-u11-weitere-redewendungen"] = CreateGuide(
                overview: "This area expands your idiom knowledge with more culturally loaded expressions used in natural native speech.",
                whyItMatters: "The more idioms you understand, the less often native German feels mysterious or overly indirect.",
                whatToPractice: "Map each idiom to the social moment it belongs to. That helps you know when it is natural to use and easier to understand when others do.",
                examples:
                [
                    Example("ins Fettnäpfchen treten", "to make an embarrassing social blunder", "An idiom for a social mistake."),
                    Example("Schwein haben", "to be lucky", "A culturally fixed idiomatic meaning.")
                ]),
            ["c1-u12-fachsprache-recht"] = CreateGuide(
                overview: "This area introduces legal and quasi-legal vocabulary used in court, contracts, and public institutions.",
                whyItMatters: "Specialized registers become important at C1 because advanced reading often crosses into professional and institutional language.",
                whatToPractice: "Treat the terms as part of a system of formal meaning, where precision matters more than conversational flexibility.",
                examples:
                [
                    Example("Die Klage ist noch offen.", "The lawsuit is still open.", "A legal-status phrase."),
                    Example("Das Urteil wurde verkündet.", "The judgment was announced.", "A formal legal statement.")
                ]),
            ["c1-u13-wissenschaftssprache"] = CreateGuide(
                overview: "This area teaches academic vocabulary and the style conventions used in scholarly discussion.",
                whyItMatters: "It supports essays, reports, research summaries, and advanced reading in knowledge-heavy domains.",
                whatToPractice: "Use the expressions to describe claims, evidence, and method in a neutral structured way.",
                examples:
                [
                    Example("Die Ergebnisse deuten darauf hin, dass ...", "The results suggest that ...", "Academic caution and interpretation."),
                    Example("Die Analyse basiert auf mehreren Quellen.", "The analysis is based on several sources.", "Method-focused academic phrasing.")
                ]),
            ["c1-u14-politische-rhetorik"] = CreateGuide(
                overview: "This area teaches the language of persuasion, framing, and public argument often used in politics and civic debate.",
                whyItMatters: "Advanced understanding of public discourse depends on hearing not just content, but rhetorical positioning.",
                whatToPractice: "Notice how speakers frame issues, appeal to values, and influence interpretation through wording.",
                examples:
                [
                    Example("Diese Maßnahme ist alternativlos.", "This measure has no alternative.", "A framing phrase that shapes debate."),
                    Example("Wir stehen vor einer historischen Aufgabe.", "We stand before a historic task.", "Rhetoric that elevates urgency and significance.")
                ]),
            ["c1-u15-kulturvergleich"] = CreateGuide(
                overview: "This area builds language for comparing cultures, norms, and perspectives with nuance rather than stereotype.",
                whyItMatters: "Advanced intercultural communication depends on careful comparison and respectful precision.",
                whatToPractice: "Focus on hedging, contrast, and contextual explanation rather than absolute claims.",
                examples:
                [
                    Example("Im Vergleich dazu wirkt ...", "By comparison, ... seems ...", "A comparison frame."),
                    Example("Das hängt stark vom gesellschaftlichen Kontext ab.", "That depends strongly on the social context.", "Context-sensitive analysis.")
                ]),
            ["c1-u16-abstrakte-themen"] = CreateGuide(
                overview: "This area teaches the language needed to discuss abstract concepts like identity, justice, responsibility, or progress.",
                whyItMatters: "At C1, fluency includes handling ideas that are not tied to immediate concrete situations.",
                whatToPractice: "Use abstract terms inside reasoned statements, explanations, and contrasts so they become active tools rather than passive vocabulary.",
                examples:
                [
                    Example("Verantwortung spielt dabei eine zentrale Rolle.", "Responsibility plays a central role in this.", "Abstract concept used analytically."),
                    Example("Die Frage nach Gerechtigkeit bleibt offen.", "The question of justice remains open.", "An abstract discussion frame.")
                ]),

            ["c2-u1-rhetorische-mittel"] = CreateGuide(
                overview: "This area teaches advanced rhetorical devices such as irony, sarcasm, exaggeration, and understatement.",
                whyItMatters: "These devices shape persuasion, humor, criticism, and literary tone at a near-native level.",
                whatToPractice: "The key skill is not only naming the device, but feeling what effect it has on meaning and audience response.",
                examples:
                [
                    Example("Ich habe dir das schon tausendmal gesagt.", "I have told you that a thousand times.", "Hyperbole for emphasis rather than literal fact."),
                    Example("Nicht schlecht.", "Not bad.", "An understatement that can mean something quite positive.")
                ]),
            ["c2-u2-synonym-nuancen"] = CreateGuide(
                overview: "This area explores near-synonyms whose difference lies in tone, register, connotation, or subtle usage conditions.",
                whyItMatters: "Near-native fluency depends on choosing the word that feels exactly right, not just one that is roughly correct.",
                whatToPractice: "Pay attention to stylistic coloring and situational fit. The skill is precision in natural expression.",
                examples:
                [
                    Example("gucken", "to look", "More casual and colloquial than several alternatives."),
                    Example("starren", "to stare", "Carries a much stronger, more intense nuance than neutral 'schauen'.")
                ]),
            ["c2-u3-amtsdeutsch"] = CreateGuide(
                overview: "This area teaches the dense, formal language used in legal, bureaucratic, and administrative texts.",
                whyItMatters: "Even highly fluent learners can struggle with this register because it compresses meaning and uses specialized phrases.",
                whatToPractice: "Read for function and legal effect rather than everyday conversational meaning. Precision is the main goal.",
                examples:
                [
                    Example("vorbehaltlich weiterer Prüfung", "subject to further review", "A formal limiting phrase."),
                    Example("im Sinne des Gesetzes", "within the meaning of the law", "A standard legal framing expression.")
                ]),
            ["c2-u4-sprichwoerter"] = CreateGuide(
                overview: "This area teaches proverbs, which carry cultural knowledge and condensed life lessons beyond literal wording.",
                whyItMatters: "Proverbs reveal how native speakers summarize experience, values, and expectations in compact idiomatic form.",
                whatToPractice: "Treat each proverb as a cultural message and learn the situations where a speaker would use it.",
                examples:
                [
                    Example("Der Apfel fällt nicht weit vom Stamm.", "The apple does not fall far from the tree.", "Children resemble their parents."),
                    Example("Wer A sagt, muss auch B sagen.", "If you say A, you must also say B.", "If you start something, you must follow through.")
                ]),
            ["c2-u5-archaische-formen"] = CreateGuide(
                overview: "This area teaches older forms and constructions still found in literature, poetry, and fixed elevated expressions.",
                whyItMatters: "Recognizing archaic forms is important for deep reading, especially when older texts or stylized modern writing appear.",
                whatToPractice: "Do not aim to use these constantly in speech. The main skill is recognition and interpretation in context.",
                examples:
                [
                    Example("des Nachts", "at night", "An archaic genitive-of-time pattern."),
                    Example("vonnöten sein", "to be necessary", "An older elevated formulation.")
                ]),
            ["c2-u6-partikel-feinheiten"] = CreateGuide(
                overview: "This area sharpens your understanding of the most subtle particle meanings, where one small word can signal doubt, concession, or softening.",
                whyItMatters: "At C2, nuance often lives in particles and tone rather than big grammar structures.",
                whatToPractice: "Focus on how context changes interpretation. The same particle can do different work in different situations.",
                examples:
                [
                    Example("Er wird wohl kommen.", "He will probably come.", "'wohl' marks assumption or probability."),
                    Example("Mag sein, dass er recht hat.", "It may be that he is right.", "A concessive, slightly distancing phrase.")
                ]),
            ["c2-u7-textsorten-stil"] = CreateGuide(
                overview: "This area teaches how style changes across genres such as journalism, science, bureaucracy, and literature.",
                whyItMatters: "Near-native reading and writing depend on recognizing genre expectations and adapting to them.",
                whatToPractice: "Notice how structure, density, tone, and implied audience shift from one text type to another.",
                examples:
                [
                    Example("Kurze Sätze und ein klarer Einstieg kennzeichnen journalistische Texte.", "Short sentences and a clear lead characterize journalistic texts.", "Genre-linked style awareness."),
                    Example("Wissenschaftliche Texte verdichten Inhalte stärker.", "Scientific texts condense content more strongly.", "A contrast in genre expectations.")
                ]),
            ["c2-u8-wortspiele"] = CreateGuide(
                overview: "This area teaches how German creates humor and layered meaning through puns, ambiguity, and sound-based play.",
                whyItMatters: "Wordplay is one of the clearest signs of near-native comprehension because it requires holding multiple meanings at once.",
                whatToPractice: "Train yourself to notice ambiguity instantly instead of forcing one single meaning too early.",
                examples:
                [
                    Example("Wenn Fliegen hinter Fliegen fliegen, fliegen Fliegen Fliegen nach.", "When flies fly behind flies, flies fly after flies.", "A classic pun on 'Fliegen' as noun and verb."),
                    Example("mehrdeutig", "ambiguous / having multiple meanings", "A key concept for understanding wordplay.")
                ]),
            ["c2-u9-rhetorische-strukturen"] = CreateGuide(
                overview: "This area teaches advanced rhetorical structures such as chiasmus, anaphora, and parallelism.",
                whyItMatters: "These patterns shape speeches, slogans, and literary language by creating rhythm, emphasis, and memorability.",
                whatToPractice: "Look at form as meaning. The arrangement of phrases itself becomes part of the message.",
                examples:
                [
                    Example("Wir werden kämpfen, wir werden nicht aufgeben, wir werden siegen.", "We will fight, we will not give up, we will win.", "Anaphora through repeated clause openings."),
                    Example("Nicht der ist frei, der tut, was er will, sondern der will, was er tut.", "Not the one is free who does what he wants, but the one wants what he does.", "A crossed chiasmus structure.")
                ]),
            ["c2-u10-lyrik-prosa"] = CreateGuide(
                overview: "This area teaches the concepts and vocabulary needed for close reading of poetry and literary prose.",
                whyItMatters: "Literary interpretation draws on the full range of language ability, from grammar to tone to cultural nuance.",
                whatToPractice: "Read for form, image, rhythm, and implication instead of only surface meaning.",
                examples:
                [
                    Example("die Strophe", "stanza", "A core poetry term."),
                    Example("das Enjambement", "enjambment", "A device where meaning runs past the line break.")
                ]),
            ["c2-u11-literarischer-wortschatz"] = CreateGuide(
                overview: "This area builds precise literary vocabulary for naming genres, roles, and devices.",
                whyItMatters: "Near-native discussion of literature depends on being able to label forms and functions accurately.",
                whatToPractice: "Use the terms in analytic statements rather than memorizing them as a disconnected list.",
                examples:
                [
                    Example("die Novelle", "novella", "A literary genre term."),
                    Example("der Protagonist", "protagonist", "A precise term for the main character.")
                ]),
            ["c2-u12-philosophische-begriffe"] = CreateGuide(
                overview: "This area teaches philosophical vocabulary for discussing ethics, existence, reason, and worldviews.",
                whyItMatters: "It supports the highest level of abstract discussion, where precise terminology shapes precise thought.",
                whatToPractice: "Use the words inside conceptual claims and contrasts so they become tools for reasoning.",
                examples:
                [
                    Example("Die Ethik hinterfragt unser Handeln.", "Ethics questions our actions.", "A key abstract concept in context."),
                    Example("Seine Weltanschauung prägt sein Handeln.", "His worldview shapes his actions.", "Philosophical vocabulary applied to interpretation.")
                ])
        };

    public static CurriculumUnit AttachGuide(CurriculumUnit unit)
    {
        if (Guides.TryGetValue(unit.Id, out var guide))
        {
            return unit with { Guide = guide };
        }

        return unit with { Guide = BuildFallbackGuide(unit) };
    }

    private static TeachingAreaGuide CreateGuide(
        string overview,
        string whyItMatters,
        string whatToPractice,
        IReadOnlyList<TeachingAreaGuideExample> examples) =>
        new()
        {
            Overview = overview,
            WhyItMatters = whyItMatters,
            WhatToPractice = whatToPractice,
            Examples = examples
        };

    private static TeachingAreaGuideExample Example(string german, string english, string? note = null) =>
        new()
        {
            German = german,
            English = english,
            Note = note
        };

    private static TeachingAreaGuide BuildFallbackGuide(CurriculumUnit unit)
    {
        var examples = unit.Exercises
            .Take(2)
            .Select(BuildExample)
            .ToList();

        if (examples.Count == 0)
        {
            examples.Add(Example(unit.Title, unit.Description, "This teaching area is ready for richer examples as the curriculum grows."));
        }

        return CreateGuide(
            overview: $"This teaching area focuses on {unit.Title}. {unit.Description}",
            whyItMatters: "You are working on this area because it supports comprehension, sentence building, and more confident real-world German use at this CEFR stage.",
            whatToPractice: "Pay attention to the repeated pattern across the exercises. The goal is to notice what changes, what stays fixed, and how the form carries meaning in context.",
            examples: examples);
    }

    private static TeachingAreaGuideExample BuildExample(Exercise exercise) => exercise switch
    {
        ClozeExercise cloze => Example(
            german: $"{cloze.TextBefore}{cloze.CorrectAnswer}{cloze.TextAfter}",
            english: exercise.Explanation,
            note: "This example is taken directly from the practice pattern in this unit."),
        MultipleChoiceExercise multipleChoice => Example(
            german: multipleChoice.Question,
            english: multipleChoice.Options[multipleChoice.CorrectOptionIndex],
            note: exercise.Explanation),
        SentenceOrderExercise sentenceOrder => Example(
            german: string.Join(" ", sentenceOrder.CorrectOrder),
            english: exercise.Explanation,
            note: "This shows the target German word order for the unit."),
        ConjugationDrillExercise drill => Example(
            german: string.Join(", ", drill.Slots.Take(3).Select(slot => $"{slot.Label} {slot.CorrectForm}")),
            english: exercise.Explanation,
            note: $"These forms come from the '{drill.BaseWord}' pattern you practice in this unit."),
        _ => Example(unitTitleFromExerciseId(exercise.Id), exercise.Explanation)
    };

    private static string unitTitleFromExerciseId(string exerciseId) => exerciseId;
}