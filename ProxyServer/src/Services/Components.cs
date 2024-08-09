public class Spell
{
    public string index { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public string url { get; set; }
}

public class SpellList
{
    public int count { get; set; }
    public List<Spell> results { get; set; }
    public SpellList() { }

    public SpellList(List<Spell> results)
    {
        this.count = results.Count;
        this.results = results;
    }
}