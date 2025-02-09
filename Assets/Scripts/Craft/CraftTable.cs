using Assets.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour, IItemHolder
{
    [LabelText("加工类型")] public CraftType SupportCraftType;

    //public GameObject ProgressBarPrefab; // 环形进度条预制体（UI Canvas下的预制体）
    public Transform ItemAttachPoint;

    private ProgressBar _progressBar;
    private bool isProcessing;
    private float timer;

    public Transform TopPoint { get => ItemAttachPoint; }

    public Item CurrentItem { get; set; }

    void Start()
    {
        // 初始化进度条
        _progressBar = GetComponentInChildren<ProgressBar>();
        _progressBar.Show(false);
    }

    void Update()
    {
        if (isProcessing)
        {
            CurrentItem.ProgressTimer += Time.deltaTime;
            var normalizedProgress = CurrentItem.ProgressTimer / CurrentItem.ProgressTime;
            if (normalizedProgress < 1)
            {
                _progressBar.UpdateProgress(normalizedProgress);
            }
            else
            {
                FinishProcessing();
            }

        }
    }

    public void TryStartProcessing()
    {
        if (ValidateCanProcess())
        {
            StartProcessing();
        }
    }

    void StartProcessing()
    {
        if (isProcessing) return;

        // 启动进度条
        _progressBar.Show(true);
        isProcessing = true;
    }

    void FinishProcessing()
    {
        Destroy(CurrentItem.gameObject);
        GameObject processedItem = Instantiate(CurrentItem.CraftTarget, ItemAttachPoint.position, Quaternion.identity);

        isProcessing = false;
        _progressBar.Show(false);
    }

    bool ValidateCanProcess()
    {
        return SupportCraftType == CurrentItem.CraftType;
    }


    public Item OnTookItem()
    {
        throw new System.NotImplementedException();
    }

    public void OnPutItem(Item item)
    {
        throw new System.NotImplementedException();
    }
}
