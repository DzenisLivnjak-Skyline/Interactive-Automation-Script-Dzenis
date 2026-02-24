namespace Interactive_Automation_Script_Dzenis
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ElementSelectorModel : IElementSelector
	{
		private readonly IDms dms;
		private IDmsElement[] elements;
		private IDmsElement selectedElement;

		public ElementSelectorModel(IDms dms)
		{
			this.dms = dms ?? throw new ArgumentNullException(nameof(dms));

			// dms.GetProtocols().First(). check docs
		}



		public IReadOnlyCollection<IDmsElement> Elements
		{
			get
			{
				return elements ?? (elements = dms.GetElements()
					.Where(element => element.State == ElementState.Active)
					.ToArray());
			}
		}

		public IDmsElement SelectedElement
		{
			get
			{
				if (selectedElement == null)
				{
					var elements = Elements;
					if (!elements.Any())
					{
						throw new InvalidOperationException("No active elements available.");
					}

					selectedElement = elements.First();
				}

				return selectedElement;
			}

			set
			{
				if (value != SelectedElement)
				{
					selectedElement = value;
				}
			}
		}

		public int SelectedParameterID { get; set; }

		public string StringValue { get; set; }

		public double DoubleValue { get; set; }

		public string ResultMessage { get; set; } = string.Empty;

		public bool IsParameterIDValid(int parameterId)
		{
			try
			{
				var stringParam = SelectedElement.GetStandaloneParameter<string>(parameterId);

				var value = stringParam.GetValue();

				return true;
			}
			catch
			{
				// Parameter doesn't exist
				return false;
			}
		}
	}
}