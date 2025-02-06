using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Configuration;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class ImportLogHandler(IOptions<TicksterCrmConfig> options, SampleAppContext dbContext)
{
    private readonly string ApiKey = options.Value.ApiKey;

    public async Task WriteToImportLog(int crmId)
    {
        var importLog = await dbContext.ImportLogs.SingleOrDefaultAsync(il => il.ApiKey == ApiKey);

        if (importLog == null)
        {
            await dbContext.ImportLogs.AddAsync(new ImportLog { ApiKey = ApiKey, Date = DateTime.Now, LastTicksterCrmId = crmId });
        }
        else
        {
            dbContext.Remove(importLog);
            await dbContext.ImportLogs.AddAsync(new ImportLog { ApiKey = ApiKey, Date = DateTime.Now, LastTicksterCrmId = crmId });
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<int> GetLastCrmId()
    {
        var importLog = await dbContext.ImportLogs.SingleOrDefaultAsync(il => il.ApiKey == ApiKey);

        return importLog?.LastTicksterCrmId ?? 0;
    }
}
