using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Configuration;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer.Importers;

public class ImportLogHandler(IOptions<TicksterCrmConfig> options, SampleAppContext dbContext)
{
    private readonly string ApiKey = options.Value.ApiKey;

    public async Task WriteToImportLog(int crmId)
    {
        var dbImportLog = await dbContext.ImportLogs.SingleOrDefaultAsync(il => il.ApiKey == ApiKey);

        if (dbImportLog == null)
        {
            await dbContext.ImportLogs.AddAsync(new ImportLog { ApiKey = ApiKey, Date = DateTime.Now, LastTicksterCrmId = crmId });
        }
        else
        {
            dbContext.Remove(dbImportLog);
            await dbContext.ImportLogs.AddAsync(new ImportLog { ApiKey = ApiKey, Date = DateTime.Now, LastTicksterCrmId = crmId });
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<int> GetLastCrmId()
    {
        var dbImportLog = await dbContext.ImportLogs.SingleOrDefaultAsync(il => il.ApiKey == ApiKey);

        return dbImportLog?.LastTicksterCrmId ?? 0;
    }
}
