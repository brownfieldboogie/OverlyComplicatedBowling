# OverlyComplicatedBowling
## To start
Requires docker. Open your terminal, navigate to the repo, and execute:
1. docker compose build
2. docker compose up
Then, open your browser and go to http://localhost:8080

## Todo
There's still a lot of stuff to do..

- implement unit of work
- implement generic repository
- use environment variables in docker
- fix https for API and Web
- make multiplayer (introduce "match" as an object containing multiple games - one pr bowler)
- fix unhandled exceptions in web on timeout
- introduce more config variables and environment variables (e.g. baseaddress for external webservice)
- more UI prettier
