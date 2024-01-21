namespace Accounts;

public class BeperkingOptie
{
    public int Id { get; set; }
    public string Beperking { get; set; }
    public List<ErvaringsDeskundige>? ErvaringsDeskundigen { get; set; }
}