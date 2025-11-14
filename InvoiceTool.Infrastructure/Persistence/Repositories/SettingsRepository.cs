using InvoiceTool.Domain.Entities;
using InvoiceTool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTool.Infrastructure.Persistence.Repositories;
internal class SettingsRepository(AppDbContext context) : ISettingsRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Settings?> GetSettingsAsync(Guid id)
    {
        return await _context.Settings.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<Settings> SaveAsync(Settings settings)
    {
        return settings.Id != Guid.Empty ? UpdateAsync(settings) : AddAsync(settings);
    }

    private async Task<Settings> AddAsync(Settings settings)
    {
        _context.Add(settings);

        await _context.SaveChangesAsync();

        return settings;
    }

    private async Task<Settings> UpdateAsync(Settings settings)
    {
        var existingSettings = await _context.Settings.FindAsync(settings.Id);

        if (existingSettings == null)
            return settings;

        _context.Entry(existingSettings).CurrentValues.SetValues(settings);

        await _context.SaveChangesAsync();

        return existingSettings;
    }
}
