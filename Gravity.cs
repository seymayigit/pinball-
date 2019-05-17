public void play(ref double xspeed, ref double yspeed, ref double newyspeed, ref double startingypos, ref double newxpos, ref double newypos, ref double oldxpos, ref double oldypos, ref double newx, ref double oldx, ref double newy, ref double oldy, ref double acc, ref double t, ref int xmouse, ref int ymouse, ref bool dragging, ref bool trace, ref bool collisiony)
        {
            
            location.x = (int)newxpos;
            location.y = (int)newypos;

            if (dragging)
            {
                acc = (double)10;
                location.x = xmouse;
                location.y= ymouse;

                startingypos = ground - location.y;

                newx = location.x;
                newy = ground - location.y;
                xspeed = (newx - oldx) / 1;
                yspeed = (newy - oldy) / 1;
                oldx = newx;
                oldy = newy;

                t = 0;

            }
            else
            {
                acc = (double)10;			// ACCELERATION   
                oldxpos = location.x;
                
				// X-AXIS
                if (location.x < 357 && 0 < location.x)
                {	
                    newxpos = oldxpos + xspeed;			// NORMAL
                }
                else
                {
                    xspeed *= -0.9;						 //	WALL
                    newxpos = oldxpos + xspeed;
                }

                // Y-AXIS
                if (0 < newypos || collisiony)
                {
                    newyspeed = yspeed - (acc * t);
                    newypos = startingypos + ((yspeed * t) - 0.5 * acc * (t * t));
                    collisiony = false;
                }
                else
                {  // Here the ball will hits the ground
                    // Initialize the ball variables again
                    startingypos = -1;
                    // Here set startingypos=-1 not 0, because if 0 newypos will be 0 every time the ball 
                    // Hits the ground so no bouncing will happens to the ball, evaluate to the 
                    // eguation below for newypos when t = 0
                    t = 0;
                    // Ball yspeed will decrease every time it hits the ground
                    // 0.75 is the elasticity coefficient - assumption
                    // The initial speed(yspeed) is 0.75 of the final speed(newyspeed)
                    yspeed = newyspeed * -0.75;													// UP MOVE
                    newypos = startingypos + ((yspeed * t) - 0.5 * acc * (t * t));
                    collisiony = true;
                }
				
				
				
				
                // Always
                // Ball xspeed will always decrease, even if it didn't hit the wall
                xspeed *= 0.99;	// Air resistance

                if (xspeed > -0.5 && xspeed < 0)
                    xspeed = 0;

                // Update the ball position
                location.x = (int)newxpos;
                location.y = (int)(ground - newypos);
                // Increase the time
                t += 0.3;
            }
        }