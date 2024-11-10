namespace CalculatorApplication
{
    public delegate T Formula<T>(T arg1, T arg2);
    public partial class FrmCalculator : Form
    {
        private CalculatorClass cal;
        public FrmCalculator()
        {
            InitializeComponent();

            cbOperator.Items.Add("+");
            cbOperator.Items.Add("-");
            cbOperator.Items.Add("*");
            cbOperator.Items.Add("/");
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            CalculatorClass cal = new CalculatorClass();

            if (string.IsNullOrWhiteSpace(txtBoxInput1.Text) ||
                string.IsNullOrWhiteSpace(txtBoxInput2.Text) ||
                string.IsNullOrWhiteSpace(cbOperator.Text))
            {
                MessageBox.Show("Please fill out all the fields before calculating", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double Input1, Input2;

            if (!double.TryParse(txtBoxInput1.Text, out Input1))
            {
                MessageBox.Show("Invalid input for the first number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(txtBoxInput2.Text, out Input2))
            {
                MessageBox.Show("Invalid input for the second number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double result;
            switch (cbOperator.SelectedItem)
            {
                case "+":
                    cal.CalculateEvent += new Formula<double>(cal.GetSum);
                    lblDisplayTotal.Text = cal.GetSum(Input1, Input2).ToString();
                    cal.CalculateEvent -= new Formula<double>(cal.GetSum); break;
                case "-":
                    cal.CalculateEvent += new Formula<double>(cal.GetDifference);
                    lblDisplayTotal.Text = cal.GetDifference(Input1, Input2).ToString();
                    cal.CalculateEvent -= new Formula<double>(cal.GetDifference); break;
                case "*":
                    cal.CalculateEvent += new Formula<double>(cal.GetProduct);
                    lblDisplayTotal.Text = cal.GetProduct(Input1, Input2).ToString();
                    cal.CalculateEvent -= new Formula<double>(cal.GetProduct); break;
                case "/":
                    cal.CalculateEvent += new Formula<double>(cal.GetQuotient);
                    lblDisplayTotal.Text = cal.GetQuotient(Input1, Input2).ToString();
                    cal.CalculateEvent -= new Formula<double>(cal.GetQuotient); break;
                default:
                    MessageBox.Show("Invalid operator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
        }
    }
    public class CalculatorClass
    {
        public Formula<double> Calculation;

        private event Formula<double> calculateEvent;

        public event Formula<double> CalculateEvent
        {
            add
            {
                calculateEvent += value;
                Console.WriteLine("Delegate Added.");
            }
            remove
            {
                calculateEvent -= value;
                Console.WriteLine("Delegate Removed.");
            }
        }

        public double GetSum(double Input1, double Input2)
        {
            return Input1 + Input2;
        }

        public double GetDifference(double Input1, double Input2)
        {
            return Input1 - Input2;
        }
        public double GetProduct(double Input1, double Input2)
        {
            return Input1 * Input2;
        }
        public double GetQuotient(double input1, double input2)
        {
            if (input2 == 0)
            {
                MessageBox.Show("Cannot be divided by zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return input1 / input2;
        }
    }
}
