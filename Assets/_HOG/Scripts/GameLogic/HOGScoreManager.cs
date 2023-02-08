using System.Collections.Generic;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGScoreManager
    {
        public HOGPlayerScoreData PlayerScoreData;

        public bool TryGetScoreByTag(ScoreTags tag, ref int scoreOut)
        {
            if (PlayerScoreData.ScoreByTag.TryGetValue(tag, out var score))
            {
                scoreOut = score;
                return true;
            }

            SetScoreByTag(tag);
            return false;
        }

        public void SetScoreByTag(ScoreTags tag, int amount = 0)
        {
            HOGManager.Instance.EventsManager.InvokeEvent(HOGEventNames.OnScoreSet, (tag, amount));
            PlayerScoreData.ScoreByTag[tag] = amount;
        }
        
        public void ChangeScoreByTagByAmount(ScoreTags tag, int amount = 0)
        {
            if (PlayerScoreData.ScoreByTag.ContainsKey(tag))
            {
                SetScoreByTag(tag, PlayerScoreData.ScoreByTag[tag] + amount);
            }
            else
            {
                SetScoreByTag(tag, amount);
            }
        } 
    }

    public class HOGPlayerScoreData
    {
        public Dictionary<ScoreTags, int> ScoreByTag = new();
    }

    public enum ScoreTags
    {
        MainScore = 0,
        KillsScore = 1,
        ShaharScore = 2,
    }
}