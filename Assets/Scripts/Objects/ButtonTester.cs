using UnityEngine;

public class ButtonTester :  MonoBehaviour, ITriggerable
{
    public void ExecuteFunction()
    {
        Debug.Log("버튼작동");
    }
    public void RevokeFunction()
    {
        Debug.Log("버튼작동해제");
    }
}
