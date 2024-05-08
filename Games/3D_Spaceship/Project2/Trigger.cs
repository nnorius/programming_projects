using BEPUphysics.Entities.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BEPUutilities;

using Vector3 = BEPUutilities.Vector3;
using BEPUphysics.CollisionRuleManagement;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.CollisionTests;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Microsoft.Xna.Framework;

namespace Project2
{
    internal class Trigger
    {
        Box Box;
        public bool collided = false;


        public Trigger(Game1 game, Vector3 position)
        {
            //Create static box of size 1x1x1 at origin
            Box = new(position, 4, 4, 4);

            //Add Box to physics space
            game._space.Add(Box);

            //Disable solver to make box generate collision events but no affect physics (like a trigger in unity)
            //More about collision rules: https://github.com/bepu/bepuphysics1/blob/master/Documentation/CollisionRules.md
            Box.CollisionInformation.CollisionRules.Personal = CollisionRule.NoSolver;

            //Add collision start listener
            //More about collision events: https://github.com/bepu/bepuphysics1/blob/master/Documentation/CollisionEvents.md
            Box.CollisionInformation.Events.ContactCreated += CollisionHappened;
        }

        //Handle collision events
        void CollisionHappened(EntityCollidable sender, Collidable other, CollidablePairHandler pair, ContactData contact)
        {
            Console.WriteLine("Collision detected.");
            collided = true;
        }

    }
}
