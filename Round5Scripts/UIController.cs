using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Toggle enableRealWheelToggle;

    [HideInInspector]
    public WheelManager wheelManager;

    public Scrollbar speedScrollbar;
    public float minSpeed = 200f;
    public float maxSpeed = 450f;

    public GameObject subMenu;
    // Start is called before the first frame update
    void Start()
    {
        wheelManager = this.GetComponent<WheelManager>();
        speedScrollbar.value = Mathf.InverseLerp(minSpeed, maxSpeed, wheelManager.motorSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TrigSubMenu();
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("alley_scene");

    }

    public void TrigSubMenu()
    {
        subMenu.gameObject.SetActive(!subMenu.gameObject.activeSelf);
    }

    public void OnToggleChange()
    {
        if (enableRealWheelToggle.isOn)
        {
            wheelManager.enableRealWheel = true;
        }
        else
        {
            wheelManager.enableRealWheel = false;
        }
    }

    public void OnSpeedScrollBarChange()
    {
        wheelManager.motorSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedScrollbar.value);
    }
}
