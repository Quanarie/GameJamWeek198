using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a very hardcoded class. It will have to change later.
/// </summary>
public class CatHealthUIChild : MonoBehaviour
{
    [SerializeField, Tooltip("Only three sprites are needed")]
    private List<Sprite> _healthSprites = new List<Sprite>();

    [SerializeField]
    private Image _imgHealth;

    public void UpdateImage(int health)
    {
        if (health == 3)
            _imgHealth.sprite = _healthSprites[0];
        else if (health == 2)
            _imgHealth.sprite = _healthSprites[1];
        else if (health == 1)
            _imgHealth.sprite = _healthSprites[2];

        if (health == 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
