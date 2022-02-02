using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Image _fillBar;
    [SerializeField] private TextMeshProUGUI _requiredPlayers;
    [SerializeField] private TextMeshProUGUI IntroText;
    [SerializeField] private GameObject IntroObject;


    [SerializeField]  private float _fillAmount;
    private float _targetToFill;
    private float _FillStart;
    private bool _isFilling;

    private float _lerpTime ;
    private float _currentlerpTime;



    public static UIManager Instance;



    public void initUI()
    {
            
        _currentlerpTime = 0f;
        _lerpTime = 20f;
        _requiredPlayers.text = LevelManager.Instance.totalToKill + "";
        Instance = this;
    }

    public void initIntro(int number)
    {

        IntroText.text = "KILL " + number + " ZOMBIES TO UNLOCK THE DOOR";
        IntroObject.gameObject.SetActive(true);
    }

    private void Update()
    {

        if (_isFilling)
        {
            float amount = Mathf.Lerp(_fillBar.fillAmount, _targetToFill, Time.deltaTime * 20f);
            _fillBar.fillAmount = amount;
        }

    }


    public void startFilling()
    {
        _requiredPlayers.text = int.Parse(_requiredPlayers.text) - 1 + "";
        if (_isFilling)
        {
            _targetToFill += _fillAmount;
            return;
        }
        _fillAmount = 1f / ((float)LevelManager.Instance.totalToKill);
        _targetToFill += _fillAmount;
        _FillStart = 0f;
        _currentlerpTime = 0f;
        _isFilling = true;

    }
}
