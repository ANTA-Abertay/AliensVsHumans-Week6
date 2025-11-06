using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerController playerControl;
    public UIDocument uiDoc;
    private VisualElement _mHealthBarMask;
    private Label _mHealthLabel;
    private int _maxHealth = 10;
    private void Start()
    {
        _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        HealthChanged();
        

    }


    void HealthChanged()
    {
        _mHealthLabel.text = $"{playerControl.health}/{_maxHealth}";
        float healthRatio = (float)PlayerController.CurrentHealth / _maxHealth;
        float healthPercent = Mathf.Lerp(0, 100, healthRatio);
        _mHealthBarMask.style.width = Length.Percent(healthPercent);
    }
}