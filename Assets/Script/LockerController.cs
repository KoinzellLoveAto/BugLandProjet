using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RakaTools.Lock
{
    public class LockerController
    {
        private int m_lockCount = 0;
        public bool IsLock => m_lockCount != 0;

        public void AddLocker()
        {
            m_lockCount++;
        }

        public void RemoveLocker()
        {
            m_lockCount--;
            if (m_lockCount <= 0)
                m_lockCount = 0;
        }

        /// <summary>
        /// Set locker count at 0
        /// </summary>
        public void ClearLockers()
        {
            m_lockCount = 0;
        }


        /* 
        ladder => 2event trigger (in & out PlayerIn/outTrigger)
        player => un script spécial pour écouté les events de l'échelle
        -> avec ce script tu va gerer ta gestion d'input pour rester accrocher
        ce dernier peut communiqué avec les autre script de ton perso
         */
    }
}