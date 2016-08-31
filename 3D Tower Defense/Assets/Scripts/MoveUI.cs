using UnityEngine;
using System.Collections;

public class MoveUI : MonoBehaviour {

    public float moveSpeed;
    public float scrnHightPercent;
    public float epsilon;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 movDist;
    private bool isOpen = true;
	
    public void Move()
    {
        movDist.Set(0, Screen.height * scrnHightPercent, 0);
        pos1 = transform.position + movDist;
        pos2 = transform.position - movDist;

        StartCoroutine(OpenClose());
    }

    private IEnumerator OpenClose()
    {
        if (isOpen)
        {
            float curDist = Vector3.Distance(transform.position, pos1);
            while (curDist > epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos1, moveSpeed);
                curDist = Vector3.Distance(transform.position, pos1);
                yield return new WaitForEndOfFrame();
            }
            isOpen = false;
        } else
        {
            float curDist = Vector3.Distance(transform.position, pos2);
            while(curDist > epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos2, moveSpeed);
                curDist = Vector3.Distance(transform.position, pos2);
                yield return new WaitForEndOfFrame();
            }
            isOpen = true;
        }
    }
}
