using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _mainMenuText;
    [SerializeField]
    private Text _exitGameText;
    [SerializeField]
    private int _maxValue;
    [SerializeField]
    private Image _fillBar;
    [SerializeField]
    private int _currentValue;
    private Player _player;
    private GameManager _gameManager;
    [SerializeField]
    private int _maxAmmo;
    [SerializeField]
    private Image _ammoBar;
    [SerializeField]
    private int _currentAmmo;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _maxValue = 100;
        _currentValue = _maxValue;
        _fillBar.fillAmount = 1;

        _maxAmmo = 15;
        _currentAmmo = _maxAmmo;
        _ammoBar.fillAmount = 1;

        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprite[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
            
        }

    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _mainMenuText.gameObject.SetActive(true);
        _exitGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Add(int i)
    {
        _currentValue += 1;

        if (_currentValue > _maxValue)
        {
            _currentValue = _maxValue;
        }

        _fillBar.fillAmount = (float)_currentValue / _maxValue;
    }

    public void Deduct(float i)
    {
        _currentValue -= 1;

        if (_currentValue < 0)
        {
            _currentValue = 0;
            _player.ThrusterDeActivate();
        }

        _fillBar.fillAmount = (float)_currentValue / _maxValue;
    }

    public void AddAMMO(int i)
    {
        _currentAmmo += 15;

        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }

        _ammoBar.fillAmount = (int)_currentAmmo / _maxAmmo;
    }

    public void DeductAMMO(float i)
    {
        _currentAmmo -= 1;

        if (_currentAmmo < 0)
        {
            _currentAmmo = 0;
        }

        _ammoBar.fillAmount = (float)_currentAmmo / _maxAmmo;
    }
}

