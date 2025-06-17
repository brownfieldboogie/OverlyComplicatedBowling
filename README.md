# OverlyComplicatedBowling
A simple game of bowling, implemented more complicated than it needs to be (on purpose).
Why? Bowling has just the right amount of rules and concepts to be simple to put into code, and still be complicated enough to easily over-engineer. That makes for a perfect playground for trying out different architectural decisions, logical components and technologies.

Inspired by [this blogpost by Ron Jeffries](https://codingdojo.org/kata/Bowling/)

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
- fix unhandled exceptions in web on timeout
- introduce more config variables and environment variables (e.g. baseaddress for external webservice)
- more UI prettier
