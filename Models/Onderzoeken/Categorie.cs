using Accounts;

namespace Onderzoeken;

public class Categorie
{
    public int Id { get; set; }
    public string Opties { get; set; }
    public List<ErvaringsDeskundige>? ErvaringsDeskundigen { get; set; }
}