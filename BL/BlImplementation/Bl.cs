
using BlApi;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMileStone MileStone =>new MileStoneImplementation();

    public ITask Task => new TaskImplementation();
}
