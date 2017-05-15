using UnityEngine;
using UnityEngine.Events;

public class PropertyChangeReaction : MonoBehaviour
{
    [SerializeField] protected ViewModelProperty property;
    private UnityAction handler;

    private void Awake()
    {
        handler = HandlePropertyChanged;
    }

    private void OnEnable()
    {
        property.GetEvent().AddListener(handler);
    }

    private void OnDisable()
    {
        property.GetEvent().RemoveListener(handler);
    }

    protected virtual void HandlePropertyChanged() { }
}