namespace Aplication.Interface.IFormaEntrega
{
    public interface IFormaEntregaQueryServices
    {
        Task<bool> FormaEntregaExists(int formaEntregaId);

    }
}
