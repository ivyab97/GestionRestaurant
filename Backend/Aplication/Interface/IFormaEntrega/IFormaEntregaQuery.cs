namespace Aplication.Interface.IFormaEntrega
{
    public interface IFormaEntregaQuery
    {
        public Task<bool> FormaEntregaIdExists(int formaEntregaId);
    }
}
