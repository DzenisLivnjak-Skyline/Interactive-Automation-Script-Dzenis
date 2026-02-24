using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;

namespace Interactive_Automation_Script_Dzenis.ValueSelection
{
	public class ValueSelectionView : Dialog
	{
		public ValueSelectionView(IEngine engine) : base(engine)
		{
			Title = "Insert Parameter Value";

			StringValueTextBox = new TextBox() { Width = 300 };
			SetStringButton = new Button("Set String Value") { Width = 150 };

			DoubleValueNumericField = new Numeric()
			{
				StepSize = 0.01,
				Decimals = 2,
				Width = 300,
			};
			SetDoubleButton = new Button("Set Double Value") { Width = 150 };

			MultilineMessageTextBox = new TextBox()
			{
				IsMultiline = true,
				Width = 500,
				Height = 100,
			};

			BackButton = new Button("Back");
			ExitButton = new Button("Exit");

			AddWidget(new Label("String Value:"), 0, 0);
			AddWidget(StringValueTextBox, 0, 1);
			AddWidget(SetStringButton, 0, 2);

			AddWidget(new Label("Double Value:"), 1, 0);
			AddWidget(DoubleValueNumericField, 1, 1);
			AddWidget(SetDoubleButton, 1, 2);

			AddWidget(MultilineMessageTextBox, 2, 0, 1, 3);

			AddWidget(BackButton, 3, 0);
			AddWidget(ExitButton, 3, 2);
		}

		public TextBox StringValueTextBox { get; }
		public Numeric DoubleValueNumericField { get; }
		public TextBox MultilineMessageTextBox { get; }
		public Button SetStringButton { get; }
		public Button SetDoubleButton { get; }
		public Button BackButton { get; }
		public Button ExitButton { get; }
	}
}