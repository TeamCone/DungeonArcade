using System.Collections;
using Game.Player;
using UnityEngine;

public class ItemController : MonoBehaviour, IItem
{
	private IThrowItem _throwItem;
	private EnumItemState _enumItemState;
	private EnumPlayer _origin;
	private Rigidbody2D _rigidbody2D;
	private SpriteRenderer _spriteRenderer;
	private const float ThrowTime = 2f;
	
	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
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

	public async void Throw()
	{
		_throwItem.Throw();
		SetState(EnumItemState.MOVING);
		TweenFacade.ThrowItem(_spriteRenderer, ThrowTime);
		await SetBackToIdle();
		
	}
	
	//set item back to idle after n seconds
	private IEnumerator SetBackToIdle()
	{
		yield return  new WaitForSeconds(ThrowTime);
		SetState(EnumItemState.IDLE);
	}
	
	
}
