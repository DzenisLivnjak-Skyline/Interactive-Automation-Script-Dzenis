namespace Interactive_Automation_Script_Dzenis
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;
	using Interactive_Automation_Script_Dzenis.ParameterSelection;
	using Interactive_Automation_Script_Dzenis.ValueSelection;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			// engine.ShowUI();

			var controller = new InteractiveController(engine);

			var elementSelectorModel = new ElementSelectorModel(engine.GetDms());

			var elementSelectionView = new ElementSelectionView(engine);
			var elementSelectionPresenter = new ElementSelectionPresenter(elementSelectionView, elementSelectorModel);

			var parameterSelectionView = new ParameterSelectionView(engine);
			var parameterSelectionPresenter = new ParameterSelectionPresenter(elementSelectorModel, parameterSelectionView);

			var valueSelectionView = new ValueSelectionView(engine);
			var valueSelectionPresenter = new ValueSelectionPresenter(elementSelectorModel, valueSelectionView, engine);

			elementSelectionPresenter.Continue += (sender, args) =>
			{
				parameterSelectionPresenter.LoadIdFrom();
				controller.ShowDialog(parameterSelectionView);
			};

			parameterSelectionPresenter.Continue += (sender, args) =>
			{
				controller.ShowDialog(valueSelectionView);
			};

			parameterSelectionPresenter.Back += (sender, args) =>
			{
				controller.ShowDialog(elementSelectionView);
			};

			valueSelectionPresenter.Back += (sender, args) =>
			{
				controller.ShowDialog(parameterSelectionView);
			};

			valueSelectionPresenter.Exit += (sender, args) =>
			{
				engine.ExitSuccess("Script finished successfully.");
			};

			elementSelectionView.ExitButton.Pressed += (sender, args) =>
			{
				engine.ExitFail("No active elements found.");
			};

			// Start the first dialog
			controller.Run(elementSelectionView);
		}
	}
}