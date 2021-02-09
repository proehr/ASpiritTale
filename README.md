# A Spirit Tale

This is a **modified** version of the VR Game A Spirit Tale. We have **removed all paid assets** from the file system for this repository. The Japanese inspired and Medieval Scene will not appear as they do in game, all other Scenes may have some missing assets as well. The purpose of this is mainly to **share our code**.

## Getting Started

* #### Game setup

To get started with developing, clone this repository and open the folder via Unity Hub.
Make sure to connect your Oculus Quest to your PC and activate its' Developer Mode via the Android App.
You can select your Oculus Device from the List of available Devices in the Unity Build Setup for Android.
If you execute "Build and Run" on your Device the Game will start.

More about general [Development Guidelines](#development-guidelines)  

* #### Controls for player movement

## Built With
Software
* [Unity3d](https://unity3d.com/de/unity/whats-new/2019.4.12) -	Unity3d Version 2019.4.12f1
* [Universal Render Pipeline](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.3/manual/index.html) - Version 7.3.1
* [Oculus XR Plugin](https://docs.unity3d.com/Packages/com.unity.xr.oculus@1.4/manual/index.html) - Version 1.4.3

Operating system
* [Windows 10](https://www.microsoft.com/de-de/windows/) - 	Windows 10 Professional 64
* [DirectX 11](https://support.microsoft.com/de-de/help/179113/how-to-install-the-latest-version-of-directx) - DirectX 11

## Development Guidelines

### Git Workflow

***Feature-Branch-Workflow***  
1. Before starting your work on a new feature, always pull the newest version of [main](https://github.com/nimaazha/OculusQuestFitnessApp/tree/main) branch.  
2. Create a new branch from main and give it a descriptive Name 
    * You can use your trello card id and name for this, which can be found in the card url (for example 12-decide-on-a-git-workflow)
3. You can now work on your feature, commit and push changes inside of your Feature Branch. Make sure to always choose **descriptive** commit messages.
4. Once you have pushed all your changes for the feature, create a pull request to main branch in GitHub and assign a team member for code review.
5. If your pull request has been approved, the pull request can be merged into main.
    * If you encounter merge conflicts, merge main branch into your feature branch **locally** and push the merge commit afterwards.

### Code Review 

This is a list of things you can look out for while doing code review:
* Does the code follow all naming conventions?
* Is the code too complex or can it be done simpler?
* Are all comments clear and helpful?
* Has the developer provided/updated documentation for their changes?
* Does the code function the way it is intended?
* Does the code follow our style guides?

### Other Guidelines

Always create your own scene if you want to test changes, the MainScene should be in a functioning state at all times.  
If you do make changes to a Scene you want to share, make sure to save your changes in unity before pushing to git.  
If you want to share Scene Elements with your team, create [Prefabs](https://docs.unity3d.com/Manual/Prefabs.html) and make sure they are placed properly.  

## License
*Student's project for module [Practical Project](https://lsf.htw-berlin.de/qisserver/rds?state=modulBeschrGast&moduleParameter=modDescr&struct=auswahlBaum&navigation=Y&next=tree.vm&nextdir=qispos/modulBeschr/gast&nodeID=auswahlBaum%7Cabschluss%3Aabschl%3D84%7Cstudiengang%3Astg%3D919%7CstgSpecials%3Avert%3D%2Cschwp%3D%2Ckzfa%3DH%2Cpversion%3D20112%7CkontoOnTop%3Apordnr%3D28584%7Cpruefung%3Apordnr%3D28562&expand=0&lastState=modulBeschrGast&asi=#auswahlBaum%7Cabschluss%3Aabschl%3D84%7Cstudiengang%3Astg%3D919%7CstgSpecials%3Avert%3D%2Cschwp%3D%2Ckzfa%3DH%2Cpversion%3D20112%7CkontoOnTop%3Apordnr%3D28584%7Cpruefung%3Apordnr%3D28562) by [INTERNATIONAL COURSE OF STUDIES MEDIA INFORMATICS](https://https://imi-bachelor.htw-berlin.de/) at [HTW Berlin](https://www.htw-berlin.de/)*  
The course was provided by [**Prof. Dr.-Ing. David Strippgen**](https://www.htw-berlin.de/hochschule/personen/person/?eid=4293) at winter semester 2020/21.  
This Game has been distributed with Unity3d.

## References
* Animation Rigging: https://www.youtube.com/watch?v=Htl7ysv10Qs
* Canvas Pointer: https://www.youtube.com/watch?v=8fT478uopco
* Controller Pointer: https://www.youtube.com/watch?v=F60UIo7Y1YY
* Dissolving Material Shader: https://glowfishinteractive.com/dissolving-the-world-part-1/
* Visual Enemy Indicator: https://www.youtube.com/watch?v=BC3AKOQUx04


## Resources
#### "Asset Store" contents 
1. [Oculus Integration for Unity](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022)
2. [Warrior Pack Bundle 1 FREE](https://assetstore.unity.com/packages/3d/animations/warrior-pack-bundle-1-free-36405)
3. [Distant Lands Free Characters](https://assetstore.unity.com/packages/3d/characters/distant-lands-free-characters-178123)
4. [Sci-Fi Sfx](https://assetstore.unity.com/packages/audio/sound-fx/sci-fi-sfx-32830)
5. [UI Sfx](https://assetstore.unity.com/packages/audio/sound-fx/ui-sfx-36989)
6. [Absolutely Free Music](https://assetstore.unity.com/packages/audio/music/absolutely-free-music-4883)
7. [Asian Style Village (Low Poly) - **11,60€**](https://assetstore.unity.com/packages/3d/environments/historic/asian-style-village-low-poly-155411)
8. [Low Poly Fantasy Medieval Village - **8,92€**](https://assetstore.unity.com/packages/3d/environments/fantasy/low-poly-fantasy-medieval-village-163701)
9. [Star Nest Skybox](https://assetstore.unity.com/packages/vfx/shaders/star-nest-skybox-63726)
10. [Cartoon Fx Free](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-free-109565)
11. [KY Magic Effects Free](https://assetstore.unity.com/packages/vfx/particles/spells/ky-magic-effects-free-21927)
12. [Unity Particle PAck 5.x](https://assetstore.unity.com/packages/essentials/asset-packs/unity-particle-pack-5-x-73777)
13. [Ball Pack](https://assetstore.unity.com/packages/3d/props/ball-pack-446)
14. [Simple Dissolve Shader](https://assetstore.unity.com/packages/vfx/shaders/simple-dissolve-shader-123865)
15. [Polylised - Medieval Desert City](https://assetstore.unity.com/packages/3d/environments/historic/polylised-medieval-desert-city-94557)
16. [Desert Kits 64 Sample](https://assetstore.unity.com/packages/3d/environments/landscapes/desert-kits-64-sample-86482)
17. [Simple FX - Cartoon Particles](https://assetstore.unity.com/packages/vfx/particles/simple-fx-cartoon-particles-67834)
18. [Furnished Cabin](https://assetstore.unity.com/packages/3d/environments/urban/furnished-cabin-71426)
19. [Lamp Model](https://assetstore.unity.com/packages/3d/props/interior/lamp-model-110960)
20. [Toon Furniture](https://assetstore.unity.com/packages/3d/props/furniture/toon-furniture-88740)
21. [Office Room Furniture](https://assetstore.unity.com/packages/3d/props/furniture/office-room-furniture-70884)
22. [Food and Kitchen Props Pack](https://assetstore.unity.com/packages/3d/props/food-and-kitchen-props-pack-85050)
23. [Snaps Prototype | Office](https://assetstore.unity.com/packages/3d/environments/snaps-prototype-office-137490)
25. [Masks pack 2](https://assetstore.unity.com/packages/3d/props/clothing/accessories/masks-pack-2-157577)
26. [Urban Night Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/urban-night-sky-134468)
27. [Free Suburban Structure Kit](https://assetstore.unity.com/packages/3d/props/free-suburban-structure-kit-142401)
28. [Horror Elements Audio Pack](https://assetstore.unity.com/packages/audio/sound-fx/horror-elements-112021#content)

#### Other Music & Sound contents
1. [Medieval Loop One by Alexander Nakarada](https://www.free-stock-music.com/alexander-nakarada-medieval-loop-one.html)
2. [Tavern Loop One by Alexander Nakarada](https://www.free-stock-music.com/alexander-nakarada-tavern-loop-one.html)
3. [Yugen by Keys of Moon](https://www.free-stock-music.com/keys-of-moon-yugen.html)
4. [Wuxia2_Orchestra by PeriTune](https://www.free-stock-music.com/peritune-wuxia2-orchestra.html)
5. [Sakura 2020 by Roa Music](https://www.free-stock-music.com/roa-music-sakura-2020.html)
6. [Ronin by yoitrax](https://www.free-stock-music.com/yoitrax-ronin.html)
7. [Rising Sun by Yoshinori Tanaka / 田中 芳典](https://www.free-stock-music.com/yoshinori-tanaka-rising-sun.html)
8. [The Legend of Narmer by WombatNoisesAudio](https://www.free-stock-music.com/wombat-noises-audio-the-legend-of-narmer.html)
9. [FREE | SOUND FX COLLECTION](https://soundbits.de/product/free-sound-fx/)
10. [Jab by Mike Koenig](https://soundbible.com/995-Jab.html)
11. [Fireworks](https://www.fesliyanstudios.com/royalty-free-sound-effects-download/fireworks-282)


#### Other content
* [Font: Asobi Memogaki Regular](http://font.sumomo.ne.jp/shigoto.html)
* [Paper Material from Japanese Scene](https://pixelbuddha.net/textures/free-vintage-japanese-paper-textures)
* [Paper Material from Medieval and Oriental Scene](https://www.deviantart.com/spar6/art/Paper-Texture-Pack-9-papers-104432181)

## Authors (sorted alphabetically)
* **Bartholomäus Berresheim** - [Github](https://github.com/Silices)
* **Juri Wiechmann** - [Github](https://github.com/LuffyGM)
* **Marie Lencer** - [Github](https://github.com/MarieLencer)
* **Nima Azimihashemi** - [Github](https://github.com/nimaazha)
* **Nuri Son**
* **Pauline Röhr** - [Github](https://github.com/proehr)
