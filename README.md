# Procedural Terrain Generation
This project used Unity to develop procedurally generated spherical planets using fractral perlin noise, custom mesh generation and dynamic shader in order to create realistic biomes and smooth variations based on varying world seeds.
## Project
![Demo Screenshot](WorldGeneration/Assets/ReadMeAssets/DemoSS.png)

### Core Features
1)  Five Distinct Planet Archetypes
      - **Earth - Like**: Mountains typically form contiguous, dome like uplifts concentrated toward continental interiors, while lower amplitude noise layers flatten out terrain near shorelines, resulting in wide coastal plains. Transition zones between elevations are gradual, supporting a varied terrain profile without harsh discontinuities. Land is dominant and large water regions are more lake-esque than large oceans.
      - **Mars - Like**: Has no water or clouds. The mountains are more slim and extreme with most of them having ice covered peaks, they form mountain ranges spanning the entire planet and theres a quick dropoff between mountainous regions and flatter terrain.
      - **Ice World**: Terrain is characterized by medium frequency noise with strong vertical amplification. Elevation is evenly distributed across the surface, resulting in a rolling, windswept topology with pervasive steep hills and limited flat zones. Unlike Earth or Mars, there are few large scale features and no dominant ranges or major valleys, just relentless elevation changes with near random distribution.
      - **Volcanic World**: Has no water or clouds and color pallete resmebles one of a planet engulfed in volcanoes and lava. Volcanoes are other considerably tall relative to surrounging regions and have an extrmeely wide base. Flat regions are few and often can be seen between valleys of differeing volcanoe bases. 
      - **Ocean - World**: Built from a noise profile heavily biased toward negative elevation, leading to a majority ocean coverage. Islands form from rare positive spikes in the base noise layer and are constrained in both height and spread by limiting vertical scale and amplitude across all layers. As a result, landforms remain low and isolated, with hilly terrain that gently rises from sea level and lacks any extended mountain systems.
  
2) World generation is fully deterministic and driven by user-input seeds, which control the offset and phase shift of each terrain layer. This ensures that each unique seed yields a distinct planetary layout, including terrain shape, biome distribution, and elevation profile. Seeds support alphnumeric and symbolic inputs.
3) Low resolution meshes offer rapid previews, while higher subdivisions create high fidelity terrains with smoother normals and more detailed geometry. Resolution settings directly affect vertex count and mesh granularity, impacting both terrain sharpness and performance.
4) Physics based water shader which Implements a custom Shader Graph using the Fresnel effect to simulate light refraction and reflectance across a dynamic water surface. Ocean depth is also accounted for giving shorelines a more prominent look while deep oceans are darker and vast.

#### [Try the project over WebGL](https://arccreate.github.io/proceduralGenerationBuild/)

## Terrain Generation on Plane
### Fractral Perlin Noise
**What is Perlin Noise?** Perlin Noise unlike normal noise is a coherent with changes occuring gradually. Singular perlin noise is smooth and doesn't resemble the jaggerdness of a mountain/land terrain; To counter this issue, octaves are used, multiple layers of noise with decreasing weight, creating more rugged and natural land generation. To control the octaves and their weight on the final height, we define 2 new variables.
- Lacunarity which controles increase in the frequency of octaves. Higher lacunarity results in higher number of detailed features.
- Persistence which controls the decrease in amplitude of octaves. Higher persistence results in more influence on final terrain, more scattered islands.  
<img src = WorldGeneration/Assets/ReadMeAssets/OctaveExplanation.png width = "300"> 

### Mesh Generation through Height Map
**Generating Simple Map of Terrain**.
The 1st iteration of the map was a black and white noise generation which had controls to modify lucanrity, offset, persistence and size of the map.
The 2nd iteration seeked to turn the black and white values into a heightmap which would then be used for coloring. This was done by iterating throughtout the whole matrix coords, and smoothstepping it over 0 - 1 where pure black is ocean and pure white is mountain peaks. This color scheme was purely generated on the height at a certain point and therefore the change from region to region is apparent and abrubt. 


## Terrain generation on Isosphere
## Biome Coloration and Shaders
## Resources Used
## Steps to Build Upon