namespace Aplication.Interface.ITipoMercaderia
{
    public interface ITipoMercaderiaQuery
    {
        Task<bool> TipoMercaderiaIdExists(int tipoMercaderiaId);

    }
}
