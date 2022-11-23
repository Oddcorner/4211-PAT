import csv

prevTeam = None
teams = {}
currentTeam = set()

def bestPlayer(players):
    def f(player):
        return -1 * (int(player[2]) + int(player[3]) + int(player[3]))

    sorted(players, key=f)

    while True:
        for player in players:
            if player not in currentTeam:
                currentTeam.add(player)
                return player
        players = teams[players[0][0]]

def pos(players, check, exc=[]):
    ans = []
    for player in players:
        for c in check:
            for e in exc:
                if c in player[1].upper() and e not in player[1].upper():
                    ans.append(player)
    if len(ans) == 0:
        ans = players
    return ans
    
    
def validPlayer(player):
    if (player[2] == "#VALUE!"
        or player[3] == "#VALUE!"
        or player[4] == "#VALUE!"):
        return False
    return True

with open("teams.csv") as f:
    rows = csv.reader(f)
    header = next(rows)

    # initalise team dictionary
    player = next(rows)
    while player is not None:
        prevTeam = player[0]
        team = []
        while player[0] == prevTeam:
            if validPlayer(player):
                team.append(tuple(player))
            try:
                player = next(rows)
            except StopIteration:
                player = None
                break
        if len(team) >= 6:
            teams[prevTeam] = team

    # compute teams for each team
    for teamName, players in teams.items():
        teamName = teamName.replace(" ", "_")
        teamName = teamName.replace("-", "_")
        teamName = teamName.replace("(", "")
        teamName = teamName.replace(")", "")
        teamName = teamName.replace(".", "")
        teamName = teamName.replace("'", "")
        try:
            RS = bestPlayer(pos(players, ["RS"]))
            MB = bestPlayer(pos(players, ["M"]))
            OH1 = bestPlayer(pos(players, ["OH","OPP"]))
            OH2 = bestPlayer(pos(players, ["OH","OPP"]))
            DS = bestPlayer(pos(players, ["DS","LIB"]))
            S = bestPlayer(pos(players, ["S"],["DS","RS"]))
            with open("team-setup.txt", "a") as w:
                w.write(
                    f"var <Player> {teamName}a = new Player({RS[2]},{RS[3]},{RS[4]}); // {RS[1]}\n"
                    f"var <Player> {teamName}b = new Player({MB[2]},{MB[3]},{MB[4]}); // {MB[1]}\n"
                    f"var <Player> {teamName}c = new Player({OH1[2]},{OH1[3]},{OH1[4]}); // {OH1[1]}\n"
                    f"var <Player> {teamName}d = new Player({OH2[2]},{OH2[3]},{OH2[4]}); // {OH2[1]}\n"
                    f"var <Player> {teamName}e = new Player({DS[2]},{DS[3]},{DS[4]}); // {DS[1]}\n"
                    f"var <Player> {teamName}f = new Player({S[2]},{S[3]},{S[4]}); // {S[1]}\n"
                    f"var <Team> {teamName} = new Team({teamName}a,{teamName}b,{teamName}c,{teamName}d,{teamName}e,{teamName}f);\n\n"
                )
            currentTeam.clear()
        except Exception as e:
            print(e)
            pass



     