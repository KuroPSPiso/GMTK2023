using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    public Rect Range;

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Color c = Gizmos.color;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(this.Range.x, this.Range.y, 0), new Vector3(this.Range.x + this.Range.width, this.Range.y, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(this.Range.x, this.Range.y + this.Range.height, 0), new Vector3(this.Range.x + this.Range.width, this.Range.y + this.Range.height, 0));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(this.Range.x, this.Range.y, 0), new Vector3(this.Range.x, this.Range.y + this.Range.height, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(this.Range.x + this.Range.width, this.Range.y, 0), new Vector3(this.Range.x + this.Range.width, this.Range.y + this.Range.height, 0));

        Gizmos.color = c;
    }
}
