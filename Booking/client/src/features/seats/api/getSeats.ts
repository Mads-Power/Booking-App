import { useQuery } from '@tanstack/react-query';

const url = '';
export const getSeats = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useSeats = () => {
  return useQuery({
    queryKey: ['seats'],
    queryFn: () => getSeats(),
  });
};
