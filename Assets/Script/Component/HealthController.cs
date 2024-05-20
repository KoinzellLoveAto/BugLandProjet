using RakaTools.Lock;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RakaEngine.Controllers.Health
{

    public class HealthController : MonoBehaviour
    {
        #region Events
        public Action<HealthController> EventSystem_onHealthChange;
        public Action<HealthController, float> EventSystem_onDammaged;
        public Action<HealthController, float> EventSystem_onHealed;
        public Action<HealthController> EventSystem_onDeath;
        public Action<HealthController> EventSystem_onFullHealthReached;
        public Action<HealthController> EventSystem_OnHealthInitialized;


        public UnityEvent<HealthController> UnityEvent_onHealthChange;
        public UnityEvent<HealthController, float> UnityEvent_onDammaged;
        public UnityEvent<HealthController, float> UnityEvent_onHealed;
        public UnityEvent<HealthController> UnityEvent_onDeath;
        public UnityEvent<HealthController> UnityEvent_onFullHealthReached;
        public UnityEvent<HealthController> UnityEvent_OnHealthInitialized;


        #endregion

        #region Variables
        /// <summary>
        /// Used to control if health should regen
        /// </summary>
        public bool IsRegenFrozen { get; private set; } = false;

        /// <summary>
        ///  Used to cannot take anymore dammage
        /// </summary>
        public bool GodMod { get; private set; } = false;

        [field: SerializeField]
        public float MinHealthPoint { get; private set; } = 0;
        [field: SerializeField]
        public float MaxHealthPoint { get; private set; } = 100;

        [field: SerializeField]
        public float StartHealth { get; private set; } = 100;
        public float currentHealth { get; private set; }


        protected LockerController m_locker_canBeHealed = new LockerController();
        protected LockerController m_locker_canBeDammaged = new LockerController();

        public bool CanBeHealed => !m_locker_canBeHealed.IsLock;
        public bool CanBeDammaged => !m_locker_canBeDammaged.IsLock;

        /// <summary>
        /// Used to avoid spaming event fullhealth if alreadyFull health
        /// </summary>
        protected bool m_haveAlreadyReachedFullLife = true;


        /// <summary>
        /// the value to regen each tick
        /// </summary>
        [field: SerializeField]
        protected float m_regenValueByTick = 5;

        /// <summary>
        /// The time between each Tick
        /// </summary>
        [field: SerializeField]
        protected float m_regenTickRate = .5f;


        protected Coroutine HealthRegenRoutine;
        #endregion

        #region Setters
        /// <summary>
        /// Used to control if health should regen
        /// </summary>
        /// <param name="a_value"></param>
        public virtual void FreezeHealthRegen(bool a_value = false) => IsRegenFrozen = a_value;

        /// <summary>
        ///  Used to cannot take anymore dammage
        /// </summary>
        /// <param name="a_value"></param>
        public virtual void SetGodMode(bool a_value) => GodMod = a_value;
        #endregion

        #region Methods


        public virtual void Initialize()
        {
            currentHealth = StartHealth;
            HealthRegenRoutine = StartCoroutine(HealthRegen_Routine());
            UnityEvent_OnHealthInitialized?.Invoke(this);
            EventSystem_OnHealthInitialized?.Invoke(this);

        }

        public void OnDestroy()
        {
            if (HealthRegenRoutine != null)
            {
                StopCoroutine(HealthRegenRoutine);
            }
        }

        /// <summary>
        /// Add a healLocker if "false" else, remove one
        /// </summary>
        /// <param name="a_value"></param>
        public void SetCanBeHealed(bool a_value)
        {
            if (!a_value)
                m_locker_canBeHealed.AddLocker();
            else
                m_locker_canBeHealed.RemoveLocker();
        }

        /// <summary>
        /// Add a dammageLocker if "false" else, remove one
        /// </summary>
        /// <param name="a_value"></param>
        public void SetCanBeDammaged(bool a_value)
        {
            if (!a_value)
                m_locker_canBeDammaged.AddLocker();
            else
                m_locker_canBeDammaged.RemoveLocker();
        }

        /// <summary>
        /// set heal locker to 0
        /// </summary>
        public void ClearCanBeHealedLocker() => m_locker_canBeHealed.ClearLockers();

        /// <summary>
        /// set dammaged locker to 0
        /// </summary>
        public void ClearCanBeDammagedLocker() => m_locker_canBeDammaged.ClearLockers();


        /// <summary>
        /// Use to dammage  Health (health will be clamped)
        /// </summary>
        /// <param name="a_amount"></param>
        public virtual void Dammage(float a_amount)
        {
            if (GodMod) return;

            if (!CanBeDammaged) return;

            currentHealth -= a_amount;

            //check if death
            if (currentHealth <= MinHealthPoint)
            {
                EventSystem_onDeath?.Invoke(this);
                UnityEvent_onDeath?.Invoke(this);
            }


            if (currentHealth <= MaxHealthPoint)
            {
                m_haveAlreadyReachedFullLife = false;
            }


            //Clamp health to not go out of range
            currentHealth = Mathf.Clamp(currentHealth, MinHealthPoint, MaxHealthPoint);


            if (currentHealth > MinHealthPoint)
            {
                //Call event due to health Consumed
                EventSystem_onHealthChange?.Invoke(this);
                EventSystem_onDammaged?.Invoke(this, a_amount);

                //Call UnityEvent
                UnityEvent_onHealthChange?.Invoke(this);
                UnityEvent_onDammaged?.Invoke(this, a_amount);
            }
        }

        /// <summary>
        /// Use to Heal (health will be clamped)
        /// </summary>
        /// <param name="a_amount"></param>
        public virtual void Heal(float a_amount)
        {
            if (!CanBeHealed) return;

            currentHealth += a_amount;


            if (currentHealth >= MaxHealthPoint && !m_haveAlreadyReachedFullLife)
            {
                m_haveAlreadyReachedFullLife = true;

                EventSystem_onFullHealthReached?.Invoke(this);
                UnityEvent_onFullHealthReached?.Invoke(this);
            }

            //Clamp Health to not go out of range
            currentHealth = Mathf.Clamp(currentHealth, MinHealthPoint, MaxHealthPoint);

            //Call event due to Health added
            EventSystem_onHealthChange?.Invoke(this);
            EventSystem_onHealed?.Invoke(this, a_amount);

            //Call UnityEvent
            UnityEvent_onHealthChange?.Invoke(this);
            UnityEvent_onHealed?.Invoke(this, a_amount);
        }

        protected virtual IEnumerator HealthRegen_Routine()
        {
            while (true)
            {
                yield return new WaitForSeconds(m_regenTickRate);
                if (!IsRegenFrozen)
                {
                    Heal(m_regenValueByTick);
                }
            }
        }
        #endregion
    }
}

