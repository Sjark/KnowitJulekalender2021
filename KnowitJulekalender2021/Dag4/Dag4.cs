namespace KnowitJulekalender2021.Dag4;

public class Dag4 : ISolution
{
    public void Execute()
    {
        var xPos = 0;
        var yPos = 0;
        var north = true;

        for (int i = 1; i <= 1000000079; i += 1)
        {
            if (north)
            {
                yPos++;
            }
            else
            {
                xPos++;
            }

            if (north && yPos % 3 == 0 && yPos % 5 != 0)
            {
                north = false;
            }
            else if (!north && xPos % 5 == 0 && xPos % 3 != 0)
            {
                north= true;
            }

            if (i == 100079 || i == 100079 || i == 1000079 || i == 10000079 || i == 100000079 || i == 1000000079)
            {
                Console.WriteLine($"{xPos},{yPos}");
            }
        }
    }
}
