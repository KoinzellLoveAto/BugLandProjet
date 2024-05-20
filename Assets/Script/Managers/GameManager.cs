using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController PlayerControllerPrefab;
    public MainMenuCanva MainCanvaMenuPrefab;

    private PlayerController m_controllerInstance;
    private MainMenuCanva m_mainMenuCanvaInstance;

    private ControlMap m_controlMap;


    public bool IsGamePaused { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            m_controlMap = new ControlMap();


        }
        else
        {
            Destroy(gameObject);
        }
        InstanciateInstanceForLevel();
        RegisterEvent();
    }


    private void InstanciateInstanceForLevel()
    {
        m_controllerInstance = Instantiate(PlayerControllerPrefab);
        m_mainMenuCanvaInstance = Instantiate(MainCanvaMenuPrefab);


        m_controlMap.MenuController.Enable();

        m_controllerInstance.InitalizeController(m_controlMap);
        m_controllerInstance.RegisterEvents();
    }


    private void RegisterEvent()
    {
        LevelEventManager.OnLevelStarted += HandleLevelStart;
        LevelEventManager.OnLevelWin += HandleLevelWin;
        m_controllerInstance.linkedCharacter.healthController.EventSystem_onDeath += HandleGameOver;
        m_controlMap.MenuController.Pause.performed += HandlePausePerformed;

    }
    private void UnRegisterEvent()
    {
        if (m_controllerInstance == null) return;

        m_controllerInstance.StopListeningInput();
        m_controllerInstance.linkedCharacter.healthController.EventSystem_onDeath -= HandleGameOver;
        m_controlMap.MenuController.Pause.performed -= HandlePausePerformed;

    }



    public static void ManagePause()
    {
        if (Instance.IsGamePaused)
        {
            Instance.m_controllerInstance.RegisterEvents();
            Instance.IsGamePaused = false;
            Time.timeScale = 1;
            Instance.m_mainMenuCanvaInstance.ShowPausePanel(false);
        }
        else
        {
            if (Instance.m_controllerInstance.linkedCharacter.healthController.currentHealth > 0)
            {
                Instance.m_controllerInstance.StopListeningInput();
                Instance.IsGamePaused = true;
                Time.timeScale = 0;
                Instance.m_mainMenuCanvaInstance.ShowPausePanel(true);
            }
        }
    }

    public static void SetPause(bool a_value)
    {
        if (!a_value)
        {
            Instance.m_controllerInstance.RegisterEvents();
            Instance.IsGamePaused = false;
            Time.timeScale = 1;
            Instance.m_mainMenuCanvaInstance.ShowPausePanel(false);
        }
        else
        {
            Instance.m_controllerInstance.StopListeningInput();
            Instance.IsGamePaused = true;
            Time.timeScale = 0;
            Instance.m_mainMenuCanvaInstance.ShowPausePanel(true);
        }
    }


    private void HandleLevelWin(int a_nextlevel)
    {
        UnRegisterEvent();
        SceneManager.LoadScene(a_nextlevel);
    }

    private void HandlePausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ManagePause();
    }

    private void HandleGameOver(HealthController a_healthController)
    {
        m_controllerInstance.linkedCharacter.healthController.EventSystem_onDeath -= HandleGameOver;
        UnRegisterEvent();
        m_mainMenuCanvaInstance.ShowGameOverPanel(true, 2f);
    }

    private void HandleLevelStart(Transform a_spawnPoint)
    {
        InstanciateInstanceForLevel();
        RegisterEvent();

        SetPause(false);

        m_controllerInstance.transform.position = a_spawnPoint.position;
    }



    public static PlayerController GetPlayerController()
    {
        return Instance.m_controllerInstance;
    }

}
