using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Heist
{
    class CollidableObject : PhysicalObject
    {


        
       

        virtual public RotatedRectangle GetCollisionRotatedRectangle()
        {
            //CO new Rectangle((int)this.pos.X, (int)this.pos.Y, 564, 235);

            //Gives the RotatedRectangle which will be used as the bounding box
            Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, camera.cameraPos);

            return new RotatedRectangle(new Rectangle((int)transformedPosforCamera.X, (int)transformedPosforCamera.Y, 564, 235), 0);
        }

        public CollidableObject(Vector2 pos, List<CollidableObject> collidableObjects, Camera camera)
            : base(pos)
        {
            //Makes sure every collidableObject is added to the list, that is later used to iterate
            //through all the collidableObjects by the collision Manager
            collidableObjects.Add(this);
            
            //Sets the camera
            this.camera = camera;
        }

        public CollidableObject(Vector2 pos, List<CollidableObject> collidableObjects, Camera camera, Vector2 dimensions)
            : base(pos,dimensions)
        {
            //Makes sure every collidableObject is added to the list, that is later used to iterate
            //through all the collidableObjects by the collision Manager
            collidableObjects.Add(this);

            //Sets the camera
            this.camera = camera;
        }

        virtual public void CollisionDetected()
        {
            //Left blank because it should only be used by inherited classes
        }

        virtual public void PassValidPos(Vector2 validPos, float validAngle)
        {
            //Left blank because it should only be used by inherited classes
        }

        
    }
}
