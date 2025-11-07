using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameUIHandler : MonoBehaviour
{
    
    public PlayerController player;
    public UIDocument uiDoc;
    private VisualElement _mHealthBarMask;
    private Label _mHealthLabel;
    private int _maxHealth = 10;
    private int health;
    private int currentHealth = 10;
    private readonly UnityEvent GetUnityEvent ;
    private void Start()
    {
        _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        HealthChanged();
    }

    private void Update()
    {
        health = player.health;
        if(currentHealth != health)
        {
            HealthChanged();
            currentHealth = health;
            
        }



    }




    void HealthChanged()
    {
        _mHealthLabel.text = $"{player.health}/{_maxHealth}";
        float healthPercent = 100f * ((float)player.health / _maxHealth);
        _mHealthBarMask.style.width = new Length(healthPercent, LengthUnit.Percent);

    }
}