using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class stores information about each decorator or pip from JSON_Deck
[System.Serializable]
public class JsonPip
{
    public string type = "pip"; // "pip", "letter", or "suit"
    public Vector3 loc; // Location of the Sprite on the Card
    public bool flip = false; // True to flip the Sprite vertically
    public float scale = 1; // The scale of the Sprite
}

// This class stores information for each rank of card
[System.Serializable]
public class JsonCard
{
    public int rank; // The rank (1-13) of this card
    public string face; // Sprite to use for each face card
    public List<JsonPip> pips = new List<JsonPip>(); // The pips on this card
}

// This class contains information about the entire deck
[System.Serializable]
public class JsonDeck
{
    public List<JsonPip> decorators = new List<JsonPip>();
    public List<JsonCard> cards = new List<JsonCard>();
}

public class JsonParseDeck : MonoBehaviour
{
    private static JsonParseDeck S { get; set; }

    [Header("Inscribed")]
    public TextAsset jsonDeckFile; // The JSONDeck

    [Header("Dynamic")]
    public JsonDeck deck;

    void Awake()
    {
        if (S != null)
        {
            Debug.LogError("JsonParseDeck.S can't be set a 2nd time!");
            return;
        }
        S = this;

        deck = JsonUtility.FromJson<JsonDeck>(jsonDeckFile.text);
    }

    /// <summary>
    /// Returns the decorator layout information for all cards.
    /// </summary>
    /// <value></value>
    static public List<JsonPip> DECORATORS
    {
        get => S.deck.decorators;
    }

    /// <summary>
    /// Returns the JsonCard matching the rank passed in.
    /// Note: The rank should be 1 (Ace) - 13 (King).
    /// </summary>
    /// <param name="rank">Must be an int in range 1 - 13</param>
    /// <returns>JsonCard information</returns>
    static public JsonCard GET_CARD_DEF(int rank)
    {
        print("this ran");
        if ((rank < 1) || (rank > S.deck.cards.Count))
        {
            Debug.LogWarning($"Illegal rank argument: {rank}");
            return null;
        }
        return S.deck.cards[rank - 1];
    }
}
