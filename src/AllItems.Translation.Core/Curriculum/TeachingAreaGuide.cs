namespace AllItems.Translation.Core.Curriculum;

public sealed record TeachingAreaGuide
{
    public required string Overview { get; init; }
    public required string WhyItMatters { get; init; }
    public required string WhatToPractice { get; init; }
    public required IReadOnlyList<TeachingAreaGuideExample> Examples { get; init; }
}

public sealed record TeachingAreaGuideExample
{
    public required string German { get; init; }
    public required string English { get; init; }
    public string? Note { get; init; }
}