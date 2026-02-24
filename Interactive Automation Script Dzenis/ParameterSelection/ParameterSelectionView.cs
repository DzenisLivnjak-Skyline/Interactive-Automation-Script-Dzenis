namespace Interactive_Automation_Script_Dzenis.ParameterSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ParameterSelectionView : Dialog
	{
		public ParameterSelectionView(IEngine engine) : base(engine)
		{
			Title = "Insert Parameter ID";
			ParameterID = new Numeric()
			{
				Minimum = 1,
				Width = 100,
				ValidationText = string.Empty,
				ValidationState = UIValidationState.NotValidated,
			};
			BackButton = new Button("Back");
			ContinueButton = new Button("Continue");

			AddWidget(new Label("Insert ID of the parameter: "), 0, 0);
			AddWidget(ParameterID, 0, 1);
			AddWidget(BackButton, 2, 0);
			AddWidget(ContinueButton, 2, 1);
		}

		public Numeric ParameterID { get; }
		public Button BackButton { get; }
		public Button ContinueButton { get; }
	}
}