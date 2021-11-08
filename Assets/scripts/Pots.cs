using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pots : MonoBehaviour
{
   [SerializeField]private int PotType;
   public ItemType potItemType;
   [SerializeField] private TextMeshProUGUI typeText;

   private void Start()
   {
      ItemType temp = (ItemType)PotType;
      InitialiseItem(temp);

   }
   private void InitialiseItem(ItemType type)
   {
      potItemType = type;
      typeText.text = Enum.GetName(typeof(ItemType), type);
   }
}
