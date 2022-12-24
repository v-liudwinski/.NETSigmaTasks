using Homework12_LiudvynskyiV.S.Enteties;
using Homework12_LiudvynskyiV.S.Enums;

namespace Homework12_LiudvynskyiV.S.Services;

public class FileHandler
{
    private readonly string _rootPath;
    private List<Visitor> _visitors;

    public FileHandler(string rootPath)
    {
        _rootPath = rootPath;
    }

    public List<Visitor> GetVisitors() => _visitors;

    public void ReadFile()
    {
        var file = File.ReadLines(_rootPath);
        foreach (var str in file)
        {
            var info = str.Split(',');
            _visitors.Add(new Visitor
            {
                Position = int.Parse(info[0]),
                Name = info[1],
                Age = int.Parse(info[2]),
                Status = GetStatus(info[3]),
                MinutesNearPaydesk = int.Parse(info[4])
            });
        }
    }

    private Status GetStatus(string status)
    {
        return status.ToLower() switch
        {
            "regular" => Status.Regular,
            "vip" => Status.VIP,
            "inactive" => Status.Inactive,
            _ => throw new ArgumentException(nameof(Status))
        };
    }
}