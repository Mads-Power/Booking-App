import { Booking } from './booking';
export type User = {
  id: number;
  name: string,
  email: string,
  phoneNumber: string,
  bookings: Booking[];
};
