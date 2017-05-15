using UnityEngine;

public class SceneExampleViewModel : ViewModel
{
    [SerializeField] public _Int first;
    public _Int First { get { return first; } }
    [SerializeField] public _Int second;
    public _Int Second { get { return second; } }
}