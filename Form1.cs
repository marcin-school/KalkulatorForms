namespace KalkulatorForms
{
    public partial class Form1 : Form
    {
        private List<String> currentNumber = new List<String>();
        private String? currentOperation;
        private double result;
        public Form1()
        {
            InitializeComponent();
        }

        private double GetParsedNumber() {
            if (currentNumber.LastOrDefault("").Equals(".")) currentNumber.Add("0");
            String rawNumber = String.Join("", currentNumber);
            return double.Parse(rawNumber.Equals("") ? "0" : rawNumber);
        }

        private void Calculate(double first, double second, String operation) {
            switch (operation) {
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
                case "*":
                    result = first * second;
                    break;
                case "/":
                    if (second == 0)
                    {
                        currentNumber.Clear();
                        currentOperation = null;
                        tbScreen.Text = "Illegal arithmetic operation";
                    }
                    result = first / second;
                    break;
            }
        }

        private void OnBtnNumberClick(object sender, EventArgs e)
        {
            String number = ((Button) sender).Text;
            if (number.Equals("0"))
            {
                // Number cannot have many zeros at begin ("0012.34" is not allowed)
                if (currentNumber.FirstOrDefault("").Equals("0") && currentNumber.Count() == 1) return;
            }
            if (number.Equals("."))
            {
                // Number must have zero or one point ("0.12.3", "34..2" are not allowed)
                if (currentNumber.Contains(".")) return;

                // If currentNumber has no elements add zero ("0.11" instead of illegal ".11")
                if (currentNumber.Count() == 0) currentNumber.Add("0");
            }
            currentNumber.Add(number);
            tbScreen.Text = String.Join("", currentNumber);
        }

        private void OnBtnOperationClick(object sender, EventArgs e)
        {
            String operation = ((Button) sender).Text;
            if (currentOperation == null)
            {
                result = GetParsedNumber();
                currentOperation = operation;
                currentNumber.Clear();
                tbScreen.Text = result.ToString();
            }
            else
            {
                Calculate(result, GetParsedNumber(), currentOperation);
                currentOperation = operation;
                currentNumber.Clear();
                tbScreen.Text = result.ToString();
            }
        }

        private void OnBtnClearClick(object sender, EventArgs e)
        {
            currentNumber.Clear();
            currentOperation = null;
            tbScreen.Text = "0";
            result = 0;
        }

        private void OnBtnResultClick(object sender, EventArgs e)
        {
            if (currentOperation != null)
            {
                Calculate(result, GetParsedNumber(), currentOperation);
                currentNumber = result.ToString().Split("").ToList();
                currentOperation = null;
                tbScreen.Text = result.ToString();
            }
        }
    }
}
