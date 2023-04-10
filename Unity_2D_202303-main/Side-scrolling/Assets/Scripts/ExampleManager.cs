using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class MemberForm
{
    public string Name;
    public int Age;

    public MemberForm(string userName, int age)
    {
        this.Name = userName;
        this.Age = age;
    }
}

public class ExampleManager : MonoBehaviour
{
    string URL = "https://script.google.com/macros/s/AKfycby1sbUN9a1nulIl8ykHEFSdlL9NM9oAnaJYbLIX6_BCOCriZXx_m4ScqvM0VlHa5UnN/exec";

    private IEnumerator Start()
    {
        // ��û�� �ϱ� ���� �۾�
        //UnityWebRequest request = UnityWebRequest.Get(URL);

        MemberForm member = new MemberForm("�����", 45);

        WWWForm form = new WWWForm();

        form.AddField("Name", member.Name);
        form.AddField("Name", member.Age);

        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        
        yield return request.SendWebRequest();


        //���信 ���� �۾�
        //print(request.downloadHandler.data);
       print(request.downloadHandler.text);
        
    }
}
