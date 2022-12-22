import { Seat } from './seat';
export type Room = {
  id: number;
  name: string;
  capacity: number;
  officeId: number;
  seats: Seat[];
};
