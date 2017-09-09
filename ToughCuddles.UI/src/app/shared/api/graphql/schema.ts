/* tslint:disable */
//  This file was automatically generated and should not be edited.

export type DominantHand = "Both" | "Right" | "Left";


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
    dominantHand: DominantHand | null,
  } | null,
};
/* tslint:enable */
