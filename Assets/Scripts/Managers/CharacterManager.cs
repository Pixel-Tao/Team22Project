
using Defines;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private const string playerPrefabName = "Player";
    private Player player;
    public Player Player { get { return player; } }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void AddItem(ItemSO itemSO)
    {
        if (itemSO.itemType == Defines.ItemType.Resource)
        {
            // 자원 증가
        }
        else
        {
            Player.AddItem(itemSO);
        }
    }

    public void LoadPlayer(JobType jobType)
    {
        // 플레이어 생성
        GameObject playerObj = ResourceManager.Instance.Instantiate(playerPrefabName);
        playerObj.name = "Player";
        Player player = Utils.GetOrAddComponent<Player>(playerObj);
        JobChange(JobType.Babarian);
    }

    public void JobChange(JobType jobType)
    {
        JobSO jobSO = ResourceManager.Instance.GetSOJobData<JobSO>(JobType.Babarian);
        Player.SetJob(jobSO);
    }
}

