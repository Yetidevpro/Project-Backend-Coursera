using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public interface IJsonFileRepository<T>
{
    List<T> GetAll();
    void SaveAll(List<T> items);
}

public class JsonFileRepository<T> : IJsonFileRepository<T>
{
    private readonly string _filePath;

    public JsonFileRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<T> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<T>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }

    public void SaveAll(List<T> items)
    {
        var json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }
}

