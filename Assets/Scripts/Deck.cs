using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Deck : MonoBehaviour
{
    public List<Card> cardDeck;
    public List<Sprite> cardImages;
    public GameObject MessagePanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI cardInDeckText;
    public Animator cardsAnim;
    public SpriteRenderer CardImage;
    public TextMeshPro CardValue;
    public GameObject DisableButtonPanel;

    void Start()
    {
        cardDeck = new List<Card>();
    }

    void Update()
    {
       
    }


    public void AddCard()
    {
        if(cardDeck.Count < 52)
        {
            Card card = new Card();
            card.createCard();
            if(checkOcccurance(card))
            {
                cardDeck.Add(card);
            }
            else
            {
                AddCard();

            }
            StartCoroutine(DisableButton(1f));
            cardsAnim.SetTrigger("add");
            cardInDeckText.text = cardDeck.Count.ToString();
        }
        else
        {
            messageText.text = "MAX CARD LIMIT REACHED";
            StartCoroutine(ShowMessages());
        }
    }

    public bool checkOcccurance(Card card)
    {
        foreach(Card deckCard in cardDeck)
        {
            if (deckCard.type == card.type)
            {
                if (deckCard.value == card.value)
                {
                    return false;
                }
            }
        }
        return true;
    }


    public void showTop()
    {
        if(cardDeck.Count  > 0)
        {
            fillCardDetails(cardDeck[cardDeck.Count-1]);
            StartCoroutine(DisableButton(2f));
        }
        else
        {
            messageText.text = "NO CARD TO SHOW";
            StartCoroutine(ShowMessages());
        }
    }

    private static System.Random rng = new System.Random();

    public void ShuffleCard()
    {
        if(cardDeck.Count > 0)
        {
            int n = cardDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cardDeck[k];
                cardDeck[k] = cardDeck[n];
                cardDeck[n] = value;
            }
            StartCoroutine(DisableButton(2f));
            cardsAnim.SetTrigger("shuffle");
            messageText.text = "SHUFFLING CARDS...";
            StartCoroutine(ShowMessages());
        }
        else
        {
            messageText.text = "NO CARD TO SHUFFLE";
            StartCoroutine(ShowMessages());
        }
        

    }

    public void RemoveCard()
    {
        if(cardDeck.Count > 0)
        {
            StartCoroutine(DisableButton(1f));
            cardDeck.RemoveAt(cardDeck.Count - 1);
            cardInDeckText.text = cardDeck.Count.ToString();
            cardsAnim.SetTrigger("remove");
        }
        else
        {
            messageText.text = "NO CARD TO REMOVE";
            StartCoroutine(ShowMessages());
        }
    }

    public void fillCardDetails(Card card)
    {
       CardImage.sprite = cardImages[(int)card.type];
        if(card.value == 11)
        {
            CardValue.text = "J";

        }else if(card.value == 12)
        {
            CardValue.text = "Q";
        }
        else if(card.value == 13)
        {
            CardValue.text = "K";
        }
        else
        {
            CardValue.text = card.value.ToString();
        }
        StartCoroutine(ShowTop());
    }


    IEnumerator DisableButton(float time)
    {
        DisableButtonPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        DisableButtonPanel.SetActive(false);
    }

    IEnumerator ShowMessages()
    {
        MessagePanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        MessagePanel.SetActive(false);

    }

    IEnumerator ShowTop()
    {
        yield return new WaitForSeconds(2f);
        CardImage.sprite = null;
        CardValue.text = "";

    }
}
