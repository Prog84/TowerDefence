using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardBase))]
public class CardBaseEditor : Editor
{
    private CardBase _cardBase;

    private void Awake()
    {
        _cardBase = (CardBase)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Card"))
        {
            _cardBase.CreateCard();
        }
        if (GUILayout.Button("Remove Card"))
        {
            _cardBase.RemoveCard();
        }
        if (GUILayout.Button("<="))
        {
            _cardBase.PrevCard();
        }
        if (GUILayout.Button("=>"))
        {
            _cardBase.NextCard();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }

}

