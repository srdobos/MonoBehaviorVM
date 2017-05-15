using UnityEngine;

#if UNITY_EDITOR
public abstract class ViewModelValidator
{
	protected GameObject targetGameObject;
	private ViewModel viewModel;

	public void ValidateViewModel(ViewModel viewModel)
	{
		if (viewModel == null)
		{
			throw new System.ArgumentNullException("viewModel", "Cannot validate null view model.");
		}

		this.viewModel = viewModel;
		PreValidate();
		Validate();
		PostValidate();
	}

	protected virtual void PreValidate()
	{
		targetGameObject = viewModel.gameObject;
	}

	protected virtual void Validate()
	{
		int accumulatedChildIndex = 0;
		var serializedFieldInfos =
			SerializationUtility.GetSerializedFieldsOfType<ViewModelProperty>(viewModel.GetType());
		foreach (var fieldInfo in serializedFieldInfos)
		{
			string displayName = UnityEditor.ObjectNames.NicifyVariableName(fieldInfo.Name);
			ViewModelProperty property = fieldInfo.GetValue(viewModel) as ViewModelProperty;
			if (property == null)
			{
				GameObject propertyGO = GetOrCreatePropertyGameObject(displayName, fieldInfo.FieldType);
				property = propertyGO.GetComponent<ViewModelProperty>();
				fieldInfo.SetValue(viewModel, property);
			}
			if (property.gameObject != targetGameObject)
			{
				property.gameObject.name = displayName;
				property.transform.SetSiblingIndex(accumulatedChildIndex);
				accumulatedChildIndex += 1;
			}
		}
	}

	private GameObject GetOrCreatePropertyGameObject(string displayName, System.Type propertyType)
	{
		GameObject propertyGO;
		Transform existingChild = targetGameObject.transform.FindChild(displayName);
		if (existingChild != null)
		{
			propertyGO = existingChild.gameObject;
		}
		else
		{
			GameObject targetRootObject = GetTargetRoot();
			propertyGO = new GameObject(displayName, propertyType);
			propertyGO.transform.SetParent(targetRootObject.transform);
		}
		return propertyGO;
	}

	protected abstract GameObject GetTargetRoot();

	protected virtual void PostValidate()
	{
	}
}

public class PrefabViewModelValidator : ViewModelValidator
{
    private GameObject prefabInstance;

    protected override void PreValidate()
    {
        prefabInstance = null;
        base.PreValidate();
    }

    protected override GameObject GetTargetRoot()
    {
        CreatePrefabInstanceIfNeeded();
        return prefabInstance;
    }

    private void CreatePrefabInstanceIfNeeded()
    {
        if (prefabInstance == null)
        {
            prefabInstance = (GameObject) UnityEditor.PrefabUtility.InstantiatePrefab(targetGameObject);
        }
    }

    protected override void PostValidate()
    {
        if (prefabInstance != null)
        {
            UnityEditor.EditorApplication.delayCall +=
                () =>
                {
                    UnityEditor.PrefabUtility.ReplacePrefab(prefabInstance, targetGameObject, UnityEditor.ReplacePrefabOptions.Default);
                    UnityEngine.Object.DestroyImmediate(prefabInstance);
                    prefabInstance = null;
                };
        }
        base.PostValidate();
    }
}

public class SceneOnlyViewModelValidator : ViewModelValidator
{
    protected override GameObject GetTargetRoot()
    {
        return targetGameObject;
    }
}

public class NullViewModelValidator : ViewModelValidator
{
	protected override void Validate()
	{
		// Do Nothing!
	}

	protected override GameObject GetTargetRoot()
	{
		throw new System.InvalidOperationException("Null Model Validator should not be asked for its target root object.");
	}
}
#endif