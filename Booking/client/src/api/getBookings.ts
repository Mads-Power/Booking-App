import { useQuery } from '@tanstack/react-query';

const url = '';
export const getBookings = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useBookings = () => {
  return useQuery({
    queryKey: ['bookings'],
    queryFn: () => getBookings(),
  });
};
