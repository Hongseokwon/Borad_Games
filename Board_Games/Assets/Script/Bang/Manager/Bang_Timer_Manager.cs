using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bang_Timer_Manager : MonoBehaviour
{
    private static Bang_Timer_Manager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Bang_Timer_Manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time_Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time_Active)
        {
            Timer_Count -= Time.deltaTime;
            Timer.GetComponent<Text>().text = ((int)Timer_Count).ToString();
        }
    }

    public GameObject Timer;
    public float Timer_Count = 60f;

    public bool Time_Active;
}
