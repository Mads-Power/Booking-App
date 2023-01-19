import { useQuery } from '@tanstack/react-query';
import { Booking } from '@type/booking';
const API_URL = import.meta.env.VITE_API_URL

const url = `${API_URL}/api/Booking`;
export const getBooking = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useBooking = () => {
  return useQuery<Booking>({
    queryKey: ['booking'],
    queryFn: () => getBooking(),
  });
};
