using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Novel : MonoBehaviour
{
    [SerializeField]
    private VisualNovel viusalNovelSystem;
    [SerializeField]
    private Selection selectionObject;

   private IEnumerator Start()
    {
        yield return new WaitUntil(() => viusalNovelSystem.UpdateDialog());
    }
}
