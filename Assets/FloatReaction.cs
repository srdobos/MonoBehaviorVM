using UnityEngine;
using UnityEngine.Events;

public class FloatReaction : PropertyChangeReaction
{
    [SerializeField] private FloatEvent reaction;

    protected override void HandlePropertyChanged()
    {
        var typedProperty = (_Float) property;
        reaction.Invoke(typedProperty.Value);
    }

    [System.Serializable] private class FloatEvent : UnityEvent<float> { }
}