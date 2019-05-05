namespace InteractiveDataDisplay.WPF
{
    public interface IMinorTicksProvider
    {
        int TicksCount { get; set; }

        double[] CreateTicks(Range range, double[] ticks);
    }
}