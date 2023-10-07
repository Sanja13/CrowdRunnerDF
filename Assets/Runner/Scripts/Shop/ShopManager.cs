using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [Header("Skins")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private SkinButton[] skinsButtons;

    [Header("Elements")]
    [SerializeField] private Sprite[] skins;

    [Header("Pricinbg")]
    [SerializeField] private int skinPrice;
    [SerializeField] private Text priceText;

    private void Awake()
    {
        priceText.text = skinPrice.ToString();
    }


    void Start()
    {
        ConfigureButtons();
        UpdatePurchaseButton();
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnlockSkin(Random.Range(0, skinsButtons.Length));

        }
        
    }
    private void ConfigureButtons()
    {
        for(int i = 0; i < skinsButtons.Length; i++)
        {
            bool unlocked=PlayerPrefs.GetInt("skinButton"+i)==1;

            skinsButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinsButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
                //onClick e event
        }
    }
    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinsButtons[skinIndex].Unlocked();
    }
    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }
    private void SelectSkin(int skinIndex) 
    {
        //Debug.Log("Skin " + skinIndex);

        for(int i =0; i < skinsButtons.Length; i++)
        {
            if (skinIndex == i)
                skinsButtons[i].Select();
            else
                skinsButtons[i].DeSelect();
        }

    }

    public void PurchesSkin()
    {
        List<SkinButton> skinButtonlist = new List<SkinButton>();
        for(int i=0;i< skinsButtons.Length;i++)
        if (!skinsButtons[i].IsUnlocked())
            skinButtonlist.Add(skinsButtons[i]);

        if (skinButtonlist.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonlist[Random.Range(0, skinButtonlist.Count)];

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataManager.instance.UseCoins(skinPrice);
        UpdatePurchaseButton();
    }
    public void UpdatePurchaseButton()
    {
        if (DataManager.instance.GetCoins() < skinPrice)
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;
    }
}
