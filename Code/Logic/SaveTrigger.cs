using Code.Infrastructure.Services;
using Code.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Code.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        private BoxCollider _collider;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!_collider) 
                _collider =GetComponent<BoxCollider>();
            Gizmos.color = new Color32(0, 255, 0, 128);
            Gizmos.DrawCube(transform.position+_collider.center, _collider.size);
        }
    }
}