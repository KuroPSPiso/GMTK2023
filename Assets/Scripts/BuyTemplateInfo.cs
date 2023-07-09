using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UI.Button;
using UnityEngine.Events;

public class BuyTemplateInfo : MonoBehaviour
{
    public Image Icon;
    public TMP_Text TMPCostValue;
    public TMP_Text TMPName;

    //private AIAsset aiAssetRef;
    private int index;

    private UnityAction selectAction;

    public void Configure(AIAsset aiAsset, int index)
    {
        this.Icon.sprite = aiAsset.Icon;
        this.TMPName.text = aiAsset.Descriptor;
        this.TMPCostValue.text = aiAsset.Cost.ToString();
        //this.aiAssetRef = aiAsset;
        this.index = index;

        selectAction += Select;
        this.GetComponent<Button>().onClick.AddListener(selectAction);
    }

    public void Select()
    {
        AIManager aiManager;
        if(Managers.TryGetAIManager(out aiManager))
        {
            aiManager.SelectedAIAsset = this.index;
            //aiManager.BuyUnit(aiAssetRef);
        }
    }
}
