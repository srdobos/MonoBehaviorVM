using UnityEngine;

public abstract class ViewModel : MonoBehaviour
{
#if UNITY_EDITOR

    // TODO: Figure out why OnValidate is not called when adding a viewmodel to an object. Find workaround.
    private void OnValidate()
    {
        var viewModelValidator = ViewModelValidatorFactory.GetValidatorForViewModel(this);
        viewModelValidator.ValidateViewModel(this);
    }

#endif
}

