using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //ITEM DATA// 
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public ItemTypeEnum itemType;
    [SerializeField]
    private int maxNumberOfItems;
    //ITEM SLOT//
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;
    
    //ITEM DESCRIPTION//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemIsSelected;

    private InventoryManager inventoryManager;


    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemTypeEnum itemType)
    {
        //Check to see if the slot is already full
        if(isFull)
        {
            return quantity;
        }

        //Update Information
        this.itemType = itemType;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        itemImage.sprite = itemSprite;
        //Update quantity
        this.quantity += quantity;
        if(this.quantity >= maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            //Return the LEFTOVER items
            int extraItems= this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }
        //Update the quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        if(thisItemIsSelected)
        {
            Debug.Log("Item is already selected" + itemName);
            bool useable = inventoryManager.UseItem(itemName);
            if(useable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if(this.quantity <= 0)
                {
                    EmptySlot();
                }
                Debug.Log("Inventory is called with item name : " + itemName);
            }
        }
        else{
            inventoryManager.DeselecteAllSlots();
            selectedShader.SetActive(true);
            thisItemIsSelected = true;

            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
        }
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = null;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = null;
    }

    public void OnRightClick()
    {
        //Create a new item
        GameObject itemToDrop = new GameObject(itemName);
        ItemScript newItem = itemToDrop.AddComponent<ItemScript>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        //CREATE AND MODIFY THE SRPITE RENDERER
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 1;
        sr.sortingLayerName = "Pickup";
        //add a collider
        itemToDrop.AddComponent<BoxCollider2D>();

        //Set the position of the item
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //Subtract the quantity
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if(this.quantity <= 0)
        {
            EmptySlot();
        }
    }
}
