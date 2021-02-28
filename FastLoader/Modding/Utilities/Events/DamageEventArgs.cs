using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon;
using Photon.Pun;

namespace FastandLow.Modding.Utilities.Events
{
    /// <summary>
    /// When damage happens, usually on an NPC or Character
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DamageEventArgs<T> : ModEventArgs<T> where T : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="dmg"></param>
        /// <param name="alive"></param>
        public DamageEventArgs(T instance, float dmg = 0, bool alive = false) : base(instance)
        {
            Damage = dmg;
            Alive = alive;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="args"></param>
        /// <param name="dmg"></param>
        /// <param name="alive"></param>
        public DamageEventArgs(T instance, object[] args, float dmg = 0, bool alive = false) : base(instance, args)
        {
            Damage = dmg;
            Alive = alive;
        }

        /// <summary>
        /// Is the Sender Alive?
        /// </summary>
        public bool Alive { get; }

        /// <summary>
        /// Is the Sender dead?
        /// </summary>
        public bool Dead => !Alive;

        /// <summary>
        /// How much damage was taken? 0 If Dies
        /// </summary>
        public float Damage { get; }

        /// <summary>
        /// Was there damage even taken?
        /// </summary>
        public bool DamageTaken => Damage > 0;
    }
}
