/* tslint:disable */
//  This file was automatically generated and should not be edited.

export type AverageWinsTicketsQuery = {
  teams:  Array< {
    name: string,
    ticketsSoldCount: number | null,
    averageWinRate: number | null,
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
  } | null,
};
/* tslint:enable */
