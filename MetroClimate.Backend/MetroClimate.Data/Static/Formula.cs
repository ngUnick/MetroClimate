using NCalc;

namespace MetroClimate.Data.Static;

public class Formula
{
    public static double EvaluateFormula(double x, string formula)
    {
        var e = new Expression(formula)
        {
            Parameters =
            {
                ["x"] = x
            }
        };
        return Convert.ToDouble(e.Evaluate());
    }
}


