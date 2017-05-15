using UnityEngine;

#if UNITY_EDITOR
public class ViewModelValidatorFactory
{
	public static ViewModelValidator GetValidatorForViewModel(ViewModel viewModel)
	{
		var prefabObject = UnityEditor.PrefabUtility.FindValidUploadPrefabInstanceRoot(viewModel.gameObject);
		bool thisIsPrefabInstance = prefabObject != null;
		if (thisIsPrefabInstance)
		{
			//Debug.Log("I AM AN INSTANCE OF A PREFAB " + viewModel.name, viewModel.gameObject);
			return new NullViewModelValidator();
		}

		var assetPath = UnityEditor.AssetDatabase.GetAssetPath(viewModel.gameObject);
		var assetOrScenePath = UnityEditor.AssetDatabase.GetAssetOrScenePath(viewModel.gameObject);
		bool thisIsSceneObject = assetPath != assetOrScenePath;
		if (thisIsSceneObject)
		{
			//Debug.Log("I am NOT an instance of a prefab. I live in a scene " + name, gameObject);
			return new SceneOnlyViewModelValidator();
		}
		return new PrefabViewModelValidator();
	}
}
#endif