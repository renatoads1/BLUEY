namespace BLUEY.Models.Repositories
{
    public interface IEmpresaRepository
    {
        public List<Empresa> GetAllEmpresa();
        public Empresa GetById(string inscFed);
    }
}
