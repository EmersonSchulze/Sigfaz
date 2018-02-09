namespace Sigfaz.Infra.ComponentModel.Telefone
{
    public interface IRegraValidacaoProvider
    {
        bool Ddd9Digitos(long? ddd);

        bool PrefixoExcecao(long? ddd, string prefixo);
    }
}
