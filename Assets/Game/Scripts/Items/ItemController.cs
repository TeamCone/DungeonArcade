using Game.Player;
using UnityEngine;

public class ItemController : MonoBehaviour, IItem
{
	private IThrowItem _throwItem;
	private EnumItemState _enumItemState;
	private EnumPlayer _origin;
	
	private void Awake()
	{
		_throwItem = GetComponent<IThrowItem>();
		_enumItemState = EnumItemState.IDLE;
	}

	public bool IsThrowable()
	{
		return _throwItem.Name() != "TreasureItem";
	}

	public EnumPlayer GetOrigin()
	{
		return _origin;
	}

	public void SetOrigin(EnumPlayer player)
	{
		_origin = player;
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
		if (GetState() == EnumItemState.PICKED)
		{
			SetState(EnumItemState.MOVING);
			_throwItem.Throw();
		}
		
	}
}
