using System.Text.Json.Serialization;
using Accounts;

namespace Onderzoeken;

public class Onderzoek
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Locatie { get; set; }
    // public string Status { get; set; }
    public string Beloning { get; set; }
    public DateTime Datum { get; set; }
    public Bedrijf? Uitvoerder { get; set; }
    public Categorie SoortOnderzoek { get; set; }
    public List<SelectieCriterium>? SelectieCriterium { get; set; }

    [JsonIgnore]
    public List<ErvaringsDeskundige>? ErvaringsDeskundigen { get; set; }
    public bool CheckedDoorBeheerder { get; set; }
}

public class OnderzoekUploadDto
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Locatie { get; set; }
    public string Beloning { get; set; }
    public DateTime Datum { get; set; }
    public string SoortOnderzoek { get; set; }
    public List<SelectieCriterium>? SelectieCriterium { get; set; }
    public bool CheckedDoorBeheerder { get; set; }
}

