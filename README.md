# Environment
* Unity: 2019.4.17
* Universal RenderPipeline(Customized): 7.3.1
* ShaderGraph(Customized): 7.3.1

# Installation
1. Clone the repository
2. Go to your Unity project
3. Set URP, ShaderGraph in this repo as your local custom packages(If you are not familiar with local package, then go to next step)
  
# Local package setup
1. Open 'mainfest.json' in 'your Unity project folder/Package'
2. Modifiy two lines for URP, ShaderGraph dependencies like below (Add when lines don't exist)

**Example**
- original
```
{
  "dependencies": {
    // ...
    "com.unity.render-pipelines.universal": "7.3.1",
    "com.unity.shadergraph": "7.3.1",
    // ...
  }
}
```
        
- after changing(absolute path)
```
{
  "dependencies": {
    // ...
    "com.unity.render-pipelines.universal": "file:D:/Projects/8_Company/URP_SG_CUSTOM/com.unity.render-pipelines.universal@7.3.1/",
    "com.unity.shadergraph": "file:D:/Projects/8_Company/URP_SG_CUSTOM/com.unity.shadergraph@7.3.1/",
    // ...
  }
}
```

- after changing(relative path)
```
{
  "dependencies": {
    // ...
    "com.unity.render-pipelines.universal": "file:../../../8_Company/URP_SG_CUSTOM/com.unity.render-pipelines.universal@7.3.1/",
    "com.unity.shadergraph": "file:../../../8_Company/URP_SG_CUSTOM/com.unity.shadergraph@7.3.1/",
    // ...
  }
}
```
