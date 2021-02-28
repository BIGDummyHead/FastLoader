using UnityEngine;

namespace FastandLow.Modding.Events
{
    /// <summary>
    /// Handles all Modded Events
    /// </summary>
    public class ModEventHandlers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="hp"></param>
        /// <param name="damage"></param>
        public delegate void EnemyDamageHandler(GameObject sender, enemyHp hp, ref float damage);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ai"></param>
        public delegate void EnemyDeathHandler(GameObject sender, basicAI ai);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ai"></param>
        public delegate void EnemySpawnHandler(GameObject sender, basicAI ai);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="hp"></param>
        public delegate void CivilianDeathHandler(GameObject sender, civilianHp hp);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="hp"></param>
        /// <param name="amount"></param>
        public delegate void CivilianDamageHandler(GameObject sender, civilianHp hp, ref float amount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ai"></param>
        public delegate void CivilianSpawnHandler(GameObject sender, civilianAI ai);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="player"></param>
        public delegate void PlayerSpawnHandler<T>(GameObject sender, T player);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="player"></param>
        public delegate void PlayerDeathHandler<T>(GameObject sender, T player);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="player"></param>
        /// <param name="dmg"></param>
        /// <param name="livesRemaining"></param>
        public delegate void PlayerDamageHandler<T>(GameObject sender, T player, ref int dmg, ref int livesRemaining);
    }
}
