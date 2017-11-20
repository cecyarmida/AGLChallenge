This solution was developed for AGL following these requirements:
http://agl-developer-test.azurewebsites.net/


The solution comprises three projects:
1. Functions is an Azure function app that:
  + Reads AGL's Json
  + Maps to a different Json structure
  + Filters out nulls and non Cats pets.
  + Orders pet names
It is published on Azure on the following link: 
https://aglmapjason.azurewebsites.net/api/JsonMapper?code=e6ImfcGcYzhoxBAvjrMyGiLlPaOwDN7YIVRKe128Av0PAh2PQuGRaQ==

2. Call Json is an MVC project that:
  + Gets Json from the Azure Function 
  + Displays the results formatted on a web browser

NOTE: Run this project to see the Cat's names grouped by gender

3. Unit Test for Function App that includes:
  + Happy path test 
  + Error test

The included file AGL.jpg provides a high level overview of the solution. 

