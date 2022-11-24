# CS4211 PAT Volleyball Model Results

This repository contains the results of 100 games from the [NAIA](https://naiastats.prestosports.com/sports/wvball/2022-23/teams?sort=name&r=0&pos=) 2022 Womens volleyball season, and compares it to the predicted results from a custom model developed using the [Process Analysis Toolkit](https://pat.comp.nus.edu.sg/).

We found that the model was able to correctly predict the outcome of 67% of the games. These results can be viewed [here](https://github.com/Oddcorner/4211-PAT/blob/main/results.csv).

## Technologies

- [Process Analysis Toolkit](https://pat.comp.nus.edu.sg/)
- [Python 3.10.6](https://www.python.org/downloads/release/python-3106/)

## Setup

To aid in the team composition setup to be used in the model (if you plan on modifying the starting player conditions), a python script to automate team setup is also included in this repository [here](https://github.com/Oddcorner/4211-PAT/blob/main/generator.py).

## Acknowledgments

Normalised team data found in [teams.csv](https://github.com/Oddcorner/4211-PAT/blob/main/teams.csv) was obtained through the efforts of team member [@yusufaine](https://github.com/yusufaine/) in his [repository](https://github.com/yusufaine/4211-webscrape).
