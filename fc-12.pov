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
  
                                   


#declare xpoint = 0;
#declare ypoint = 0; 
#declare zpoint = 0;

#declare colorr = 0.5;
#declare colorg = 0.5;
#declare colorb = 0.5;

#fopen MyFile "GeneratePoints\GeneratePoints\GeneratePoints\bin\Debug\test.txt" read


#while (defined(MyFile))
     #read (MyFile,Vector)
      sphere { Vector,    0.006
      texture {
      pigment{ rgb <1,0,0>}
      }      }
  #end


/*
#declare nrx = 0;
#while(nrx < endNum)       
              #declare diceRoll = rand(Rnd_1);  
              #declare arrayIndex = 0;  
              
                                   
            #switch(diceRoll) 
                #range(0,(1/12))
                  #declare arrayIndex = 0;                                                                                                             
                #break                                                    
                
                #range((1/12),(2/12))
                  #declare arrayIndex = 1;                                                                                                              
                #break                         
                
                
                #range((2/12),(3/12))
                  #declare arrayIndex = 2;                                                                            
                #break                                                                                              

                #range((3/12),(4/12))
                  #declare arrayIndex = 3;                                                                            
                #break                                                                                              

                #range((4/12),(5/12))
                  #declare arrayIndex = 4;                                                                            
                #break                                                                                              

                #range((5/12),(6/12))
                  #declare arrayIndex = 5;                                                                            
                #break                                                                                              

                #range((6/12),(7/12))
                  #declare arrayIndex = 6;                                                                            
                #break                                                                                              

                #range((7/12),(8/12))
                  #declare arrayIndex = 7;                                                                            
                #break                                                                                              

                #range((8/12),(9/12))
                  #declare arrayIndex = 8;                                                                            
                #break                                                                                              

                #range((9/12),(10/12))
                  #declare arrayIndex = 9;                                                                            
                #break                                                                                              
                
                #range((10/12),(11/12))
                  #declare arrayIndex = 10;                                                                            
                #break                                                                                              

                #range((11/12),(12/12))
                  #declare arrayIndex = 11;                                                                            
                #break                                                                                              
                
                
             
            #end                 
            
              
               
                
                #declare newAnchorColor = anchorColors[arrayIndex];
                
                #declare colorr = (colorr + newAnchorColor.red) /2;
                #declare colorg = (colorg + newAnchorColor.green) /2;
                #declare colorb = (colorb + newAnchorColor.blue) /2;
                
                #declare xpoint = (xpoint + anchorsX[arrayIndex])/2;
                #declare ypoint = (ypoint + anchorsY[arrayIndex])/2;
                #declare zpoint = (zpoint + anchorsZ[arrayIndex])/2;   
                
              
                object {Ball translate<xpoint,ypoint,zpoint> pigment{color rgb<colorr,colorg,colorb> } }                                       
                                                                                                      

    #declare nrx = nrx + 1;
#end


*/

//object {Trace translate<xpoint,ypoint,zpoint>  }                                       

