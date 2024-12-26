namespace Day23;

public class Computer
{
    public string Name { get; set; }
    public List<string> Connections { get; set; }
    public Computer(string name) 
    {
        Name = name;
        Connections = new List<string>();
    }


    public void Connect(string name)
    {
        if (!Connections.Contains(name))
        {
            Connections.Add(name);
        }
    }

    public List< List<string>> GetConnections(List<Computer> connects, int layers)
    {
        var self = new List<string> { Name };

        if (layers == 0) return new List<List<string>> { self };
            
        var connectedComputers = connects.Where(n =>  Connections.Contains(n.Name)).ToList();
            
        layers--;
            
        return connectedComputers.SelectMany(c => c.GetConnections(connects, layers).Select(ny => self.Concat(ny).ToList() ) ).ToList() ;
    }

    internal void MutualConnected(List<Computer> computers)
    {
        Connections.ForEach(c =>
        {
            var com = computers.Find(cm => cm.Name == c);
            com.Connect(Name);
        });
    }
}