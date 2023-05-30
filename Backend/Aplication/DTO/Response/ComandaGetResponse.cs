namespace Aplication.DTO.Response
{
    public class ComandaGetResponse
    {
        public Guid id { get; set; }
        public IList<MercaderiaGetResponse> mercaderias { get; set; }
        public FormaEntregaResponse formaEntrega { get; set; }
        public double total { get; set; }
        public DateTime fecha { get; set; }

    }
}
