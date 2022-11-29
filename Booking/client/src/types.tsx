export interface Room {
  id: number;
  name: string;
  capacity: number;
  office: OfficeType;
  seats: Seat[];
}

export interface Seat {
  id: string;
  seatId: string;
  isTaken: boolean;
}

// Måter å bruke type eller enum
export type OfficeType = "Oslo" | "Drammen";
export enum OfficeEnum {
  Oslo = "Oslo",
  Drammen = "Drammen",
}

const getOfficeType = (type: OfficeEnum) => type === OfficeEnum.Drammen;
