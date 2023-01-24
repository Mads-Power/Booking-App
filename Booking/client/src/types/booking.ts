export type Booking = {
  id: number;
  seatId: number;
  userId: number;
  date: string;
};

export type DeleteBooking = {
  userId: number;
  date: string;
};

export type TUnbook = DeleteBooking & Partial<Booking>
