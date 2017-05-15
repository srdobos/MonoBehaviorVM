using UnityEngine;
using UnityEngine.Events;

public class StringReaction : PropertyChangeReaction
{
    [SerializeField] private StringEvent reaction;

    protected override void HandlePropertyChanged()
    {
        var stringProperty = (_String) property;
        reaction.Invoke(stringProperty.Value);
    }

    [System.Serializable] private class StringEvent : UnityEvent<string> { }
}