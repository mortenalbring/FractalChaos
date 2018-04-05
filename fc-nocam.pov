#include "math.inc"

               
 
      
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
  translate <0, 8, 0>   // <x y z> position of light
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
  translate <0, -8, 0>   // <x y z> position of light
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
  translate <0, 0, 0>   // <x y z> position of light
}  

  
// create a regular point light source                                  
light_source {
  0*x                  // light's position (translated below)
  color rgb <1,1,1>    // light's color
  translate <0, 0, 0>
}

      
                                  



#fopen anchorsFile strAnchorsFile read

#while (defined(anchorsFile))
     #read (anchorsFile,Vector1,Vector2)
      sphere { Vector1,  nAnchorRadius
      texture {        
        
      pigment{ 
        rgbf Vector2 transmit nAnchorTransmit
      }
      
      }
      finish {reflection 0.2 ambient 0.4 }
      }  
      
      
  #end


#fopen dataPointsFile strDatapointsFile read
#declare pp = 0;

#while (defined(dataPointsFile) & pp < nPointStop)  
#declare pp = pp + 1;
     #read (dataPointsFile,Vector1,Vector2)         
     
      sphere { Vector1,    nDataPointRadius
      texture {
      pigment{ rgb Vector2}
      }      }
  #end

                            

#fclose dataPointsFile    
#fclose anchorsFile