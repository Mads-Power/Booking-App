import { Booking } from './booking';
export type User = {
  name: string,
  email: string,
  phoneNumber?: string,
  bookings: Booking[];
};
