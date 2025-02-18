using AtmService.Models;
using System.Text.Json;

namespace AtmService.Repositories;

public class JsonAtmRepository : ICrudRepository<Atm>
{
    private readonly string _filePath;

    public JsonAtmRepository()
    {
        _filePath = Path.Combine("data", "Atms.json");
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private async Task<List<Atm>> ReadAtmsAsync()
    {
        if (new FileInfo(_filePath).Length == 0)
        {
            return new List<Atm>();
        }

        using FileStream stream = File.OpenRead(_filePath);
        var atms = await JsonSerializer.DeserializeAsync<List<Atm>>(stream) ?? new List<Atm>();
        return atms;
    }

    private async Task WriteAtmsAsync(List<Atm> atms)
    {
        using FileStream stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, atms, new JsonSerializerOptions { WriteIndented = true });
    }

    public async Task Add(Atm entity)
    {
        var atms = await ReadAtmsAsync();
        atms.Add(entity);
        await WriteAtmsAsync(atms);
    }

    public async Task Delete(Guid id)
    {
        var atms = await ReadAtmsAsync();
        var atm = atms.FirstOrDefault(a => a.Id == id);
        if (atm != null)
        {
            atms.Remove(atm);
            await WriteAtmsAsync(atms);
        }
    }

    public async Task<Atm> Get(Guid id)
    {
        var atms = await ReadAtmsAsync();
        return atms.FirstOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<Atm>> GetAll()
    {
        return await ReadAtmsAsync();
    }

    public async Task Update(Atm entity)
    {
        var atms = await ReadAtmsAsync();
        var index = atms.FindIndex(a => a.Id == entity.Id);
        if (index != -1)
        {
            atms[index] = entity;
            await WriteAtmsAsync(atms);
        }
    }
}