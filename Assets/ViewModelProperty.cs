
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewModelProperty : MonoBehaviour
{
    [SerializeField] private UnityEvent changedEvent;

    public UnityEvent GetEvent()
    {
        return changedEvent;
    }

    protected void RaisePropertyChanged()
    {
        changedEvent.Invoke();
    }
}

public class ViewModelProperty<TProperty> : ViewModelProperty
{
    [SerializeField] private TProperty value;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        RaisePropertyChanged();
    }
    #endif

    public TProperty Value
    {
        get { return value; }
        set
        {
            if (!EqualityComparer<TProperty>.Default.Equals(this.value, value))
            {
                this.value = value;
                RaisePropertyChanged();
            }
        }
    }
}