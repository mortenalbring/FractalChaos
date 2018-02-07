#include "math.inc"

#declare tclock=0.1;

camera {	
	location <2*sin(2*pi*tclock), 2, -4*cos(2*pi*tclock)>		           
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

           

                                   



#fopen anchorsFile "GeneratePoints\GeneratePoints\GeneratePoints\bin\Debug\anchors.txt" read

#while (defined(anchorsFile))
     #read (anchorsFile,Vector)
      sphere { Vector,    0.06
      texture {
      pigment{ rgb <1,1,0>}
      }      }
  #end


#fopen MyFile "GeneratePoints\GeneratePoints\GeneratePoints\bin\Debug\test2.txt" read


#while (defined(MyFile))
     #read (MyFile,Vector)
      sphere { Vector,    0.010
      texture {
      pigment{ rgb <1,0,0>}
      }      }
  #end

                            

