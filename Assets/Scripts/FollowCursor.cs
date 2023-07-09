using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    public Image selectedItemPreview;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Cursor.visible) Cursor.visible = false;
        this.transform.position = Input.mousePosition;

        AIManager aiManager;
        if (Managers.TryGetAIManager(out aiManager)) selectedItemPreview.sprite = (aiManager.SelectedAIAsset == -1)? null : aiManager.AIAssets[aiManager.SelectedAIAsset].Icon;
    }
}
