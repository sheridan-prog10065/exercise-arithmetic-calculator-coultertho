using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MathCalculator;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// field variable to remember expression history
    /// </summary>
    private ObservableCollection<string> _expList;

    public MainPage()
    {
        InitializeComponent();

        //initialize field variables
        _expList = new ObservableCollection<string>();

        //bind collection view for history with list
        _lstExpHistory.ItemsSource = _expList;
    }

    /// <summary>
    /// takes the left and right operands, and the operation type.  Displays the output expression
    /// </summary>
    /// <param name="sender">button clicked</param>
    /// <param name="e"></param>
    public void OnCalculate(object sender, EventArgs e)
    {
        //Obtain user input: left op, right op, operator
        string leftOperandInput = _txtLeftOp.Text;
        double leftOperand = double.Parse(leftOperandInput);

        double rightOperand = double.Parse(_txtRightOp.Text);

        string opInput = (string)_pckOperand.SelectedItem;
        char op = opInput[0];

        //Check operator chosen and perform corresponding calculation
        double result = PerformArithmeticOperation(op, leftOperand, rightOperand);

        //Display the expression
        string expression = $"{leftOperand} {op} {rightOperand} = {result}";
        _txtMathExp.Text = expression;

        //remember calculated expression history
        _expList.Add(expression);
    }

    /// <summary>
    /// calculates the resulting value of the left and right operands with the corresponding operation type
    /// </summary>
    /// <param name="opInput">arithmetic operation type</param>
    /// <param name="leftOperand">left operand</param>
    /// <param name="rightOperand">right operand</param>
    /// <returns>expression result</returns>
    public double PerformArithmeticOperation(char op, double leftOperand, double rightOperand)
    {
        //check operation type and perform corresponding operation
        double result;

        switch (op)
        {
            case '+':
                result = PerformAddition(leftOperand, rightOperand);
                break;

            case '-':
                result = PerformSubtraction(leftOperand, rightOperand);
                break;

            case '*':
                result = PerformMultiplication(leftOperand, rightOperand);
                break;

            case '/':
                result = PerformDivision(leftOperand, rightOperand);
                break;

            case '%':
                result = PerformDivRemainder(leftOperand, rightOperand);
                break;

            default:
                Debug.Assert(false, "Unknown arithmetic operation.  Default result returned");
                result = 0;
                break;
        }

        return result;
    }

    public double PerformAddition(double leftOperand, double rightOperand)
    {
        double result;
        result = leftOperand + rightOperand;
        return result;
    }

    public double PerformSubtraction(double leftOperand, double rightOperand)
    {
        double result = leftOperand - rightOperand;
        return result;
    }

    private double PerformMultiplication(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }

    private double PerformDivision(double leftOperand, double rightOperand)
    {
        //check if div operation is int div or real div
        string divOp = (string)_pckOperand.SelectedItem;
        if (divOp.Contains("int", StringComparison.OrdinalIgnoreCase))
        {
            //perform int div
            int leftIntOp = (int)leftOperand;
            int rightIntOp = (int)rightOperand;
            int result = leftIntOp / rightIntOp;
            return result;
        }
        else
        {
            double result = leftOperand / rightOperand;
            return result;
        }
    }

    private double PerformDivRemainder(double leftOperand, double rightOperand)
    {
        return leftOperand % rightOperand;
    }
}