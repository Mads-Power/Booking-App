import { Booking } from './booking';
export type Seat = {
  id: number;
  name: string;
  roomId: number;
  bookings: Booking[];
};
