namespace Aplication.Interface.ITipoMercaderia
{
    public interface ITipoMercaderiaQueryServices
    {
        Task<bool> TipoMercaderiaExists(int tipoMercaderiaId);

    }
}
