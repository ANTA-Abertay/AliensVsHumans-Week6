using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController player;
    public UIDocument uiDoc;
    private VisualElement _mHealthBarMask;
    private Label _mHealthLabel;
    private Label _mLevelLabel;
    private readonly int _maxHealth = 10;
    private int _health;
    private int _currentHealth = 10;
    private int _currentLevel;
    private int _level = 1;
    private float _timer;
    private void Start()
    {
        _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        _mLevelLabel = uiDoc.rootVisualElement.Q<Label>("LevelLabel");
        HealthChanged();
    }

    private void Update()
    {
        _currentLevel = gameManager.currentLevel;
        _health = player.health;
        _timer -= Time.deltaTime;
        if(_currentHealth != _health)
        {
            HealthChanged();
            _currentHealth = _health;
            
        }

        
        if (_level != _currentLevel);
        {
            levelChange();
            _level = _currentLevel;
        }

        if (_health == 0);
        {
            Death();
        }
        if (_currentLevel == 20);
        {
            Win();
        }

    }

    void HealthChanged()
    {
        _mHealthLabel.text = $"{player.health}/{_maxHealth}";
        float healthPercent = 100f * ((float)player.health / _maxHealth);
        _mHealthBarMask.style.width = new Length(healthPercent, LengthUnit.Percent);

    }

    void levelChange()
    {
        while(_timer >= -4)
        {
            _timer = 0;
            _mLevelLabel.text = $"{"level Complete"}";
            if (_timer == -2)
            {
                _mLevelLabel.text = $"{"level"}currentLevel";
            }
            
            
        }
        
        
    }
    
    void Win()
    {
        while (_timer >= -4)
        {
            _timer = 0;
            _mLevelLabel.text = $"{"You Win"}";
            if (_timer == -2)
            {
                _mLevelLabel.text = $"{"INFINITE MODE"}";
            }

        }
    }
    
    void Death()
    { 
        _timer = 2;
        while (_timer >= 0)
        {
            _mLevelLabel.text = $"{"GAME OVER"}";
        }
        
    }
    
}