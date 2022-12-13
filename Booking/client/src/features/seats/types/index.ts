import { Booking } from "../../bookings/types";

export type Seat = {
  id: number;
  name: string;
  roomId: number;
  bookings: Booking[]
};
