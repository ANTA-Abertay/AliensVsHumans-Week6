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
    private int _level;

    private float _timer;
    private bool _labelVisible = false;

    private void Start()
    {
        _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        _mLevelLabel = uiDoc.rootVisualElement.Q<Label>("LevelLabel");
        
        _mLevelLabel.style.display = DisplayStyle.None;
        _labelVisible = false;
        HealthChanged();
        _level = gameManager.currentLevel;
    }

    private void Update()
    {
       _timer -= Time.deltaTime; 
        if (_labelVisible)
        {
            if (_timer <= 0)
            {
                HideLevelLabel();
            }
        }
        _currentLevel = gameManager.currentLevel;
        _health = player.health;
        
        if (_currentHealth != _health)
        {
            HealthChanged();
            _currentHealth = _health;
        }
        
        if (_level != _currentLevel)
        {
            _level = _currentLevel;
            LevelChange();
        }
        
        if (_health <= 0)
        {
            Death();
        }
        
        if (_currentLevel == 20)
        {
            Win();
        }
    }
    void HealthChanged()
    {
        _mHealthLabel.text = $"{_currentHealth}/{_maxHealth}";
        float healthPercent = ( (float)_currentHealth / (float)_maxHealth ) * 100f;
        _mHealthBarMask.style.width = new Length(healthPercent, LengthUnit.Percent);
        _mHealthBarMask.MarkDirtyRepaint();
    }
    void LevelChange()
    {
        _timer = 2f; // visible for 2 seconds
        ShowLevelLabel("Level Complete!");
    }

    void Win()
    {
        _timer = 3f; // visible for 3 seconds
        ShowLevelLabel("You Win!");
    }

    void Death()
    {
        _timer = 3f;
        ShowLevelLabel("GAME OVER");
    }
    
    void ShowLevelLabel(string message)
    {
        _mLevelLabel.text = message;
        _mLevelLabel.style.display = DisplayStyle.Flex;
        _labelVisible = true;
    }

    void HideLevelLabel()
    {
        _mLevelLabel.style.display = DisplayStyle.None;
        _labelVisible = false;
        _timer = 0;
    }
}
