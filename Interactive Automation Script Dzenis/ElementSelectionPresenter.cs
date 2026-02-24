namespace Interactive_Automation_Script_Dzenis
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ElementSelectionPresenter
	{
		private readonly ElementSelectionView view;
		private readonly IElementSelector model;
		private Dictionary<string, IDmsElement> elementsByName;

		public ElementSelectionPresenter(ElementSelectionView view, IElementSelector model)
		{
			this.view = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			LoadFromModel();
			view.ContinueButton.Pressed += OnContinueButtonPressed;
			view.ElementsDropDown.Changed += ElementsDropDownOnChange;
		}

		public event EventHandler<EventArgs> Continue;

		public event EventHandler<EventArgs> Exit;

		private void LoadFromModel()
		{
			var elements = model.Elements;

			if (!elements.Any())
			{
				view.DisableSelectionControls();
				view.ShowNoElementsMessage();
				return;
			}

			elementsByName = model.Elements.ToDictionary(element => element.Name);

			view.ElementsDropDown.SetOptions(elementsByName.Keys);
			view.ElementsDropDown.Selected = model.SelectedElement.Name;
		}

		private void StoreToModel()
		{
			string selected = view.ElementsDropDown.Selected;
			model.SelectedElement = elementsByName[selected];
		}

		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Continue?.Invoke(this, EventArgs.Empty);
		}

		private void ElementsDropDownOnChange(object sender, DropDown.DropDownChangedEventArgs e)
		{
			model.SelectedParameterID = 0;
		}
	}
}