namespace Accounts;

public class BeperkingOptie
{
    public string Id { get; set; }
    public string Beperking { get; set; }
    public List<ErvaringsDeskundige>? ErvaringsDeskundigen { get; set; }
}