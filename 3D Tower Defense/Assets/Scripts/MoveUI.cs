using UnityEngine;
using System.Collections;

public class MoveUI : MonoBehaviour {

    public float openCloseSpeed;
    public float scrnHeightPercent;
    public float epsilon;

    private Vector3 pos1;
    private Vector3 pos2;
    private bool isOpen = true;
	
    public void Move()
    {
        Vector3 moveDist = new Vector3(0, Screen.height * scrnHeightPercent, 0);
        pos1 = transform.position + moveDist;
        pos2 = transform.position - moveDist;

        float moveSpeed = Vector3.Distance(pos1, pos2) / openCloseSpeed * Time.deltaTime;

        StartCoroutine(OpenClose(moveSpeed));
    }

    private IEnumerator OpenClose(float moveSpeed)
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
