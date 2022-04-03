using Code.Data;
using UnityEngine;

namespace Code.Infrastructure.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress Progress { get; set; }

        public void ClearProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}