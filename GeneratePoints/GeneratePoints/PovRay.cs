using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GeneratePoints.Models;

namespace GeneratePoints
{
    public class PovRay
    {

        public static string PreparePovRayFilesWithIniAndAnchorOutline(Settings settings, List<string> datapointsFilenames,
            string anchorsFilename, List<AnchorPoint> anchorPoints,
            string dirName)
        {
                        var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                return "";
            }

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }

            const string nocamFile = "fc-scene-layout.pov";
            var nocamPath = Path.Combine(directory, nocamFile);
            var withoutPath = datapointsFilenames.Select(file => Path.GetFileName(file)).ToList();
            anchorsFilename = Path.GetFileName(anchorsFilename);

            datapointsFilenames = withoutPath;
            var compiledFilename = Path.GetFileName(datapointsFilenames.First()) + ".pov";

            var compiledFile = Path.Combine(newDir, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }


            var noCamText = File.ReadAllText(nocamPath);


            var fileNameStr = "{";
            for (var index = 0; index < datapointsFilenames.Count; index++)
            {
                var file = datapointsFilenames[index];
                fileNameStr = fileNameStr + "\"" + file + "\"";
                if (index < datapointsFilenames.Count - 1)
                {
                    fileNameStr = fileNameStr + ",";
                }
            }

            fileNameStr = fileNameStr + "}";

            var myClockVar = "#declare Start = 0;\r\n#declare End = " + (settings.Calculation.FrameCount - 1) +
                             ";\r\n#declare MyClock = Start+(End-Start)*clock;\r\n";


            var filenameVar = "#declare FileNames = array[" + datapointsFilenames.Count + "] " + fileNameStr + ";\r\n";

            var pointsFileVar = "#declare strDatapointsFile = FileNames[MyClock]; \r\n";
            if (datapointsFilenames.Count == 1)
            {
                pointsFileVar = "#declare strDatapointsFile = FileNames[0]; \r\n";
            }

            var anchorsFileVar = "#declare strAnchorsFile = \"" + anchorsFilename + "\"; \r\n";
            var anchorRadiusVar = "#declare nAnchorRadius = " + settings.Render.AnchorRadius + "; \r\n";
            var datapointRadius = "#declare nDataPointRadius = " + settings.Render.DataPointRadius + "; \r\n";
            var pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "; \r\n";
            if (settings.Render.RenderProgressively)
            {
                pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "*clock; \r\n";
            }

            var anchorTransmit = "#declare nAnchorTransmit = " + settings.Render.AnchorTransmit + "; \r\n";

            var background = " background { color rgb <1, 1, 1> }";
            var clock = 0;
            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*" + settings.Render.CameraZoom + "," +
                settings.Render.CameraYOffset + ", cos(2*pi*" + clock + ")*" + settings.Render.CameraZoom +
                ">\t\t \r\n\tlook_at <" +
                settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            if (settings.Render.RotateCamera)
            {
                cameraString =
                    "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*clock)*" + settings.Render.CameraZoom + "," +
                    settings.Render.CameraYOffset + ", cos(2*pi*clock)*" + settings.Render.CameraZoom +
                    ">\t\t  \r\n\tlook_at <" +
                    settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                    ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";
            }

            foreach (var a in anchorPoints)
            {
                foreach (var o in anchorPoints)
                {
                    if (a.X == o.X && a.Y == o.Y && a.Z == o.Z)
                    {
                        continue;
                    }
                    var cylinderStr = $@"cylinder {{
 <{a.X},{a.Y},{a.Z}>,
 <{o.X},{o.Y},{o.Z}>,
 0.0005
open
 pigment {{ rgb <{a.R},{a.G},{a.B}> transmit 0.9 }}
}}                                  
";

                    cameraString += cylinderStr;
                }
            }

            var variableStrings = new List<string>
            {
                myClockVar,
                filenameVar,
                pointsFileVar,
                anchorsFileVar,
                anchorRadiusVar,
                datapointRadius,
                pointStop,
                anchorTransmit
            };
            if (!settings.Render.TransparentBackground)
            {
                variableStrings.Add(background);
            }

            variableStrings.Add(cameraString);

            var vString = string.Join("\r\n", variableStrings.ToArray());

            noCamText = vString + noCamText;


            File.WriteAllText(compiledFile, noCamText);

            WritePovrayIniFile(settings, dirName, compiledFilename);
            return compiledFilename;
            
            
        }

        public static string PreparePovRayFilesWithIniNew(Shape shape, Settings settings, List<string> datapointsFilenames, 
            string anchorsFilename,
            string dirName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                return "";
            }

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }
            anchorsFilename = Path.GetFileName(anchorsFilename);
            var withoutPath = datapointsFilenames.Select(file => Path.GetFileName(file)).ToList();
            datapointsFilenames = withoutPath;
            var compiledFilename = Path.GetFileName(datapointsFilenames.First()) + ".pov";
            var compiledFile = Path.Combine(newDir, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }
            
            var fileNameStr = "{";
            for (var index = 0; index < datapointsFilenames.Count; index++)
            {
                var file = datapointsFilenames[index];
                fileNameStr = fileNameStr + "\"" + file + "\"";
                if (index < datapointsFilenames.Count - 1)
                {
                    fileNameStr = fileNameStr + ",";
                }
            }
            fileNameStr = fileNameStr + "}";
            


            

            //For if there are multiple data point files
            var povVarFilesIndex = "MyClock";
            if (datapointsFilenames.Count == 1)
            {
                povVarFilesIndex = "0";
            }

            var povVarNPointStop = settings.Calculation.MaxDataPoints.ToString();
            if (settings.Render.RenderProgressively)
            {
                povVarNPointStop = settings.Calculation.MaxDataPoints.ToString() + "*clock";
            }

            var povVarCameraLocationX = settings.Render.CameraZoom.ToString();
            var povVarCameraLocationY = settings.Render.CameraYOffset;
            var povVarCameraLocationZ = settings.Render.CameraZoom.ToString();
            if (settings.Render.RotateCamera)
            {
                povVarCameraLocationX = $"sin(2*pi*clock)*{settings.Render.CameraZoom}";
                povVarCameraLocationZ = $"cos(2*pi*clock)*{settings.Render.CameraZoom}";
            }

            var povCameraLocation = $"<{povVarCameraLocationX},{povVarCameraLocationY},{povVarCameraLocationZ}>";
            var lookAt = $"<{settings.Render.LookAt[0]},{settings.Render.LookAt[1]},{settings.Render.LookAt[2]}>";
            

            var nAnchorVertFilter = 0.0;
            var renderBool = 0;
            if (settings.Render.RenderText)
            {
                renderBool = 1;
            }

            var areaLightBool = 0;
            if (settings.Render.IncludeAreaLights)
            {
                areaLightBool = 1;
            }
            
            var povContent = $@"
#include ""math.inc""
#include ""textures.inc""
#include ""glass.inc""
#declare Start = 0;
#declare End = {(settings.Calculation.FrameCount - 1)};
#declare MyClock = Start+(End-Start)*clock;

#declare FileNames = array[{datapointsFilenames.Count}] {fileNameStr};

#declare strDatapointsFile = FileNames[{povVarFilesIndex}]; 

#declare strAnchorsEdgesFile = ""{Path.GetFileName(shape.AnchorEdgePointsFile)}""; 
#declare strAnchorsVertexFile = ""{Path.GetFileName(shape.AnchorVertexPointsFile)}"";


#declare nDataPointRadius = {settings.Render.DataPointRadius}; 
 
#declare nDataPointPhong = {settings.Render.DataPointPhong};
#declare nDataPointPhongSize = 0.25;
#declare nDataPointAmbient = {settings.Render.DataPointAmbient};
#declare nDataPointDiffuse = 0.25;
#declare nDataPointReflection = 0.25;
#declare nDataPointSpecular = 0.25;
#declare nDataPointRoughness = 0.25;

#declare nAnchorVertRadius = {settings.Render.AnchorRadius};
#declare nAnchorVertFilter = {nAnchorVertFilter};

#declare nAnchorEdgeRadius = {settings.Render.EdgePointRadius}; 
#declare nAnchorEdgeFilter = {nAnchorVertFilter};
 
#declare nPointStop = {povVarNPointStop}; 
#declare nRenderText = {renderBool};
#declare nRenderAreaLights = {areaLightBool};
#declare nAnchorTransmit = {settings.Render.AnchorTransmit}; 

camera {{	
	location {povCameraLocation}		  
	look_at {lookAt}       	
	rotate <0,0,0>
}}

#fopen anchorsFile1 strAnchorsVertexFile read

#while (defined(anchorsFile1))
     #read (anchorsFile1,Vector1,Vector2)
   
light_source {{
  Vector1          
  color rgb Vector2  
        fade_distance 1
    fade_power 5
}}     
     
light_source {{
  Vector1
  color rgb Vector2
  spotlight                 
  point_at <0, 0, 0>      
  radius 1                
  tightness 10            
  falloff 1               
}}
#end

#if (nRenderAreaLights=1) 

light_source {{
  <0,0,0>             // light's position (translated below)
  color rgb 1.0       // light's color
  area_light
  <8, 0, 0> <0, 0, 8> // lights spread out across this distance (x * z)
  4, 4                // total number of lights in grid (4x*4z = 16 lights)
  adaptive 0          // 0,1,2,3...
  jitter              // adds random softening of light
  circular            // make the shape of the light circular
  orient              // orient light
  translate <0, 0, 100>   // <x y z> position of light
}}

#end


#fopen anchorsFile2 strAnchorsEdgesFile read
#while (defined(anchorsFile2))
     #read (anchorsFile2,Vector1,Vector2)
   
        sphere {{ Vector1, nAnchorEdgeRadius
      texture {{ pigment{{ rgb Vector2 filter nAnchorEdgeFilter }} }} }}
    
#end

#fclose anchorsFile1
#fclose anchorsFile2

#fopen dataPointsFile strDatapointsFile read
#declare pp = 0;
#declare dtSeconds1 = val(datetime(now,""%M"")) * 60 + val(datetime(now,""%S""));
#while (defined(dataPointsFile) & pp < nPointStop)  
#declare pp = pp + 1;
     #read (dataPointsFile,Vector1,Vector2)         
     
     sphere {{ 
        Vector1, nDataPointRadius
        texture {{ 
           pigment {{ 
             rgb Vector2
             }} 
           }} 
           finish {{ 
              phong nDataPointPhong
              phong_size nDataPointPhongSize               
              ambient nDataPointAmbient
              diffuse nDataPointDiffuse
              reflection nDataPointReflection
              specular nDataPointSpecular
              roughness nDataPointRoughness              
           }} 
      }}
#end
                            

#fclose dataPointsFile    


#declare dtSeconds2 = val(datetime(now,""%M"")) * 60 + val(datetime(now,""%S""));
#declare dtDiff = dtSeconds2 - dtSeconds1;

#declare parseTimeText = concat(""Parse time = "", str(dtDiff,0,0), "" s"");
#declare pointText = concat(""n = "",str(pp,0,0));
#declare radiusText = concat(""r = "",str(nDataPointRadius,0,5));

#declare titleText = ""Pentagon Chaos Game"";


#if (nRenderText=1) 
text {{ ttf ""arial.ttf"", titleText , 0.02, 0.0 // thickness, offset
       texture{{ pigment{{ color rgb<0.5,0.5,0.5>*1.3 }} 
              
                finish {{ phong 0.1 ambient 0.9 }}
              }} 
       scale<0.2,0.2,0.2>*0.1       
       translate<-1.2,0.97,0>
       rotate<0,-180,0>
      }} 



text {{ ttf ""arial.ttf"", radiusText, 0.02, 0.0 // thickness, offset
       texture{{ pigment{{ color rgb<0.5,0.5,0.5>*1.3 }} 
              
                finish {{ phong 0.1 ambient 0.9 }}
              }} 
       scale<0.2,0.2,0.2>*0.1       
       translate<-1.2,0.92,0>
       rotate<0,-180,0>
      }} 


text {{ ttf ""arial.ttf"", pointText, 0.02, 0.0 // thickness, offset
       texture{{ pigment{{ color rgb<0.5,0.5,0.5>*1.3 }} 
              
                finish {{ phong 0.1 ambient 0.9 }}
              }} 
       scale<0.2,0.2,0.2>*0.1       
       translate<-1.2,0.90,0>
       rotate<0,-180,0>
      }} 


text {{ ttf ""arial.ttf"", parseTimeText, 0.02, 0.0 // thickness, offset
       texture{{ pigment{{ color rgb<0.5,0.5,0.5>*1.3 }} 
              
                finish {{ phong 0.1 ambient 0.9 }}
              }} 
       scale<0.2,0.2,0.2>*0.1       
       translate<-1.2,0.95,0>
       rotate<0,-180,0>
      }} 
      
text {{ ttf ""arial.ttf"", str(clock,0,3), 0.02, 0.0 // thickness, offset
       texture{{ pigment{{ color rgb<0.5,0.5,0.5>*1.3 }} 
              
                finish {{ phong 0.1 ambient 0.9 }}
              }} 
       scale<0.2,0.2,0.2>*0.1       
       translate<-1.2,0.88,0>
       rotate<0,-180,0>
      }}             

text {{ ttf ""arial.ttf"", str(nPointStop,0,0), 0.02, 0.0 // thickness, offset
       texture{{ pigment{{ color rgb<0.5,0.5,0.5>*1.3 }} 
              
                finish {{ phong 0.1 ambient 0.9 }}
              }} 
       scale<0.2,0.2,0.2>*0.1       
       translate<-1.2,0.84,0>
       rotate<0,-180,0>
      }}       
#end                            


";

            


            File.WriteAllText(compiledFile, povContent);

            WritePovrayIniFile(settings, dirName, compiledFilename);
            return compiledFilename;
        }
        
        public static string PreparePovRayFilesWithIni(Settings settings, List<string> datapointsFilenames,
            string anchorsFilename,
            string dirName)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                return "";
            }

            var newDir = Path.Combine(directory, dirName);

            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }

            const string nocamFile = "fc-scene-layout.pov";
            var nocamPath = Path.Combine(directory, nocamFile);
            var withoutPath = datapointsFilenames.Select(file => Path.GetFileName(file)).ToList();
            anchorsFilename = Path.GetFileName(anchorsFilename);

            datapointsFilenames = withoutPath;
            var compiledFilename = Path.GetFileName(datapointsFilenames.First()) + ".pov";

            var compiledFile = Path.Combine(newDir, compiledFilename);
            if (File.Exists(compiledFile))
            {
                File.Delete(compiledFile);
            }


            var noCamText = File.ReadAllText(nocamPath);


            var fileNameStr = "{";
            for (var index = 0; index < datapointsFilenames.Count; index++)
            {
                var file = datapointsFilenames[index];
                fileNameStr = fileNameStr + "\"" + file + "\"";
                if (index < datapointsFilenames.Count - 1)
                {
                    fileNameStr = fileNameStr + ",";
                }
            }

            fileNameStr = fileNameStr + "}";

            var myClockVar = "#declare Start = 0;\r\n#declare End = " + (settings.Calculation.FrameCount - 1) +
                             ";\r\n#declare MyClock = Start+(End-Start)*clock;\r\n";


            var filenameVar = "#declare FileNames = array[" + datapointsFilenames.Count + "] " + fileNameStr + ";\r\n";

            var pointsFileVar = "#declare strDatapointsFile = FileNames[MyClock]; \r\n";
            if (datapointsFilenames.Count == 1)
            {
                pointsFileVar = "#declare strDatapointsFile = FileNames[0]; \r\n";
            }

            var anchorsFileVar = "#declare strAnchorsFile = \"" + anchorsFilename + "\"; \r\n";
            var anchorRadiusVar = "#declare nAnchorRadius = " + settings.Render.AnchorRadius + "; \r\n";
            var datapointRadius = "#declare nDataPointRadius = " + settings.Render.DataPointRadius + "; \r\n";
            var pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "; \r\n";
            if (settings.Render.RenderProgressively)
            {
                pointStop = "#declare nPointStop = " + settings.Calculation.MaxDataPoints + "*clock; \r\n";
            }

            var anchorTransmit = "#declare nAnchorTransmit = " + settings.Render.AnchorTransmit + "; \r\n";

            var background = " background { color rgb <1, 1, 1> }";
            var clock = 0;
            var cameraString =
                "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*" + clock + ")*" + settings.Render.CameraZoom + "," +
                settings.Render.CameraYOffset + ", cos(2*pi*" + clock + ")*" + settings.Render.CameraZoom +
                ">\t\t \r\n\tlook_at <" +
                settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";

            if (settings.Render.RotateCamera)
            {
                cameraString =
                    "\n\n\ncamera {\t\r\n\tlocation <sin(2*pi*clock)*" + settings.Render.CameraZoom + "," +
                    settings.Render.CameraYOffset + ", cos(2*pi*clock)*" + settings.Render.CameraZoom +
                    ">\t\t  \r\n\tlook_at <" +
                    settings.Render.LookAt[0] + "," + settings.Render.LookAt[1] + "," + settings.Render.LookAt[2] +
                    ">       \t\r\n\trotate <0,0,0>\r\n}\r\n";
            }

            var variableStrings = new List<string>
            {
                myClockVar,
                filenameVar,
                pointsFileVar,
                anchorsFileVar,
                anchorRadiusVar,
                datapointRadius,
                pointStop,
                anchorTransmit
            };
            if (!settings.Render.TransparentBackground)
            {
                variableStrings.Add(background);
            }

            variableStrings.Add(cameraString);

            var vString = string.Join("\r\n", variableStrings.ToArray());

            noCamText = vString + noCamText;


            File.WriteAllText(compiledFile, noCamText);

            WritePovrayIniFile(settings, dirName, compiledFilename);
            return compiledFilename;
        }

        private static void WritePovrayIniFile(Settings settings, string dirname, string povFilename)
        {
            var iniFile = povFilename + ".ini";

            var lines = new List<string>
            {
                "Input_File_Name=" + povFilename + "\n",
                "Output_File_Name=" + povFilename + "\r\n",
                "Initial_Frame=1",
                "Final_Frame=" + settings.Calculation.FrameCount
            };
            var iniPath = dirname + "/" + iniFile;
            File.WriteAllLines(iniPath, lines);
        }
    }
}