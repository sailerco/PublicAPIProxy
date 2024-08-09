/// <summary> Represents a spell overview with the details provided by the API. </summary>
public class Spell
{
    public string index { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public string url { get; set; }
}

/// <summary> Represents a list of <c><see cref="Spell"/></c>, including spell count, provided by the API. </summary>
public class SpellList
{
    public int count { get; set; }
    public List<Spell> results { get; set; }

    /// <summary>Initializes a new instance of the <c>SpellList</c> class.</summary>
    public SpellList() { }

    /// <summary> Initializes a new instance of the <c>SpellList</c> class with a specified list of spells. </summary>
    /// <param name="results">The list of <c><see cref="Spell"/></c> to initialize the <c>SpellList</c> with.</param>
    public SpellList(List<Spell> results)
    {
        this.count = results.Count;
        this.results = results;
    }
}