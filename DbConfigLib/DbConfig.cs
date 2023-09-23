using System.Text.Json;

namespace ADONetDemo.DbConfigLib;

public class DbConfig
{
    public string Host { get; set; }
    public string DataBase { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"Server={Host};Database={DataBase};Uid={Login};Pwd={Password};";
    }

    public static DbConfig ImportFromJsonConfig(string path = "db_config.json")
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<DbConfig>(json);
    }
}