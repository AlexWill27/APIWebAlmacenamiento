namespace MiPrimerAPI.Models

{
    public class RequestModel
    {

        public int ResultCount { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public string NextPage { get; set; }

        // Agrega otras propiedades según sea necesario

    }
}
