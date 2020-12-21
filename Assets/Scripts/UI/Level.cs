using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Sprite _levelLock;
    [SerializeField] private Sprite _levelAvailable;
    [SerializeField] private Sprite _levelZeroStar;
    [SerializeField] private Sprite _levelOneStar;
    [SerializeField] private Sprite _levelTwoStar;
    [SerializeField] private Sprite _levelThreeStar;
    [SerializeField] private Image _levelImage;
    [SerializeField] private string _levelName;
    [SerializeField] private Button _selectionButton;

    private void OnDisable()
    {
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    private void OnSelectionButtonClick()
    {
        SceneManager.LoadScene(_levelName);
    }

    public void Init(int countLevel, int levelProgress)
    {
        _levelName = "level" + countLevel;
        if (levelProgress == 0)
        {
            _levelImage.sprite = _levelLock;
        }
        else if (levelProgress == 1)
        {
            _levelImage.sprite = _levelAvailable;
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }
        else if (levelProgress == 2)
        {
            _levelImage.sprite = _levelZeroStar;
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }
        else if (levelProgress == 3)
        {
            _levelImage.sprite = _levelOneStar;
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }
        else if (levelProgress == 4)
        {
            _levelImage.sprite = _levelTwoStar;
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }
        else if (levelProgress == 5)
        {
            _levelImage.sprite = _levelThreeStar;
        }
    }
}
