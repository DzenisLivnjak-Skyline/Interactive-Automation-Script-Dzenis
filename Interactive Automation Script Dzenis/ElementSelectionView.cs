namespace Interactive_Automation_Script_Dzenis
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ElementSelectionView : Dialog
	{
		public ElementSelectionView(IEngine engine) : base(engine)
		{
			Title = "Select Element";
			ElementsDropDown = new DropDown { IsSorted = true, IsDisplayFilterShown = true, Width = 300 };
			ContinueButton = new Button("Continue");
			ExitButton = new Button("Exit") { IsVisible = false };
			NoElementsLabel = new Label("No active elements found.") { IsVisible = false };
			SelectElementLabel = new Label("Select an element: ");

			// ElementsDropDown.ValidationState

			AddWidget(SelectElementLabel, 0, 0);
			AddWidget(ElementsDropDown, 0, 1);
			AddWidget(ContinueButton, 0, 2);
			AddWidget(NoElementsLabel, 1, 0);
			AddWidget(ExitButton, 1, 1);
		}
		
		public Label SelectElementLabel { get; }

		public DropDown ElementsDropDown { get; }

		public Button ContinueButton { get; }

		public Button ExitButton { get; }

		public Label NoElementsLabel { get; }

		public void DisableSelectionControls()
		{
			ElementsDropDown.IsEnabled = false;
			ContinueButton.IsEnabled = false;
			ElementsDropDown.IsVisible = false;
			ContinueButton.IsVisible = false;
			SelectElementLabel.IsVisible = false;
		}



		public void ShowNoElementsMessage()
		{
			NoElementsLabel.IsVisible = true;
			ExitButton.IsVisible = true;
		}
	}
}