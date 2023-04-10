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
        // 요청을 하기 위한 작업
        //UnityWebRequest request = UnityWebRequest.Get(URL);

        MemberForm member = new MemberForm("변사또", 45);

        WWWForm form = new WWWForm();

        form.AddField("Name", member.Name);
        form.AddField("Name", member.Age);

        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        
        yield return request.SendWebRequest();


        //응답에 대한 작업
        //print(request.downloadHandler.data);
       print(request.downloadHandler.text);
        
    }
}
