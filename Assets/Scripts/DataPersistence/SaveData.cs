using System;

namespace Asteroids.DataPersistence.Data
{
    [Serializable]
    public class SaveData
    {
        public int HighScore = 0;
        public float MasterVolume = 1.0f;
        public float EffectsVolume = 1.0f;
        public float MusicVolume = 1.0f;
        public float UIVolume = 1.0f;
    }
}