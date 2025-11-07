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
        float healthRatio = _maxHealth / player.health ;
        float healthPercent = (100 *  healthRatio);
        _mHealthBarMask.style.width = (healthPercent);
    }
}