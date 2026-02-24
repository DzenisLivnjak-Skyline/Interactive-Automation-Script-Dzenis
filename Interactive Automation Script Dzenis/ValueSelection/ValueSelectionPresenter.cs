using System;
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Interactive_Automation_Script_Dzenis;

namespace Interactive_Automation_Script_Dzenis.ValueSelection
{
	public class ValueSelectionPresenter
	{
		private readonly ValueSelectionView view;
		private readonly IElementSelector model;
		private readonly IEngine engine;

		public ValueSelectionPresenter(IElementSelector model, ValueSelectionView view, IEngine engine)
		{
			this.view = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));
			this.engine = engine ?? throw new ArgumentNullException(nameof(engine));

			view.BackButton.Pressed += BackButton_Pressed;
			view.ExitButton.Pressed += ExitButton_Pressed;
			view.SetStringButton.Pressed += SetStringButton_Pressed;
			view.SetDoubleButton.Pressed += SetDoubleButton_Pressed;
		}

		public event EventHandler<EventArgs> Exit;
		public event EventHandler<EventArgs> Back;

		private void SetStringButton_Pressed(object sender, EventArgs e)
		{
			StoreStringToModel();
			SetStringParameterValue();
			view.MultilineMessageTextBox.Text = model.ResultMessage;
		}

		private void SetDoubleButton_Pressed(object sender, EventArgs e)
		{
			StoreDoubleToModel();
			SetDoubleParameterValue();
			view.MultilineMessageTextBox.Text = model.ResultMessage;
		}

		private void ExitButton_Pressed(object sender, EventArgs e)
		{
			Exit?.Invoke(this, EventArgs.Empty);
		}

		private void BackButton_Pressed(object sender, EventArgs e)
		{
			view.DoubleValueNumericField.Value = 0;
			view.StringValueTextBox.Text = string.Empty;
			view.MultilineMessageTextBox.Text = string.Empty;
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void StoreStringToModel()
		{
			model.StringValue = view.StringValueTextBox.Text;
		}

		private void StoreDoubleToModel()
		{
			model.DoubleValue = view.DoubleValueNumericField.Value;
		}

		private void SetStringParameterValue()
		{
			try
			{
				var selectedElement = model.SelectedElement;
				var paramID = model.SelectedParameterID;
				var paramValue = view.StringValueTextBox.Text;
				model.ResultMessage = string.Empty;

				if (selectedElement.State == ElementState.Active)
				{
					var parameter = selectedElement.GetStandaloneParameter<string>(paramID);
					parameter.SetValue(paramValue);
					engine.Sleep(2000); // check
					var newValue = parameter.GetValue();

					if (newValue == paramValue)
					{
						model.ResultMessage = "Success: String parameter set successfully!";
					}
					else
					{
						model.ResultMessage = $"Failed to set string value '{paramValue}' on parameter ID {paramID}.";
					}
				}
				else
				{
					model.ResultMessage = $"Error: Element '{selectedElement.Name}' is not active.";
				}
			}
			catch (Exception exception)
			{
				model.ResultMessage = $"Error: {exception.Message}";
			}
		}

		private void SetDoubleParameterValue()
		{
			try
			{
				var selectedElement = model.SelectedElement;
				var paramID = model.SelectedParameterID;
				var paramValue = view.DoubleValueNumericField.Value;
				model.ResultMessage = string.Empty;

				if (selectedElement.State == ElementState.Active)
				{
					var parameter = selectedElement.GetStandaloneParameter<double?>(paramID);
					parameter.SetValue(paramValue);
					engine.Sleep(2000); // check
					var newValue = parameter.GetValue();

					if (newValue == paramValue)
					{
						model.ResultMessage = "Success: Double parameter set successfully!";
					}
					else
					{
						model.ResultMessage = $"Failed to set double value '{paramValue}' on parameter ID {paramID}.";
					}
				}
				else
				{
					model.ResultMessage = $"Error: Element '{selectedElement.Name}' is not active.";
				}
			}
			catch (Exception exception)
			{
				model.ResultMessage = $"Error: {exception.Message}";
			}
		}
	}
}