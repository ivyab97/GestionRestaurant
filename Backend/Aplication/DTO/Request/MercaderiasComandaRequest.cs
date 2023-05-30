namespace Aplication.DTO.Request
{
    public class MercaderiasComandaRequest
    {
        public Guid comandaId { get; set; }
        public IList<int> mercaderias { get; set; }
    }
}
