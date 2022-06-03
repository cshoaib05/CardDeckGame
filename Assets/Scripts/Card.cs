using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    public enum Type { spade , club, heart, diamond}
    public Type type;
    public int value; // 1 - 13 
    
    public void createCard()
    {
        type = (Type)Random.Range(0, 4);
        value = Random.Range(1, 14);

    }

}
