using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon;
using Photon.Pun;

namespace FastandLow.Modding.Utilities.Events
{
    public sealed class DamageEventArgs<T> : ModEventArgs<T> where T : MonoBehaviourPunCallbacks
    {
        public DamageEventArgs(T instance, float dmg = 0, bool alive = false) : base(instance)
        {
            Damage = dmg;
            Alive = alive;
        }

        public DamageEventArgs(T instance, object[] args, float dmg = 0, bool alive = false) : base(instance, args)
        {
            Damage = dmg;
            Alive = alive;
        }

        public bool Alive { get; }
        public bool Dead => !Alive;
        public float Damage { get; }
        public bool DamageTaken => Damage > 0;
    }
}
