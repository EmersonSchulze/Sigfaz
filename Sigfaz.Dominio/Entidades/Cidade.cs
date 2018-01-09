namespace Sigfaz.Dominio.Entidades
{
    public class Cidade
    {
        public int CidadeId { get; set; }

        public string Nome { get; set; }

        public int EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
    }
}