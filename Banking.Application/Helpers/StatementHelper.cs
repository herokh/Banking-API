namespace Banking.Application.Helpers
{
    public static class StatementHelper
    {
        public static decimal CalculateActualAmount(decimal rawAmount, double feeAsPercent)
        {
            var amountOfFee = (rawAmount * (decimal)feeAsPercent) / 100;
            return rawAmount - amountOfFee;
        }
    }
}
