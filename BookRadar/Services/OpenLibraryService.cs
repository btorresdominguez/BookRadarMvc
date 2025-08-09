using BookRadar.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace BookRadar.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://openlibrary.org/search.json?author=";
        private const string EditionsUrl = "https://openlibrary.org";

        public OpenLibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LibroViewModel>> BuscarLibrosPorAutorAsync(string nombreAutor)
        {
            string url = $"{BaseUrl}{Uri.EscapeDataString(nombreAutor)}";
            var response = await _httpClient.GetStringAsync(url);

            var json = JObject.Parse(response);
            var docs = json["docs"];

            var libros = new List<LibroViewModel>();

            foreach (var d in docs)
            {
                string editorial = "No disponible";

                // 1. Intentar obtener la editorial directamente del search
                if (d["publisher"] != null && d["publisher"].HasValues)
                {
                    editorial = d["publisher"].First().ToString();
                }
                else
                {
                    // 2. Si no hay, buscar usando editions.json
                    var workKey = d["key"]?.ToString(); // Ej: "/works/OL82583W"
                    if (!string.IsNullOrEmpty(workKey))
                    {
                        try
                        {
                            string editionsUrl = $"{EditionsUrl}{workKey}/editions.json";
                            var editionsResponse = await _httpClient.GetStringAsync(editionsUrl);
                            var editionsJson = JObject.Parse(editionsResponse);

                            var entries = editionsJson["entries"];
                            if (entries != null && entries.HasValues)
                            {
                                var firstEdition = entries.First;
                                if (firstEdition["publishers"] != null && firstEdition["publishers"].HasValues)
                                {
                                    editorial = firstEdition["publishers"].First().ToString();
                                }
                            }
                        }
                        catch
                        {
                            editorial = "No disponible";
                        }
                    }
                }

                libros.Add(new LibroViewModel
                {
                    Titulo = d["title"]?.ToString(),
                    AnioPublicacion = d["first_publish_year"]?.ToObject<int?>(),
                    Editorial = editorial
                });
            }

            return libros;
        }
    }
}