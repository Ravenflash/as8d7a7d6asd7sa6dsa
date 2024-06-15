using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenflash.GamePrototype
{
    public class SaveSystem : ISaveSystem
    {
        const string KEY_STAGE_ID = "Stage_ID";
        const string KEY_TOTAL_SCORE = "Total_Score";

        public void Save(SaveData data)
        {
            PlayerPrefs.SetInt(KEY_STAGE_ID, data.stageId);
            PlayerPrefs.SetInt(KEY_TOTAL_SCORE, data.totalScore);
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            int stageId = PlayerPrefs.GetInt(KEY_STAGE_ID);
            return new SaveData(PlayerPrefs.GetInt(KEY_STAGE_ID), PlayerPrefs.GetInt(KEY_TOTAL_SCORE));

        }

        public void Clear() => Save(new SaveData());
    }
}
