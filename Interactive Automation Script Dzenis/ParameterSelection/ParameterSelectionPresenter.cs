namespace Interactive_Automation_Script_Dzenis.ParameterSelection
{
	using System;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ParameterSelectionPresenter
	{
		private readonly ParameterSelectionView view;
		private readonly IElementSelector model;

		public ParameterSelectionPresenter(IElementSelector model, ParameterSelectionView view)
		{
			this.view = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			view.BackButton.Pressed += BackButton_Pressed;
			view.ContinueButton.Pressed += ContinueButton_Pressed;
		}

		public event EventHandler<EventArgs> Continue;

		public event EventHandler<EventArgs> Back;

		public void LoadIdFrom()
		{
			view.ParameterID.Value = model.SelectedParameterID;
		}

		public void SaveIdFrom()
		{
			model.SelectedParameterID = (int)view.ParameterID.Value;
		}

		private void BackButton_Pressed(object sender, EventArgs e)
		{
			SaveIdFrom();
			Back?.Invoke(sender, e);
		}

		private void ContinueButton_Pressed(object sender, EventArgs e)
		{
			int parameterId = (int)view.ParameterID.Value;

			if (!model.IsParameterIDValid(parameterId))
			{
				view.ParameterID.ValidationText = $"Parameter ID {parameterId} does not exist on element '{model.SelectedElement.Name}'";
				view.ParameterID.ValidationState = UIValidationState.Invalid;
				return;
			}

			view.ParameterID.ValidationText = "";
			view.ParameterID.ValidationState = UIValidationState.Valid;
			SaveIdFrom();
			Continue?.Invoke(sender, e);
		}
	}
}