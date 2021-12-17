namespace KnowitJulekalender2021.Dag16;

public class Dag16 : ISolution
{
    public void Execute()
    {
        var stromPriser = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag16\\strømpriser.txt");
        var stromPriserNext = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag16\\strømpriser_next.txt");

        var cheapestHour = FindCheapesteHour(stromPriser);
        var priceStrat1 = GetStrategy1Price(cheapestHour, stromPriserNext);
        int priceStrat2 = GetStrategy2Price(cheapestHour, stromPriser, stromPriserNext);

        if (priceStrat1 > priceStrat2)
        {
            Console.WriteLine($"2,{priceStrat1 - priceStrat2}");
        }
        else
        {
            Console.WriteLine($"1,{priceStrat2 - priceStrat1}");
        }
    }

    private int GetStrategy2Price(int cheapestHour, string[] stromPriser, string[] stromPriserNext)
    {
        var totalCost = 0;
        var maxHours = stromPriserNext.Select(a => a.Length).Max();

        for (int y = cheapestHour; y < maxHours; y += 24)
        {
            var dayPrice = GetDayPrice(y, stromPriserNext);
            totalCost += dayPrice;


            var nextDay = y + 24;

            if (nextDay < maxHours)
            {
                var todayHistoricPrice = GetDayPrice(y, stromPriser);
                var nextDayHistoricPrice = GetDayPrice(nextDay, stromPriser);

                if (nextDayHistoricPrice > todayHistoricPrice)
                {
                    totalCost += GetDayPrice(y + 1, stromPriserNext);
                    y += 24;
                }
            }
        }

        return totalCost;
    }

    private int GetDayPrice(int hour, string[] stromPriser)
    {
        for (int x = 0; x < stromPriser.Length; x++)
        {
            var price = stromPriser[x];

            if (price.Length > hour && price[hour] != ' ')
            {
                return stromPriser.Length - x - 1;
            }
        }

        throw new Exception("ERROR");
    }

    private int GetStrategy1Price(int cheapestHour, string[] stromPriser)
    {
        var totalCost = 0;

        for (int y = cheapestHour; y < stromPriser.Select(a => a.Length).Max(); y += 24)
        {
            totalCost += GetDayPrice(y, stromPriser);
        }

        return totalCost;
    }

    private int FindCheapesteHour(string[] stromPriser)
    {
        var prices = new List<int>();

        for (int i = 0; i < 24; i++)
        {
            var dayPrice = 0;

            for (int y = i; y < stromPriser.Select(a => a.Length).Max(); y += 24)
            {
                dayPrice += GetDayPrice(y, stromPriser);
            }

            prices.Add(dayPrice);
        }

        return prices.IndexOf(prices.Min());
    }
}
