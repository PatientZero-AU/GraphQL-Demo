/* tslint:disable */
//  This file was automatically generated and should not be edited.

export type AverageWinsTicketsQuery = {
  teams:  Array< {
    name: string,
    ticketsSoldCount: number | null,
    winRateAvg: number | null,
  } | null > | null,
};

export type VenueTicketSalesQuery = {
  venues:  Array< {
    name: string,
    ticketSales:  Array< {
      item1: string | null,
      item2: number | null,
    } | null > | null,
  } | null > | null,
};

export type AllTeamsQuery = {
  teams:  Array< {
    name: string,
    contestants:  Array< {
      id: string | null,
      name: string,
    } | null > | null,
  } | null > | null,
};

export type ContestantQueryVariables = {
  contestantId: string,
};

export type ContestantQuery = {
  contestant:  {
    id: string | null,
    name: string,
    dominantHand: number,
    heightCm: number | null,
    weightKg: number | null,
    reachCm: number | null,
    strikesMin: number | null,
  } | null,
};

export type AllMatchesQuery = {
  matches:  Array< {
    id: string | null,
    date: string | null,
    homeTeam:  {
      name: string,
    } | null,
    awayTeam:  {
      name: string,
    } | null,
  } | null > | null,
};

export type MatchQueryVariables = {
  matchId: string,
};

export type MatchQuery = {
  match:  {
    date: string | null,
    id: string | null,
    homeTeam:  {
      name: string,
      heightCmAvg: number | null,
      weightKgAvg: number | null,
      reachCmAvg: number | null,
      strikesMinAvg: number | null,
    } | null,
    homeOdds: number | null,
    awayTeam:  {
      name: string,
      heightCmAvg: number | null,
      weightKgAvg: number | null,
      reachCmAvg: number | null,
      strikesMinAvg: number | null,
    } | null,
    awayOdds: number | null,
    umpire:  {
      name: string,
    } | null,
  } | null,
};
/* tslint:enable */
