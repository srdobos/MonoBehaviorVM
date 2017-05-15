using System;
using UnityEngine;
using UnityEngine.Events;

public class CombineLatestOperator : ViewModelPropertyOperator
{
	[SerializeField] private ViewModelProperty one;
	[SerializeField] private ViewModelProperty two;
	private UnityAction action;

	private void Awake()
	{
		action = HandlePropertiesChanged;
	}

	private void OnEnable()
	{
		one.GetEvent().AddListener(action);
		two.GetEvent().AddListener(action);
	}

	private void OnDisable()
	{
		one.GetEvent().RemoveListener(action);
		two.GetEvent().RemoveListener(action);
	}

	private void HandlePropertiesChanged()
	{
		// TODO: Raise event?
	}
}