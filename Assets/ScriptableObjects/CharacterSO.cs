using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_JobData", menuName = "Datas/SO_JobData")]
public class JobSO : ScriptableObject
{
    public string displayTitle;
    public string description;
    public GameObject characterModelPrefab;
    public Defines.JobType jobType;

    public StatData stat;
}
