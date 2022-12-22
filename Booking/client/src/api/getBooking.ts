import { useQuery } from '@tanstack/react-query';
import { Booking } from '@type/booking';

const url = 'http://localhost:51249/api/Booking';
export const getBooking = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useBooking = () => {
  return useQuery<Booking[]>({
    queryKey: ['booking'],
    queryFn: () => getBooking(),
  });
};
