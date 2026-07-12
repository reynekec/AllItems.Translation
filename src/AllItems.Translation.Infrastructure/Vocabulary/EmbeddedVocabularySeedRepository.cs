using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using AllItems.Translation.Core.Abstractions;
using AllItems.Translation.Core.Curriculum;

namespace AllItems.Translation.Infrastructure.Vocabulary;

/// <summary>Reads the bulk vocabulary word lists from JSON files embedded in the Core assembly.</summary>
public sealed class EmbeddedVocabularySeedRepository : IVocabularySeedRepository
{
    private static readonly Assembly ContentAssembly = typeof(VocabularyWord).Assembly;
    private readonly ConcurrentDictionary<CefrLevel, IReadOnlyList<VocabularyWord>> _cache = new();

    public IReadOnlyList<VocabularyWord> GetWords(CefrLevel level) =>
        _cache.GetOrAdd(level, Load);

    private static IReadOnlyList<VocabularyWord> Load(CefrLevel level)
    {
        var resourceName = $"AllItems.Translation.Core.Curriculum.VocabularySeed.{level}.json";
        using var stream = ContentAssembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded vocabulary seed resource '{resourceName}' was not found.");

        var words = JsonSerializer.Deserialize<List<VocabularyWord>>(stream, JsonOptions);
        return words ?? [];
    }

    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
}
