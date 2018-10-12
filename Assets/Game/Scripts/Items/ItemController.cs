using Game.Player;
using UnityEngine;

public class ItemController : MonoBehaviour, IItem
{
	private IThrowItem _throwItem;
	private EnumItemState _enumItemState;
	private void Awake()
	{
		_throwItem = GetComponent<IThrowItem>();
		_enumItemState = EnumItemState.IDLE;
	}

	public bool IsThrowable()
	{
		throw new System.NotImplementedException();
	}

	public EnumPlayer GetOrigin()
	{
		throw new System.NotImplementedException();
	}

	public void SetOrigin(EnumPlayer player)
	{
		throw new System.NotImplementedException();
	}

	public EnumItemState GetState()
	{
		return _enumItemState;
	}

	public void SetState(EnumItemState state)
	{
		_enumItemState = state;
	}

	public void Throw()
	{
		if (GetState() != EnumItemState.PICKED)
		{
			return;
		}
		_throwItem.Throw();
	}
}
