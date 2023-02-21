using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bang_Message_Manager : MonoBehaviour
{
    private static Bang_Message_Manager instance = null;

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

    public static Bang_Message_Manager Instance
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create_Message(string _Message,int _Line)
    {
        Move_Message(_Line);

        Message_List[Cnt % 11].SetActive(true);
        Message_List[Cnt % 11].GetComponent<Text>().text = _Message;
        Message_List[Cnt % 11].GetComponent<RectTransform>().sizeDelta = new Vector2(Size_X, Size_Y * _Line);
        Message_List[Cnt % 11].GetComponent<RectTransform>().anchoredPosition = new Vector2(Pos_X, Pos_Y + (0.5f * _Line * Size_Y));
        
        Message_Cnt[Cnt % 11] = 0;
        ++Cnt;
    }

    private void Move_Message(int _Line)
    {
        for(int i=0; i<11;++i)
        {
            if(Message_List[i].activeSelf)
            {
                Vector2 temp = Message_List[i].GetComponent<RectTransform>().anchoredPosition;
                temp.y += _Line * Size_Y;
                Message_List[i].GetComponent<RectTransform>().anchoredPosition = temp;
                Message_Cnt[i] += _Line;
            }

            if(Message_Cnt[i] > 9)
                Message_List[i].SetActive(false);

        }
    }
    
    public GameObject[] Message_List;
    private int[] Message_Cnt = new int[11];

    private int Cnt = 0;
    private float Size_X = 300f;
    private float Size_Y = 26.04f;
    private float Pos_X = 362f;
    private float Pos_Y = 123.6f;
}
