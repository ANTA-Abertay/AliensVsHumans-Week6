using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class GameUIHandler : MonoBehaviour
{
    
    public GameObject player;
    public UIDocument uiDoc;
    private VisualElement _mHealthBarMask;
    private Label _mHealthLabel;
    private int _maxHealth = 10;
    private readonly UnityEvent GetUnityEvent ;
    private void Start()
    {
        _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
    }

    private void Update()
    {
        



    }




    void HealthChanged()
    {
        _mHealthLabel.text = $"{player.health}/{_maxHealth}";
        float healthRatio = (float)player.CurrentHealth / _maxHealth;
        float healthPercent = Mathf.Lerp(0, 100, healthRatio);
        _mHealthBarMask.style.width = Length.Percent(healthPercent);
    }
}