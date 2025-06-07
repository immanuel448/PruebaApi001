using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();
        try
        {
            //se crea una lsita para almacenar las consultas
            List<UserResponse> listaResultados = new List<UserResponse>();
            //se realizan 3 consultas
            for (int i = 0; i < 3; i++)
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)

                {
                    string json = await response.Content.ReadAsStringAsync();
                    var userData = JsonSerializer.Deserialize<UserResponse>(json);
                    listaResultados.Add(userData);
                }
            }

            //se obtienen los datos de cada una de las 3 consultas
            foreach (var userData in listaResultados)
            {
                Console.WriteLine($"Nombre: {userData.results[0].name.first} {userData.results[0].name.last}");
                Console.WriteLine($"Ciudad: {userData.results[0].location.city}, {userData.results[0].location.country}");
                Console.WriteLine($"Email: {userData.results[0].email}");
                Console.WriteLine($"Edad: {userData.results[0].dob.age}");
                Console.WriteLine($"Foto: {userData.results[0].picture.large}\n\n");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

class UserResponse
{
    public User[] results { get; set; }
}

class User
{
    public Name name { get; set; }
    public Location location { get; set; }
    public string email { get; set; }
    public Dob dob { get; set; }
    public Picture picture { get; set; }
}

class Name
{
    public string first { get; set; }
    public string last { get; set; }
}

class Location
{
    public string city { get; set; }
    public string country { get; set; }
}

class Dob
{
    public int age { get; set; }
}

class Picture
{
    public string large { get; set; }
}