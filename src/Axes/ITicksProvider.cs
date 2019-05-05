namespace InteractiveDataDisplay.WPF
{
    public interface ITicksProvider
    {
        MinorTicksProvider MinorProvider { get; set; }

        Range Range { get; set; }

        void DecreaseTickCount();

        double[] GetMinorTicks(Range range);

        double[] GetTicks();

        void IncreaseTickCount();
    }
}