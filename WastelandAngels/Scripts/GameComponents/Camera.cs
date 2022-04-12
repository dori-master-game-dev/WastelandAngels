using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using WLA.System;

namespace WLA.GameComponents
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }

        private Matrix camTransMat;
        private Matrix resTransMat;

        private Vector3 camTransVec;
        private Vector3 resTransVec;

        public Camera(Vector2 position)
        {
            Position = position;

            Transform = Matrix.Identity;
            camTransMat = Matrix.Identity;
            resTransMat = Matrix.Identity;

            camTransVec = Vector3.Zero;
            resTransVec = Vector3.Zero;

            UpdateViewTransformationMatrix();
        }

        public void Follow(Vector2 position)
        {
            Position = position;

            UpdateViewTransformationMatrix();
        }

        public void Follow(GameObject gameObject)
        {
            Position = gameObject.GetCenter();

            UpdateViewTransformationMatrix();
        }

        private void UpdateViewTransformationMatrix()
        {
            camTransVec.X = -(int)Position.X;
            camTransVec.Y = -(int)Position.Y;

            Matrix.CreateTranslation(ref camTransVec, out camTransMat);

            resTransVec.X = Constants.WIDTH / 2f;
            resTransVec.Y = Constants.HEIGHT / 2f;

            Matrix.CreateTranslation(ref resTransVec, out resTransMat);

            Transform = camTransMat * resTransMat;
        }
    }
}
