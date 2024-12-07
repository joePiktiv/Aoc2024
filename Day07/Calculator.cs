namespace Day07;
public class Calculator
{
    public static long CalculateLeftToRight(List<long> elements, List<string> operators)
    {        
        var numOfOperators = operators.Count;
        var last = elements.LastOrDefault();
        if (numOfOperators == 0) return last;
        var lastOperator = operators.LastOrDefault();
        
        var restElements = elements.Where((element, index) => index != elements.Count - 1).ToList();
        var restOperator = operators.Where((o, index) => index != operators.Count -1).ToList();
        
        if (lastOperator == "add") return CalculateLeftToRight(restElements, restOperator) + last;
        if (lastOperator == "||") return long.Parse( CalculateLeftToRight(restElements, restOperator).ToString() + "" + last.ToString());
        return CalculateLeftToRight(restElements, restOperator) * last;
    }
    public static long Calculate ( List<int> elements, List<string> operators) 
    {
        var numOfOperators = operators.Count;
        var firstElement = elements.FirstOrDefault();
        if (numOfOperators == 0) return firstElement;
        var firstOperator = operators.FirstOrDefault();
        if (firstOperator == "add") 
        {
            var restElements = elements.Where((element, index) => index != 0).ToList();
            var restOperator = operators.Where((o, index) => index != 0).ToList();
            var addResult = firstElement + Calculate(restElements, restOperator);
            return addResult;
        }
        else if (firstOperator == "mul" )
        {
            if (numOfOperators == 1)
            {
                return firstElement * elements.Last();
            }
            var indexFirstAdd = operators.IndexOf("add");
            indexFirstAdd = indexFirstAdd == -1 ? 0 : indexFirstAdd; 
            var elementsBeforeAdd = elements.Where((element, index) => index <= indexFirstAdd).ToList();
            var restElementsAfterAdd = elements.Where((element, index) => index > indexFirstAdd).ToList(); 
            var operatorsBeforeAdd = operators.Where((element, index) => index < indexFirstAdd).ToList();
            var restOperatorsAfterAdd = operators.Where((element, index) => index > indexFirstAdd ).ToList();
            var firstPart = Calculate(elementsBeforeAdd, operatorsBeforeAdd);
            var restPart = Calculate(restElementsAfterAdd, restOperatorsAfterAdd);
            var mulResult = firstPart + restPart;
            return mulResult;
        }
        return 0;
    }
}