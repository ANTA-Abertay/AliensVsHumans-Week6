using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerController playerControl;
    public UIDocument uiDoc;

    private Label _mHealthLabel;
    private int _maxHealth = 10;
    private void Start()
    {
        if (playerControl.onHealthChange)
        {
            _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
            HealthChanged();
        }

    }


    void HealthChanged()
    {
        _mHealthLabel.text = $"{playerControl.health}/{_maxHealth}";
    }
}