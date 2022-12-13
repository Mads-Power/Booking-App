import { useQuery, useMutation } from '@tanstack/react-query';
import { Booking } from '../types';

export type CreateBooking = {
  data: {
    seatId: number;
    userId: number;
    date: string;
  };
};

const url = 'http://localhost:51249/api/Booking/Book';
export const putBookingBook = async ({ data }: CreateBooking) => {
  const requestOptions = {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' },
    body: JSON.stringify({
      data
    }),
  };
  const res = await fetch(url, requestOptions);
  console.log(res);
  return res.json();
};

export const useBook = () => {
  return useMutation({
    mutationFn: putBookingBook
  });
};
