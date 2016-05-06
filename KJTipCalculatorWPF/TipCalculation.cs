namespace KJTipCalculatorWPF
{
    /// <summary>
    /// Model for tip calculator.
    /// </summary>
    /// <remarks>
    /// Set the Subtotal, TipPercentage, and NumberInParty properties, then
    /// read the values of the computed Tip, Total, and PerPerson properties.
    /// </remarks>
    internal class TipCalculation
    {
        public decimal Subtotal = 0;
        public int TipPercentage = 20;
        public int NumberInParty = 1;

        public decimal Tip => (decimal) (TipPercentage * 0.01 * (double) Subtotal);

        public decimal Total => Subtotal + Tip;

        public decimal PerPerson => (NumberInParty > 1) ? (Total / NumberInParty) : Total;
    }
}
