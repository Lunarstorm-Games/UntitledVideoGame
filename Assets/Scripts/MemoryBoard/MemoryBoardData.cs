using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class MemoryBoardData
{
    public string meleeSkeletonHistory;
    public string mageSkeletonHistory;
    public string bruteSkeletonHistory;
    public string goblinHistory;
    public string orcHistory;
    public string lichHistory;
    
    public MemoryBoardData(MemoryBoardUIController memoryBoardUIController)
    {
        meleeSkeletonHistory = memoryBoardUIController.MeleeSkeletonTracker.text;
        mageSkeletonHistory = memoryBoardUIController.MageSkeletonTracker.text;
        bruteSkeletonHistory = memoryBoardUIController.BruteSkeletonTracker.text;
        goblinHistory = memoryBoardUIController.GoblinTracker.text;
        orcHistory = memoryBoardUIController.OrcTracker.text;
        lichHistory = memoryBoardUIController.LichTracker.text;
    }


   
}
