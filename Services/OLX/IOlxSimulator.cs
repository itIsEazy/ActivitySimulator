namespace ActivitySimulator.Services.OLX
{
    using System.Threading.Tasks;

    public interface IOlxSimulator
    {
        Task SearchInOlx();

        Task CollectAllOffersFor(string searchTerm);
    }
}
