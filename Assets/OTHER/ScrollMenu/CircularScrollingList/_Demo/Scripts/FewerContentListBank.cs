using UnityEngine;

public class FewerContentListBank : BaseListBank
{
	private string[] contents = {
		"Settings", "New Game", "Continue",
	};

	public override string GetListContent(int index)
	{
		return contents[index];
	}

	public override int GetListLength()
	{
		return contents.Length;
	}

}
