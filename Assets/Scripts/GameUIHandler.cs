using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    //public 
    public PlayerController player;
    public UIDocument uiDoc;

    private VisualElement _mHealthBarMask;
    private Label _mHealthLabel;
    private Label _mLevelLabel;

    //private
    private readonly int _maxHealth = 20;
    private int _health;
    private int _currentHealth = 20;
    private int _currentLevel = 1;
    private int _level = 1;

    private float _timer;
    private bool _labelVisible = false;

    private void Start()
    {
        // get visual elements
        _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        _mLevelLabel = uiDoc.rootVisualElement.Q<Label>("LevelLabel");
        
        // set the alert label off
        _mLevelLabel.style.display = DisplayStyle.None;
        _labelVisible = false;
        // sets health bar up
        HealthChanged();
    }

    private void Update()
    {
        _currentLevel = GameManager.Instance.currentLevel;
        _health = player.health;
        _timer -= Time.deltaTime; // timer
        
        //cancels/ hides alerts
       _timer -= Time.deltaTime; 
        if (_labelVisible)
        {
            if (_timer <= 0)
            {
                HideLevelLabel();
            }
        }
        
        // if health changes calls the function to update the bar
        if (_currentHealth != _health)
        {
            _currentHealth = _health;
            HealthChanged();
        }
        
        //if level changes call the next level function
        if (_level != _currentLevel)
        {
            _level = _currentLevel;
            LevelChange();
        }
        
        //if player dies calls the death alert function
        if (_health <= 0)
        {
            Death();
        }
        
        //if win condition met call win alert function
        if (_currentLevel == 20)
        {
            Win();
        }
    }
    void HealthChanged()
    {
        _mHealthLabel.text = $"{_currentHealth}/{_maxHealth}"; // changes the text label to updated health
        float healthPercent =  (((float)_currentHealth / (float)_maxHealth ) *100f); //get the health percentage
        _mHealthBarMask.style.width = new Length(healthPercent, LengthUnit.Percent); // sets bar width to the health percent
        _mHealthBarMask.MarkDirtyRepaint(); // redraws the bar
    }
    void LevelChange()
    {
        _timer = 2f; // visible for 2 seconds
        ShowLevelLabel("Level Complete!"); // changes label to level complete
    }

    void Win()
    {
        _timer = 3f; // visible for 3 seconds
        ShowLevelLabel("You Win!"); // changes label to you win
    }

    void Death()
    {
        _timer = 3f;
        ShowLevelLabel("GAME OVER"); // changes label to game over
    }
    
    //shows label
    void ShowLevelLabel(string message)
    {
        _mLevelLabel.text = message; // sets the chosen functions message to display Label
        
        //makes the label visible
        _mLevelLabel.style.display = DisplayStyle.Flex; 
        _labelVisible = true;
    }

    //hides label
    void HideLevelLabel()
    {
        //makes the label no longer visible
        _mLevelLabel.style.display = DisplayStyle.None;
        _labelVisible = false;
    }
}
