using Common.Enums;

namespace EndpointProtector.Models.Ram;

public record Storage
{
    public double Size { get; private set; }
    public UnitRepresentation Unit { get; private set; }
    public Storage(double size, UnitRepresentation unit)
    {
        Size = size;
        Unit = unit;
    }

    public override string ToString() => string.Format("{0:0.##} {1}", Size, Unit);
}