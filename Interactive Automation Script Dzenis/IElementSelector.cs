namespace Interactive_Automation_Script_Dzenis
{
	using System.Collections.Generic;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public interface IElementSelector
	{
		IReadOnlyCollection<IDmsElement> Elements { get; }

		IDmsElement SelectedElement { get; set; }

		int SelectedParameterID { get; set; }

		string StringValue { get; set; }

		double DoubleValue { get; set; }

		string ResultMessage { get; set; }

		bool IsParameterIDValid(int parameterId);
	}
}