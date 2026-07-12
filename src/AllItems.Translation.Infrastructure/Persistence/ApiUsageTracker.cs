using AllItems.Translation.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AllItems.Translation.Infrastructure.Persistence;

public sealed class ApiUsageTracker(AppDbContext db, IClock clock) : IApiUsageTracker
{
    public async Task RecordCharactersAsync(int characterCount, CancellationToken cancellationToken = default)
    {
        var yearMonth = clock.UtcNow.ToString("yyyy-MM");
        var record = await db.ApiUsageRecords.FirstOrDefaultAsync(r => r.YearMonth == yearMonth, cancellationToken);
        if (record is null)
        {
            record = new Core.Domain.ApiUsageRecord { YearMonth = yearMonth, CharacterCount = 0 };
            db.ApiUsageRecords.Add(record);
        }

        record.CharacterCount += characterCount;
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<long> GetCurrentMonthUsageAsync(CancellationToken cancellationToken = default)
    {
        var yearMonth = clock.UtcNow.ToString("yyyy-MM");
        var record = await db.ApiUsageRecords.FirstOrDefaultAsync(r => r.YearMonth == yearMonth, cancellationToken);
        return record?.CharacterCount ?? 0;
    }
}
