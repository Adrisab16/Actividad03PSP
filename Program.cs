using System.Net.Http.Json;


[Serializable]
public class DtoInformation
{
    public int count { get; set; }
    public int pages { get; set; }
    public string? next { get; set; }
    public string? prev { get; set; }
}

[Serializable]
public class DtoOutput
{
    public DtoInformation info { get; set; }
    public List<CharacterDto> results { get; set; }
}
[Serializable]
public class CharacterDto
{
    public int id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string species { get; set; }
    public string type { get; set; }
    public string gender { get; set; }
    public Location origin { get; set; }
    public Location location { get; set; }
    public string image { get; set; }
    public List<string> episode { get; set; }
    public string url { get; set; }
    public string created { get; set; }
    public string show()
    {
        return "Character name ->" + (this.name ?? "N/A") + "\n" +
               "Chapters appeared -> " + this.episode.Count;
    }
}
[Serializable]
public class Location
{
    public string name { get; set; }
    public string url { get; set; }
}
class Program
{
    static async Task Main()
    {
        // URL de la API
        string apiUrl = "https://rickandmortyapi.com/api/character/?name=Summer";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Lanzamos la solicitud a la API
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                //Si la solicitud fue correcta se gestiona el resultado
                if (response.IsSuccessStatusCode)
                {
                    // Tratamos el resultado como un String
                    string content = await response.Content.ReadAsStringAsync();
                    // Deserializamos el contenido de la respuesta
                    DtoOutput respuesta = JsonContent.DeserializeObject<DtoOutput>(content);
                    // Obtenemos el personaje que buscamos de la respuesta 
                    CharacterDto summer = respuesta.results[0];
                    // Imprimimos el resultado
                    Console.WriteLine(summer.show());
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
