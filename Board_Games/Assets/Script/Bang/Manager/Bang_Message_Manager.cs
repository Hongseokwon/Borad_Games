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
        Active_Message_List = new LinkedList<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create_Message(string _Message,int _Line)
    {
        Message_List[Cnt % 11].SetActive(true);
        Message_List[Cnt % 11].GetComponent<Text>().text = _Message;
        Message_List[Cnt % 11].GetComponent<RectTransform>().sizeDelta = new Vector2(Size_X, Size_Y * _Line);
        Message_List[Cnt % 11].GetComponent<RectTransform>().anchoredPosition = new Vector2(Pos_X, Pos_Y + (0.5f * _Line));

        Move_Message(_Line);
        Check_Message();

        Active_Message_List.AddLast(Message_List[Cnt % 11]);
        ++Cnt;
    }

    private void Move_Message(int _Line)
    {
        foreach(GameObject Obj in Active_Message_List)
        {
            Vector2 temp = Obj.GetComponent<RectTransform>().anchoredPosition;
            temp.y += _Line * Size_Y;
            Obj.GetComponent<RectTransform>().anchoredPosition = temp;
        }
    }

    private void Check_Message()
    {
        if (Active_Message_List.Count < 1 &&
            Active_Message_List.First.Value.GetComponent<RectTransform>().anchoredPosition.y > 384)
        {
            Active_Message_List.First.Value.SetActive(false);
            Active_Message_List.RemoveFirst();
        }
    }

    private LinkedList<GameObject> Active_Message_List;

    public GameObject[] Message_List;

    private int Cnt = 0;
    private float Size_X = 300f;
    private float Size_Y = 26.04f;
    private float Pos_X = 362f;
    private float Pos_Y = 123.6f;
}
