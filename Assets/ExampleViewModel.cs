using UnityEngine;

public class ExampleViewModel : ViewModel
{
    [SerializeField] public _Int current;
    public _Int Current { get { return current; } }
    [SerializeField] public _Int maximum;
    public _Int Maximum { get { return maximum; } }
    [SerializeField] public _Float ratio;
    public _Float Ratio { get { return ratio; } }
}