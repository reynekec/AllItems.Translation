using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Core.Abstractions;

/// <summary>The bulk, frequency-ranked vocabulary word list per CEFR level - separate from Training's hand-authored exercises.</summary>
public interface IVocabularySeedRepository
{
    IReadOnlyList<VocabularyWord> GetWords(CefrLevel level);
}
