using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.NetWorking;

public class MemberForm
{
    public int index;
    public string name;
    public int age;
    public int gender;

    public MemberForm()
}

public class ExampleManager : MonoBehaviour
{

    IEnumerator Start()
    {
        form.AddField();
        using ()
        {
            yield return request.SendWebRequest();
            downloadHandler.text;

            // ���信 ���� �۾�

        }
    }

    using (UnityWebRequest request = UnityWebRequest.Get(URL))
    {
        yield return request.SendWebRequest();
        MemberForm json = JsonUtility.FromJson<MemberForm>(request.downloadHandler.text);

    // ���信 ���� �۾�
    print(json.index);
    }
    
    public void NextScene()
{

}
    
    }