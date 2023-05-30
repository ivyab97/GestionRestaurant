namespace Aplication.DTO.Response
{
    public class ComandaResponse
    {
        public Guid id { get; set; }
        public IList<MercaderiaComandaResponse> mercaderias { get; set; }
        public FormaEntregaResponse formaEntrega { get; set; }
        public double total { get; set; }
        public DateTime fecha { get; set; }

    }
}
