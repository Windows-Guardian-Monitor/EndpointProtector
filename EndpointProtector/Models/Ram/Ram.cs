namespace EndpointProtector.Models.Ram
{
    internal record Ram
    {
        public double Size { get; private set; }
        public RamBytes Unit { get; private set; }
        public Ram(double size, RamBytes unit)
        {
            Size = size;
            Unit = unit;
        }

        public override string ToString()
        {
            return String.Format("{0:0.##} {1}", Size, Unit);
        }
    }
}
