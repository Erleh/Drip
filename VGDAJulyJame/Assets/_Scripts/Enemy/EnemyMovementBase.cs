using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyMovementBase
{
    // what to do when the path is found
    void OnPathFound(Vector2[] path, bool pathFound);

    // how the path will be followed
    IEnumerator FollowPath();
}
