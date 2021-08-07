# ARtemis

# Introduction

<img src="https://user-images.githubusercontent.com/66104180/128610057-202270a9-be67-402c-aa03-5a56262e612b.jpg" width="400"/>
<img src="https://user-images.githubusercontent.com/66104180/128610070-dad78d68-e419-4d8a-a459-c4a62f3be232.jpg" width="400"/>

![Hnet-image (1)](https://user-images.githubusercontent.com/66104180/128610188-7c7a7b90-a421-4b87-bfe2-dd49104e78cf.gif)

Video demonstarting usage of the app: https://youtu.be/s7s3jTgDD7k 
https://youtu.be/1yLuDBYvhYk

ARtemis is an app that turns your smartphone into augmented reality aiming 
device for your bow (possibly for others projectile weapons too). It simplifies
the aiming process. Classical aiming devices for bow require you to perform overlapping
of three points - target, some point on the aiming device and some point on the bow string. 
The situation becomes even more difficult because the string is right in front of your 
eye and is blurred which creates an error during aiming. ARtemis solves all this problems
because the 3d picture of environment is being projected on a 2d screen of the phone thus
eliminating the necessity of using three points for aiming but only two - target and virtual
crosshair on the screen of the phone. Just like in the videogame. Note that in order to use
this app you should have some way to attach your device firmly to your bow. It is mandatory for the phone
to be motionless relative to bow. You can use some standart phone holders for it with some slight 
modifications. For now ARtemis works only with Huawei AR Engine and on Huawei and Honor devices
supporting AR, but in the future there will be support of other AR engines. Instructions of
using the app is below. 

# Version 0.1

Basic functionality: measuring distance to target, setting crosshairs for different positions (distances).

# Version 0.2

Added virtual practice target in the form of enemy archer. It will explode to pieces if you shoot at him.
Demonstration: https://youtu.be/1yLuDBYvhYk
Added functions of clearing and resetting different elemnts.
Added HUD button to turn hud on/off.

# Installation

The list of currently supported devices:
P30 / P30Pro / P40 / P40Pro / P40Pro+
Mate20 / Mate20Pro / Mate20RS / Mate 20X / Mate20X (5G) / Mate30 / Mate30Pro / Mate30RS / Mate30 (5G) / Mate30Pro (5G) / Mate X / Mate XS
Nova6 / Nova6-5G / Nova7 / Nova7Pro
Honor V20 / Honor 20 / Honor 20Pro / Honor V30 / Honor V30Pro / Honor 30S / Honor 30 Pro / Honor 30 Pro+ / Honor 10 X Lite

Huawei AR Engine must be installed on your device. It is a separate app that is being distributed through Huawei App Gallery.

In order to build a project you will need Unity 2017.4.4 with up-to-date Android SDK and JDK. 
Make sure you use Android 9.0 as target and minimum API level and also turn off Multithread Rendering.
Don't forget to calibrate the acceleration check in the code for your bow in order for the app to
correctly recognize the shot. 

# Operating instructions:

<img src="https://user-images.githubusercontent.com/66104180/128610104-fd4b698f-8f61-4826-b8fb-f41a3c079418.jpg" width="600"/>

Before using the app shoot several times so that your physical set up will come into equilibrium.

Firstly you should set up a target. Walk to the target and press the Target button (7). After this 
the app will remember a position of a target and will start measuring distance to it (8).

Then walk to your desired shooting position. Now you should perform a calibrating shot
for this position. Hold your bow. There is a gyroscope (3) and you can make an app
remember the tilt of your bow (4) by pressing Calibrate Gyro (7). Put the red crosshair (1)
ontop of the target and perform a shot. Stand still after the shot. Press Set button (7). The
blue crosshair (2) will appear. Using the directional buttons (6) align the blue crosshair
with the actual point where your arrow went. The red crosshair should remain ontop of point you
were aiming at. After you finished aligning the crosshair - press Ready (7). Also the virtual 
marker (5) will appear around you so that later you can return to ths very position. Now the 
device is calibrated for this position or for this destance. So every shot from this distance 
will go to the point beneath this blue crosshair. You can repeat this for several distances 
if you want. If you want to reset last crosshair and position - press Reset Last button (7).
If you want to clear all markers and crosshairs - press Clear All Markers button (7).
If you want to install practice dummy - press Dummy button (7). The dummy will be installed into
your current position. If you shoot dummy in the torso - it will explode. Before this make sure
at least one crosshair is set (The app checks if the first crosshair is adjasent to torso center).
You can reset the dummy pressing Dummy button again. If you want to turn interface elements off or on 
press HUD button (7). 
