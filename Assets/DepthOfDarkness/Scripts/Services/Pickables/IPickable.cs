using UnityEngine;

namespace DD.Game {
    public interface IPickable {
        void Pick();
        void Drop();
        Transform GetTransform();
        SpriteRenderer GetRenderer();
        Shader GetDefaultShader();
    }
}