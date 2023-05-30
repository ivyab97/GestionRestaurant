namespace Aplication.DTO.Response
{
    public class MercaderiaGetResponse
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public TipoMercaderiaResponse tipo { get; set; }
        public string imagen { get; set; }

    }
}
