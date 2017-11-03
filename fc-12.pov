#include "math.inc"

#declare tclock=0.1;

camera {	
	location <5*sin(2*pi*tclock), 2, -5*cos(2*pi*tclock)>		           
	look_at <0,0,0>       	
	rotate <0,0,0>
}                

      
light_source {
  <0,0,0>             // light's position 
  color rgb 1.0       // light's color
  area_light
  <8, 0, 0> <0, 0, 8> // lights spread out across this distance (x * z)
  5, 5                // total number of lights in grid (4x*4z = 16 lights)
  adaptive 0          // 0,1,2,3...
  jitter              // adds random softening of light
  circular            // make the shape of the light circular
  orient              // orient light
  translate <40, 80, -40>   // <x y z> position of light
}
      

    

fog{ fog_type   2
     distance   100
     color      rgb<1,1,1> *0.8 
     fog_offset 0.1
     fog_alt    1.5
     turbulence 1.8
   } 



plane { y, -11
		pigment { checker rgb <0.1, 0.1, 0.1> rgb <1.0, 1.0, 1.0> scale 5 }
		finish { reflection 0.2 ambient 0.4 }
}  


	

sky_sphere {
		pigment { gradient y
			color_map {
				[0 rgb <0.5, 0.6, 1> ]
				[1 rgb <0, 0, 1> ]
			}
		}
		pigment { 
		    wrinkles turbulence 0.6
			color_map {
				[0 rgbt <1,1,1,1>]
				[0.5 rgbt <0.98, 0.99, 0.99, .6>]
				[1 rgbt <1, 1, 1, 1>]
		}
        scale <.8, .1, .8>
    }
} 

           
#declare Rnd_1 = seed (1153);           

#declare Ball =
sphere{<0,0,0>,0.006
       texture{
        
        finish {phong 1}
       } 
}   

#declare Trace = sphere{<0,0,0>,0.15
       texture{
                                   
        pigment{color rgb<0.5,0.5,0.5> }
        finish {phong 1 reflection 0.2}
       } 
}   


#declare startNum = 0;
#declare endNum = (2400000);   


#declare Anchor = sphere{<0,0,0>,0.05
       texture{
        pigment{color rgb<1,0.65,0.65>}
        finish {phong 1 reflection 0.1}
       } 
}      
           
#declare anchorPoints = 12;

#declare anchorsX = array[anchorPoints]{0,0,0,0,1,1,-1,-1,1.6,-1.6,1.6,-1.6};
#declare anchorsY = array[anchorPoints]{1,-1,1,1,1.6,-1.6,1.6,-1.6,0,0,0,0};
#declare anchorsZ = array[anchorPoints]{1.6,1.6,-1.6,-1.6,0,0,0,0,1,1,-1,-1};
           
#declare anchorColors = array[anchorPoints]{
rgb<1,0,0>,
rgb<1,0.501960784,0>,
rgb<1,1,0>,
rgb<0.501960784,1,0>,
rgb<0,1,0>,
rgb<0,1,0.501960784>,
rgb<0,1,1>,
rgb<0,0.501960784,1>,
rgb<0,0,1>,
rgb<0.498039216,0,1>,
rgb<1,0,1>,
rgb<1,0,0.498039216>
}



#declare anchorIndex = 0;
#while(anchorIndex < anchorPoints)
    object {Anchor translate<anchorsX[anchorIndex],anchorsY[anchorIndex],anchorsZ[anchorIndex]> pigment{color anchorColors[anchorIndex] transmit 0.5} }
    
    light_source {
  0*x                  
  color anchorColors[anchorIndex]  
 translate<anchorsX[anchorIndex],anchorsY[anchorIndex],anchorsZ[anchorIndex]>
}
  
    #declare anchorIndex = anchorIndex + 1;
#end
  
     
