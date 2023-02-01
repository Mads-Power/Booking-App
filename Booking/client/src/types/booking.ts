export type Booking = {
  id: number;
  seatId: number;
  email: string;
  date: string;
};

export type DeleteBooking = {
  email: string;
  date: string;
};

export type TUnbook = DeleteBooking & Partial<Booking>
