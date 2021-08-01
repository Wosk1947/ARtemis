# ARtemis

# Introduction

<img src="https://user-images.githubusercontent.com/66104180/127740710-99eb2960-6bb5-4822-b3a3-8e7599a883ef.jpg" width="400"/>
<img src="https://user-images.githubusercontent.com/66104180/127740700-0287b059-ac49-4f09-ab9d-5c4b54e9fd73.jpg" width="400"/>

Video demonstarting usage of the app: https://youtu.be/s7s3jTgDD7k

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

# Installation

The list of currently supported devices:
P30 / P30Pro / P40 / P40Pro / P40Pro+
Mate20 / Mate20Pro / Mate20RS / Mate 20X / Mate20X (5G) / Mate30 / Mate30Pro / Mate30RS / Mate30 (5G) / Mate30Pro (5G) / Mate X / Mate XS
Nova6 / Nova6-5G / Nova7 / Nova7Pro
Honor V20 / Honor 20 / Honor 20Pro / Honor V30 / Honor V30Pro / Honor 30S / Honor 30 Pro / Honor 30 Pro+ / Honor 10 X Lite

Huawei AR Engine must be installed on your device. It is a separate app that is being distributed through Huawei App Gallery.

In order to build a project you will need Unity 2017.4.4 with up-to-date Android SDK and JDK. 
Make sure you use Android 9.0 as target and minimum API level and also turn off Multithread Rendering. 

# Operating instructions:

<img src="https://user-images.githubusercontent.com/66104180/127740649-e414acc7-dfeb-4af0-b6b7-c7f15739e55b.jpg" width="600"/>

Firstly you should set up a target. Walk to the target and press the Target button (7). After this 
the app will remember a position of a target and will start measuring distance to it (8).

Then walk to your desired shooting position. Now you should perform a calibrating shot
for this position. Hold your bow. There is a gyroscope (3) and you can make an app
remember the tilt of your bow (4) by pressing Calibrate Gyro (7). Put the red crosshair (1)
ontop of the target and perform a shot. Stand still after the shot. Press Set button (7). The
red dot (2) will appear. Using the directional buttons (6) align the red dot
with the actual point where your arrow went. The red crosshair should remain ontop of point you
were aiming at. After you finished aligning the dot - press Ready (7). Also the virtual 
marker (5) will appear around you so that later you can return to ths very position. Now the 
device is calibrated for this position or for this destance. So every shot from this distance 
will go to the point beneath this blue crosshair. You can repeat this for several distances 
if you want. 
