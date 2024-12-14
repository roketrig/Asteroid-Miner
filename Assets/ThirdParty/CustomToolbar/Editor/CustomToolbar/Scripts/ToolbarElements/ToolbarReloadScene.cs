using System;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEditor;
using UnityToolbarExtender;
using ProjectBase.Core;

[Serializable]
internal class ToolbarReloadScene : BaseToolbarElement
{
    private static GUIContent reloadSceneBtn;

    public override string NameInList => "[Button] Reload scene";

    public override void Init()
    {
        reloadSceneBtn = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath($"Assets/ThirdParty/CustomToolbar/Editor/CustomToolbar/Icons/ReloadIcon.png", typeof(Texture2D)), "Reload scene");
    }

    protected override void OnDrawInList(Rect position)
    {

    }

    protected override void OnDrawInToolbar()
    {
        EditorGUIUtility.SetIconSize(new Vector2(17, 17));
        if (GUILayout.Button(reloadSceneBtn, ToolbarStyles.commandButtonStyle))
        {
            if (EditorApplication.isPlaying)
            {
                LevelManager.Instance.ReloadLevel();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
