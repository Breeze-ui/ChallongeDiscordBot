﻿
namespace ChallongeCSharpDriver.Main {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ChallongeCSharpDriver.Core;
    using ChallongeCSharpDriver.Core.Results;
    using ChallongeCSharpDriver.Core.Queries;
    using System.Threading.Tasks;
    using ChallongeCSharpDriver.Main.Objects;

    public class Tournaments {
        ChallongeAPICaller caller;
        private string TournamentSubdomain { get; }

        public Tournaments(ChallongeAPICaller caller, string subdomain = "") {
            this.caller = caller;
            this.TournamentSubdomain = subdomain;
        }

        public async Task<List<StartedTournament>> getStartedTournaments() {
            List<TournamentResult> tournamentResultList = await new TournamentsQuery() {
                state = TournamentState.InProgress
            }
            .call(caller);
            List<StartedTournament> tournamentList = new List<StartedTournament>();
            foreach (TournamentResult result in tournamentResultList) {
                tournamentList.Add(new TournamentObject(result, caller));
            }
            return tournamentList;
        }

        public async Task<TournamentObject> getTournament(string tournamentID) {
            TournamentResult tournamentResult = await new TournamentQuery(tournamentID, TournamentSubdomain).call(caller);
            return new TournamentObject(tournamentResult, caller);
        }
    }
}
