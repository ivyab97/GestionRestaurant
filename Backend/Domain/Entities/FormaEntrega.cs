namespace Domain.Entities
{
    public class FormaEntrega
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion { get; set; }

        public IList<Comanda> Comanda { get; set; }
    }
}
