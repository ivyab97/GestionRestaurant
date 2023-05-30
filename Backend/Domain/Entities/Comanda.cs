namespace Domain.Entities
{
    public class Comanda
    {
        public Guid ComandaId { get; set; }
        public int FormaEntregaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; } 

        public IList<ComandaMercaderia> ComandaMercaderia { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
    }
}
