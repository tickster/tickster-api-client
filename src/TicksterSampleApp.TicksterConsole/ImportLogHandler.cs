using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TicksterSampleApp.Domain.Models;
using TicksterSampleApp.Infrastructure.Contexts;

namespace TicksterSampleApp.Importer;

public class ImportLogHandler(IConfiguration configuration, SampleAppContext dbContext)
{
    private readonly string ApiKey = configuration["Tickster:CrmApi:ApiKey"];

    public async Task WriteToImportLogAsync(int crmId)
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
