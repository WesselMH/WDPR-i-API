namespace Accounts;

public class Beschikbaarheid
{
    public int Id { get; set; }
    public List<ErvaringsDeskundige> ErvaringsDeskundigen { get; set; }
    public DateTime Dag { get; set; }
    public DateTime StartTijd { get; set; }
    public DateTime EindTijd { get; set; }

}