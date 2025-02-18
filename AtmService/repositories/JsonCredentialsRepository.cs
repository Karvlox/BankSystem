using System.Text.Json;

namespace AtmService.Repositories;

public class JsonCredentialsRepository : ICredentialRepository
{
    private readonly string _filePath;

    public JsonCredentialsRepository()
    {
       _filePath = Path.Combine("data", "credentials.json");
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

    public async Task<bool> WriteCredentials(Guid guid)
    {
        try
        {
            var json = JsonSerializer.Serialize(guid);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Guid> ReadCredentials()
    {
        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<Guid>(json);
        }
        catch (Exception e)
        {
            return Guid.Empty;
        }
    }
}