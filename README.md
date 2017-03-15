[![Build status](https://ci.appveyor.com/api/projects/status/jt3jtnvqnyfstq25?svg=true)](https://ci.appveyor.com/project/Branimir123/photolife)
[![Coverage Status](https://coveralls.io/repos/github/Branimir123/PhotoLife/badge.svg?branch=master)](https://coveralls.io/github/Branimir123/PhotoLife?branch=master)

# Photo Life

## Interested in photography? Ambitious to learn from the best? Ambitious to share your own photos and receive reviews? This is the place for you!
---------------------------
## Fancy code coverage *RGB LED strip*

#### :bowtie: Using the *[Wemos D1](https://www.wemos.cc/product/d1.html)* (based on *ESP8266* and programmable in the *Arduino IDE*) I managed to create a simple sketch that changes RGB LED strip's colors according to the percentage of **code coverage**.

#### This is the schema I used to connect the RGB strip. I didn't use a breadboard. The N-Channel MOSFETs I bought are common - *[IRLN540N](http://www.infineon.com/dgdl/irl540n.pdf?fileId=5546d462533600a40153565fbd752565)*

![Connecting the RGB LED strip](/WemosD1-CodeCoverageLEDStrip/HowToConnect.png)

## TODOs: 
- Extract services
- Use cloud platform to store the photos
- Write tests
- Bring up code coverage to at least 80%
- Make admin page where news can be added, posts can be managed, users can be deleted
- Make user pages where they can see news and add posts (photos, they want to be seen and reviewed)
- Implement adding comments
- Implement counting views 
- Implement counting likes (for posts only)
- Improve user experience
- Impove user interface
