namespace ActivitySimulator.Services.OLX
{
    using System.Threading.Tasks;

    public interface IOlxSimulator
    {
        Task SearchInOlx();

        Task CollectAllOffersFor(string searchTerm);

        Task CollectAllOffersFor(string searchTerm, int maxPage);
    }
}
