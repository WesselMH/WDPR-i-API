namespace Accounts;

public class Beschikbaarheid
{
    public int Id { get; set; }
    public ErvaringsDeskundige ErvaringsDeskundige { get; set; }
    public DateTime Dag { get; set; }
    public DateTime StartTijd { get; set; }
    public DateTime EindTijd { get; set; }

//heb nog een nagedacht over waar en hoe we dingen ophalen en volgenmij is dit betere optie en zouden we het ORM hier classes moeten voor laten maken als nodig
    public List<BenaderOptie> benadering { get; set; }
    
}