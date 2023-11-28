using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetter : Singleton<GameSetter>
{
    #region ����
    [Header("�׷���")]
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private int vSyncCount = 0;

    [Header("�Ҹ�")]
    [SerializeField] private float soundVolume = 1;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] AudioSource audioSource;

    Dictionary<int, int> frameDict;
    #endregion ����

    #region �ʱ� ����
    protected override void Awake()
    {
        base.Awake();

        AssignObjects();

    }

    private void Start()
    {
        SetDictionaries();

        SceneManager.activeSceneChanged += OnSceneChanged;
        LoadSetting();
        SetAllValues();
        AddEvents();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignObjects();
        //SetDictionaries();
        //LoadSetting();
        SetAllValues();
    }

    private void AssignObjects()
    {
        volumeSlider = GameObject.Find("Volume Slider").GetComponent <Slider>();
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    private void AddEvents()
    {
        SaveManager.Instance.SaveAll.AddListener(SaveSetting);
    }
    #endregion �ʱ� ����

    #region �� ����
    void SetDictionaries()
    {
        frameDict = new Dictionary<int, int> {
            { 0, 30 },
            { 1, 60 },
            { 2, 120 },
        };
    }

    private void SetAllValues()
    {
        Application.targetFrameRate = targetFPS;
        audioSource.volume = soundVolume;
        QualitySettings.vSyncCount = vSyncCount;
        SaveSetting();
    }

    public void SetFrameRate(int value)
    {
        targetFPS = frameDict[value];
        Application.targetFrameRate = targetFPS;
        SaveSetting();
    }

    public void SetVSyncCount(int value)
    {
        vSyncCount = value;
        QualitySettings.vSyncCount = vSyncCount;
        SaveSetting();
    }

    public void SetVolume()
    {
        soundVolume = (float)volumeSlider.value / volumeSlider.maxValue;
        audioSource.volume = soundVolume;
        SaveSetting();
    }
    #endregion �� ����

    #region ����
    public void SaveSetting()
    {
        SaveManager.instance.SaveTargetFPS(targetFPS);
        SaveManager.instance.SaveVSyncCount(vSyncCount);
        SaveManager.instance.SaveSoundVolume(soundVolume);
    }

    public void LoadSetting()
    {
        targetFPS = SaveManager.instance.LoadTargetFPS();
        vSyncCount = SaveManager.instance.LoadVSyncCount();
        soundVolume = SaveManager.instance.LoadSoundVolume();
    }
    #endregion ����
}
