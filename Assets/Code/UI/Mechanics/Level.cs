using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VRMain.Assets.Code.UI.Mechanics
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private int _level;
        [SerializeField]
        private GameObject _lock;
        private bool _isLocked = true;
        public int GetLevel => _level;
        public void Unlock()
        {
            _isLocked = false;
            _lock.SetActive(false);
        }

        public void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => GoToLevel());
        }

        public void GoToLevel()
        {
            if(!_isLocked)
            {
                SceneManager.LoadScene(_level);
            }
        }
    }
}