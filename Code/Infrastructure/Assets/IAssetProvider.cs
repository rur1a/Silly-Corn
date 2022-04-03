using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.Assets
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}