# Apex Legends - Stat Tracker
![Pathfinder, our true love](https://trackercdn.com/cdn/apex.tracker.gg/legends/pathfinder-concept-bg-small.jpg)

> Our one and only MRVN - Pathfinder <3

## Screenshots
![OverView Page](/Images/OverViewPage.png)

> Overview page. This shows the current map with countdown untill the next, the current shop that can be filtered through and a panel to look up any players statistics.

![PlayerStats Page](/Images/PlayerStatsPage.png)

> Player statistics page. This shows the lifetime statistics on the left and the statistics per legend on the right.

## About This Project
### Project Description
This little .NET application shows the current shop and map rotation, and can be used to look up the stats of any Apex Legends player.
Written in C# for a Tool Development assignment at [Digital Arts and Entertainment](https://www.digitalartsandentertainment.be/), Howest Belgium.

### Why Apex Legends?
I decided to use this API for my assignment, not only because I am a huge Apex Legends fan, but also because it's a big dream of mine to one day land a job at Respawn Entertainment (We're allowed to dream big, right?). Apex Legends also ties in nicely with my studies at DAE.

### Switching Between Local And API Data
This should be quite straightforward. To use API data, press the "REFRESH SHOP" or "SEARCH PLAYER" buttons to fetch their data from the API. If you want to use the local data, press the "USE LOCAL DATA" buttons next to the previously stated buttons. In case something goes wrong fetching data from the API (Too many requests, API unreachable, ...), the application will fallback to the local data.

## Project API's
This project uses the following API's to fetch its data:
- [Apex Legends Status API](https://portal.apexlegendsapi.com/): Used to fetch the current shop and map rotation.
- [Tracer Network's API](https://apex.tracker.gg/site-api): Used to fetch a detailed player overview.
